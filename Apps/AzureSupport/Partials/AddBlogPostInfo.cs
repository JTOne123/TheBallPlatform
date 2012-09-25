﻿using System;
using System.IO;
using TheBall;

namespace AaltoGlobalImpact.OIP
{
    partial class AddBlogPostInfo : IAddOperationProvider
    {
        public bool PerformAddOperation(InformationSourceCollection sources)
        {
            if(Title == "")
                throw new InvalidDataException("Blog title is mandatory");
            Blog blog = Blog.CreateDefault();
            VirtualOwner owner = VirtualOwner.FigureOwner(this);
            blog.SetLocationAsOwnerContent(owner, blog.ID);
            blog.Title = Title;
            blog.ReferenceToInformation.Title = blog.Title;
            blog.ReferenceToInformation.URL = ReferenceToInformation.GetDefaultViewURL(blog);
            blog.Published = DateTime.Now;
            StorageSupport.StoreInformation(blog);
            BlogContainer blogContainer = BlogContainer.RetrieveFromOwnerContent(owner, "default");
            blogContainer.AddNewBlogPost(blog);
            StorageSupport.StoreInformation(blogContainer);
            Title = "";
            return true;
        }
    }
}