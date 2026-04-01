@echo off
setlocal EnableDelayedExpansion

echo ========================================
echo   Shoppe Startup Script
echo ========================================
echo.

set "SCRIPT_DIR=%~dp0"

where node >nul 2>&1
if %errorlevel% neq 0 (
    echo [Error] Node.js not found
    pause
    exit /b 1
)
echo [OK] Node.js is installed

where dotnet >nul 2>&1
if %errorlevel% neq 0 (
    echo [Error] .NET SDK not found
    pause
    exit /b 1
)
echo [OK] .NET SDK is installed

echo.
echo [1/2] Starting frontend...
cd /d "%SCRIPT_DIR%frontend"
if %errorlevel% neq 0 (
    echo [Error] Cannot access frontend directory
    pause
    exit /b 1
)
start "" cmd /k "title Shoppe Frontend && npm run dev"

echo [Wait] Waiting 3 seconds for frontend...
timeout /t 3 /nobreak >nul

echo.
echo [2/2] Starting backend API...
cd /d "%SCRIPT_DIR%backend\ShoppeAPI"
if %errorlevel% neq 0 (
    echo [Error] Cannot access backend directory
    pause
    exit /b 1
)
start "" cmd /k "title Shoppe Backend && dotnet run"

cd /d "%SCRIPT_DIR%"

echo.
echo ========================================
echo   Services starting...
echo   - Frontend: http://localhost:3000
echo   - Backend:  http://localhost:9000
echo ========================================
echo.
echo Press any key to close this window
echo (Services will continue running in separate windows)
pause
