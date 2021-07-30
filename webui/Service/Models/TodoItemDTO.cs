using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webui.Service.Models
{
    public class TodoItemDTO
    {
        public TodoItemDTO()
        {
        }

        public long? Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Note { get; set; }
    }
}
