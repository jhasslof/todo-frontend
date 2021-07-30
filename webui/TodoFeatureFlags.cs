using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webui.Mapper;
using webui.Models;
using webui.Service;

namespace webui
{
    public class TodoFeatureFlags
    {
        private static LaunchDarklyCredentials LaunchDarklyCredentials { get; set; } = new LaunchDarklyCredentials();

        private readonly static IEnumerable<FeatureFlagViewModel> ControllerSupportedFeatureFlags = new[] {
            new FeatureFlagViewModel { Key = "todo-extra-info" },
            new FeatureFlagViewModel { Key = "new-welcome-message", UiOnly = true },
        };

        public static IEnumerable<FeatureFlagViewModel> GetFeatureFlagsInUse(IConfiguration configuration, TodoServiceAgent serviceAgent)
        {
            configuration.GetSection("LaunchDarklyCredentials").Bind(LaunchDarklyCredentials);

            //We can only use feature flags implemented by the controller
            //UiOnly == true : Feature Flag is not used by the backend service
            //UiOnly == false : We can only activate feature flags implemented by the controller and the backend service'
            IEnumerable<FeatureFlagViewModel> serviceSupportedFeatureFlags = serviceAgent.SupportedFeatureFlags().ToList().ConvertAll(new Converter<string, FeatureFlagViewModel>(ViewModelFeatureFlagMapper.Map));
            var featurFlagsInUse = new List<FeatureFlagViewModel>();
            foreach (var featureFlag in ControllerSupportedFeatureFlags)
            {
                if (featureFlag.UiOnly)
                {
                    featurFlagsInUse.Add(new FeatureFlagViewModel { Key = featureFlag.Key, UiOnly = true });
                }
                else if (serviceSupportedFeatureFlags.Contains(featureFlag))
                {
                    featurFlagsInUse.Add(new FeatureFlagViewModel { Key = featureFlag.Key });
                }
            }
            foreach (var featureFlag in featurFlagsInUse)
            {
                featureFlag.State = LaunchDarklyCredentials.LdClient.BoolVariation(featureFlag.Key, LaunchDarklyCredentials.LdUser, false);
            }
            return featurFlagsInUse;
        }
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
