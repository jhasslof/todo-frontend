using System.Collections.Generic;
using System.Threading.Tasks;
using webui.Service.Models;

namespace webui.Service
{
    public class TodoServiceAsyncAgent
    {
        private ITodoServiceAsyncContext Context { get; set; }

        public TodoServiceAsyncAgent(ITodoServiceAsyncContext context)
        {
            Context = context;
        }

        public Task<IEnumerable<Models.TodoItemDTO>> Todo()
        {
            return Context.TodoItems();
        }

        public Task<TodoItemDTO> Get(int id)
        {
            return Context.Get(id);
        }

        internal async Task Update(TodoItemDTO editItem)
        {
            await Context.Update(editItem);
        }

        internal async Task CreateAsync(Models.TodoItemDTO newItem)
        {
            await Context.Create(newItem);
        }

        internal async Task Delete(int id)
        {
            await Context .Delete(id);
        }
        internal Task<IEnumerable<FeatureFlagDTO>> SupportedFeatureFlags()
        {
            return Context.SupportedFeatureFlags();
        }
    }
}
