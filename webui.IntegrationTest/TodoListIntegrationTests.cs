﻿using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using webui.IntegrationTest.Helpers;
using webui.IntegrationTest.TestServices;
using webui.Service;
using Xunit;

namespace webui.IntegrationTest
{
    public class TodoListIntegrationTests : IClassFixture<WebApplicationFactory<webui.Startup>>
    {
        private readonly WebApplicationFactory<webui.Startup> _factory;

        public TodoListIntegrationTests(WebApplicationFactory<webui.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Index")]
        [InlineData("/Todo/Privacy")]
        public async Task Get_Endpoints_ReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Get_Todolist_ReturnsListOfTodoItemsSuccessfully()
        {
            string[] a = { "a", "b" };

            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                        {
                            services.AddScoped<ITodoServiceContext>(s => new TodoServiceTestContext(new[] {
                                "Mjölk",
                                "Smör",
                                "Bröd",
                                "Ägg",
                                "Ost",
                            }));
                        }
                    );
                }).CreateClient();
            int numberOfRows = 5;
            int numberOfColumns = 4;

            // Act
            var defaultPage = await client.GetAsync("/");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

            //Assert
            var tableElement = content.QuerySelector("table > tbody");
            Assert.Equal(numberOfRows, tableElement.Children.Length);
            foreach (var row in tableElement.Children)
            {
                Assert.Equal(numberOfColumns, row.Children.Length);

                var todoName = row.Children[1].TextContent;
                Assert.IsType<string>(todoName);
                Assert.False(string.IsNullOrEmpty(todoName));
                Debug.WriteLine($"Todo: {todoName.Trim()}");
            }
        }
    }
}
