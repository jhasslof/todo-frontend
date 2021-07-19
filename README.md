# ASP.NET Core MVC demo application with Selenium UI tests

## Acknowledgements
Thanks to:
* Scott Hansleman for getting me started on selenium-standalone in [BrowserIntegrationTesting with selenium](https://www.hanselman.com/blog/RealBrowserIntegrationTestingWithSeleniumStandaloneChromeAndASPNETCore21.aspx)
* Bertrand Thomas for the updates needed for [Quick fix for integration testing with Selenium in ASP.NET Core 3.1](https://blog-bertrand-thomas.devpro.fr/2020/01/27/fix-breaking-change-asp-net-core-3-integration-tests-selenium/)
* Andrew Lock for how to [creating parameterised tests in xUnit](https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/)


## Prerequisites

* [.Net SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
* [Visual Studio 2019 16.4 or later](https://visualstudio.microsoft.com/downloads/?utm_medium=microsoft&utm_source=docs.microsoft.com&utm_campaign=inline+link&utm_content=download+vs2019) or [VSCode](https://code.visualstudio.com/download) with C# extensions
* A Chrome browser that is up to date

### Selenium standalone server
In branch `4-ui_tests_for_create_and_edit_views` we are going to use [selenium-standalone](https://www.npmjs.com/package/selenium-standalone) wich require:

1. Verify your java installation is up to date.

   Example: 

   ```powershell
   c:\> java -version
   openjdk version "16.0.1"
   ```

  * If you need to install java:
    * Download [jdk16](https://jdk.java.net/16/) as zip-file on Windows
    * Extract zip to `c:\java`
    * Add bin folder to system `PATH` --> `C:\java\jdk-16.0.1\bin`

2. Install node.js + npm  
   [Download and install node.js + npm](https://nodejs.org/en/download/)  
   
   `Note:` This .mis-file installs both node.js and npm. "Choco install nodejs..." does not include npm. 

3. Install npm package selenium-standalone as a global package
   
   ```powershell
   npm install -g selenium-standalone@latest
   selenium-standalone install
   npm install chromedriver
   ```
   
   To address all `high severity vulnerabilities` (including breaking changes), run:
   ```powershell
   npm audit fix --force
   ```

   test:
   ```powershell
   c:\> selenium-standalone start
   ```
   Accept jdk network access for all networks

4. [Trust the ASP.NET Core HTTPS development certificate on Windows and macOS](https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-5.0&tabs=visual-studio#trust-the-aspnet-core-https-development-certificate-on-windows-and-macos) 

   To trust the dotnet dev certificate, perform the one-time step:

   ```powershell
   dotnet dev-certs https --trust
   ```

# Usage

## Branches
* master
* 1-add_selenium_ui_test
* 2-integrationtest-and-dependency-injection
* 3-selenium-standalone-web-host
* 4-ui_tests_for_create_and_edit_views

## Getting started
1. Start by examining the web application in the master branch.  
2. Move consecutively through the other branches.
   * Run and examine the corresponding tests in the test explorer.

## Hints
### View/Hide Browser during tests
you control if the Chrome browser shall be visable during tests by adding or removeing the chromeOption `headless` in `.\webui.test\appsettings.json`
```json
{
  "chromeBrowserDriver": {
    "chromeOptions": [
      "headless"
    ]
  },
  ...
```

### If you need to debug selenium-standalone
Open `.\webui.test\SeleniumServerFactory.cs` and set the `ProcessStartInfo.WindowStyle = Normal`

```c#
_process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "selenium-standalone"
                    ,Arguments = "start"
                    ,UseShellExecute = true 
                    ,WindowStyle = ProcessWindowStyle.Hidden //Set to Normal for debugg
                }
            };
```

# Resources

* [ASP.NET Core fundamentals](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/?view=aspnetcore-3.1&tabs=windows)
* [Adding a xunit test project](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test)
* [Getting Started with xUnit.net](https://xunit.net/docs/getting-started/netfx/visual-studio)
* [UI tests with Selenium and ASP.NET Core MVC](https://code-maze.com/automatic-ui-testing-selenium-asp-net-core-mvc/)
* [Integration tests in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1)
