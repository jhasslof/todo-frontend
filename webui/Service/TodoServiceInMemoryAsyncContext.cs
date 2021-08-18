using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webui.Service.Models;

namespace webui.Service
{
    public class TodoServiceInMemoryAsyncContext : ITodoServiceAsyncContext
    {
        private IList<TodoItemDTO> TodoItemsData { get; set; }

        private IList<FeatureFlagDTO> SupportedFeatureFlagsData { get; set; }

        public TodoServiceInMemoryAsyncContext()
        {
            TodoItemsData = new List<TodoItemDTO>
            {
                new TodoItemDTO{Id = 1, Name = "Buy new phone", IsComplete = false},
                new TodoItemDTO{Id = 2, Name = "Go running", IsComplete = false},
                new TodoItemDTO{Id = 3, Name = "Code new demo", IsComplete = false},
                new TodoItemDTO{Id = 4, Name = "Make dinner", IsComplete = false},
            };
        }

        // Simulate calling api service to get todo data
        public async Task<IEnumerable<TodoItemDTO>> TodoItems()
        {
            return await Task.Run(() => TodoItemsData);
        }

        public async Task<TodoItemDTO> Get(int todoItemId)
        {
            return await Task.Run(() => GetTodoItem(todoItemId));
        }
        private TodoItemDTO GetTodoItem(int todoItemId)
        {
            return TodoItemsData.FirstOrDefault(i => i.Id == todoItemId);
        }

        public async Task Update(TodoItemDTO editItem)
        {
            await Task.Run(() =>
                {
                    var item = TodoItemsData.SingleOrDefault(i => i.Id == editItem.Id);
                    if (item != null)
                    {
                        item.Name = editItem.Name;
                        item.IsComplete = editItem.IsComplete;
                    }
                    else { throw new ApplicationException($"Error saving '{editItem.Name}'. Item Id #{editItem.Id} not found."); }
                }
            );
        }

        public async Task Delete(int todoItemId)
        {
            await Task.Run(() =>
                {
                    var item = GetTodoItem(todoItemId);
                    TodoItemsData.Remove(item);
                }
            );
        }

        public async Task Create(TodoItemDTO newItem)
        {
            await Task.Run(() =>
                {
                    if (0 != newItem.Id)
                    {
                        throw new ApplicationException($"Error saving '{newItem.Name}'. Invalid item id '{newItem.Id}'.");
                    }
                    newItem.Id = NextFreeId();
                    TodoItemsData.Add(newItem);
                }
            );
        }

        private long NextFreeId()
        {
            return TodoItemsData.Max(i => i.Id) + 1;
        }

        public async Task<IEnumerable<FeatureFlagDTO>> SupportedFeatureFlags()
        {
            if(SupportedFeatureFlagsData == null)
            {
                return await Task.Run(() => new List<FeatureFlagDTO>().ToArray());
            }
            return await Task.Run(() => SupportedFeatureFlagsData.ToArray());
        }
    }
}
