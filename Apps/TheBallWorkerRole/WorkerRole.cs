using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Nito.AsyncEx;
using TheBall.Infra.AzureRoleSupport;

namespace TheBallWorkerRole
{
    public class WorkerRole : AcceleratorRole
    {
        protected override string ComponentName => "TheBallWorkerConsole";
        protected override AzureRoleType RoleType => AzureRoleType.WorkerRole;
        protected override string AppRootFolder => RoleEnvironment.GetLocalResource("WorkerFolder").RootPath;
        protected override string AppConfigPath => @"X:\Configs\WorkerConsole.json";
    }
}
