using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webui.Models;

namespace webui
{
    public class Settings
    {

        public Settings(IConfiguration configuration)
        {
            var a = configuration.GetSection("LaunchDarklyCredentials");
            configuration.GetSection("LaunchDarklyCredentials").Bind(LaunchDarklyCredentials);
        }

        public static LaunchDarklyCredentials LaunchDarklyCredentials { get; set; } = new LaunchDarklyCredentials();

        public readonly static IEnumerable<FeatureFlagViewModel> ControllerSupportedFeatureFlags = new[] {
            new FeatureFlagViewModel { Key = "todo-extra-info" },
            new FeatureFlagViewModel { Key = "new-welcome-message", UiOnly = true },
        };

    }


    public class LaunchDarklyCredentials
    {
        private string user;
        private string sdkKey;

        public string User
        {
            get => user;
            set
            {
                if (string.IsNullOrEmpty(user))
                {
                    user = value;
                    LdUser = LaunchDarkly.Sdk.User.WithKey(value);
                }
            }

        }
        public string SdkKey { 
            get => sdkKey;
            set
            {
                if (string.IsNullOrEmpty(sdkKey))
                {
                    sdkKey = value;
                    LdClient = new LaunchDarkly.Sdk.Server.LdClient(value);
                }

            } 
        }

        //LdClient should be a singleton
        //https://docs.launchdarkly.com/sdk/server-side/dotnet
        public LaunchDarkly.Sdk.Server.LdClient LdClient { get; private set; }

        public LaunchDarkly.Sdk.User LdUser { get; private set; }

    }
}
