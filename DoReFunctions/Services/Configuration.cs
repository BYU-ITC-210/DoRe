using System;
using Azure.Identity;
using Azure.Core;

namespace DnsForItLearningLabs 
{
    internal static class Configuration
    {
        // Only allow ManagedIdentityCredential and EnvironmentCredential
        static readonly DefaultAzureCredentialOptions s_credOptions = new DefaultAzureCredentialOptions()
        {
            ExcludeAzureCliCredential = true,
            ExcludeAzureDeveloperCliCredential = true,
            ExcludeAzurePowerShellCredential = true,
            ExcludeEnvironmentCredential = false,
            ExcludeInteractiveBrowserCredential = true,
            ExcludeManagedIdentityCredential = false,
            ExcludeSharedTokenCacheCredential = true,
            ExcludeVisualStudioCodeCredential = true,
            ExcludeVisualStudioCredential = true,
            ExcludeWorkloadIdentityCredential = true
        };

        public static string DnsZone { get; private set; }
        public static TokenCredential AzureCredential { get; private set; }

        static Configuration()
        {
            DnsZone = Environment.GetEnvironmentVariable("dns_zone", EnvironmentVariableTarget.Process) ?? string.Empty;
            AzureCredential = new DefaultAzureCredential(s_credOptions);
        }
    }
}
