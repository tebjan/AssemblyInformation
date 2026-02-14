@echo off
:: Install .NET Assembly Information shell extension
:: Must be run as Administrator

net session >nul 2>&1
if %errorlevel% neq 0 (
    echo This script must be run as Administrator.
    echo Right-click and select "Run as administrator".
    pause
    exit /b 1
)

set "INSTALL_DIR=%ProgramFiles%\AssemblyInformation"

echo Installing .NET Assembly Information to %INSTALL_DIR% ...

:: Create install directory
if not exist "%INSTALL_DIR%" mkdir "%INSTALL_DIR%"

:: Copy files
copy /y "%~dp0AssemblyInformation.exe" "%INSTALL_DIR%\" >nul
copy /y "%~dp0AssemblyInformation.dll" "%INSTALL_DIR%\" >nul

:: Register shell extension
regsvr32 /s "%INSTALL_DIR%\AssemblyInformation.dll"
if %errorlevel% neq 0 (
    echo Failed to register shell extension.
    pause
    exit /b 1
)

echo.
echo Installation complete.
echo Right-click any .dll or .exe in Explorer to see "Assembly Information".
echo.
echo To uninstall, run uninstall.cmd as Administrator.
pause
