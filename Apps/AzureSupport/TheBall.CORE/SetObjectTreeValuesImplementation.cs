using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Linq;

namespace TheBall.CORE
{
    public class SetObjectTreeValuesImplementation
    {
        public static void ExecuteMethod_CreateInternalObjects(IInformationObject rootObject, NameValueCollection httpFormData)
        {
            var internalObjectProperties = httpFormData.AllKeys.Where(key => key.Contains("___")).ToArray();
            foreach (var objectProp in internalObjectProperties)
            {
                initializeChainObjects(rootObject, objectProp);
            }
        }

        public static NameValueCollection GetTarget_FieldValues(IInformationObject rootObject, NameValueCollection httpFormData)
        {
            NameValueCollection fieldEntries = new NameValueCollection();
            string objectID = rootObject.ID;
            foreach (var key in httpFormData.AllKeys)
            {
                if (String.IsNullOrEmpty(key))
                    continue;
                var value = httpFormData[key];
                if (key.StartsWith("File_") == false && key.StartsWith("Object_") == false && key.StartsWith("FileEmbedded_") == false)
                {
                    if (key.Contains("___"))
                    {
                        object actualContainingObject;
                        string fieldPropertyName;
                        ModifyInformationSupport.InitializeChainAndReturnPropertyOwningObject(rootObject, key, out actualContainingObject, out fieldPropertyName);
                        IInformationObject containingObject = actualContainingObject as IInformationObject;
                        if (containingObject == null)
                            throw new NotSupportedException("Object property setting at creation only supported for IInformationObject types");
                        fieldEntries.Add(containingObject.ID + "_" + fieldPropertyName, value);
                    }
                    else
                    {
                        string fixedKey = prefixWithIDIfMissing(key, "", objectID);
                        fieldEntries.Add(fixedKey, value);
                    }
                }
            }
            return fieldEntries;
        }

        public static void ExecuteMethod_DecodeEncodedRawHTMLValues(NameValueCollection fieldValues)
        {
            var keysToDecode = fieldValues.AllKeys.Where(key => key.Contains("_ENC.")).Select(
                key => new { OldKey = key, NewKey = key.Replace("_ENC.", "_") }).ToArray();
            foreach (var keyToDecode in keysToDecode)
            {
                var encodedValue = fieldValues[keyToDecode.OldKey];
                var decodedValue = WebUtility.HtmlDecode(encodedValue);
                fieldValues.Remove(keyToDecode.OldKey);
                fieldValues.Add(keyToDecode.NewKey, decodedValue);
            }
        }

        public static NameValueCollection GetTarget_ObjectLinkValues(IInformationObject rootObject, NameValueCollection httpFormData)
        {
            NameValueCollection objectEntries = new NameValueCollection();
            if (httpFormData == null)
                return objectEntries;
            string objectID = rootObject.ID;
            foreach (var key in httpFormData.AllKeys)
            {
                if (key.StartsWith("Object_"))
                {
                    var value = httpFormData[key];
                    string newKey = prefixWithIDIfMissing(key, "Object_", objectID);
                    objectEntries.Add(newKey, value);
                }
            }
            return objectEntries;
        }

        private static bool isPrefixedStringBeginningWithGuid(string str, string prefix)
        {
            int guidLength = 36; // hyphenated 12345678-1234-1234-1234-123456789abc
            int prefixLength = prefix.Length;
            if (str.Length < prefixLength + guidLength)
                return false;
            string candidateGuidStr = str.Substring(prefixLength, guidLength);
            Guid guidResult;
            return Guid.TryParse(candidateGuidStr, out guidResult);
        } 

        private static string prefixWithIDIfMissing(string str, string prefix, string idToInject)
        {
            if (str.StartsWith(prefix) == false)
                return str;
            if (isPrefixedStringBeginningWithGuid(str, prefix))
                return str;
            if(prefix.Length > 0)
                return str.Replace(prefix, prefix + idToInject + "_");
            return idToInject + "_" + str;
        }

        public static Dictionary<string, MediaFileData> GetTarget_BinaryContentFiles(IInformationObject rootObject, NameValueCollection httpFormData, HttpFileCollection httpFileData)
        {
            if (httpFileData == null)
                return new Dictionary<string, MediaFileData>();
            Dictionary<string, MediaFileData> resultDict = new Dictionary<string, MediaFileData>(httpFileData.Count);
            string objectID = rootObject.ID;

            foreach (var key in httpFormData.AllKeys)
            {
                if (key.StartsWith("File_"))
                {
                    HttpPostedFile httpFile = null;
                    if (httpFileData.AllKeys.Contains(key))
                        httpFile = httpFileData[key];
                    string dictKey = prefixWithIDIfMissing(key, "File_", objectID);
                    resultDict.Add(dictKey, httpFile != null ? new MediaFileData { HttpFile = httpFile} : null);
                }
            }
            foreach (var key in httpFileData.AllKeys)
            {
                if (key.StartsWith("File_"))
                {
                    string dictKey = prefixWithIDIfMissing(key, "File_", objectID);
                    if (resultDict.ContainsKey(dictKey) == false)
                    {
                        var httpFile = httpFileData[key];
                        resultDict.Add(dictKey, httpFile != null ? new MediaFileData { HttpFile = httpFile } : null);
                    }
                }
            }
            return resultDict;
        }

        public static void ExecuteMethod_AddEncodedFormDataToBinaryFiles(IInformationObject rootObject, NameValueCollection httpFormData, Dictionary<string, MediaFileData> binaryContentFiles)
        {
            const string base64CommaValue = "base64,";
            const int base64Length = 7; // base64CommaValue.Length;

            string objectID = rootObject.ID;
            foreach (var key in httpFormData.AllKeys)
            {
                if (key.StartsWith("FileEmbedded_"))
                {
                    string dictKey = prefixWithIDIfMissing(key, "FileEmbedded_", objectID);
                    dictKey = dictKey.Replace("FileEmbedded_", "File_");
                    string formValue = httpFormData[key];
                    MediaFileData mediaFileData = null;
                    if (String.IsNullOrEmpty(formValue) == false)
                    {
                        int fileNameEndIX = formValue.IndexOf(':');
                        string fileName = formValue.Substring(0, fileNameEndIX);
                        fileName = Path.GetFileName(fileName);
                        int base64CommaIX = formValue.IndexOf(base64CommaValue, System.StringComparison.Ordinal);
                        var base64Data = formValue.Substring(base64CommaIX + base64Length);
                        // Strip off remaining comma separated datas in case the field value has those
                        var existingDataCommaIX = base64Data.IndexOf(',');
                        if (existingDataCommaIX > 0)
                            base64Data = base64Data.Substring(0, existingDataCommaIX);
                        byte[] realData = Convert.FromBase64String(base64Data);
                        mediaFileData = new MediaFileData
                            {
                                FileName = fileName,
                                FileContent = realData
                            };
                    }
                    binaryContentFiles.Add(dictKey, mediaFileData);
                }
            }
        }

        public static void ExecuteMethod_SetFieldValues(IInformationObject rootObject, NameValueCollection fieldValues)
        {
            ModifyInformationSupport.SetFieldValues(rootObject, fieldValues);
        }

        public static void ExecuteMethod_SetObjectLinks(IInformationObject rootObject, NameValueCollection objectLinkValues)
        {
            ModifyInformationSupport.SetObjectLinks(rootObject, objectLinkValues);
        }

        public static void ExecuteMethod_SetBinaryContent(IInformationObject rootObject, Dictionary<string, MediaFileData> binaryContentFiles)
        {
            foreach (var fileKey in binaryContentFiles.Keys)
            {
                string contentInfo = fileKey.Substring(5); // Substring("File_".Length);
                ModifyInformationSupport.SetBinaryContent(InformationContext.CurrentOwner, contentInfo, rootObject,
                                                          binaryContentFiles[fileKey]);
            }
        }

        public static void ExecuteMethod_StoreCompleteObject(IInformationObject rootObject)
        {
            rootObject.StoreInformation(InformationContext.CurrentOwner, true);
        }

        private static void initializeChainObjects(IInformationObject createdObject, string objectProp)
        {
            object actualContainingObject;
            string fieldPropertyName;
            ModifyInformationSupport.InitializeChainAndReturnPropertyOwningObject(createdObject, objectProp, out actualContainingObject, out fieldPropertyName);
        }

    }
}