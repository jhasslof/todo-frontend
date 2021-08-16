using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace webui.Models
{
    public class TodoViewModel : ViewModelWithFeatureFlags
    {
        public TodoViewModel(IFeatureFlags featureFlags):base(featureFlags)
        {
        }

        public string Environment;
        public string Version;
        public IList<TodoItemViewModel> TodoItems;
    }
}
