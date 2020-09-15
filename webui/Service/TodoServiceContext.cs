using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webui.Service.Models;

namespace webui.Service
{
    public class TodoServiceContext : ITodoServiceContext
    {
        private IList<TodoItem> TodoItemsData { get; set; }

        public TodoServiceContext()
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
            }
            else { throw new ApplicationException($"Error saving '{editItem.Name}'. Item Id #{editItem.Id} not found."); }
        }

        public void Delete(int id)
        {
            var item = Get(id);
            TodoItemsData.Remove(item);
        }

        public void Create(Models.TodoItem newItem)
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
    }
}
