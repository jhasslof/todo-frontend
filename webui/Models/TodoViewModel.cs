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

        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string ErrorMessage { get; set; }
    }

}
