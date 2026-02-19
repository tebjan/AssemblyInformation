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

:: Register context menu for .dll files
reg add "HKCR\dllfile\shell\AssemblyInformation" /ve /d "Assembly Information" /f >nul
reg add "HKCR\dllfile\shell\AssemblyInformation" /v "Icon" /d "\"%INSTALL_DIR%\AssemblyInformation.exe\"" /f >nul
reg add "HKCR\dllfile\shell\AssemblyInformation\command" /ve /d "\"%INSTALL_DIR%\AssemblyInformation.exe\" \"%%1\"" /f >nul

:: Register context menu for .exe files
reg add "HKCR\exefile\shell\AssemblyInformation" /ve /d "Assembly Information" /f >nul
reg add "HKCR\exefile\shell\AssemblyInformation" /v "Icon" /d "\"%INSTALL_DIR%\AssemblyInformation.exe\"" /f >nul
reg add "HKCR\exefile\shell\AssemblyInformation\command" /ve /d "\"%INSTALL_DIR%\AssemblyInformation.exe\" \"%%1\"" /f >nul

echo.
echo Installation complete.
echo Right-click any .dll or .exe in Explorer to see "Assembly Information".
echo.
echo To uninstall, run uninstall.cmd as Administrator.
pause
