using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webui.Service.Models;

namespace webui.IntegrationTest.TestServices
{
    public class TodoServiceAsyncTestContext : Service.ITodoServiceAsyncContext
    {
        private IList<TodoItemDTO> TodoItemsData { get; set; }
        private IList<FeatureFlagDTO> SupportedFeatureFlagsData { get; set; }

        public TodoServiceAsyncTestContext(string[] todos, string[] supportedFeatureFlags)
        {
            TodoItemsData = new List<TodoItemDTO>();
            for (int i = 0; i < todos.Length; i++)
            {
                string todo = todos[i];
                TodoItemsData.Add(new TodoItemDTO { Id = i + 1, Name = todo, IsComplete = false });
            }

            SupportedFeatureFlagsData = new List<FeatureFlagDTO>();
            foreach(var flag in supportedFeatureFlags)
            {
                SupportedFeatureFlagsData.Add(new FeatureFlagDTO { Key = flag });
            } 
        }

        // Simulate calling api service to get todo data

        public async Task<IEnumerable<TodoItemDTO>> TodoItems()
        {
            return await Task.Run(() => TodoItemsData);
        }

        public async Task<TodoItemDTO> Get(int todoItemId)
        {
            return await Task.Run(() => TodoItemsData.FirstOrDefault(i => i.Id == todoItemId));
        }

        public Task Update(TodoItemDTO editItem)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int todoItemId)
        {
            throw new NotImplementedException();
        }

        public Task Create(TodoItemDTO newItem)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FeatureFlagDTO>> SupportedFeatureFlags()
        {
            return await Task.Run(() => SupportedFeatureFlagsData.ToArray());
        }
    }
}
