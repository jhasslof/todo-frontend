using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace webui.Mapper
{
    public static class ServiceModelMapperExtension
    {
        public static Service.Models.TodoItem Map(this Service.Models.TodoItem todoItem, IFormCollection collection, IEnumerable<Models.FeatureFlagViewModel> featureFlags)
        {
            Models.TodoItemDetailsViewModel viewModel = new Models.TodoItemDetailsViewModel().Map(collection, featureFlags);
            return todoItem.Map(viewModel);
        }

        public static Service.Models.TodoItem Map(this Service.Models.TodoItem todoItem, webui.Models.TodoItemDetailsViewModel item)
        {
            if (item.featureFlags.First().State)
            {
                return TodoExtraInfoMap(item);
            }
            else
            {
                return new Service.Models.TodoItem
                {
                    Id = item.TodoItem.Id > 0 ? item.TodoItem.Id : (long?)null,
                    Name = item.TodoItem.Name,
                    IsComplete = item.TodoItem.IsComplete
                };
            }
        }

        private static Service.Models.TodoItem TodoExtraInfoMap(Models.TodoItemDetailsViewModel item)
        {
            return new Service.Models.TodoItem
            {
                Id = item.TodoItem.Id > 0 ? item.TodoItem.Id : (long?)null,
                Name = item.TodoItem.Name,
                IsComplete = item.TodoItem.IsComplete,
                Note = item.TodoItem.Note
            };
        }
    }
}
