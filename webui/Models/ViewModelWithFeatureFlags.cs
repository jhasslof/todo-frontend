using System.Collections.Generic;
using System.Linq;

namespace webui.Models
{
    public class ViewModelWithFeatureFlags
    {
        public ViewModelWithFeatureFlags(IFeatureFlags featureFlags)
        {
            FeatureFlags = featureFlags;
        }
        public IFeatureFlags FeatureFlags;
    }
}
