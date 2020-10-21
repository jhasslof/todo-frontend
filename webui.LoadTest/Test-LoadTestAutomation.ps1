# Distribure test to a temp folder
if((Test-Path c:\temp\jmeter-test) -eq $false){
    mkdir c:\temp\jmeter-test
    mkdir c:\temp\jmeter-test\params
    mkdir c:\temp\jmeter-test\results
}
cp .\webui-test-plan.jmx c:\temp\jmeter-test -Force
cp .\connection-parameters.csv c:\temp\jmeter-test\params -Force
cp .\todo-items-parameters.csv c:\temp\jmeter-test\params -Force
#ls  c:\temp\jmeter-test
#ls  c:\temp\jmeter-test\params

# Run test in the temp folder
${env:JMETER_HOME} = 'C:\jmeter' # update to your local installation
Push-Location ${env:JMETER_HOME}\bin
.\jmeter -n -t 'c:\temp\jmeter-test\webui-test-plan.jmx' -JparamDir='c:\temp\jmeter-test\params' -JresultDir='c:\temp\jmeter-test\results'
Pop-Location
Get-Content C:\temp\jmeter-test\results\todo-webui-result.csv