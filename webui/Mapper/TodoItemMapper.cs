using Microsoft.AspNetCore.Http;

namespace webui.Mapper
{
    public class TodoItemMapper
    {
        public static Service.Models.TodoItem Map(IFormCollection collection)
        {
            var viewModel = TodoItemModelViewMapper.Map(collection);
            return Map(viewModel);
        }

        public static Service.Models.TodoItem Map(webui.Models.TodoItemViewModel item)
        {
            return new Service.Models.TodoItem
            {
                Id = item.Id > 0 ? item.Id : (long?)null,
                Name = item.Name,
                IsComplete = item.IsComplete
            };
        }
    }
}
