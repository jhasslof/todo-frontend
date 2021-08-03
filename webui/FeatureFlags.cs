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
    public class FeatureFlags
    {
        private static LaunchDarklyCredentials LaunchDarklyCredentials { get; set; }
        private readonly static IEnumerable<FeatureFlagViewModel> ControllerSupportedFeatureFlags = new[] {
            new FeatureFlagViewModel { Key = "todo-extra-info" },
            new FeatureFlagViewModel { Key = "new-welcome-message", UiOnly = true },
        };
        private static object bindConfigLoc = new object();

        public static IEnumerable<FeatureFlagViewModel> GetFeatureFlagsInUse(IConfiguration configuration, TodoServiceAsyncAgent serviceAgent)
        {
            lock (bindConfigLoc)
            {
                if (null == LaunchDarklyCredentials)
                {
                    var flagDefaultValuesConfig = configuration.GetSection("FeatureFlags:Defaults").GetChildren().ToList();
                    foreach (var flagDefaultValueConfig in flagDefaultValuesConfig)
                    {
                        var flagDefault = new FeatureFlagDefault();
                        flagDefaultValueConfig.Bind(flagDefault);
                        var featureFlag = ControllerSupportedFeatureFlags.Single(f => f.Key == flagDefault.Key);
                        featureFlag.State = flagDefault.State;
                    }
                    LaunchDarklyCredentials = new LaunchDarklyCredentials();
                    configuration.GetSection("FeatureFlags:LaunchDarklyCredentials").Bind(LaunchDarklyCredentials);
                }
            }

            //We can only use feature flags implemented by the controller
            //UiOnly == true : Feature Flag is not used by the backend service
            //UiOnly == false : We can only activate feature flags implemented by the controller and the backend service'
            var task = Task.Run(() => serviceAgent.SupportedFeatureFlags());
            var flags = (IEnumerable<Service.Models.FeatureFlagDTO>)task.GetType().GetProperty("Result").GetValue(task);
            IEnumerable<FeatureFlagViewModel> serviceSupportedFeatureFlags = flags.ToList().ConvertAll(new Converter<Service.Models.FeatureFlagDTO, FeatureFlagViewModel>(ViewModelFeatureFlagMapper.Map));
            var featurFlagsInUse = new List<FeatureFlagViewModel>();
            foreach (var featureFlag in ControllerSupportedFeatureFlags)
            {
                if (featureFlag.UiOnly)
                {
                    featurFlagsInUse.Add(new FeatureFlagViewModel { Key = featureFlag.Key, UiOnly = true, State = featureFlag.State });
                }
                else if (serviceSupportedFeatureFlags.Contains(featureFlag))
                {
                    featurFlagsInUse.Add(new FeatureFlagViewModel { Key = featureFlag.Key, State = featureFlag.State });
                }
            }
            foreach (var featureFlag in featurFlagsInUse)
            {
                if (LaunchDarklyCredentials.LdClient != null)
                {
                    featureFlag.State = LaunchDarklyCredentials.LdClient.BoolVariation(featureFlag.Key, LaunchDarklyCredentials.LdUser, false);
                }
            }
            return featurFlagsInUse;

        }
    }

    public class FeatureFlagDefault
    {
        public string Key { get; set; }
        public bool State { get; set; }
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
                if (!string.IsNullOrEmpty(value) && string.IsNullOrEmpty(user))
                {
                    user = value;
                    LdUser = LaunchDarkly.Sdk.User.WithKey(value);
                }
            }

        }
        public string SdkKey
        {
            get => sdkKey;
            set
            {
                if (!string.IsNullOrEmpty(value) && string.IsNullOrEmpty(sdkKey))
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
