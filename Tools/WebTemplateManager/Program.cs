﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using TheBall;
using TheBall.CORE;

namespace WebTemplateManager
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                //SecurityNegotiationManager.EchoClient().Wait();
                //return;
                if (args.Length != 2)
                {
                    Console.WriteLine("Usage: WebTemplateManager.exe <groupID> <connection string>");
                    return;
                }
                Debugger.Launch();
                string connStr = args[1];
                string grpID = args[0];
                //string connStr = String.Format("DefaultEndpointsProtocol=http;AccountName=theball;AccountKey={0}",
                //                               args[0]);
                //connStr = "UseDevelopmentStorage=true";
                bool debugMode = false;

                StorageSupport.InitializeWithConnectionString(connStr, debugMode);
                InformationContext.InitializeFunctionality(3, true);
                InformationContext.Current.InitializeCloudStorageAccess(
                    Properties.Settings.Default.CurrentActiveContainerName);

                string directory = Directory.GetCurrentDirectory();
                if (directory.EndsWith("\\") == false)
                    directory = directory + "\\";
                string[] allFiles =
                    Directory.GetFiles(directory, "*", SearchOption.AllDirectories)
                             .Select(str => str.Substring(directory.Length))
                             .ToArray();
                VirtualOwner owner = VirtualOwner.FigureOwner("grp/" + grpID);
                FileSystemSupport.UploadTemplateContent(allFiles, owner,
                                                        RenderWebSupport.DefaultPublicWwwTemplateLocation, true,
                                                        Preprocessor, ContentFilterer, InformationTypeResolver);
                RenderWebSupport.RenderWebTemplate(grpID, true, "AaltoGlobalImpact.OIP.Blog",
                                                   "AaltoGlobalImpact.OIP.Activity");
            }
            catch
            {

            }
        }

        private static void Preprocessor(BlobStorageContent content)
        {
            if (content.FileName.EndsWith("_DefaultView.html"))
                ReplaceHtmlExtensionWithPHtml(content);
            if (content.FileName.EndsWith("oip-layout-landing.html"))
                ReplaceHtmlExtensionWithPHtml(content);
        }

        private static void ReplaceHtmlExtensionWithPHtml(BlobStorageContent content)
        {
            content.FileName = content.FileName.Substring(0, content.FileName.LastIndexOf(".html")) + ".phtml";
        }

        private static bool ContentFilterer(BlobStorageContent content)
        {
            string fileName = content.FileName;
            if (fileName.EndsWith("readme.txt"))
                return false;
            if (fileName.Contains("_DefaultView.phtml"))
            {
                bool isBlogDefaultView = fileName.EndsWith(".Blog_DefaultView.phtml");
                bool isActivityDefaultView = fileName.EndsWith(".Activity_DefaultView.phtml");
                if (isBlogDefaultView == false && isActivityDefaultView == false)
                    return false;
            }
            return true;
        }

        private static string InformationTypeResolver(BlobStorageContent content)
        {
            string webtemplatePath = content.FileName;
            string blobInformationType;
            if (webtemplatePath.EndsWith(".phtml"))
            {
                //if (webtemplatePath.Contains("/oip-viewtemplate/"))
                if (webtemplatePath.EndsWith("_DefaultView.phtml"))
                    blobInformationType = StorageSupport.InformationType_RuntimeWebTemplateValue;
                else
                    blobInformationType = StorageSupport.InformationType_WebTemplateValue;
            }
            else if (webtemplatePath.EndsWith(".html"))
            {
                string htmlContent = Encoding.UTF8.GetString(content.BinaryContent);
                bool containsMarkup = htmlContent.Contains("THEBALL-CONTEXT");
                if (containsMarkup == false)
                    blobInformationType = StorageSupport.InformationType_GenericContentValue;
                else
                {
                    blobInformationType = webtemplatePath.EndsWith("_DefaultView.html")
                                              ? StorageSupport.InformationType_RuntimeWebTemplateValue
                                              : StorageSupport.InformationType_WebTemplateValue;
                }
            }
            else
                blobInformationType = StorageSupport.InformationType_GenericContentValue;
            return blobInformationType;
        }

    }
}
