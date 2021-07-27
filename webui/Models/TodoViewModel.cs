using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webui.Models
{
    public class TodoViewModel
    {
        public TodoViewModel()
        {
        }

        public IList<TodoItemViewModel> todoItems;
        public IEnumerable<FeatureFlagViewModel> featureFlags;
    }

    public class TodoItemDetailsViewModel
    {
        public TodoItemDetailsViewModel()
        {
        }

        public TodoItemViewModel TodoItem { get; set; }
        public IEnumerable<FeatureFlagViewModel> featureFlags;
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
    }

    public class FeatureFlagViewModel
    {
        public FeatureFlagViewModel()
        {
        }

        public string Key { get; set; }
        public bool State { get; set; }
    }
}
