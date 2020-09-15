using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace webui.test
{
    //
    // Example of helper funtions
    //

    class UITestHelpers
    {
        public static void WaitForElementByClassNameExists(ChromeDriver driver, string className)
        {
            Thread.Sleep(1000);
            var timeout = 10000; // in milliseconds
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeout));
            wait.Until(ExpectedConditions.ElementExists(By.ClassName(className)));
        }

        public static void WaitForElementByIdExists(ChromeDriver driver, string id)
        {
            Thread.Sleep(1000);
            var timeout = 10000; // in milliseconds
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeout));
            wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
        }

        public static string GetRandomString(int length = 5)
        {
            if (length > 31)
            {
                throw new ArgumentException("Max random string length is 31");
            }
            return Guid.NewGuid().ToString().Substring(1, length);
        }

        //public static void LoginUser(ChromeDriver driver, Uri loginPage)
        //{
        //    driver.Navigate().GoToUrl(loginPage);
        //    //WaitForElementByIdExists(driver, "UserName");

        //    driver.FindElementById("UserName").SendKeys(TestConfiguration.TestData.Name);
        //    driver.FindElementById("Password").SendKeys(TestConfiguration.TestData.Password);
        //    driver.FindElementById("btnLogin").Click();
        //}

        public static void NavigateTo(ChromeDriver driver, string menuItemName)
        {
            var menu = driver.FindElementByClassName("main-menu");
            var menuItems = menu.FindElements(By.ClassName("navigationElement"));
            foreach (var item in menuItems)
            {
                if (item.TagName == "a" && item.Text == menuItemName)
                {
                    item.Click();
                    return;
                }
            }
            throw new Exception($"Menu Item: {menuItemName} Not found!");
        }
    }
}
