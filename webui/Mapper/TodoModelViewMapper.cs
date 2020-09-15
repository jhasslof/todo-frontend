using Microsoft.AspNetCore.Http;
using System;

namespace webui.Mapper
{
    public class TodoModelViewMapper
    {
        public static Models.TodoViewModel Map(IFormCollection collection, Exception ex = null)
        {
            return new Models.TodoViewModel
            {
                Id = (collection.ContainsKey("Id") ? Convert.ToInt64(collection["Id"]) : 0),
                Name = collection["Name"],
                IsComplete = ConvertCheckBoxValueToBool(collection["IsComplete"]),
                ErrorMessage = (ex == null ? "" : ex.Message)
            };
        }

        public static Models.TodoViewModel Map(Service.Models.TodoItem item)
        {
            return new Models.TodoViewModel
            {
                Id = item.Id.Value,
                Name = item.Name,
                IsComplete = item.IsComplete
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
