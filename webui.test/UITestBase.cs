using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace webui.test
{
    public class UITestBase
    {
        public TodoListTestConfiguration TestConfiguration { get; private set; } = new TodoListTestConfiguration();

        public Uri BaseUri
        {
            get
            {
                return new Uri($"https://{TestConfiguration.ServiceHost.Name}:{TestConfiguration.ServiceHost.Port}");
            }
        }

        protected ChromeDriver NewBrowserDriver()
        {
            var chromeOptions = new ChromeOptions();
            if (TestConfiguration.ChromeBrowserDriver.ChromeOptions != null)
            {
                foreach (var option in TestConfiguration.ChromeBrowserDriver.ChromeOptions)
                {
                    chromeOptions.AddArgument(option);
                }
            }
            //chromeOptions.AddArgument("headless");
            //chromeOptions.AddArgument("test-type");
            //chromeOptions.AddArgument("disable-gpu");
            //chromeOptions.AddArgument("no-first-run");
            //chromeOptions.AddArgument("no-default-browser-check");
            //chromeOptions.AddArgument("ignore-certificate-errors");
            //chromeOptions.AddArgument("start-maximized");

            var driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, chromeOptions);
            return driver;
        }
    }
}
