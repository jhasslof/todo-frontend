using Microsoft.AspNetCore.Http;
using System;

namespace webui.Mapper
{
    public class TodoItemModelViewMapper
    {
        public static Models.TodoItemViewModel Map(IFormCollection collection, Exception ex = null)
        {
            if (collection.ContainsKey("TodoItem.Id"))
            {
                return new Models.TodoItemViewModel
                {
                    Id = (collection.ContainsKey("TodoItem.Id") ? Convert.ToInt64(collection["TodoItem.Id"]) : 0),
                    Name = collection["TodoItem.Name"],
                    IsComplete = ConvertCheckBoxValueToBool(collection["TodoItem.IsComplete"]),
                    ErrorMessage = (ex == null ? "" : ex.Message)
                };
            }
            else
            {
                return new Models.TodoItemViewModel
                {
                    Id = (collection.ContainsKey("Id") ? Convert.ToInt64(collection["Id"]) : 0),
                    Name = collection["Name"],
                    IsComplete = ConvertCheckBoxValueToBool(collection["IsComplete"]),
                    ErrorMessage = (ex == null ? "" : ex.Message)
                };
            }
        }

        public static Models.TodoItemViewModel Map(Service.Models.TodoItem item)
        {
            return new Models.TodoItemViewModel
            {
                Id = item.Id.Value,
                Name = item.Name,
                IsComplete = item.IsComplete
            };
        }

        public static Models.FeatureFlagViewModel Map(string featureFlagKey)
        {
            return new Models.FeatureFlagViewModel
            {
                Key = featureFlagKey,
                State = Controllers.TodoController.ldClient.BoolVariation(featureFlagKey, Controllers.TodoController.ldUser, false)
            };
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
