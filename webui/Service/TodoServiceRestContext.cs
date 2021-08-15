using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using webui.Service.Models;

namespace webui.Service
{
    public class TodoServiceRestContext : ITodoServiceAsyncContext
    {
        private Uri _apiBaseUri;


        public TodoServiceRestContext(IConfiguration configuration)
        {
            _apiBaseUri = new Uri(configuration["TodoApiUrl"]);
        }

        // https://www.yogihosting.com/aspnet-core-consume-api/
        public async Task<IEnumerable<TodoItemDTO>> TodoItems()
        {
            List<TodoItemDTO> todoItems = new List<TodoItemDTO>();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = _apiBaseUri;
                using (var response = await httpClient.GetAsync("/api/TodoItems/"))
                {
                    response.EnsureSuccessStatusCode();
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    todoItems = JsonConvert.DeserializeObject<List<TodoItemDTO>>(apiResponse);
                }
            }
            return todoItems;
        }

        public async Task Create(TodoItemDTO newItem)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = _apiBaseUri;
                var response = await httpClient.PostAsync("/api/TodoItems/", 
                                                            new StringContent(JsonConvert.SerializeObject(newItem), 
                                                                                Encoding.UTF8, 
                                                                                "application/json"));
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<TodoItemDTO> Get(int todoItemId)
        {
            TodoItemDTO todoItem = new TodoItemDTO();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = _apiBaseUri;
                using (var response = await httpClient.GetAsync("/api/TodoItems/" + todoItemId))
                {
                    response.EnsureSuccessStatusCode();
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    todoItem = JsonConvert.DeserializeObject<TodoItemDTO>(apiResponse);
                }
            }
            return todoItem;
        }

        public async Task Update(TodoItemDTO editItem)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = _apiBaseUri;
                var response = await httpClient.PutAsync("/api/TodoItems/" + editItem.Id,
                                                            new StringContent(JsonConvert.SerializeObject(editItem),
                                                                                Encoding.UTF8,
                                                                                "application/json"));
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task Delete(int todoItemId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = _apiBaseUri;
                var response = await httpClient.DeleteAsync("/api/TodoItems/" + todoItemId);
                response.EnsureSuccessStatusCode();
            }
        }


        public async Task<IEnumerable<FeatureFlagDTO>> SupportedFeatureFlags()
        {
            List<FeatureFlagDTO> featureFlags = new List<FeatureFlagDTO>();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = _apiBaseUri;
                using (var response = await httpClient.GetAsync("/api/TodoFeatureFlags"))
                {
                    response.EnsureSuccessStatusCode();
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    featureFlags = JsonConvert.DeserializeObject<List<FeatureFlagDTO>>(apiResponse);
                }
            }
            return featureFlags;
        }
    }
}
