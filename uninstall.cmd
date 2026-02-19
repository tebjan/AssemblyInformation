@echo off
:: Uninstall .NET Assembly Information shell extension
:: Must be run as Administrator

net session >nul 2>&1
if %errorlevel% neq 0 (
    echo This script must be run as Administrator.
    echo Right-click and select "Run as administrator".
    pause
    exit /b 1
)

set "INSTALL_DIR=%ProgramFiles%\AssemblyInformation"

echo Uninstalling .NET Assembly Information ...

:: Remove context menu registry entries
reg delete "HKCR\dllfile\shell\AssemblyInformation" /f >nul 2>&1
reg delete "HKCR\exefile\shell\AssemblyInformation" /f >nul 2>&1

:: Remove files
if exist "%INSTALL_DIR%\AssemblyInformation.exe" del /f /q "%INSTALL_DIR%\AssemblyInformation.exe"
if exist "%INSTALL_DIR%" rmdir /q "%INSTALL_DIR%" 2>nul

echo.
echo Uninstallation complete.
pause
