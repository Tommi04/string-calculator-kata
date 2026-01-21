$publishDir = Join-Path $PSScriptRoot "..\Publish"
$exePath = Join-Path $publishDir "string-calculator-kata.exe"

if (-not (Test-Path $exePath)) {
    Write-Host "Error: Executable not found at '$exePath'" -ForegroundColor Red
    Write-Host "Please run 'publish' task first." -ForegroundColor Yellow
    exit 1
}

$publishDir = (Resolve-Path $publishDir).Path
$currentPath = [Environment]::GetEnvironmentVariable('Path', 'User')

if ($null -eq $currentPath) {
    $currentPath = ''
}

# Add publish directory to PATH so 'scalc' command works
if ($currentPath -notlike "*$publishDir*") {
    if ($currentPath -ne '') {
        $newPath = "$currentPath;$publishDir"
    } else {
        $newPath = $publishDir
    }

    [Environment]::SetEnvironmentVariable('Path', $newPath, 'User')
    Write-Host "Added '$publishDir' to user PATH." -ForegroundColor Green
    Write-Host ""
    Write-Host "Restart your terminal for changes to take effect." -ForegroundColor Yellow
    Write-Host "Then you can run: scalc" -ForegroundColor Cyan
} else {
    Write-Host "'$publishDir' is already in user PATH." -ForegroundColor Yellow
    Write-Host "You can run: scalc" -ForegroundColor Cyan
}
