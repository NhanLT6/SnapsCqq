# Get the directory where the script is located
$scriptPath = Split-Path -Parent -Path $MyInvocation.MyCommand.Definition

Set-Location $scriptPath
dotnet run