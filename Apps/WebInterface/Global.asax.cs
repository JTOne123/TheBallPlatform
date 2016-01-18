﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Common;
using System.Data.Linq.Mapping;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Routing;
using System.Web.DynamicData;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using AzureSupport;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure;
using Microsoft.WindowsAzure;
using SQLite.TheBall.Payments;
using SQLiteSupport;
using Stripe;
using TheBall;
using TheBall.CORE.InstanceSupport;
using MetaModel = System.Web.DynamicData.MetaModel;
using Process = System.Diagnostics.Process;

namespace WebInterface
{
    public class Global : System.Web.HttpApplication
    {

        private static string ensureXDrive()
        {
            //bool hasDriveX = DriveInfo.GetDrives().Any(item => item.Name.ToLower().StartsWith("X"));
            bool hasDriveX = false;
            if (!hasDriveX)
            {
                var infraAccountName = CloudConfigurationManager.GetSetting("CoreFileShareAccountName");
                var infraAccountKey = CloudConfigurationManager.GetSetting("CoreFileShareAccountKey");
                bool isCloud = infraAccountName != null && infraAccountKey != null;
                if (isCloud)
                {
                    var netPath = Path.Combine(Environment.SystemDirectory, "net.exe");
                    //var args = $@"use X: \\{infraAccountName}.file.core.windows.net\tbcore /u:{infraAccountName} {infraAccountKey}";
                    string sharedLocation = $@"\\{infraAccountName}.file.core.windows.net\tbcore";
                    var args = $@"use {sharedLocation} /u:{infraAccountName} {infraAccountKey}";
                    var startInfo = new ProcessStartInfo(netPath) { UseShellExecute = false, Arguments = args };
                    var netProc = new Process { StartInfo = startInfo };
                    netProc.Start();
                    netProc.WaitForExit();
                    return sharedLocation;
                }
            }
            return null;

        }

        protected void Application_Start(object sender, EventArgs e)
        {
            string connStr;

            ServicePointManager.DefaultConnectionLimit = 500;
            ServicePointManager.UseNagleAlgorithm = false;
            ServicePointManager.Expect100Continue = false;

            string initedPath = ensureXDrive();

            var infraDriveRoot = initedPath ?? Environment.GetEnvironmentVariable("TBCoreFolder");

             /*
             var infraDriveRoot = DriveInfo.GetDrives().Any(drive => drive.Name.StartsWith("X"))
                 ? @"X:\"
                 : Environment.GetEnvironmentVariable("TBCoreFolder");
            */
            string infraConfigFullPath =  Path.Combine(infraDriveRoot, @"Configs\InfraShared\InfraConfig.json");
            RuntimeConfiguration.UpdateInfraConfig(infraConfigFullPath).Wait();

            var instances = InfraSharedConfig.Current.InstanceNames;
            foreach (var instance in instances)
            {
                RuntimeConfiguration.UpdateInstanceConfig(instance).Wait();
            }

            var appInstanceKey = InfraSharedConfig.Current.AppInsightInstrumentationKey;
            if (!String.IsNullOrEmpty(appInstanceKey))
            {
                TelemetryConfiguration.Active.InstrumentationKey = appInstanceKey;
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //if(Request.Path == "default.htm")
            //    Response.RedirectPermanent("anon/default/oip-anon-landing-page.phtml", true);
            //if(Request.Path.ToLower().StartsWith("/theball") == false)
            //    Response.Redirect("/theballanon/oip-layouts/oip-edit-default-layout-jeroen.html", true);
            if (!Request.IsLocal && !Request.IsSecureConnection)
            {
                bool isWebSocket = Request.Path.StartsWith("/websocket/");
                bool isIndexAspx = Request.Path.StartsWith("index.aspx");
                // TODO: Line below is a hack, that's assuming www.prefix
                bool isWww = Request.Url.DnsSafeHost.StartsWith("www.") ||
                             Request.Url.DnsSafeHost.StartsWith("teaching.") ||
                             Request.Url.DnsSafeHost.StartsWith("ptt.") ||
                             Request.Url.DnsSafeHost.StartsWith("7lk.") ||
                             Request.Url.DnsSafeHost.StartsWith("globalimpact.") ||
                             Request.Url.DnsSafeHost.StartsWith("apps.") ||
                             Request.Url.DnsSafeHost.StartsWith("newglobal.") ||
                             Request.Url.DnsSafeHost.StartsWith("ptt-") ||
                             Request.Url.DnsSafeHost.StartsWith("ams.welearnit.org") ||
                             Request.Url.DnsSafeHost.StartsWith("ams-2015.welearnit.org") ||
                             Request.Url.DnsSafeHost.StartsWith("izenzei.probroz.info");
                if (isWebSocket == false && isIndexAspx == false && isWww == false)
                {
                    string redirectUrl = Request.Url.ToString().Replace("http:", "https:");
                    Response.Redirect(redirectUrl, true);
                }
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var ctx = HttpContext.Current;
            var request = ctx.Request;
            var authorization = request.Headers["Authorization"];
            if (authorization != null && authorization.StartsWith("DeviceAES:"))
            {
                string[] parts = authorization.Split(':');
                string trustID = parts[2];
                ctx.User = new GenericPrincipal(new GenericIdentity(trustID), new string[] { "DeviceAES"});
            } else
                AuthenticationSupport.SetUserFromCookieIfExists(HttpContext.Current);
        }

        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}