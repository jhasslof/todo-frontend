using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using webui.Service.Models;

namespace webui.IntegrationTest.TestServices
{
    public class TodoServiceTestContext : Service.ITodoServiceContext
    {
        private IList<TodoItemDTO> TodoItemsData { get; set; }
        private IList<string> SupportedFeatureFlagsData { get; set; }

        public TodoServiceTestContext(string[] todos, string[] supportedFeatureFlags)
        {
            TodoItemsData = new List<TodoItemDTO>();
            for (int i = 0; i < todos.Length; i++)
            {
                string todo = todos[i];
                TodoItemsData.Add(new TodoItemDTO { Id = i + 1, Name = todo, IsComplete = false });
            }

            SupportedFeatureFlagsData = new List<string>(supportedFeatureFlags);
        }

        public IEnumerable<TodoItemDTO> TodoItems()
        {
            // Simulate calling api service to get todo data
            return TodoItemsData;
        }

        public TodoItemDTO Get(int id)
        {
            return TodoItemsData.FirstOrDefault(i => i.Id == id);
        }

        public void Update(TodoItemDTO editItem)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(TodoItemDTO newItem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> SupportedFeatureFlags()
        {
            return SupportedFeatureFlagsData;
        }
    }
}
