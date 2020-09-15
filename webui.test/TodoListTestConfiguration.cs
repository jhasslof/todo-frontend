using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace webui.test
{
    public class TodoListTestConfiguration
    {
        public ChromeBrowserDriverConfiguration ChromeBrowserDriver { get; private set; } = new ChromeBrowserDriverConfiguration();
        
        public ServiceHostConfiguration ServiceHost { get; private set; } = new ServiceHostConfiguration();
        
        public TestData TestData { get; private set; } = new TestData();

        public TodoListTestConfiguration()
        {
            var config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                    .Build();

            config.GetSection("chromeBrowserDriver").Bind(ChromeBrowserDriver);
            config.GetSection("serviceHost").Bind(ServiceHost);

            List<RowItem> rowItemList = new List<RowItem>();
            var rows = config.GetSection("testData").GetChildren().ToList();
            foreach (var row in rows)
            {
                var rowItem = new RowItem();
                row.Bind(rowItem);
                rowItemList.Add(rowItem);
            }
            TestData.rowItems = rowItemList.ToArray();
        }
    }

    public class ChromeBrowserDriverConfiguration
    {
        public string[] ChromeOptions { get; set; }
    }

    public class ServiceHostConfiguration
    {
        public string Name { get; set; }
        public string Port { get; set; }
    }

    public class TestData
    {
        public RowItem[] rowItems;
    }

    public class RowItem
    {
        internal RowItem() { }
        public RowItem(string id, string name)
        {
            Id = id;
            Name = name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
