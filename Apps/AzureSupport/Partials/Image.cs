﻿using System;
using System.Web;
using TheBall;

namespace AaltoGlobalImpact.OIP
{
    partial class Image : IBeforeStoreHandler, IAddOperationProvider
    {
        public void PerformBeforeStoreUpdate()
        {
            if (ReferenceToInformation == null)
                ReferenceToInformation = OIP.ReferenceToInformation.CreateDefault();
            this.ReferenceToInformation.Title = this.Title;
            ReferenceToInformation.URL = DefaultViewSupport.GetDefaultViewURL(this);
        }

        public bool PerformAddOperation(string commandName, InformationSourceCollection sources, string requesterLocation, HttpFileCollection files)
        {
            string[] cmdIDList = commandName.Split('_');
            if (cmdIDList.Length < 2)
                return false;
            string cmd = cmdIDList[0];
            string cmdID = cmdIDList[1];
            if (cmdID != this.ID)
                return false;
            var defaultSource = sources.GetDefaultSource();
            ImageGroup currImageGroup = (ImageGroup) defaultSource.RetrieveInformationObject();
            VirtualOwner owner = VirtualOwner.FigureOwner(this);
            Image addedImage = Image.CreateDefault();
            addedImage.CopyContentFrom(this);
            addedImage.ImageData = MediaContent.CreateDefault();
            HttpPostedFile postedFile = files[this.ImageData.ID];
            if (postedFile != null && String.IsNullOrWhiteSpace(postedFile.FileName) == false)
            {
                addedImage.SetMediaContent(owner, addedImage.ImageData.ID, postedFile);
            }
            currImageGroup.ImagesCollection.CollectionContent.Add(addedImage);
            currImageGroup.StoreInformationMasterFirst(owner);
            return true;
        }
    }
}