﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TheBall;
using TheBall.CORE;
using TheBall.CORE.InstanceSupport;
using Process = System.Diagnostics.Process;

namespace WebCoreLayer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            initializePlatform();
            services.AddMvcCore().AddAuthorization();
            var authBuilder = services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(cookieOptions =>
                {
                    cookieOptions.Cookie.Name = "THEBALL_AUTH";
                    cookieOptions.LoginPath = "/Login/Login";
                    cookieOptions.LogoutPath = "/Login/Login";
                    cookieOptions.Events.OnSigningIn += async context =>
                    {
                        var currPrincipal = context.Principal;
                        var claimEmailValue = currPrincipal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
                        if(claimEmailValue == null)
                            throw new SecurityException("Email address from claim missing");

                        var emailID = Email.GetIDFromEmailAddress(claimEmailValue);
                        var email = await ObjectStorage.RetrieveFromSystemOwner<Email>(emailID);
                        if(email == null)
                            throw new SecurityException("Unknown email");
                        var emailAddress = email.EmailAddress;
                        var account = await ObjectStorage.RetrieveFromSystemOwner<Account>(email.Account);

                        var roles = account?.GroupMemberships.ToArray() ?? new string[0];
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Email.ToString(), emailAddress));
                        claims.Add(new Claim(ClaimTypes.Sid.ToString(), account.ID));
                        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role.ToString(), role)));
                        ClaimsIdentity ci = new ClaimsIdentity(new TheBallIdentity(account.ID, emailAddress, account.ID), claims);
                        var principal = new ClaimsPrincipal(ci);
                        context.Principal = principal;
                        /*
                        var loginUrl = Login.GetLoginUrlFromEmailAddress(emailAddress);
                        var loginID = Login.GetLoginIDFromLoginURL(loginUrl);
                        var login = await ObjectStorage.RetrieveFromSystemOwner<Login>(loginID);

                        var salt = login.PasswordSalt;
                        var accountID = login.Account;
                        */
                        /*
                        var currPrincipal = context.Principal;
                        var email = context.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
                        context.Principal = new GenericPrincipal(new TheBallIdentity(email, email email))
                        {

                        };*/

                    };
                });
            foreach (var instanceName in InfraSharedConfig.Current.InstanceNames)
            {
                addAdditionalAuthentication(instanceName, authBuilder);
            }
        }

        private void addAdditionalAuthentication(string instanceName, AuthenticationBuilder authBuilder)
        {
            var providerPrefix = instanceName;
            var config = RuntimeConfiguration.GetConfiguration(instanceName);
            var secureConfig = config.SecureConfig;

            bool hasGoogle = !String.IsNullOrEmpty(secureConfig.GoogleOAuthClientID);
            if(hasGoogle)
                authBuilder.AddGoogle(providerPrefix + "_Google", googleOptions =>
                    {
                        var clientId = secureConfig.GoogleOAuthClientID;
                        googleOptions.ClientId = clientId;
                        var clientSecret = secureConfig.GoogleOAuthClientSecret;
                        googleOptions.ClientSecret = clientSecret;
                    });

            bool hasFB = !String.IsNullOrEmpty(secureConfig.FacebookOAuthClientID);
            if(hasFB) 
                authBuilder.AddFacebook(providerPrefix + "_Facebook", facebookOptions =>
                    {
                        var clientId = secureConfig.FacebookOAuthClientID;
                        facebookOptions.ClientId = clientId;
                        var clientSecret = secureConfig.FacebookOAuthClientSecret;
                        facebookOptions.ClientSecret = clientSecret;
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseInformationContext();
            app.UseAuthentication();
            //app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute("AuthAccount", "auth/account/{*path}", new { controller = "auth", action = "Account" });
                routes.MapRoute("AuthGroup", "auth/grp/{groupId}/{*path}", new { controller = "auth", action = "Group" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }

        private void initializePlatform()
        {
            ServicePointManager.DefaultConnectionLimit = 500;
            ServicePointManager.UseNagleAlgorithm = false;
            ServicePointManager.Expect100Continue = false;

            string initedPath = ensureXDrive();

            var infraDriveRoot = initedPath ?? Environment.GetEnvironmentVariable("TBCoreFolder") ?? @"X:\";

            /*
            var infraDriveRoot = DriveInfo.GetDrives().Any(drive => drive.Name.StartsWith("X"))
                ? @"X:\"
                : Environment.GetEnvironmentVariable("TBCoreFolder");
           */
            string infraConfigFullPath = Path.Combine(infraDriveRoot, @"Configs\InfraShared\InfraConfig.json");
            RuntimeConfiguration.UpdateInfraConfig(infraConfigFullPath).Wait();

            var instances = InfraSharedConfig.Current.InstanceNames;
            foreach (var instance in instances)
            {
                RuntimeConfiguration.UpdateInstanceConfig(instance).Wait();
            }
        }


        private static string ensureXDrive()
        {
            bool hasDriveX = DriveInfo.GetDrives().Any(item => item.Name.ToLower().StartsWith("x"));
            //bool hasDriveX = false;
            if (!hasDriveX)
            {
                var infraAccountName = "";// CloudConfigurationManager.GetSetting("CoreFileShareAccountName");
                var infraAccountKey = ""; //CloudConfigurationManager.GetSetting("CoreFileShareAccountKey");
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

    }
}
