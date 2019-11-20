using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AaltoGlobalImpact.OIP;
using TheBall.Core;
using TheBall.Index;

namespace TheBall.Interface
{
    partial class GenericObject : IAdditionalFormatProvider, IBeforeStoreHandler, IIndexedDocument
    {
        AdditionalFormatContent[] IAdditionalFormatProvider.GetAdditionalContentToStore(string masterBlobETag)
        {
            return this.GetFormattedContentToStore(masterBlobETag, AdditionalFormatSupport.WebUIFormatExtensions);
        }

        string[] IAdditionalFormatProvider.GetAdditionalFormatExtensions()
        {
            return this.GetFormatExtensions(AdditionalFormatSupport.WebUIFormatExtensions);
        }

        public object FindObjectByID(string objectId)
        {
            throw new System.NotSupportedException();
        }

        public async Task PerformBeforeStoreUpdate()
        {
            var owner = InformationContext.CurrentOwner;
            var currentCollectionWrapper = await ObjectStorage.RetrieveFromOwnerContentA<GenericCollectionableObject>(owner, ID);
            if (IncludeInCollection)
            {
                if (currentCollectionWrapper == null)
                {
                    currentCollectionWrapper = new GenericCollectionableObject();
                    currentCollectionWrapper.ID = ID;
                    currentCollectionWrapper.SetLocationAsOwnerContent(owner, ID);
                }
                currentCollectionWrapper.ValueObject = this;
                await currentCollectionWrapper.StoreInformationAsync();
            }
            else
            {
                if(currentCollectionWrapper != null)
                    await currentCollectionWrapper.DeleteInformationObjectAsync(owner);
            }

        }

        partial void DoPostDeleteExecute(IContainerOwner owner, ref Task task)
        {
            task = Task.Run(async () =>
            {
                var currentCollectionWrapper =
                    await ObjectStorage.RetrieveFromOwnerContentA<GenericCollectionableObject>(owner, ID);
                if (currentCollectionWrapper != null)
                    await currentCollectionWrapper.DeleteInformationObjectAsync(owner);
            });
        }

        /*
        public Document GetLuceneDocument(string indexName)
        {
            return null;
            // TODO: Implement index fetch for content
            Document doc = new Document();
            var fieldsWithIndexingInfo = getValuesWithIndexingInfo("").ToArray();
            return doc;
        }*/

        private IEnumerable<GenericValueLocator> getValuesWithIndexingInfo(string predeccorFieldNamePrefix)
        {
            var values = Values;
            var myValuesWithIndexingInfos = values.Where(value => String.IsNullOrEmpty(value.IndexingInfo) == false);
            var myValueLocators = myValuesWithIndexingInfos.Select(value => new GenericValueLocator
            {
                FullName = (predeccorFieldNamePrefix ?? "") + value.ValueName,
                Value = value
            });
            var myValuesWithChildren = values.Where(value => value.Object != null || value.ObjectArray.Count > 0);
            var myChildrenValueLocators = myValuesWithChildren.SelectMany(value =>
            {
                IEnumerable<GenericValueLocator> childrenValues;
                var valuePredeccorPrefix = predeccorFieldNamePrefix + value.ValueName + ".";
                var singleObject = value.Object;
                if (singleObject != null)
                {
                    childrenValues = singleObject.getValuesWithIndexingInfo(valuePredeccorPrefix);
                }
                else
                    childrenValues = new GenericValueLocator[0];
                var objectChildrenValues = value.ObjectArray.SelectMany(objVal => objVal.getValuesWithIndexingInfo(valuePredeccorPrefix));
                childrenValues = childrenValues.Concat(objectChildrenValues);
                return childrenValues;
            });
            return myValueLocators.Concat(myChildrenValueLocators);
        }


        class GenericValueLocator
        {
            public string FullName;
            public GenericValue Value;
        }
    }
}