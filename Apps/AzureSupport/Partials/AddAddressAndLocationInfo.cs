﻿using System;
using System.IO;
using System.Linq;
using TheBall;

namespace AaltoGlobalImpact.OIP
{
    partial class AddImageInfo : IAddOperationProvider
    {
        public bool PerformAddOperation(InformationSourceCollection sources, string requesterLocation)
        {
            if(ImageTitle == "")
                throw new InvalidDataException("Image title is mandatory");
            IInformationObject container = sources.GetDefaultSource().RetrieveInformationObject();
            Image image = Image.CreateDefault();
            VirtualOwner owner = VirtualOwner.FigureOwner(this);
            image.SetLocationAsOwnerContent(owner, image.ID);
            image.Title = ImageTitle;
            StorageSupport.StoreInformationMasterFirst(image, owner);
            DefaultViewSupport.CreateDefaultViewRelativeToRequester(requesterLocation, image, owner);
            return false;
        }
    }

    partial class AddImageGroupInfo : IAddOperationProvider
    {
        public bool PerformAddOperation(InformationSourceCollection sources, string requesterLocation)
        {
            if(ImageGroupTitle == "")
                throw new InvalidDataException("Image group title is mandatory");
            IInformationObject container = sources.GetDefaultSource().RetrieveInformationObject();
            ImageGroup imageGroup = ImageGroup.CreateDefault();
            VirtualOwner owner = VirtualOwner.FigureOwner(this);
            imageGroup.SetLocationAsOwnerContent(owner, imageGroup.ID);
            imageGroup.Title = ImageGroupTitle;
            StorageSupport.StoreInformationMasterFirst(imageGroup, owner);
            DefaultViewSupport.CreateDefaultViewRelativeToRequester(requesterLocation, imageGroup, owner);
            return false;
        }
    }

    partial class AddAddressAndLocationInfo : IAddOperationProvider
    {
        public bool PerformAddOperation(InformationSourceCollection sources, string requesterLocation)
        {
            if (LocationName == "")
                throw new InvalidDataException("Location name is mandatory");
            IInformationObject container = sources.GetDefaultSource().RetrieveInformationObject();
            AddressAndLocation location = AddressAndLocation.CreateDefault();

            VirtualOwner owner = VirtualOwner.FigureOwner(this);
            location.SetLocationAsOwnerContent(owner, location.ID);
            location.Location.LocationName = LocationName;
            location.ReferenceToInformation.Title = location.Location.LocationName;
            location.ReferenceToInformation.URL = DefaultViewSupport.GetDefaultViewURL(location);
            StorageSupport.StoreInformationMasterFirst(location, owner);
            DefaultViewSupport.CreateDefaultViewRelativeToRequester(requesterLocation, location, owner);

            //LocationContainer locationContainer = LocationContainer.RetrieveFromOwnerContent(owner, "Locations");
            //// Referencelocation etag in place
            //location.MasterETag = location.ETag;
            //locationContainer.Locations.CollectionContent.Add(location);
            //StorageSupport.StoreInformation(locationContainer);

            //AccountContainer accountContainer = container as AccountContainer;
            //GroupContainer groupContainer = container as GroupContainer;
            //if (accountContainer != null)
            //{
            //    accountContainer.AccountModule.LocationCollection.CollectionContent.Add(location);
            //} else if(groupContainer != null)
            //{
            //    //groupContainer.Locations.CollectionContent.Add(location);
            //}
            //StorageSupport.StoreInformation(container);
            return false;
            
            /*
            location.Address = Address;
            location.Location.Longitude.TextValue = this.Longitude;
            location.Location.Latitude.TextValue = this.Latitude;
            location.Location.LocationName = this.LocationName;
            container.AccountModule.LocationCollection.CollectionContent.Add(location);
            StorageSupport.StoreInformation(container);
            VirtualOwner owner = VirtualOwner.FigureOwner(this);
            MapContainer mapContainer = MapContainer.RetrieveFromOwnerContent(owner, "default");
            mapContainer.MapMarkers.CollectionContent.Clear();
            mapContainer.MapMarkers.CollectionContent.AddRange(
                container.AccountModule.LocationCollection.CollectionContent.
                    Select(loc =>
                               {
                                   MapMarker marker = MapMarker.CreateDefault();
                                   marker.Location = loc.Location;
                                   marker.LocationText = string.Format("{0},{1}",
                                                                       loc.Location.Latitude.TextValue,
                                                                       loc.Location.Longitude.TextValue);
                                   return marker;
                               }));
            StorageSupport.StoreInformation(mapContainer);
            LocationName = "";
            Address = StreetAddress.CreateDefault();
            Longitude = "";
            Latitude = "";
             */
            return true;
        }
    }
}