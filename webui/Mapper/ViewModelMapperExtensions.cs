using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace webui.Mapper
{
    public static class ViewModelMapperExtensions
    {
        public static Models.TodoViewModel Map(this Models.TodoViewModel todoVm, IList<Service.Models.TodoItem> serviceItems, IEnumerable<Models.FeatureFlagViewModel> featureFlags)
        {
            var vmTodoItems = new List<Models.TodoItemViewModel>();
            foreach(var serviceItem in serviceItems)
            {
                vmTodoItems.Add(new Models.TodoItemViewModel().Map(serviceItem));
            }
            todoVm.featureFlags = featureFlags;
            todoVm.todoItems = vmTodoItems;
            return todoVm;
        }

        public static Models.TodoItemDetailsViewModel Map(this Models.TodoItemDetailsViewModel todoItemDetailsVm, Service.Models.TodoItem serviceItem, IEnumerable<Models.FeatureFlagViewModel> featureFlags)
        {
            todoItemDetailsVm.TodoItem = new Models.TodoItemViewModel().Map(serviceItem);
            todoItemDetailsVm.featureFlags = featureFlags;
            return todoItemDetailsVm;
        }

        public static Models.TodoItemViewModel Map(this Models.TodoItemViewModel todoItemVm, Service.Models.TodoItem serviceItem)
        {
            todoItemVm.Id = serviceItem.Id.Value;
            todoItemVm.Name = serviceItem.Name;
            todoItemVm.IsComplete = serviceItem.IsComplete;
            return todoItemVm;
        }

        public static Models.TodoItemDetailsViewModel Map(this Models.TodoItemDetailsViewModel todoItemVm, IFormCollection collection, IEnumerable<Models.FeatureFlagViewModel> featureFlags, Exception ex = null)
        {
            todoItemVm.TodoItem = new Models.TodoItemViewModel
            {
                Id = (collection.ContainsKey("TodoItem.Id") ? Convert.ToInt64(collection["TodoItem.Id"]) : 0),
                Name = collection["TodoItem.Name"],
                IsComplete = ConvertCheckBoxValueToBool(collection["TodoItem.IsComplete"]),
                ErrorMessage = (ex == null ? "" : ex.Message)
            };
            todoItemVm.featureFlags = featureFlags;
            return todoItemVm;
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
