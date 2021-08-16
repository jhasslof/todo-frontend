using System.Collections.Generic;

namespace webui.Models
{
    public class TodoItemDetailsViewModel : ViewModelWithFeatureFlags
    {
        public TodoItemDetailsViewModel(IFeatureFlags featureFlags):base(featureFlags)
        {
        }

        public TodoItemViewModel TodoItem { get; set; }
    }
}
