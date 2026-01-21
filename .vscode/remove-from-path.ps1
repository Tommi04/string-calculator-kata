$publishDir = Join-Path $PSScriptRoot "..\Publish"
$publishDir = (Resolve-Path $publishDir -ErrorAction SilentlyContinue).Path

if ($null -eq $publishDir) {
    Write-Host "publish directory not found." -ForegroundColor Red
    exit 1
}

$currentPath = [Environment]::GetEnvironmentVariable('Path', 'User')

if ($null -ne $currentPath) {
    $newPath = ($currentPath -split ';' | Where-Object { $_ -ne $publishDir -and $_ -ne '' }) -join ';'
    [Environment]::SetEnvironmentVariable('Path', $newPath, 'User')
    Write-Host "Removed '$publishDir' from user PATH." -ForegroundColor Green
} else {
    Write-Host "User PATH is empty." -ForegroundColor Yellow
}
