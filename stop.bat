@echo off
setlocal EnableDelayedExpansion

:: 设置代码页为 UTF-8
chcp 65001 >nul 2>&1

echo ========================================
echo   Shoppe 项目停止脚本
echo ========================================
echo.

echo [Stop] Stopping services...
echo.

:: 停止后端
echo [1/2] Stopping backend (dotnet.exe)...
taskkill /F /IM dotnet.exe >nul 2>&1
echo [Done] Backend stopped

:: 停止前端
echo [2/2] Stopping frontend (node.exe)...
taskkill /F /IM node.exe >nul 2>&1
echo [Done] Frontend stopped

echo.
echo ========================================
echo   All services stopped
echo ========================================
echo.
pause
