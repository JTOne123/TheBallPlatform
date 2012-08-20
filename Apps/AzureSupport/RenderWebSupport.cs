﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace TheBall
{
    public static class RenderWebSupport
    {
        private const string RootTagBegin = "<!-- THEBALL-CONTEXT-ROOT-BEGIN:";
        private const string CollectionTagBegin = "<!-- THEBALL-CONTEXT-COLLECTION-BEGIN:";
        private const string ObjectTagBegin = "<!-- THEBALL-CONTEXT-OBJECT-BEGIN:";
        private const string CommentEnd = " -->";
        private const string ContextTagEnd = "<!-- THEBALL-CONTEXT-END:";
        private const string TheBallPrefix = "<!-- THEBALL-";
        private const string AtomTagBegin = "[!ATOM]";
        private const string AtomTagEnd = "[ATOM!]";
        private const string MemberAtomPattern = @"(?<fulltag>\[!ATOM](?<membername>\w*)\[ATOM!])";

        public static string RenderTemplateWithContent(string templatePage, object content)
        {
            StringReader reader = new StringReader(templatePage);
            StringBuilder result = new StringBuilder(templatePage.Length);
            Stack<StackContextItem> contextStack = new Stack<StackContextItem>();
            int rootTagLen = RootTagBegin.Length;
            int rootTagsTotalLen = RootTagBegin.Length + CommentEnd.Length;
            int collTagLen = CollectionTagBegin.Length;
            int collTagsTotalLen = CollectionTagBegin.Length + CommentEnd.Length;
            int objTagLen = ObjectTagBegin.Length;
            int objTagsTotalLen = ObjectTagBegin.Length + CommentEnd.Length;
            int atomTagLen = AtomTagBegin.Length;
            int atomTagsTotalLen = AtomTagBegin.Length + AtomTagEnd.Length;
            for(string line = reader.ReadLine(); line != null; line = reader.ReadLine())
            {
                if(line.StartsWith(TheBallPrefix) == false && line.Contains("[!ATOM]") == false)
                {
                    result.AppendLine(line);
                    continue;
                }
                if(line.StartsWith(RootTagBegin))
                {
                    // TODO: Multiple container support; type and instance ID mapping
                    string typeName = line.Substring(rootTagLen, line.Length - rootTagsTotalLen).Trim();
                    StackContextItem rootItem = new StackContextItem(content, content.GetType(), null, true, false );
                    if(contextStack.Count != 0)
                        throw new InvalidDataException("Context stack already has a root item before: " + typeName);
                    contextStack.Push(rootItem);
                } else if(line.StartsWith(CollectionTagBegin))
                {
                    string memberName = line.Substring(collTagLen, line.Length - collTagsTotalLen).Trim();
                    StackContextItem currCtx = contextStack.Peek();
                    Type type = GetMemberType(currCtx, memberName);
                    object contentValue = GetPropertyValue(currCtx, memberName);
                    StackContextItem collItem = new StackContextItem(contentValue, type, memberName, false, true);
                    contextStack.Push(collItem);
                } else if(line.StartsWith(ObjectTagBegin))
                {
                    string memberName = line.Substring(objTagLen, line.Length - objTagsTotalLen).Trim();
                    StackContextItem currCtx = contextStack.Peek();
                    if (memberName == "*")
                    {
                        // Put top item again to stack
                        contextStack.Push(currCtx);
                    } else
                    {
                        Type type = GetMemberType(currCtx, memberName);
                        object contentValue = GetPropertyValue(currCtx, memberName);
                        StackContextItem objItem = new StackContextItem(contentValue, type, memberName, false, false);
                        contextStack.Push(objItem);
                    }
                } else if(line.StartsWith(ContextTagEnd))
                {
                    contextStack.Pop();
                } else // ATOM line
                {
                    StackContextItem currCtx = contextStack.Peek();
                    var contentLine = Regex.Replace(line, MemberAtomPattern,
                                  match =>
                                      {
                                          string memberName = match.Groups["membername"].Value;
                                          object value = GetPropertyValue(currCtx, memberName);
                                          return (value ?? (currCtx.MemberName + "." + memberName + " is null")).ToString();
                                      });
                    result.AppendLine(contentLine);
                }
                
            }
            return result.ToString();
        }

        private static object GetPropertyValue(StackContextItem currCtx, string propertyName)
        {
            if(currCtx.Content == null)
                throw new InvalidDataException("Object: " + currCtx.MemberName + " does not have content (was retrieving value: " + propertyName + ")");
            Type type = currCtx.ItemType;
            PropertyInfo pi = type.GetProperty(propertyName);
            return pi.GetValue(currCtx.Content, null);
        }

        private static Type GetMemberType(StackContextItem containingItem, string memberName)
        {
            Type containingType = containingItem.ItemType;
            PropertyInfo pi = containingType.GetProperty(memberName);
            if(pi == null)
                throw new InvalidDataException("Type: " + containingType.Name + " does not contain property: " + memberName);
            return pi.PropertyType;
        }
    }

    public class StackContextItem
    {
        public StackContextItem(object content, Type itemType, string memberName, bool isRoot, bool isCollection)
        {
            Content = content;
            //TypeName = typeName;
            ItemType = itemType;
            MemberName = memberName;
            IsRoot = isRoot;
            IsCollection = isCollection;
        }

        public bool IsInvalidContext = false;
        public object Content;
        //public string TypeName;
        public Type ItemType;
        public string MemberName;
        public bool IsRoot = false;
        public bool IsCollection = false;
        public int CurrCollectionItem = 0;
    }
}