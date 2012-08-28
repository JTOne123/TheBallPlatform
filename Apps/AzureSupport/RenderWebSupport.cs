﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace TheBall
{
    public static class RenderWebSupport
    {
        private static int RootTagLen;
        private static int RootTagsTotalLen;
        private static int CollTagLen;
        private static int CollTagsTotalLen;
        private static int ObjTagLen;
        private static int ObjTagsTotalLen;
        private static int AtomTagLen;
        private static int AtomTagsTotalLen;
        private const string RootTagBegin = "<!-- THEBALL-CONTEXT-ROOT-BEGIN:";
        private const string CollectionTagBegin = "<!-- THEBALL-CONTEXT-COLLECTION-BEGIN:";
        private const string ObjectTagBegin = "<!-- THEBALL-CONTEXT-OBJECT-BEGIN:";
        private const string CommentEnd = " -->";
        private const string ContextTagEnd = "<!-- THEBALL-CONTEXT-END:";
        private const string TheBallPrefix = "<!-- THEBALL-";
        private const string AtomTagBegin = "[!ATOM]";
        private const string AtomTagEnd = "[ATOM!]";
        private const string MemberAtomPattern = @"(?<fulltag>\[!ATOM](?<membername>\w*)\[ATOM!])";

        static RenderWebSupport()
        {
            RootTagLen = RootTagBegin.Length;
            RootTagsTotalLen = RootTagBegin.Length + CommentEnd.Length;
            CollTagLen = CollectionTagBegin.Length;
            CollTagsTotalLen = CollectionTagBegin.Length + CommentEnd.Length;
            ObjTagLen = ObjectTagBegin.Length;
            ObjTagsTotalLen = ObjectTagBegin.Length + CommentEnd.Length;
            AtomTagLen = AtomTagBegin.Length;
            AtomTagsTotalLen = AtomTagBegin.Length + AtomTagEnd.Length;
        }

        public static string RenderTemplateWithContent(string templatePage, object content)
        {
            StringBuilder result = new StringBuilder(templatePage.Length);
            Stack<StackContextItem> contextStack = new Stack<StackContextItem>();
            List<ErrorItem> errorList = new List<ErrorItem>();

            string[] lines = templatePage.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            int endIndexExclusive = lines.Length;
            ProcessLinesScope(lines, 0, ref endIndexExclusive, result, content, contextStack, errorList);
            if(errorList.Count > 0)
            {
                result.Insert(0, RenderErrorListAsHtml(errorList, "Errors - Databinding"));
            }
            return result.ToString();
        }

        private static void ProcessLinesScope(string[] lines, int startIndex, ref int endIndexExclusive, StringBuilder result, object rootContent, Stack<StackContextItem> contextStack, List<ErrorItem> errorList)
        {
            bool hasEmptyStackToBegin = contextStack.Count == 0;
            for (int currLineIX = startIndex; currLineIX < endIndexExclusive; currLineIX++)
            {
                string line = lines[currLineIX];
                try
                {
                    if (IgnoreBindingsTillEnd(line))
                    {
                        break;
                    }

                    bool hasStackContext = ProcessLine(lines, ref currLineIX, line, result, rootContent, contextStack, errorList);
                    // If stack is empty and we had context to begin with, stop here
                    if (hasStackContext == false && hasEmptyStackToBegin == false)
                    {
                        endIndexExclusive = currLineIX + 1;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    StackContextItem item = contextStack.Count > 0 ? contextStack.Peek() : null;
                    ErrorItem errorItem = new ErrorItem(ex, item, line);
                    errorList.Add(errorItem);
                }
            }
        }

        private static bool ProcessLine(string[] lines, ref int currLineIx, string line, StringBuilder result, object content, Stack<StackContextItem> contextStack, List<ErrorItem> errorList)
        {
            if (line.StartsWith(TheBallPrefix) == false && line.Contains("[!ATOM]") == false)
            {
                result.AppendLine(line);
                return contextStack.Count > 0;
            }
            if (line.StartsWith(RootTagBegin))
            {
                // TODO: Multiple container support; type and instance ID mapping
                string typeName = line.Substring(RootTagLen, line.Length - RootTagsTotalLen).Trim();
                StackContextItem parent = contextStack.Count > 0 ? contextStack.Peek() : null;
                StackContextItem rootItem = new StackContextItem(content, parent, content.GetType(), null, true, false);
                if (contextStack.Count != 0)
                    throw new InvalidDataException("Context stack already has a root item before: " + typeName);
                contextStack.Push(rootItem);
            }
            else if (line.StartsWith(CollectionTagBegin))
            {
                string memberName = line.Substring(CollTagLen, line.Length - CollTagsTotalLen).Trim();
                Stack<StackContextItem> collStack = new Stack<StackContextItem>();
                StackContextItem currCtx = contextStack.Peek();
                StackContextItem collItem = null;
                try
                {
                    Type type = GetMemberType(currCtx, memberName);
                    object contentValue = GetPropertyValue(currCtx, memberName);
                    StackContextItem parent = currCtx;
                    collItem = new StackContextItem(contentValue, parent, type, memberName, false, true);
                } catch(Exception ex)
                {
                    if (collItem == null)
                        collItem = new StackContextItem("Invalid Collection Context", contextStack.Peek(), typeof(string), "INVALID", false, true);
                }
                collStack.Push(collItem);
                int scopeStartIx = currLineIx + 1;
                int scopeEndIx = lines.Length; // Candidate, the lower call will adjust properly
                // Get scope length
                ProcessLinesScope(lines, scopeStartIx, ref scopeEndIx, new StringBuilder(), content, collStack,
                                  new List<ErrorItem>());
                bool isFirstRound = true;
                while (collItem.IsNotFullyProcessed)
                {
                    var currErrorList = isFirstRound ? errorList : new List<ErrorItem>();
                    collStack.Push(collItem);
                    ProcessLinesScope(lines, scopeStartIx, ref scopeEndIx, result, content, collStack,
                                        currErrorList);
                    if(collStack.Count > 0)
                        throw new InvalidDataException("Collection stack should be empty at this point");
                    isFirstRound = false;
                    collItem.CurrCollectionItem++;
                }
                // Jump to the end tag (as the next loop will progress by one)
                currLineIx = scopeEndIx - 1;
            }
            else if (line.StartsWith(ObjectTagBegin))
            {
                string memberName = line.Substring(ObjTagLen, line.Length - ObjTagsTotalLen).Trim();
                StackContextItem currCtx = contextStack.Peek();
                if (memberName == "*")
                {
                    // Put top item again to stack
                    contextStack.Push(currCtx);
                }
                else
                {
                    StackContextItem objItem = null;
                    try
                    {
                        Type type = GetMemberType(currCtx, memberName);
                        object contentValue = GetPropertyValue(currCtx, memberName);
                        StackContextItem parent = currCtx;
                        objItem = new StackContextItem(contentValue, parent, type, memberName, false, false);
                    } finally
                    {
                        if(objItem == null)
                            objItem = new StackContextItem("Invalid Context", contextStack.Peek(), typeof(string), "INVALID", false, false);
                        contextStack.Push(objItem);
                    }
                }
            }
            else if (line.StartsWith(ContextTagEnd))
            {
                StackContextItem popItem = contextStack.Pop();
                if (contextStack.Count == 0)
                    return false;
            }
            else // ATOM line
            {
                StackContextItem currCtx = contextStack.Peek();
                bool isCollection = false;
                if (currCtx.IsCollection == true)
                    isCollection = true;
                var contentLine = Regex.Replace(line, MemberAtomPattern,
                                                match =>
                                                    {
                                                        string memberName = match.Groups["membername"].Value;
                                                        object value = GetPropertyValue(currCtx, memberName);
                                                        return (value ?? (currCtx.MemberName + "." + memberName + " is null")).ToString();
                                                    });
                result.AppendLine(contentLine);
            }
            return contextStack.Count > 0;
        }

        public static string RenderErrorListAsHtml(List<ErrorItem> errorList, string errorLabel)
        {
            StringBuilder errorHtml = new StringBuilder();
            errorHtml.AppendLine("<h1>" + errorLabel + "</h1>");
            errorHtml.AppendLine("<p><table border=\"1\"><tr><th>Context</th><th>Error</th><th>Line</th></tr>");
            var tableList = errorList.
                Where(errorItem => errorItem.CurrentStackItem != null || errorItem.CurrentContextName != null).
                Select(errorItem =>
                           {
                               string currentContextName;
                               if (errorItem.CurrentStackItem != null)
                               {
                                   currentContextName = errorItem.CurrentStackItem.ItemType.Name + "." +
                                                       errorItem.CurrentStackItem.MemberName;
                               }
                               else
                               {
                                   currentContextName = errorItem.CurrentContextName;
                               }
                               string errorMessage = errorItem.ErrorException != null
                                                         ? errorItem.ErrorException.Message
                                                         : errorItem.ErrorMessage;
                               return new
                                          {
                                              Name = currentContextName,
                                              ErrorMessage = errorMessage,
                                              CurrLine = errorItem.CurrentLine.Trim()
                                          };

                           });
            foreach(var tableRow in tableList.Distinct())
            {
                errorHtml.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>",
                                       tableRow.Name, tableRow.ErrorMessage, tableRow.CurrLine);
            }
            errorHtml.AppendLine("</table></p>");
            return errorHtml.ToString();
        }

        private static bool IgnoreBindingsTillEnd(string line)
        {
            return line.StartsWith("<!-- IGNOREBINDINGS FROM THIS POINT ONWARDS -->");
        }

        private static object GetPropertyValue(StackContextItem currCtx, string propertyName)
        {
            if(currCtx.CurrContent == null)
                throw new InvalidDataException("Object: " + currCtx.MemberName + " does not have content (was retrieving value: " + propertyName + ")");
            //Type type = currCtx.ItemType;
            Type type = currCtx.CurrContent.GetType();
            PropertyInfo pi = type.GetProperty(propertyName);
            return pi.GetValue(currCtx.CurrContent, null);
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
}