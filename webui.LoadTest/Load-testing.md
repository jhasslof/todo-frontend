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
    - Edit fields for `Filename` in  `Simple Data Writer` & `Summary Report` to match your fliesystem. 
2. Run testplan and examine result
    - Run the test plan and examine the results.
    - Make changes in the assert pages and see how that affect the test result

- *Ref: https://jmeter.apache.org/usermanual/build-web-test-plan.html*

# Testing the todo list

1. Prepp testplan
    - Open the testplan  `..\webui.LoadTest\webui-test-plan.jmx`
    - Edit fields for `Filename` in  `Simple Data Writer`, `Summary Report`& `Aggregate Graph` to match your fliesystem. 
2. Start the Todo application
```powershell
C:\..\todo-frontend> cd .\webui
C:\..\todo-frontend\webui> dotnet run
```
- Test in a browser that your app is running at https://localhost:5001

2. Run testplan and examine result
    - Run the test plan and examine the results.
    - Make changes in the assert pages and see how that affect the test result

# Run Load Test from command line
* [Use CLI mode](https://jmeter.apache.org/usermanual/best-practices.html#lean_mean)

`TODO: Add parameters and testresult output here`
 

# JMeter Best Practices
* https://jmeter.apache.org/usermanual/best-practices.html


* http://sqa.fyicenter.com/1000074_Auto_Flush_JMeter_Test_Result_to_File.html
--> Edit C:\jmeter\bin\jmeter.properties
