﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureSupport;
using Nito.AsyncEx;
using TheBall;
using TheBall.CORE;
using TheBall.CORE.InstanceSupport;

namespace OperationRunnerTool
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncContext.Run(() => MainAsync(args));
        }


        private static string getConfigOption(string optionPrefix, string[] args, bool required = true)
        {
            var optionPrefixPart = $"-{optionPrefix.ToLower()}:";
            var configOption =
                args.FirstOrDefault(arg => arg.ToLower().StartsWith(optionPrefixPart))?.Substring(optionPrefixPart.Length);
            if(configOption == null && required)
                throw new ArgumentException("Mandatory option missing: " + optionPrefix);
            return configOption;
        }

        private static string[] getOperationParameterValuePairs(string[] args)
        {
            var paramOption = getConfigOption("p", args, false);
            if(paramOption == null)
                return new string[0];
            var parameterValuePairs = paramOption.Split(',').Select(str => str.Trim()).ToArray();
            return parameterValuePairs;
        }

        private static byte[] getOperationParameterObjectAsContentBytes(string[] args)
        {
            var paramOption = getConfigOption("p", args, false);
            if (paramOption == null)
                return null;
            var jsonCandidateContent = paramOption.Trim();
            if (!jsonCandidateContent.StartsWith("{") || !jsonCandidateContent.EndsWith("}"))
                return null;
            var byteResult = Encoding.UTF8.GetBytes(jsonCandidateContent);
            return byteResult;
        }

        static async void MainAsync(string[] args)
        {
            var configRoot = getConfigOption("cr", args);
            var instanceName = getConfigOption("i", args);
            var ownerInfo = getConfigOption("owner", args, false)?.Replace("_", "/");
            var remoteExecute = getConfigOption("remoteExecute", args, false);
            var useWorker = remoteExecute != null && Boolean.Parse(remoteExecute);
            var operationOwner = ownerInfo != null ? VirtualOwner.FigureOwner(ownerInfo) : SystemSupport.SystemOwner;
            var operationName = getConfigOption("op", args);
            var nameValueParameters = getOperationParameterValuePairs(args).Select(parValuePair =>
            {
                var splitPair = parValuePair.Split(':');
                if(splitPair.Length < 2)
                    throw new ArgumentException("Value part missing for parameter: " + parValuePair);
                var parName = splitPair[0];
                var parValue = splitPair[1];
                return new
                {
                    Name = parName,
                    Value = parValue
                };
            }).ToArray();
            var jsonParameters = getOperationParameterObjectAsContentBytes(args);

            var formValues = nameValueParameters.ToDictionary(item => item.Name, item => item.Value);

            var infraConfigPath = Path.Combine(configRoot, "InfraShared", "InfraConfig.json");
            //var secureConfigPath = Path.Combine(configRoot, instanceName, "SecureConfig.json");
            //var instanceConfigPath = Path.Combine(configRoot, instanceName, "InstanceConfig.json");

            await RuntimeConfiguration.InitializeRuntimeConfigs(infraConfigPath);

            var iCtx = InformationContext.InitializeToLogicalContext(operationOwner, instanceName);

            var httpOperationData = new HttpOperationData()
            {
                OwnerRootLocation = operationOwner.GetOwnerPrefix(),
                OperationName = operationName,
                FormValues = formValues,
                RequestContent = jsonParameters
            };

            if (useWorker)
                await OperationSupport.QueueHttpOperationAsync(httpOperationData);
            else
                await OperationSupport.ExecuteHttpOperationAsync(httpOperationData);
        }
    }

#if examples

    -cr:D:\UserData\Kalle\work\abs\home.theball.me_infrashare\Configs -i:home.theball.me -op:TheBall.CORE.CreateGroup -p:GroupID:ab4487b1-d9ad-4de9-a361-3929a8884b51

    -cr:D:\UserData\Kalle\work\abs\home.theball.me_infrashare\Configs -i:home.theball.me -op:TheBall.CORE.SetGroupMembership -p:GroupID:ab4487b1-d9ad-4de9-a361-3929a8884b51,AccountID:2856ef1c-af21-488b-8ed4-0bb72f152e0a,Role:Collaborator

    -cr:D:\UserData\Kalle\work\abs\home.theball.me_infrashare\Configs -i:home.theball.me -op:TheBall.CORE.UpdateContainerOwnerTemplates -p:OwnerRootLocation:grp/cc6db374-b530-485e-bb08-b9003725a7f5,TemplateName:cpanel

    -cr:D:\UserData\Kalle\work\abs\home.theball.me_infrashare\Configs -i:home.theball.me -op:TheBall.CORE.UpdateContainerOwnerTemplates -remoteExecute:true -p:OwnerRootLocation:acc/2856ef1c-af21-488b-8ed4-0bb72f152e0a,TemplateName:cpanel

    -cr:D:\UserData\Kalle\work\abs\home.theball.me_infrashare\Configs -i:home.theball.me -op:TheBall.CORE.UpdateTemplateForAllGroups -remoteExecute:false -p:TemplateName:cpanel
    -cr:D:\UserData\Kalle\work\abs\home.theball.me_infrashare\Configs -i:home.theball.me -op:TheBall.CORE.UpdateTemplateForAllGroups -remoteExecute:true -p:TemplateName:cpanel

    -cr:D:\UserData\Kalle\work\abs\home.theball.me_infrashare\Configs -i:home.theball.me -op:TheBall.CORE.UpdateTemplateForAllAccounts -remoteExecute:false -p:TemplateName:cpanel
    -cr:D:\UserData\Kalle\work\abs\home.theball.me_infrashare\Configs -i:home.theball.me -op:TheBall.CORE.UpdateTemplateForAllAccounts -remoteExecute:true -p:TemplateName:cpanel

    -cr:D:\UserData\Kalle\work\abs\home.theball.me_infrashare\Configs -i:home.theball.me -op:TheBall.CORE.UpdateAccountMembershipStatuses -remoteExecute:false -p:AccountID:2856ef1c-af21-488b-8ed4-0bb72f152e0a,GroupID:cc6db374-b530-485e-bb08-b9003725a7f5
    -cr:D:\UserData\Kalle\work\abs\home.theball.me_infrashare\Configs -i:home.theball.me -op:TheBall.CORE.UpdateGroupMembershipStatuses -remoteExecute:false -p:AccountID:2856ef1c-af21-488b-8ed4-0bb72f152e0a,GroupID:cc6db374-b530-485e-bb08-b9003725a7f5
    -cr:D:\UserData\Kalle\work\abs\home.theball.me_infrashare\Configs -i:home.theball.me -owner:grp/ab4487b1-d9ad-4de9-a361-3929a8884b51 -op:TheBall.Interface.SaveGroupDetails -remoteExecute:false -p:"{GroupName: \"Test Belt Promo\"}"

    -cr:D:\UserData\Kalle\work\abs\home.theball.me_infrashare\Configs -i:members.ikwondo.com -op:TheBall.CORE.CreateGroup -p:GroupID:f6a9652b-4065-404e-9749-01d40ba5f26a
    -cr:D:\UserData\Kalle\work\abs\home.theball.me_infrashare\Configs -i:members.ikwondo.com -owner:grp/f6a9652b-4065-404e-9749-01d40ba5f26a -op:TheBall.Interface.SaveGroupDetails -remoteExecute:false -p:"{GroupName: \"Platform Admin\"}"
    -cr:D:\UserData\Kalle\work\abs\home.theball.me_infrashare\Configs -i:members.ikwondo.com -op:TheBall.CORE.SetGroupMembership -remoteExecute:true -p:GroupID:f6a9652b-4065-404e-9749-01d40ba5f26a,AccountID:4cb27608-79fc-4ae5-9428-e45a0f2326e6,Role:Initiator

#endif

}
