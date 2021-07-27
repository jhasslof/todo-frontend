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

        public IEnumerable<Models.TodoItem> Todo()
        {
            return Context.TodoItems();
        }

        public Models.TodoItem Get(int id)
        {
            return Context.Get(id);
        }

        internal void Update(TodoItem editItem)
        {
            Context.Update(editItem);
        }

        internal void Create(Models.TodoItem newItem)
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
