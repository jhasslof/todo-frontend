using System.Collections.Generic;
using System.Linq;

namespace webui.Models
{
    public class ViewModelWithFeatureFlags
    {
        public ViewModelWithFeatureFlags(IEnumerable<FeatureFlagViewModel> featureFlags)
        {
            FeatureFlags = featureFlags;
        }
        public IEnumerable<FeatureFlagViewModel> FeatureFlags;
        public bool FeatureFlagIsActive(string featureFlagKey)
        {
            var flag = FeatureFlags.SingleOrDefault(f => f.Key == featureFlagKey);
            return (flag == null ? false : flag.State);
        }
    }
}
