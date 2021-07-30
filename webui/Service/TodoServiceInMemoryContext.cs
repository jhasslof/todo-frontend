using System;
using System.Collections.Generic;
using System.Linq;
using webui.Service.Models;

namespace webui.Service
{
    public class TodoServiceInMemoryContext : ITodoServiceContext
    {
        private IList<TodoItem> TodoItemsData { get; set; }

        private readonly IEnumerable<string> supportedFeatureFlags = new[] { "todo-extra-info" };  

        public TodoServiceInMemoryContext()
        {
            TodoItemsData = new List<TodoItem>
            {
                new TodoItem{Id = 1, Name = "Buy new phone", IsComplete = false},
                new TodoItem{Id = 2, Name = "Go running", IsComplete = false},
                new TodoItem{Id = 3, Name = "Code new demo", IsComplete = false},
                new TodoItem{Id = 4, Name = "Make dinner", IsComplete = false},
            };
        }

        public IEnumerable<TodoItem> TodoItems()
        {
            // Simulate calling api service to get todo data
            return TodoItemsData;
        }

        public TodoItem Get(int id)
        {
            return TodoItemsData.FirstOrDefault(i => i.Id == id);
        }

        public void Update(TodoItem editItem)
        {
            var item = TodoItemsData.SingleOrDefault(i => i.Id == editItem.Id);
            if(item != null)
            {
                item.Name = editItem.Name;
                item.IsComplete = editItem.IsComplete;
                item.Note = editItem.Note;
            }
            else { throw new ApplicationException($"Error saving '{editItem.Name}'. Item Id #{editItem.Id} not found."); }
        }

        public void Delete(int id)
        {
            var item = Get(id);
            TodoItemsData.Remove(item);
        }

        public void Create(TodoItem newItem)
        {
            if(newItem.Id.HasValue)
            {
                throw new ApplicationException($"Error saving '{newItem.Name}'. Invalid item id '{newItem.Id}'.");
            }
            newItem.Id = NextFreeId();
            TodoItemsData.Add(newItem);
        }

        private long NextFreeId()
        {
            return TodoItemsData.Max(i => i.Id.Value) + 1;
        }

        public IEnumerable<string> SupportedFeatureFlags()
        {
            return supportedFeatureFlags;
        }
    }
}
