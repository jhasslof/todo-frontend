using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using webui.Models;
using webui.Service;

namespace webui
{
    public interface IFeatureFlags
    {
        IEnumerable<FeatureFlagViewModel> GetFeatureFlagList();
        bool FeatureFlagIsActive(string featureFlagKey);
    }

    public class FeatureFlags : IFeatureFlags
    {
        IEnumerable<FeatureFlagViewModel> _featureFlags;

        public FeatureFlags(IConfiguration configuration, ITodoServiceAsyncContext context)
        {
            _featureFlags = FeatureFlagsFactory.GetFeatureFlagsInUse(configuration, context);
        }

        IEnumerable<FeatureFlagViewModel> IFeatureFlags.GetFeatureFlagList()
        {
            return _featureFlags;
        }

        public bool FeatureFlagIsActive(string featureFlagKey)
        {
            var flag = _featureFlags.SingleOrDefault(f => f.Key == featureFlagKey);
            return (flag == null ? false : flag.State);
        }

    }
}
