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

:: Unregister shell extension
if exist "%INSTALL_DIR%\AssemblyInformation.dll" (
    regsvr32 /u /s "%INSTALL_DIR%\AssemblyInformation.dll"
)

:: Remove files
if exist "%INSTALL_DIR%\AssemblyInformation.exe" del "%INSTALL_DIR%\AssemblyInformation.exe"
if exist "%INSTALL_DIR%\AssemblyInformation.dll" del "%INSTALL_DIR%\AssemblyInformation.dll"
if exist "%INSTALL_DIR%" rmdir "%INSTALL_DIR%"

echo.
echo Uninstallation complete.
pause
