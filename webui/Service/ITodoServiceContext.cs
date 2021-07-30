using System;
using System.Collections.Generic;
using webui.Service.Models;

namespace webui.Service
{
    public interface ITodoServiceContext
    {
        public IEnumerable<TodoItemDTO> TodoItems();
        public TodoItemDTO Get(int id);
        void Update(TodoItemDTO editItem);
        void Delete(int id);
        void Create(Models.TodoItemDTO newItem);
        public IEnumerable<string> SupportedFeatureFlags();
    }
}