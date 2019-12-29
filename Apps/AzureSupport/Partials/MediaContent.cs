﻿using System;
using System.IO;
using System.Runtime.Serialization;
using System.Web;
using Microsoft.WindowsAzure.Storage.Blob;
using TheBall;
using TheBall.Core;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using TheBall.Core.StorageCore;

namespace AaltoGlobalImpact.OIP
{
    partial class MediaContent
    {
        public string ContentUrl
        {
            get { return RenderWebSupport.GetUrlFromRelativeLocation(RelativeLocation); }
        }

        public string ContentUrlBase
        {
            get
            {
                return RenderWebSupport.GetUrlFromRelativeLocation(
                    RenderWebSupport.GetLocationWithoutExtension(RelativeLocation));
            }
        }

        [DataMember]
        public string OriginalFileName { get; set; }

        [DataMember]
        public string FileExt { get; set; }

        [DataMember]
        public string AdditionalFormatFileExt
        {
            get
            {
                string additionalFormatFileExt;
                if (FileExt == ".jpg" || FileExt == ".jpeg")
                    additionalFormatFileExt = ".jpg";
                else if (FileExt == ".png" || FileExt == ".bmp")
                    additionalFormatFileExt = ".png";
                else if (FileExt == ".gif")
                    additionalFormatFileExt = ".gif";
                else
                    additionalFormatFileExt = null;
                return additionalFormatFileExt;
            }
            set
            {
                // Empty body to please the (de)serialization
            }
        }

        [DataMember]
        public long ContentLength { get; set; }

        public async Task SetMediaContent(IContainerOwner containerOwner, string contentObjectID, object mediaContent)
        {
            if(ID != contentObjectID)
                return;
            ClearCurrentContent(containerOwner);
            MediaFileData mediaFileData = mediaContent as MediaFileData;
            if(mediaFileData == null)
                throw new NotSupportedException("Not supported mediaContent object in SetMediaContent");
            var storageService = CoreServices.GetCurrent<IStorageService>();
            if (mediaFileData.HttpFile != null)
            {
                var postedContent = mediaFileData.HttpFile;
                FileExt = Path.GetExtension(postedContent.FileName).ToLower();
                ContentLength = postedContent.Length;
                string locationFileName = ID + FileExt;
                SetLocationAsOwnerContent(containerOwner, locationFileName);
                await storageService.UploadBlobStreamA(containerOwner, locationFileName,
                    postedContent.OpenReadStream());
            }
            else
            {
                FileExt = Path.GetExtension(mediaFileData.FileName).ToLower();
                ContentLength = mediaFileData.FileContent.Length;
                string locationFileName = ID + FileExt;
                SetLocationAsOwnerContent(containerOwner, locationFileName);
                await storageService.UploadBlobDataA(containerOwner, locationFileName, mediaFileData.FileContent);
            }
            UpdateAdditionalMediaFormats();
        }

        public async Task SetMediaContent(string contentFileName, byte[] contentData)
        {
            var owner = InformationContext.CurrentOwner;
            ClearCurrentContent(owner);
            FileExt = Path.GetExtension(contentFileName);
            ContentLength = contentData.Length;
            string locationFileName = ID + FileExt;
            var storageService = CoreServices.GetCurrent<IStorageService>();
            SetLocationAsOwnerContent(owner, locationFileName);
            await storageService.UploadBlobDataA(owner, locationFileName, contentData);
            await UpdateAdditionalMediaFormats();
        }

        public async Task UpdateAdditionalMediaFormats()
        {
            await RemoveAdditionalMediaFormats();
            await CreateAdditionalMediaFormats();
        }

        public async Task CreateAdditionalMediaFormats()
        {
            if (FileExt == ".jpg" || FileExt == ".jpeg" || FileExt == ".png" || FileExt == ".gif" || FileExt == ".bmp")
                await OIP.CreateAdditionalMediaFormats.ExecuteAsync(new CreateAdditionalMediaFormatsParameters { MasterRelativeLocation = RelativeLocation });
        }

        public async Task RemoveAdditionalMediaFormats()
        {
            await ClearAdditionalMediaFormats.ExecuteAsync(new ClearAdditionalMediaFormatsParameters { MasterRelativeLocation = RelativeLocation });
        }

        public async Task ClearCurrentContent(IContainerOwner containerOwner)
        {
            var storageService = CoreServices.GetCurrent<IStorageService>();
            var blobfullPath = storageService.GetOwnerContentLocation(containerOwner, RelativeLocation);
            await storageService.DeleteBlobA(blobfullPath);
            await RemoveAdditionalMediaFormats();
        }

        public async Task<string> GetMD5FromStorage()
        {
            try
            {
                var owner = InformationContext.CurrentOwner;
                var storageService = CoreServices.GetCurrent<IStorageService>();
                var blobItem = await storageService.GetBlobItemA(owner, RelativeLocation);
                return blobItem.ContentMD5;
            }
            catch
            {
                return null;
            }
        }

        public static string CalculateComparableMD5(byte[] data)
        {
            var md5Check = System.Security.Cryptography.MD5.Create();
            md5Check.TransformBlock(data, 0, data.Length, null, 0);
            md5Check.TransformFinalBlock(new byte[0], 0, 0);
            byte[] hashBytes = md5Check.Hash;
            string hashVal = Convert.ToBase64String(hashBytes);
            return hashVal;
        }

        public async Task<byte[]> GetContentData()
        {
            var storageService = CoreServices.GetCurrent<IStorageService>();
            var owner = InformationContext.CurrentOwner;
            try
            {
                byte[] result = await storageService.DownloadBlobDataA(owner, RelativeLocation, true);
                return result;
            }
            catch
            {
                return null;
            }
        }

    }
}