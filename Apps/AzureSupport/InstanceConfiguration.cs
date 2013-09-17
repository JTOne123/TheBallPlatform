﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure;

namespace TheBall
{
    public static class InstanceConfiguration
    {
        public static readonly string AWSAccessKey;
        public static readonly string AWSSecretKey;
        public static readonly string EmailFromAddress;
        public static readonly string EmailValidationSubjectFormat;
        public static readonly string EmailValidationMessageFormat;
        public static readonly string EmailDeviceJoinSubjectFormat;
        public static readonly string EmailDeviceJoinMessageFormat;
        public static readonly string EmailGroupJoinSubjectFormat;
        public static readonly string EmailGroupJoinMessageFormat;
        public static readonly string EmailInputJoinSubjectFormat;
        public static readonly string EmailInputJoinMessageFormat;
        public static readonly string EmailValidationURLWithoutID;
        public static readonly string AzureStorageConnectionString;
        public static readonly string WorkerActiveContainerName;

        static InstanceConfiguration()
        {
            #region Data storage

            const string ConnStrFileName = @"C:\users\kalle\work\ConnectionStringStorage\theballconnstr.txt";
            if (File.Exists(ConnStrFileName))
                AzureStorageConnectionString = File.ReadAllText(ConnStrFileName);
            else
                AzureStorageConnectionString = CloudConfigurationManager.GetSetting("DataConnectionString");
            WorkerActiveContainerName = CloudConfigurationManager.GetSetting("WorkerActiveContainerName");
            #endregion

            #region Email

            const string SecretFileName = @"C:\users\kalle\work\ConnectionStringStorage\amazonses.txt";
            string configString;
            if (File.Exists(SecretFileName))
                configString = File.ReadAllText(SecretFileName);
            else
                configString = CloudConfigurationManager.GetSetting("AmazonSESAccessInfo");
            string[] strValues = configString.Split(';');
            AWSAccessKey = strValues[0];
            AWSSecretKey = strValues[1];

            EmailFromAddress = CloudConfigurationManager.GetSetting("EmailFromAddress"); // "no-reply-theball@msunit.citrus.fi"
            EmailDeviceJoinMessageFormat = CloudConfigurationManager.GetSetting("EmailDeviceJoinMessageFormat");
            EmailDeviceJoinSubjectFormat = CloudConfigurationManager.GetSetting("EmailDeviceJoinSubjectFormat");
            EmailInputJoinSubjectFormat = CloudConfigurationManager.GetSetting("EmailInputJoinSubjectFormat");
            EmailInputJoinMessageFormat = CloudConfigurationManager.GetSetting("EmailInputJoinMessageFormat");
            EmailValidationSubjectFormat = CloudConfigurationManager.GetSetting("EmailValidationSubjectFormat");
            EmailValidationMessageFormat = CloudConfigurationManager.GetSetting("EmailValidationMessageFormat");
            EmailGroupJoinSubjectFormat = CloudConfigurationManager.GetSetting("EmailGroupJoinSubjectFormat");
            EmailGroupJoinMessageFormat = CloudConfigurationManager.GetSetting("EmailGroupJoinMessageFormat");
            EmailValidationURLWithoutID = CloudConfigurationManager.GetSetting("EmailValidationURLWithoutID");
#if hardcoded
            EmailDeviceJoinMessageFormat = @"Your confirmation is required to trust the following device '{0}' to be joined to trust within {1} ID {2}. 

Click the following link to confirm this action:
{3}";
            EmailInputJoinMessageFormat = @"Your confirmation is required to allow the following information source '{0}' to be fetched within {1} ID {2}. 

Click the following link to confirm this action:
{3}";

            EmailValidationMessageFormat = @"Welcome to The Open Innovation Platform!

You have just joined the collaboration platform by Aalto Global Impact. Your email address '{0}' has been registered on the OIP system. Before you start your collaboration we simply need to confirm that you did register your email. Please follow the link below during which you might be redirected to perform the authentication on OIP.

Use the following link to complete your registration (the link is valid for 30 minutes after which you need to resend the validation):
{1}

Wishing you all the best from OIP team!";

            EmailGroupJoinMessageFormat = @"You have been invited to join in the collaboration platform by Aalto Global Impact to collaborate in the group: {0}. 

Use the following link to accept the invitation and join the group:
{1}

The link is valid for 14 days, after which you need to request new invitation.";
#endif

            #endregion
        }

    }
}
