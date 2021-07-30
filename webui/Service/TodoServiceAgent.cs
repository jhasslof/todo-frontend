using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webui.Service.Models;

namespace webui.Service
{
    public class TodoServiceAgent
    {
        private ITodoServiceContext Context { get; set; }

        public TodoServiceAgent(ITodoServiceContext context)
        {
            Context = context;
        }

        public IEnumerable<Models.TodoItemDTO> Todo()
        {
            return Context.TodoItems();
        }

        public Models.TodoItemDTO Get(int id)
        {
            return Context.Get(id);
        }

        internal void Update(TodoItemDTO editItem)
        {
            Context.Update(editItem);
        }

        internal void Create(Models.TodoItemDTO newItem)
        {
            Context.Create(newItem);
        }

        internal void Delete(int id)
        {
            Context.Delete(id);
        }
        internal IEnumerable<string> SupportedFeatureFlags()
        {
            return Context.SupportedFeatureFlags();
        }
    }
}
