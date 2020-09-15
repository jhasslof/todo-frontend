using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Net.Http;
using System.Threading;
using Xunit;

namespace webui.test
{
    public class UITestBase  : IClassFixture<SeleniumServerFactory<Startup>>, IDisposable
    {
        public SeleniumServerFactory<Startup> Server { get; }

        public ChromeDriver Browser { get; }

        public HttpClient Client { get; }

        public ILogs Logs { get; }

        public TodoListTestConfiguration TestConfiguration { get; private set; } = new TodoListTestConfiguration();

        public Uri BaseUri
        {
            get
            {
                //only use when testing on remote service
                return new Uri($"https://{TestConfiguration.ServiceHost.Name}:{TestConfiguration.ServiceHost.Port}");
            }
        }

        public UITestBase(SeleniumServerFactory<Startup> server)
        {
            Server = server;
            Client = server.CreateClient(); //weird side effecty thing here. This call shouldn't be required for setup, but it is.

            Browser = NewBrowserDriver();
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

        public void Dispose()
        {
            Browser.Dispose();
        }
    }
}
