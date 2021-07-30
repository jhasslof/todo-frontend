# ASP.NET Core MVC demo application with Selenium UI tests
![Build Status](https://ketchdigital.visualstudio.com/todo-AppService/_apis/build/status/jhasslof.todo-frontend?branchName=main)

## Acknowledgements
Thanks to:
* Scott Hansleman for getting me started on selenium-standalone in [BrowserIntegrationTesting with selenium](https://www.hanselman.com/blog/RealBrowserIntegrationTestingWithSeleniumStandaloneChromeAndASPNETCore21.aspx)
* Bertrand Thomas for the updates needed for [Quick fix for integration testing with Selenium in ASP.NET Core 3.1](https://blog-bertrand-thomas.devpro.fr/2020/01/27/fix-breaking-change-asp-net-core-3-integration-tests-selenium/)
* Andrew Lock for how to [creating parameterised tests in xUnit](https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/)


## Prerequisites

* [.Net SDK 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)
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

# Continuous Integration

1. Prepare an AzureDevops project for running your CI builds.  
   * [Build Repositories](https://docs.microsoft.com/en-us/azure/devops/pipelines/repos/github?view=azure-devops&tabs=yaml)
 

2. Create a build definition: `/.azure-pipelines.yml`and [Create a CI build](https://docs.microsoft.com/en-us/azure/devops/pipelines/repos/github?view=azure-devops&tabs=yaml#create-pipelines-in-multiple-azure-devops-organizations-and-projects) 

   * [Azure Pipelines Tasks](https://github.com/microsoft/azure-pipelines-tasks)
   * [YAML build file reference](https://docs.microsoft.com/en-us/azure/devops/pipelines/yaml-schema?view=azure-devops&tabs=schema%2Cparameter-schema) 
   
3. [Add PR CI validation](https://docs.microsoft.com/en-us/azure/devops/pipelines/repos/github?view=azure-devops&tabs=yaml#protected-branches)  

4. [Add status badge to your readme file](https://docs.microsoft.com/en-us/azure/devops/pipelines/create-first-pipeline?view=azure-devops&tabs=net%2Ctfs-2018-2%2Cbrowser#add-a-status-badge-to-your-repository)  

[![Build Status](https://ketchdigital.visualstudio.com/todo-AppService/_apis/build/status/jhasslof.todo-frontend?branchName=main)](https://ketchdigital.visualstudio.com/todo-AppService/_build/latest?definitionId=22&branchName=main)

# Add Launch Darkly Feature Flags

1. Follow the [Getting started](https://docs.launchdarkly.com/home/getting-started/feature-flags) guide and create a feature flag at the Launch Darkly web site  
2. [Install the SDK in your project][https://docs.launchdarkly.com/sdk/server-side/dotnet]  
3. Add a [Feature Flag implementation](https://azuredevopslabs.com/labs/vstsextend/launchdarkly/)  
* https://raw.githubusercontent.com/Microsoft/azuredevopslabs/master/labs/vstsextend/launchdarkly/codesnippet/HomeController.cs  

## Feature Flag Resources
* [FeatureToggle intro](https://martinfowler.com/bliki/FeatureToggle.html)
* [Implementing Feature Toggles](https://martinfowler.com/articles/feature-toggles.html)
* [Launch Darkly Guides](https://docs.launchdarkly.com/guides) 
* [KeystoneInterface](https://martinfowler.com/bliki/KeystoneInterface.html) 
* [Feature flag hierarchy](https://docs.launchdarkly.com/guides/best-practices/flag-hierarchy)

# Integrate with Jira 

## Integrate Github and Jira
[Overview of Jira dev tools integration](https://support.atlassian.com/jira-cloud-administration/docs/integrate-with-development-tools/)  

[Integrate Jira with GitHub](https://support.atlassian.com/jira-cloud-administration/docs/integrate-with-github/)  

[Setup the Github for Jira App](https://github.com/integrations/jira)  

[Process issues with smart commits](https://support.atlassian.com/jira-software-cloud/docs/process-issues-with-smart-commits/)  

[Enable Smart Commits in GitHub](https://support.atlassian.com/jira-cloud-administration/docs/enable-smart-commits/)  

[Reference issues in your development work](https://support.atlassian.com/jira-software-cloud/docs/reference-issues-in-your-development-work/)  

## Integrate Azure DevOps pipelines and Jira

[Azure Pipelines integration with Jira Software](https://devblogs.microsoft.com/devops/azure-pipelines-integration-with-jira-software/)

Jira App: [Azure Pipelines for Jira](https://marketplace.atlassian.com/apps/1220515/azure-pipelines-for-jira?hosting=cloud&tab=overview)  
Tutorial: [Integrate with Jira Issue tracking](https://github.com/microsoft/azure-pipelines-jira/blob/master/tutorial.md)  

* Can I also see builds performed by Azure Pipelines in Jira?  

  `The integration currently supports traceability for deployments (releases) only. Viewing build information in Jira is not supported.`

* Does the integration work for YAML Pipelines?  

  `The integration currently supports traceability for deployments from classic releases only. Builds and deployments from YAML pipelines is not supported.`

# Resources

* [ASP.NET Core fundamentals](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/?view=aspnetcore-3.1&tabs=windows)
* [Adding a xunit test project](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test)
* [Getting Started with xUnit.net](https://xunit.net/docs/getting-started/netfx/visual-studio)
* [UI tests with Selenium and ASP.NET Core MVC](https://code-maze.com/automatic-ui-testing-selenium-asp-net-core-mvc/)
* [Integration tests in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1)
* [Learn Razor Pages](https://www.learnrazorpages.com/)  
* [Simplify DisplayName Calls In Razor Views](https://khalidabuhakmeh.com/simplify-displayname-calls-in-razor) 