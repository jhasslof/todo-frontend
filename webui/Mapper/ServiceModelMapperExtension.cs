using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace webui.Mapper
{
    public static class ServiceModelMapperExtension
    {
        public static Service.Models.TodoItemDTO Map(this Service.Models.TodoItemDTO todoItem, IFormCollection collection, IEnumerable<Models.FeatureFlagViewModel> featureFlags)
        {
            Models.TodoItemDetailsViewModel viewModel = new Models.TodoItemDetailsViewModel(featureFlags).Map(collection);
            return todoItem.Map(viewModel);
        }

        public static Service.Models.TodoItemDTO Map(this Service.Models.TodoItemDTO todoItem, webui.Models.TodoItemDetailsViewModel item)
        {
            todoItem.Id = item.TodoItem.Id; // > 0 ? item.TodoItem.Id : (long?)null;
            todoItem.Name = item.TodoItem.Name;
            todoItem.IsComplete = item.TodoItem.IsComplete;
            if (item.FeatureFlagIsActive("todo-extra-info"))
            {
                todoItem.Note = item.TodoItem.Note;
            }
            return todoItem;
        }
    }
}
