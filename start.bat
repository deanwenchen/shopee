@echo off
echo ========================================
echo   Shoppe 项目启动脚本
echo ========================================
echo.

:: 检查 Node.js 是否安装
where node >nul 2>&1
if %errorlevel% neq 0 (
    echo [错误] 未找到 Node.js，请先安装 Node.js
    pause
    exit /b 1
)

:: 检查 .NET 是否安装
where dotnet >nul 2>&1
if %errorlevel% neq 0 (
    echo [错误] 未找到 .NET SDK，请先安装 .NET 8 SDK
    pause
    exit /b 1
)

:: 检查 MySQL 是否运行
echo [检查] 检查 MySQL 服务...
mysql -u shoppe -pshoppe123 -e "SELECT 1;" >nul 2>&1
if %errorlevel% neq 0 (
    echo [警告] MySQL 服务可能未运行，后端可能启动失败
)

echo.
echo [1/2] 启动前端服务...
start "Shoppe Frontend" cmd /k "cd /d frontend && npm run dev"

timeout /t 3 /nobreak >nul

echo.
echo [2/2] 启动后端 API...
start "Shoppe Backend" cmd /k "cd /d backend\ShoppeAPI && dotnet run"

echo.
echo ========================================
echo   服务启动中...
echo   - 前端：http://localhost:3000
echo   - 后端：http://localhost:9000
echo ========================================
echo.
echo 按任意键关闭此窗口 (不会停止服务)
pause >nul
