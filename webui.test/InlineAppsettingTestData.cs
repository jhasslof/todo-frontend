using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace webui.test
{
    public static class InlineAppsettingTestData
    {
        private static IEnumerable<object[]> dataItems;
        public static IEnumerable<object[]> Items { 
            get
            {
                if(dataItems == null)
                {
                    var config = new ConfigurationBuilder()
                                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                            .Build();

                    //http://www.codewrecks.com/blog/index.php/2020/03/14/net-core-configuration-array-with-bind/
                    List<RowItem> rowItemList = new List<RowItem>();
                    var rows = config.GetSection("testData").GetChildren().ToList();
                    foreach (var row in rows)
                    {
                        var rowItem = new RowItem();
                        row.Bind(rowItem);
                        rowItemList.Add(rowItem);
                    }
                    IList<object[]> testData = new List<object[]>();
                    foreach (RowItem item in rowItemList)
                    {
                        object id = item.Id;
                        object name = item.Name;
                        testData.Add(new object[] { id, name });
                    }
                    dataItems = testData;
                }
                return dataItems;
            }
         }
    }
}
