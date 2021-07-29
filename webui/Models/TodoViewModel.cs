using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webui.Models
{
    public class ViewModelWithFeatureFlags
    {
        public ViewModelWithFeatureFlags()
        {
        }
        public IEnumerable<FeatureFlagViewModel> featureFlags;
        public bool FeatureFlagIsActive(string featureFlagKey)
        {
            var flag = featureFlags.SingleOrDefault(f => f.Key == featureFlagKey);
            return (flag == null ? false : flag.State);
        }
    }
     public class TodoViewModel : ViewModelWithFeatureFlags
    {
        public TodoViewModel()
        {
        }

        public IList<TodoItemViewModel> todoItems;
    }

    public class TodoItemDetailsViewModel : ViewModelWithFeatureFlags
    {
        public TodoItemDetailsViewModel()
        {
        }

        public TodoItemViewModel TodoItem { get; set; }
    }

    public class TodoItemViewModel
    {
        public TodoItemViewModel()
        {
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string ErrorMessage { get; set; }
        public string Note { get; set; }
    }

    public class FeatureFlagViewModel
    {
        public FeatureFlagViewModel()
        {
        }

        public string Key { get; set; }
        public bool State { get; set; } = false;
        public bool UiOnly { get; set; } = false;

        public override bool Equals(object obj)
        {
            return this.Key == ((FeatureFlagViewModel)obj).Key;
        }
        public override int GetHashCode()
        {
            //removes CS0659
            return base.GetHashCode();
        }
    }
}
