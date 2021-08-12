using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace webui.test
{
    // Thanks to:
    // https://www.hanselman.com/blog/RealBrowserIntegrationTestingWithSeleniumStandaloneChromeAndASPNETCore21.aspx
    // https://blog-bertrand-thomas.devpro.fr/2020/01/27/fix-breaking-change-asp-net-core-3-integration-tests-selenium/
    //
    public class SeleniumServerFactory<TStartup> : WebApplicationFactory<Startup> where TStartup : class
    {
        public string RootUri { get; set; } //Save this use by tests

        Process _process;
        IWebHost _host;

        public SeleniumServerFactory()
        {
            ClientOptions.BaseAddress = new Uri("https://localhost"); //will follow redirects by default

            _process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "selenium-standalone"
                    ,Arguments = "start"
                    ,UseShellExecute = true 
                    ,WindowStyle = ProcessWindowStyle.Hidden //Open console to view selenium-standalone output, good for debugging.
                }
            };
            _process.Start();

            CreateServer(CreateWebHostBuilder()); //In net.core 3.1 we need make explicit call to CreateServer here
        }

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            //Real TCP port
            _host = builder.Build();
            _host.Start();
            RootUri = _host.ServerFeatures.Get<IServerAddressesFeature>().Addresses.LastOrDefault(); //Last is https://localhost:5001!

            //Fake Server we won't use...this is lame. Should be cleaner, or a utility class
            return new TestServer(new WebHostBuilder().UseStartup<TStartup>());
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var builder = WebHost.CreateDefaultBuilder(Array.Empty<string>()) //TODO: Read the settings from webui.test appsettings.json
                            .UseEnvironment("Development") // Enable environment = Development
                            .UseSolutionRelativeContentRoot("webui"); // Enable loading CSS by setting content root to the SUT project

            builder.UseStartup<webui.TestHostStartup>(); // Use SUT Startup.cs when starting the website. If you want to have a custom test startup
                                            // this is the place to inject another class.
            return builder;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _host.Dispose();
                _process.CloseMainWindow(); //Be sure to stop Selenium Standalone
                _process.Close();
                _process.Dispose();
            }
        }
    }
}