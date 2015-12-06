﻿using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SecuritySupport
{
    public class SymmetricSupport
    {
        private AesManaged CurrProvider = new AesManaged()
        {
            Mode = CipherMode.CBC,
            Padding = PaddingMode.PKCS7,
            BlockSize = AES_BLOCKSIZE,
            FeedbackSize = AES_FEEDBACK_SIZE
        };
        private static RNGCryptoServiceProvider RndSupport = new RNGCryptoServiceProvider();

        public const PaddingMode PADDING_MODE = PaddingMode.PKCS7;
        public const CipherMode AES_MODE = CipherMode.CBC;
        public const int AES_FEEDBACK_SIZE = 8;
        public const int AES_KEYSIZE = 256;
        public const int AES_BLOCKSIZE = 128;

        public string EncryptStringToBase64(string plainText)
        {
            var encrypted = EncryptString(plainText);
            return Convert.ToBase64String(encrypted);
        }

        public byte[] EncryptString(string plainText)
        {
            var encryptor = CurrProvider.CreateEncryptor();

            // Create the streams used for encryption. 
            byte[] encrypted;
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt, Encoding.UTF8))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
            return encrypted;
        }

        public string DecryptString(byte[] cipherData)
        {
            var decryptor = CurrProvider.CreateDecryptor();
            string plainText;
            using (MemoryStream msDecrypt = new MemoryStream(cipherData))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt, Encoding.UTF8))
                    {

                        // Read the decrypted bytes from the decrypting stream 
                        // and place them in a string.
                        plainText = srDecrypt.ReadToEnd();
                    }
                }
            }
            return plainText;
        }


        public string DecryptStringFromBase64(string cipherText)
        {
            byte[] cipherData = Convert.FromBase64String(cipherText);
            return DecryptString(cipherData);
        }

        
        public byte[] EncryptData(byte[] plainText)
        {
            var encryptor = CurrProvider.CreateEncryptor();

            // Create the streams used for encryption. 
            byte[] encrypted;
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (BinaryWriter swEncrypt = new BinaryWriter(csEncrypt))
                    {

                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
            return encrypted;
        }

        public byte[] DecryptData(byte[] cipherText)
        {
            var decryptor = CurrProvider.CreateDecryptor();

            using (MemoryStream plainTextStream = new MemoryStream())
            {
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (BinaryReader srDecrypt = new BinaryReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            //plainText = srDecrypt.ReadBytes(int.MaxValue);
                            srDecrypt.BaseStream.CopyTo(plainTextStream);
                            //plainText = srDecrypt.ReadBytes(1024*1024);
                        }
                    }
                }
                return plainTextStream.ToArray();
            }
        }


        public void InitializeFromSharedSecret(string textvalue)
        {
            byte[] sharedData = Encoding.UTF8.GetBytes(textvalue);
            InitializeFromSharedSecret(sharedData);
        }

        public void InitializeFromSharedSecret(byte[] sharedData)
        {
            SHA256 sha256 = new SHA256Managed();
            //byte[] key = dataToHash.Take(16).ToArray();
            //byte[] iv = dataToHash.Skip(16).ToArray();
            byte[] hash = sha256.ComputeHash(sharedData);
            InitializeFromKeyAndIV128Bit(hash);
        }

        public void InitializeFromFull(byte[] keyAndIV)
        {
            CurrProvider.KeySize = AES_KEYSIZE;
            byte[] key = keyAndIV.Take(32).ToArray();
            byte[] iv = keyAndIV.Skip(32).ToArray();
            CurrProvider.Key = key;
            CurrProvider.IV = iv;
        }

        public void InitializeFromKeyAndIV128Bit(byte[] keyAndIV)
        {
            CurrProvider.KeySize = 128;
            byte[] key = keyAndIV.Take(16).ToArray();
            byte[] iv = keyAndIV.Skip(16).ToArray();
            CurrProvider.Key = key;
            CurrProvider.IV = iv;
        }

        public byte[] CurrentKey
        {
            get { return CurrProvider != null ? CurrProvider.Key : null; }
        }

        public void InitializeNew()
        {
            CurrProvider.KeySize = AES_KEYSIZE;
            CurrProvider.GenerateKey();
            CurrProvider.GenerateIV();
        }

        public static byte[] GetRandomBytes(int byteAmount)
        {
            byte[] result = new byte[byteAmount];
            RndSupport.GetBytes(result);
            return result;
        }

        public byte[] GetKeyWithIV()
        {
            return CurrProvider.Key.Concat(CurrProvider.IV).ToArray();
        }
    }
}