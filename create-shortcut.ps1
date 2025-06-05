# Get the directory where the script is located
$scriptPath = Split-Path -Parent -Path $MyInvocation.MyCommand.Definition
$projectRoot = $scriptPath

# Define paths
$desktopPath = [Environment]::GetFolderPath('Desktop')
$shortcutName = "SnapsCqq"
$exePath = Join-Path $projectRoot "bin\net481\publish\SnapsCqq.exe"

# Check if exe exists, if not build it
if (-not (Test-Path $exePath)) {
    Write-Host "Executable not found. Building release version..." -ForegroundColor Yellow

    # Navigate to project root and run publish command
    Push-Location $projectRoot
    dotnet publish -c Release -f net481
    Pop-Location

    # Verify the exe was created
    if (-not (Test-Path $exePath)) {
        Write-Host "Failed to build executable at: $exePath" -ForegroundColor Red
        exit 1
    }

    Write-Host "Build completed successfully!" -ForegroundColor Green
}

# Create shortcut
$shortcutPath = Join-Path $desktopPath "$shortcutName.lnk"
$WshShell = New-Object -comObject WScript.Shell
$Shortcut = $WshShell.CreateShortcut($shortcutPath)
$Shortcut.TargetPath = $exePath
$Shortcut.WorkingDirectory = (Split-Path -Parent -Path $exePath)

# Optional: Set an icon if you have one
$iconPath = Join-Path $scriptPath "cursor-default-click-outline.ico"
if (Test-Path $iconPath) {
    $Shortcut.IconLocation = $iconPath
}

$Shortcut.Save()

Write-Host "Shortcut created on desktop: $shortcutPath" -ForegroundColor Green
