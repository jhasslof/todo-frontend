using System;
using System.Collections.Generic;
using webui.Service.Models;

namespace webui.Service
{
    public interface ITodoServiceContext
    {
        public IEnumerable<TodoItem> TodoItems();
        public TodoItem Get(int id);
        void Update(TodoItem editItem);
        void Delete(int id);
        void Create(Models.TodoItem newItem);
        public IEnumerable<string> SupportedFeatureFlags();
    }
}