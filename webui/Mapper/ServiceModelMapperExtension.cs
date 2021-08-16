using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace webui.Mapper
{
    public static class ServiceModelMapperExtension
    {
        public static Service.Models.TodoItemDTO Map(this Service.Models.TodoItemDTO todoItem, IFormCollection collection, IFeatureFlags featureFlags)
        {
            Models.TodoItemDetailsViewModel viewModel = new Models.TodoItemDetailsViewModel(featureFlags).Map(collection);
            return todoItem.Map(viewModel);
        }

        public static Service.Models.TodoItemDTO Map(this Service.Models.TodoItemDTO todoItem, webui.Models.TodoItemDetailsViewModel item)
        {
            todoItem.Id = item.TodoItem.Id; // > 0 ? item.TodoItem.Id : (long?)null;
            todoItem.Name = item.TodoItem.Name;
            todoItem.IsComplete = item.TodoItem.IsComplete;
            if (item.FeatureFlags.FeatureFlagIsActive("ta-7-notes-web-ui"))
            {
                todoItem.Notes = item.TodoItem.Notes;
            }
            return todoItem;
        }
    }
}
