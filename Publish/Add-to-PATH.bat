@echo off
REM Add string-calculator-kata to PATH
REM This batch file will run the PowerShell script with elevated privileges

setlocal enabledelayedexpansion

REM Get the directory where this batch file is located
set SCRIPT_DIR=%~dp0

REM Check if running as administrator
net session >nul 2>&1
if %errorLevel% neq 0 (
    echo Requesting administrator privileges...
    powershell.exe -Command "Start-Process cmd -ArgumentList '/c, \"%~f0\"' -Verb RunAs"
    exit /b
)

REM Run the PowerShell script with proper execution policy
powershell.exe -ExecutionPolicy Bypass -File "%SCRIPT_DIR%add-to-path.ps1"

pause
