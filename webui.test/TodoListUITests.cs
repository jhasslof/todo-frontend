using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using Xunit;

namespace webui.test
{
    public class TodoListUITests : UITestBase
    {
        [Fact]
        public void TodoList_ViewStartpage_Succeed()
        {
            using (var driver = this.NewBrowserDriver())
            {

                driver.Navigate().GoToUrl(this.BaseUri);

                var headline = driver.FindElementById("ListHeadline");
                Assert.Equal("Things todo", headline.Text);
            }
        }

        [Fact]
        public void TodoList_AssertTodoItemRows_MatchingAppsettingTestData()
        {
            using (var driver = this.NewBrowserDriver())
            {
                driver.Navigate().GoToUrl(this.BaseUri);

                var rows = driver.FindElementsByXPath("//table/tbody/tr");
                int assertedRows = 0;
                foreach (var itemToAssert in TestConfiguration.TestData.rowItems)
                {
                    foreach (var row in rows)
                    {
                        var columns = row.FindElements(By.TagName("td"));
                        if (columns[0].Text == itemToAssert.Id)
                        {
                            Assert.Equal(itemToAssert.Name, columns[1].Text);
                            assertedRows++;
                        }
                    }
                }

                Assert.Equal(TestConfiguration.TestData.rowItems.Count(), assertedRows);
            }
        }

        [Theory]
        [InlineData("2", "Go running")]
        [InlineData("3", "Code new demo")]
        public void TodoList_AssertTodoItemRows_MatchingInlineData(string idToFind, string nameToFind)
        {
            using (var driver = this.NewBrowserDriver())
            {
                driver.Navigate().GoToUrl(this.BaseUri);

                var rowColumnsToFind = driver.FindElementsByXPath("//table/tbody/tr")
                       .SingleOrDefault(r => r.FindElements(By.TagName("td"))[0].Text == idToFind).FindElements(By.TagName("td"));

                Assert.Equal(idToFind, rowColumnsToFind.ElementAt(0).Text);
                Assert.Equal(nameToFind, rowColumnsToFind.ElementAt(1).Text);
            }
        }

        [Theory]
        [MemberData(nameof(InlineAppsettingTestData.Items), MemberType = typeof(InlineAppsettingTestData))]
        public void TodoList_AssertTodoItemRows_MatchingInlineDataFromAppsetting(string idToFind, string nameToFind)
        {
            using (var driver = this.NewBrowserDriver())
            {
                driver.Navigate().GoToUrl(this.BaseUri);

                var rowColumnsToFind = driver.FindElementsByXPath("//table/tbody/tr")
                       .SingleOrDefault(r => r.FindElements(By.TagName("td"))[0].Text == idToFind).FindElements(By.TagName("td"));

                Assert.Equal(idToFind, rowColumnsToFind.ElementAt(0).Text);
                Assert.Equal(nameToFind, rowColumnsToFind.ElementAt(1).Text);
            }
        }
    }
}
