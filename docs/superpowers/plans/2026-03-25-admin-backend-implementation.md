# Shoppe 管理后台实施计划

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** 为 Shoppe 电商平台构建独立的管理后台系统，包含前后端项目和基础管理功能

**Architecture:**
- 后端：独立的 AdminAPI 项目（.NET 8 WebAPI），与现有 ShoppeAPI 并列
- 前端：独立的 admin 项目（Vue 3 + Element Plus），与现有 frontend 并列
- 数据库：复用现有 MySQL，新增管理相关表

**Tech Stack:**
- 前端：Vue 3.4 + TypeScript + Element Plus 2.5 + Pinia + Vue Router + Axios
- 后端：.NET 8 WebAPI + EF Core 8 + MySQL + JWT
- 数据库：MySQL 8.0

---

## 文件结构总览

### 后端 (AdminAPI)

```
backend/AdminAPI/
├── Features/
│   ├── Auth/
│   │   ├── AuthController.cs
│   │   ├── DTOs/AuthDtos.cs
│   │   └── Services/
│   │       ├── IAuthService.cs
│   │       ├── AuthService.cs
│   │       ├── IJwtService.cs
│   │       └── JwtService.cs
│   ├── Admins/
│   │   ├── AdminController.cs
│   │   ├── DTOs/AdminDtos.cs
│   │   └── Services/
│   │       ├── IAdminService.cs
│   │       └── AdminService.cs
│   ├── Roles/
│   │   ├── RoleController.cs
│   │   ├── DTOs/RoleDtos.cs
│   │   └── Services/
│   │       ├── IRoleService.cs
│   │       └── RoleService.cs
│   ├── Permissions/
│   │   ├── PermissionController.cs
│   │   └── DTOs/PermissionDtos.cs
│   └── Users/
│       ├── UserController.cs
│       └── DTOs/UserDtos.cs
├── Models/
│   ├── AdminUser.cs
│   ├── Role.cs
│   ├── Permission.cs
│   ├── AdminRole.cs
│   ├── RolePermission.cs
│   └── OperationLog.cs
├── Data/
│   └── AdminDbContext.cs
├── Middleware/
│   ├── PermissionAuthorizationMiddleware.cs
│   └── ExceptionHandlingMiddleware.cs
├── appsettings.json
├── appsettings.Development.json
└── Program.cs
```

### 前端 (admin)

```
admin/
├── src/
│   ├── api/
│   │   ├── auth.ts
│   │   ├── admin.ts
│   │   ├── role.ts
│   │   ├── permission.ts
│   │   └── user.ts
│   ├── stores/
│   │   ├── auth.ts
│   │   └── app.ts
│   ├── router/
│   │   └── index.ts
│   ├── views/
│   │   ├── Login.vue
│   │   ├── Dashboard.vue
│   │   ├── system/
│   │   │   ├── AdminList.vue
│   │   │   ├── RoleList.vue
│   │   │   └── PermissionTree.vue
│   │   ├── user/
│   │   │   └── UserList.vue
│   │   └── settings/
│   │       └── SystemSettings.vue
│   ├── components/
│   │   ├── AdminLayout.vue
│   │   ├── Sidebar.vue
│   │   └── Header.vue
│   ├── directives/
│   │   └── permission.ts
│   ├── utils/
│   │   ├── request.ts
│   │   └── auth.ts
│   ├── App.vue
│   └── main.ts
├── public/
├── index.html
├── package.json
├── tsconfig.json
├── vite.config.ts
└── README.md
```

---

## Phase 1: AdminAPI 后端基础架构

### Task 1: 创建 AdminAPI 项目骨架

**Files:**
- Create: `backend/AdminAPI/AdminAPI.csproj`
- Create: `backend/AdminAPI/Program.cs`
- Create: `backend/AdminAPI/appsettings.json`
- Create: `backend/AdminAPI/appsettings.Development.json`

- [ ] **Step 1: 创建 .NET 8 WebAPI 项目**

```bash
cd backend
dotnet new webapi -n AdminAPI --no-https
```

Expected: Project created with basic template

- [ ] **Step 2: 安装 NuGet 包**

```bash
cd AdminAPI
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package System.IdentityModel.Tokens.Jwt
dotnet add package BCrypt.Net-Next
dotnet add package Swashbuckle.AspNetCore
```

Expected: All packages installed

- [ ] **Step 3: 配置 Program.cs**

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Configure JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!)
        )
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run("http://localhost:9001");
```

- [ ] **Step 4: 配置 appsettings.json**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=shoppe_admin;Uid=root;Pwd=your_password;"
  },
  "JwtSettings": {
    "Issuer": "ShoppeAdmin",
    "Audience": "ShoppeAdminUser",
    "SecretKey": "YourSuperSecretKeyThatIsAtLeast32CharactersLong123456"
  },
  "CorsSettings": {
    "AllowedOrigins": "http://localhost:3001"
  }
}
```

- [ ] **Step 5: 验证项目可以构建**

```bash
dotnet build
```

Expected: Build succeeded with 0 errors

- [ ] **Step 6: Commit**

```bash
git add backend/AdminAPI/
git commit -m "feat: create AdminAPI project skeleton"
```

---

### Task 2: 创建数据库模型

**Files:**
- Create: `backend/AdminAPI/Models/AdminUser.cs`
- Create: `backend/AdminAPI/Models/Role.cs`
- Create: `backend/AdminAPI/Models/Permission.cs`
- Create: `backend/AdminAPI/Models/AdminRole.cs`
- Create: `backend/AdminAPI/Models/RolePermission.cs`
- Create: `backend/AdminAPI/Models/OperationLog.cs`
- Create: `backend/AdminAPI/Data/AdminDbContext.cs`

- [ ] **Step 1: 创建 AdminUser 模型**

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminAPI.Models;

[Table("admin_users")]
public class AdminUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required]
    [MaxLength(255)]
    public string PasswordHash { get; set; }

    [MaxLength(100)]
    public string? Email { get; set; }

    [MaxLength(20)]
    public string? Phone { get; set; }

    public int Status { get; set; } = 1; // 0=disabled, 1=enabled

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }

    // Navigation
    public ICollection<AdminRole> AdminRoles { get; set; } = new List<AdminRole>();
}
```

- [ ] **Step 2: 创建 Role 模型**

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminAPI.Models;

[Table("roles")]
public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<AdminRole> AdminRoles { get; set; } = new List<AdminRole>();
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
```

- [ ] **Step 3: 创建 Permission 模型**

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminAPI.Models;

[Table("permissions")]
public class Permission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string Code { get; set; }

    public int Type { get; set; } // 1=Directory, 2=Menu, 3=Button, 4=API

    public int? ParentId { get; set; }

    [MaxLength(255)]
    public string? Path { get; set; }

    [MaxLength(100)]
    public string? Icon { get; set; }

    [MaxLength(255)]
    public string? ApiPath { get; set; }

    public int Sort { get; set; } = 0;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
```

- [ ] **Step 4: 创建关联表模型**

```csharp
// AdminRole.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminAPI.Models;

[Table("admin_roles")]
public class AdminRole
{
    [Key]
    public int AdminId { get; set; }
    public int RoleId { get; set; }

    [ForeignKey("AdminId")]
    public AdminUser Admin { get; set; } = null!;

    [ForeignKey("RoleId")]
    public Role Role { get; set; } = null!;
}
```

```csharp
// RolePermission.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminAPI.Models;

[Table("role_permissions")]
public class RolePermission
{
    [Key]
    public int RoleId { get; set; }
    public int PermissionId { get; set; }

    [ForeignKey("RoleId")]
    public Role Role { get; set; } = null!;

    [ForeignKey("PermissionId")]
    public Permission Permission { get; set; } = null!;
}
```

- [ ] **Step 5: 创建 OperationLog 模型**

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminAPI.Models;

[Table("operation_logs")]
public class OperationLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public int AdminId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Action { get; set; }

    [Required]
    [MaxLength(50)]
    public string Module { get; set; }

    [MaxLength(4000)]
    public string? Details { get; set; }

    [MaxLength(50)]
    public string? IpAddress { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
```

- [ ] **Step 6: 创建 AdminDbContext**

```csharp
using Microsoft.EntityFrameworkCore;
using AdminAPI.Models;

namespace AdminAPI.Data;

public class AdminDbContext : DbContext
{
    public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options) { }

    public DbSet<AdminUser> AdminUsers { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<AdminRole> AdminRoles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<OperationLog> OperationLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // AdminRole composite key
        modelBuilder.Entity<AdminRole>()
            .HasKey(ar => new { ar.AdminId, ar.RoleId });

        // RolePermission composite key
        modelBuilder.Entity<RolePermission>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });

        // AdminUser index
        modelBuilder.Entity<AdminUser>()
            .HasIndex(au => au.Username)
            .IsUnique();
    }
}
```

- [ ] **Step 7: 更新 Program.cs 注册 DbContext**

```csharp
builder.Services.AddDbContext<AdminDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
```

- [ ] **Step 8: Commit**

```bash
git add backend/AdminAPI/Models/ backend/AdminAPI/Data/
git commit -m "feat: create database models and DbContext"
```

---

### Task 3: 实现认证模块

**Files:**
- Create: `backend/AdminAPI/Features/Auth/AuthController.cs`
- Create: `backend/AdminAPI/Features/Auth/DTOs/AuthDtos.cs`
- Create: `backend/AdminAPI/Features/Auth/Services/IAuthService.cs`
- Create: `backend/AdminAPI/Features/Auth/Services/AuthService.cs`
- Create: `backend/AdminAPI/Features/Auth/Services/IJwtService.cs`
- Create: `backend/AdminAPI/Features/Auth/Services/JwtService.cs`

- [ ] **Step 1: 创建认证 DTOs**

```csharp
using System.ComponentModel.DataAnnotations;

namespace AdminAPI.Features.Auth;

public record LoginRequest(
    [Required][MaxLength(50)] string Username,
    [Required][MaxLength(50)] string Password
);

public record LoginResponse(
    int AdminId,
    string Username,
    string Token,
    string RefreshToken,
    List<string> Permissions
);

public record RefreshTokenRequest(
    [Required] string RefreshToken
);

public record AuthAdminDto(
    int Id,
    string Username,
    string? Email,
    List<string> Permissions
);
```

- [ ] **Step 2: 创建 IJwtService 接口**

```csharp
namespace AdminAPI.Features.Auth.Services;

public interface IJwtService
{
    string GenerateToken(int adminId, string username, List<string> permissions);
    string GenerateRefreshToken();
    int? ValidateToken(string token);
}
```

- [ ] **Step 3: 实现 JwtService**

```csharp
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AdminAPI.Features.Auth.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(int adminId, string username, List<string> permissions)
    {
        var jwtSettings = _config.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, adminId.ToString()),
            new(ClaimTypes.Name, username)
        };

        foreach (var permission in permissions)
        {
            claims.Add(new Claim("permission", permission));
        }

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddHours(2);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public int? ValidateToken(string token)
    {
        try
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidateAudience = true,
                ValidAudience = jwtSettings["Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var adminId = int.Parse(jwtToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return adminId;
        }
        catch
        {
            return null;
        }
    }
}
```

- [ ] **Step 4: 创建 IAuthService 接口**

```csharp
namespace AdminAPI.Features.Auth.Services;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(string username, string password);
    Task<bool> LogoutAsync(int adminId);
    Task<LoginResponse?> RefreshTokenAsync(string refreshToken);
}
```

- [ ] **Step 5: 实现 AuthService**

```csharp
using AdminAPI.Data;
using AdminAPI.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace AdminAPI.Features.Auth.Services;

public class AuthService : IAuthService
{
    private readonly AdminDbContext _db;
    private readonly IJwtService _jwtService;

    public AuthService(AdminDbContext db, IJwtService jwtService)
    {
        _db = db;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse?> LoginAsync(string username, string password)
    {
        var admin = await _db.AdminUsers
            .Include(a => a.AdminRoles)
                .ThenInclude(ar => ar.Role)
                    .ThenInclude(r => r.RolePermissions)
                        .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(a => a.Username == username);

        if (admin == null || admin.Status != 1)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash))
            return null;

        var permissions = admin.AdminRoles
            .SelectMany(ar => ar.Role.RolePermissions)
            .Select(rp => rp.Permission.Code)
            .Distinct()
            .ToList();

        admin.LastLoginAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        var token = _jwtService.GenerateToken(admin.Id, admin.Username, permissions);
        var refreshToken = _jwtService.GenerateRefreshToken();

        return new LoginResponse(
            admin.Id,
            admin.Username,
            token,
            refreshToken,
            permissions
        );
    }

    public Task<bool> LogoutAsync(int adminId)
    {
        // TODO: Implement token blacklist if needed
        return Task.FromResult(true);
    }

    public async Task<LoginResponse?> RefreshTokenAsync(string refreshToken)
    {
        // TODO: Implement refresh token validation and rotation
        return null;
    }
}
```

- [ ] **Step 6: 创建 AuthController**

```csharp
using Microsoft.AspNetCore.Mvc;
using AdminAPI.Features.Auth.Services;

namespace AdminAPI.Features.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.LoginAsync(request.Username, request.Password);
        if (result == null)
            return Unauthorized(new { message = "用户名或密码错误" });

        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        // TODO: Get adminId from token
        await _authService.LogoutAsync(0);
        return Ok();
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<LoginResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var result = await _authService.RefreshTokenAsync(request.RefreshToken);
        if (result == null)
            return Unauthorized(new { message = "Token 已失效" });

        return Ok(result);
    }
}
```

- [ ] **Step 7: 在 Program.cs 注册服务**

```csharp
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
```

- [ ] **Step 8: Commit**

```bash
git add backend/AdminAPI/Features/Auth/
git commit -m "feat: implement authentication module"
```

---

## Phase 2: 前端基础架构

### Task 4: 创建 Vue 3 前端项目

**Files:**
- Create: `admin/package.json`
- Create: `admin/vite.config.ts`
- Create: `admin/tsconfig.json`
- Create: `admin/index.html`
- Create: `admin/src/main.ts`
- Create: `admin/src/App.vue`

- [ ] **Step 1: 创建 Vite + Vue 3 + TypeScript 项目**

```bash
cd D:\Claude\Figma\Shoppe
npm create vite@latest admin -- --template vue-ts
```

Expected: Project created

- [ ] **Step 2: 安装依赖**

```bash
cd admin
npm install
npm install element-plus @element-plus/icons-vue
npm install pinia vue-router axios
npm install -D @types/node
```

Expected: All packages installed

- [ ] **Step 3: 配置 vite.config.ts**

```typescript
import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import { fileURLToPath, URL } from 'node:url'

export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    port: 3001,
    proxy: {
      '/api': {
        target: 'http://localhost:9001',
        changeOrigin: true
      }
    }
  }
})
```

- [ ] **Step 4: 配置 tsconfig.json**

```json
{
  "compilerOptions": {
    "target": "ES2020",
    "useDefineForClassFields": true,
    "module": "ESNext",
    "lib": ["ES2020", "DOM", "DOM.Iterable"],
    "skipLibCheck": true,
    "moduleResolution": "bundler",
    "allowImportingTsExtensions": true,
    "resolveJsonModule": true,
    "isolatedModules": true,
    "noEmit": true,
    "jsx": "preserve",
    "strict": true,
    "noUnusedLocals": true,
    "noUnusedParameters": true,
    "noFallthroughCasesInSwitch": true,
    "paths": {
      "@/*": ["./src/*"]
    }
  },
  "include": ["src/**/*.ts", "src/**/*.tsx", "src/**/*.vue"],
  "references": [{ "path": "./tsconfig.node.json" }]
}
```

- [ ] **Step 5: 配置 main.ts**

```typescript
import { createApp } from 'vue'
import { createPinia } from 'pinia'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import * as ElementPlusIconsVue from '@element-plus/icons-vue'
import App from './App.vue'
import router from './router'

const app = createApp(App)
const pinia = createPinia()

// 注册 Element Plus 图标
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
  app.component(key, component)
}

app.use(pinia)
app.use(router)
app.use(ElementPlus)

app.mount('#app')
```

- [ ] **Step 6: 配置 App.vue**

```vue
<template>
  <router-view />
</template>

<script setup lang="ts">
</script>

<style>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

#app {
  font-family: 'Helvetica Neue', Helvetica, 'PingFang SC', 'Hiragino Sans GB', 'Microsoft YaHei', Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  height: 100vh;
}
</style>
```

- [ ] **Step 7: 验证项目可以运行**

```bash
npm run dev
```

Expected: Vite dev server started on http://localhost:3001

- [ ] **Step 8: Commit**

```bash
git add admin/
git commit -m "feat: create Vue 3 + TypeScript frontend project"
```

---

（计划继续，包含更多任务和步骤...）

## 后续任务清单

### Task 5: 实现管理员管理模块（后端）
- 创建 AdminController
- 创建 AdminService
- 实现 CRUD 接口

### Task 6: 实现角色管理模块（后端）
- 创建 RoleController
- 创建 RoleService
- 实现角色 CRUD 和权限分配

### Task 7: 实现权限管理模块（后端）
- 创建 PermissionController
- 实现权限树接口

### Task 8: 前端路由和布局
- 创建 router/index.ts
- 创建 AdminLayout 组件
- 创建 Sidebar 和 Header 组件

### Task 9: 前端登录页面
- 创建 Login.vue
- 实现表单验证
- 对接登录 API

### Task 10: 前端管理员管理页面
- 创建 AdminList.vue
- 实现表格、分页、新增、编辑、删除

### Task 11: 前端角色管理页面
- 创建 RoleList.vue
- 实现角色权限分配

### Task 12: 前端权限树页面
- 创建 PermissionTree.vue
- 实现树形控件

### Task 13: 初始化数据库和种子数据
- 创建数据库
- 插入默认管理员账号
- 插入基础权限数据

### Task 14: 集成测试和验证
- 测试登录流程
- 测试权限控制
- 验证所有 CRUD 接口

---

**计划完成后的审查循环：**

1. 调用 plan-document-reviewer 审查此计划文档
2. 根据审查反馈修复问题
3. 最多 3 次迭代，然后提交给用户审查

**执行方式选择：**

计划完成后，用户可以选择：
1. **Subagent-Driven**（推荐）- 每个任务派遣一个子代理执行
2. **Inline Execution** - 在当前会话中使用 executing-plans 技能执行
