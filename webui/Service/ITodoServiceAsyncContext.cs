using System.Collections.Generic;
using System.Threading.Tasks;
using webui.Service.Models;

namespace webui.Service
{
    public interface ITodoServiceAsyncContext
    {
        Task<IEnumerable<TodoItemDTO>> TodoItems();
        Task<TodoItemDTO> Get(int todoItemId);
        Task Update(TodoItemDTO editItem);
        Task Delete(int todoItemId);
        Task Create(Models.TodoItemDTO newItem);
        public Task<IEnumerable<FeatureFlagDTO>> SupportedFeatureFlags();
    }
}