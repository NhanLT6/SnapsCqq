# Get the directory where the script is located
$scriptPath = Split-Path -Parent -Path $MyInvocation.MyCommand.Definition

# Define paths
$desktopPath = [Environment]::GetFolderPath('Desktop')
$shortcutName = "SnapsCqq.lnk"
$shortcutPath = Join-Path $desktopPath $shortcutName
$targetScript = Join-Path $scriptPath "run-project.ps1"

# Create shortcut
$WshShell = New-Object -comObject WScript.Shell
$Shortcut = $WshShell.CreateShortcut($shortcutPath)
$Shortcut.TargetPath = "C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe"
$Shortcut.Arguments = "-ExecutionPolicy Bypass -WindowStyle Hidden -File `"$targetScript`""
$Shortcut.WorkingDirectory = $scriptPath

# Optional: Set an icon if you have one
$Shortcut.IconLocation = Join-Path $scriptPath "cursor-default-click-outline.ico"

$Shortcut.Save()

Write-Host "Shortcut created on desktop: $shortcutPath" -ForegroundColor Green