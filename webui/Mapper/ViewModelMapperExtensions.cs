using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace webui.Mapper
{
    public static class ViewModelMapperExtensions
    {
        public static Models.TodoViewModel Map(this Models.TodoViewModel todoVm, IList<Service.Models.TodoItemDTO> serviceItems)
        {
            var vmTodoItems = new List<Models.TodoItemViewModel>();
            foreach (var serviceItem in serviceItems)
            {
                var todoItem = new Models.TodoItemViewModel().Map(serviceItem);
                if (todoVm.FeatureFlagIsActive("todo-extra-info"))
                {
                    todoItem.MapTodoExtraInfo(serviceItem);
                }
                vmTodoItems.Add(todoItem);
            }
            todoVm.TodoItems = vmTodoItems;
            return todoVm;
        }

        public static Models.TodoItemViewModel Map(this Models.TodoItemViewModel todoItemVm, Service.Models.TodoItemDTO serviceItem)
        {
            todoItemVm.Id = serviceItem.Id;
            todoItemVm.Name = serviceItem.Name;
            todoItemVm.IsComplete = serviceItem.IsComplete;
            return todoItemVm;
        }
        public static Models.TodoItemViewModel MapTodoExtraInfo(this Models.TodoItemViewModel todoItemVm, Service.Models.TodoItemDTO serviceItem)
        {
            todoItemVm.Note = serviceItem.Note;
            return todoItemVm;
        }

        public static Models.TodoItemDetailsViewModel Map(this Models.TodoItemDetailsViewModel todoItemDetailsVm, Service.Models.TodoItemDTO serviceItem)
        {
            todoItemDetailsVm.TodoItem = new Models.TodoItemViewModel().Map(serviceItem);
            if (todoItemDetailsVm.FeatureFlagIsActive("todo-extra-info"))
            {
                todoItemDetailsVm.TodoItem.MapTodoExtraInfo(serviceItem);
            }
            
            return todoItemDetailsVm;
        }

        public static Models.TodoItemDetailsViewModel Map(this Models.TodoItemDetailsViewModel todoItemDetailsVm, IFormCollection collection, Exception ex = null)
        {
            todoItemDetailsVm.TodoItem = new Models.TodoItemViewModel
            {
                Id = (collection.ContainsKey("TodoItem.Id") ? Convert.ToInt64(collection["TodoItem.Id"]) : 0),
                Name = collection["TodoItem.Name"],
                IsComplete = ConvertCheckBoxValueToBool(collection["TodoItem.IsComplete"]),
                ErrorMessage = (ex == null ? "" : ex.Message)
            };
            
            if (todoItemDetailsVm.FeatureFlagIsActive("todo-extra-info"))
            {
                todoItemDetailsVm.TodoItem.Note = collection["TodoItem.Note"];
            }
            return todoItemDetailsVm;
        }

        public static bool ConvertCheckBoxValueToBool(string checkboxResult)
        {
            return Convert.ToBoolean(ConvertCheckBoxValueToString(checkboxResult));
        }

        public static string ConvertCheckBoxValueToString(string checkboxResult)
        {
            //https://www.learnrazorpages.com/razor-pages/forms/checkboxes
            return (checkboxResult).Split(",")[0];
        }
    }
}
