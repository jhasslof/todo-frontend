# Load testing with
```
    _    ____   _    ____ _   _ _____       _ __  __ _____ _____ _____ ____
   / \  |  _ \ / \  / ___| | | | ____|     | |  \/  | ____|_   _| ____|  _ \
  / _ \ | |_) / _ \| |   | |_| |  _|    _  | | |\/| |  _|   | | |  _| | |_) |
 / ___ \|  __/ ___ \ |___|  _  | |___  | |_| | |  | | |___  | | | |___|  _ <
/_/   \_\_| /_/   \_\____|_| |_|_____|  \___/|_|  |_|_____| |_| |_____|_| \_\ 5.3

Copyright (c) 1999-2020 The Apache Software Foundation
https://jmeter.apache.org/
```


# Pre reqs  
- Java version: Install latest JDK version (min ver 8) - https://jmeter.apache.org/usermanual/get-started.html#java_versions  
See [Readme.md](..\Readme.md) for downloading and installing JDK.

- SSL Support - https://jmeter.apache.org/usermanual/get-started.html#opt_ssl

- Download JMeter - http://jmeter.apache.org/download_jmeter.cgi

- JMeter Installation - https://jmeter.apache.org/usermanual/get-started.html#install  
Unzip to `c:\jmeter`

# Get Started
https://jmeter.apache.org/usermanual/get-started.html

1. Start JMeter

```powershell
C:\> cd jmeter
C:\jmeter> .\bin\jmeter.bat
```
1. Prepp testplan
    - Open testplan `..\webui.LoadTest\demo-jmeter-web-test-plan.jmx`
    - Edit fields for `Filename` in  `Simple Data Writer` to match your fliesystem. 
2. Run testplan and examine result
    - Run the test plan and examine the results.
    - Make changes in the assert pages and see how that affect the test result
3. Examine the `jmeter.log` file to see that there are no errors.

- *Ref: https://jmeter.apache.org/usermanual/build-web-test-plan.html*

# Testing the todo list

1. Prepp testplan
    - Open the testplan  `..\webui.LoadTest\webui-test-plan.jmx`
    - Edit fields for `Filename` in `Site Connection Config`, `Todo Items Config` & `Simple Data Writer` to match your fliesystem.  
2. Start the Todo application
```powershell
C:\..\todo-frontend> cd .\webui
C:\..\todo-frontend\webui> dotnet run
```
- Test in a browser that your app is running at https://localhost:5001

2. Run testplan and examine result
    - Run the test plan and examine the results.
    - Make changes in the assert pages and see how that affect the test result

## Examine how parameters are used
- *Ref:  https://www.blazemeter.com/blog/jmeter-parameterization-the-complete-guide*

## Configure JMeter to overwrite result data
Open file `...\jmeter\bin\jmeter.properties\jmeter.properties` and set property:
```
resultcollector.action_if_file_exists=DELETE
```
* *Ref: http://sqa.fyicenter.com/1000074_Auto_Flush_JMeter_Test_Result_to_File.html*

## View test result data as table in VS Code

1. Add the VS Code extension [Edit csv](https://marketplace.visualstudio.com/items?itemName=janisdd.vscode-edit-csv)
2. Open the csv result file in VS Code and select `edit as csv`

## Run the Load Test from command line
* [Use CLI mode](https://jmeter.apache.org/usermanual/best-practices.html#lean_mean)
* [How to define properties on the command line](https://jmeter.apache.org/usermanual/functions.html#__P)
* [JMeter data source CSV Data Set Config from command line](https://stackoverflow.com/questions/24931419/jmeter-data-source-csv-data-set-config-from-command-line)
* [Properties Customization Guide](https://www.blazemeter.com/blog/apache-jmeter-properties-customization)

### Example:
1. Open powershell in this folder
2. Run the commands
```powershell
# Distribure test to a temp folder
mkdir c:\temp\jmeter-test
mkdir c:\temp\jmeter-test\params
mkdir c:\temp\jmeter-test\results
cp .\webui-test-plan.jmx c:\temp\jmeter-test
cp .\connection-parameters.csv c:\temp\jmeter-test\params
cp .\todo-items-parameters.csv c:\temp\jmeter-test\params
ls  c:\temp\jmeter-test
ls  c:\temp\jmeter-test\params

# Run test in the temp folder
${env:JMETER_HOME} = 'C:\jmeter' # update to your local installation
Push-Location ${env:JMETER_HOME}\bin
.\jmeter -n -t 'c:\temp\jmeter-test\webui-test-plan.jmx' -JparamDir='c:\temp\jmeter-test\params' -JresultDir='c:\temp\jmeter-test\results'
Pop-Location
code C:\temp\jmeter-test\results\todo-webui-result.csv
```

# JMeter Best Practices
* https://jmeter.apache.org/usermanual/best-practices.html


