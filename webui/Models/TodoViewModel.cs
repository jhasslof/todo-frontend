using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace webui.Models
{
    public class TodoViewModel : ViewModelWithFeatureFlags
    {
        public TodoViewModel(IEnumerable<FeatureFlagViewModel> featureFlags):base(featureFlags)
        {
        }

        public IList<TodoItemViewModel> todoItems;
    }
}
