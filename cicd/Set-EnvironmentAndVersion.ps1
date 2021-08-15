[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)] [string] $rgName,
    [Parameter(Mandatory=$true)] [string] $appName,
    [Parameter(Mandatory=$true)] [string] $TodoControllerVersion,
    [Parameter(Mandatory=$true)] [string] $EnvironmentName
)
$ErrorActionPreference = 'Stop'

[string[]] $appsettings = $(
   "TodoControllerVersion=$TodoControllerVersion",
   "ASPNETCORE_ENVIRONMENT=$EnvironmentName"
);


az functionapp config appsettings set -g $rgName `
                                      -n $appName `
                                      --settings $appsettings