﻿using System;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Http;
using TheBall;
using TheBall.CORE.InstanceSupport;

namespace AzureSupport
{
    public static class WebSupport
    {
        public static void EndResponseWithStatusCode(this HttpContext context, int statusCode)
        {
            var response = context.Response;
            response.StatusCode = statusCode;
            //response.Flush();
            //response.SuppressContent = true;
            //context.ApplicationInstance.CompleteRequest();
        }

        public static string GetLoginUrl(IPrincipal identityPrincipal)
        {
            return identityPrincipal.Identity.Name;
        }

        public static string GetLoginUrl(HttpContext context)
        {
            return GetLoginUrl(context.User);
        }

        /// <summary>
        /// Encodes a string to be represented as a string literal. The format
        /// is essentially a JSON string.
        /// 
        /// The string returned includes outer quotes 
        /// Example Output: "Hello \"Rick\"!\r\nRock on"
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string EncodeJsString(string s)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\"");
            foreach (char c in s)
            {
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        int i = (int)c;
                        if (i < 32 || i > 127)
                        {
                            sb.AppendFormat("\\u{0:X04}", i);
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }
            sb.Append("\"");

            return sb.ToString();
        }
    }
}