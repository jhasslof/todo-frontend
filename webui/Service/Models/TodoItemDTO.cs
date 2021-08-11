using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webui.Service.Models
{
    public class TodoItemDTO
    {
        public long Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public bool IsComplete { get; set; } = false;
        public string Note { get; set; } = "";
    }

    public class FeatureFlagDTO
    {
        public string Key { get; set; } = "";
        public bool State { get; set; }
    }
}
