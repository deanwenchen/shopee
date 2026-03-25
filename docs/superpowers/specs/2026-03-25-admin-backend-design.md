# 管理后台系统设计文档

**创建日期**: 2026-03-25
**状态**: Draft
**作者**: Claude Code

---

## 1. 概述

### 1.1 项目目标

为 Shoppe 电商平台构建独立的管理后台系统，提供后台账号管理、权限控制、C 端用户管理等基础管理功能。

### 1.2 设计原则

- **分离部署**: 管理后台与 C 端完全独立，包括前端和后端
- **权限隔离**: 基于 RBAC 模型的细粒度权限控制
- **安全优先**: JWT 认证、操作日志、密码策略
- **可扩展性**: 模块化设计，便于后续添加新功能

---

## 2. 架构设计

### 2.1 项目结构

```
D:\Claude\Figma\Shoppe\
├── backend/
│   ├── ShoppeAPI/          # C 端 API（现有，保持不变）
│   │   ├── Features/
│   │   │   └── Auth/       # C 端用户认证
│   │   ├── Models/
│   │   └── Program.cs
│   │
│   └── AdminAPI/           # 管理后台 API（新建）
│       ├── Features/
│       │   ├── Auth/       # 管理员认证
│       │   ├── Admins/     # 管理员管理
│       │   ├── Roles/      # 角色管理
│       │   ├── Permissions/# 权限管理
│       │   └── Users/      # C 端用户管理
│       ├── Models/
│       ├── Middleware/
│       └── Program.cs
│
├── admin/                  # 管理后台前端（新建）
│   ├── src/
│   │   ├── views/
│   │   ├── components/
│   │   ├── stores/
│   │   ├── router/
│   │   └── api/
│   └── package.json
│
└── frontend/               # C 端前端（现有，保持不变）
```

### 2.2 技术栈

| 层级 | 技术 | 版本 |
|------|------|------|
| 前端框架 | Vue 3 + TypeScript | 3.4+ |
| UI 组件库 | Element Plus | 2.5+ |
| 状态管理 | Pinia | 2.1+ |
| 路由 | Vue Router | 4.3+ |
| HTTP 客户端 | Axios | 1.6+ |
| 构建工具 | Vite | 5.0+ |
| 后端框架 | .NET 8 WebAPI | 8.0 |
| ORM | EF Core | 8.0 |
| 数据库 | MySQL | 8.0+ |
| 认证 | JWT | - |

---

## 3. 数据库设计

### 3.1 ER 图

```
┌─────────────────┐     ┌─────────────────────┐     ┌─────────────────┐
│  AdminUsers     │     │  AdminRoles         │     │  Roles          │
├─────────────────┤     ├─────────────────────┤     ├─────────────────┤
│ Id (PK)         │◄────│ AdminId (FK)        │     │ Id (PK)         │
│ Username        │     │ RoleId (FK)         │────►│ Name            │
│ PasswordHash    │     └─────────────────────┘     │ Description     │
│ Email           │                                 │ CreatedAt       │
│ Phone           │                                 └─────────────────┘
│ Status          │                                          │
│ CreatedAt       │                                          │
│ LastLoginAt     │                                          ▼
└─────────────────┘                                 ┌─────────────────────┐
       │                                            │  RolePermissions    │
       │                                            ├─────────────────────┤
       ▼                                            │ RoleId (FK)         │
┌─────────────────────┐                             │ PermissionId (FK)   │──┐
│  OperationLogs      │                             └─────────────────────┘  │
├─────────────────────┤                                                      │
│ Id (PK)             │                                                      │
│ AdminId (FK)        │                                                      │
│ Action              │                                                      ▼
│ Module              │                                          ┌─────────────────┐
│ Details (JSON)      │                                          │  Permissions    │
│ IpAddress           │                                          ├─────────────────┤
│ CreatedAt           │                                          │ Id (PK)         │
└─────────────────────┘                                          │ Name            │
                                                                 │ Code            │
                                                                 │ Type            │
                                                                 │ ParentId        │
                                                                 │ Path            │
                                                                 └─────────────────┘
```

### 3.2 数据表定义

#### 3.2.1 AdminUsers（管理员表）

| 字段 | 类型 | 说明 |
|------|------|------|
| Id | int | 主键，自增 |
| Username | varchar(50) | 用户名，唯一 |
| PasswordHash | varchar(255) | 密码哈希 |
| Email | varchar(100) | 邮箱 |
| Phone | varchar(20) | 手机号 |
| Status | int | 状态：0-禁用，1-启用 |
| CreatedAt | datetime | 创建时间 |
| UpdatedAt | datetime | 更新时间 |
| LastLoginAt | datetime? | 最后登录时间 |

#### 3.2.2 Roles（角色表）

| 字段 | 类型 | 说明 |
|------|------|------|
| Id | int | 主键，自增 |
| Name | varchar(50) | 角色名，唯一 |
| Description | varchar(255) | 描述 |
| CreatedAt | datetime | 创建时间 |

#### 3.2.3 Permissions（权限表）

| 字段 | 类型 | 说明 |
|------|------|------|
| Id | int | 主键，自增 |
| Name | varchar(100) | 权限名称 |
| Code | varchar(100) | 权限标识，唯一 |
| Type | int | 类型：1-目录，2-菜单，3-按钮，4-API |
| ParentId | int? | 父权限 ID |
| Path | varchar(255) | 前端路由路径（菜单类型） |
| Icon | varchar(100) | 图标（菜单类型） |
| ApiPath | varchar(255) | API 路径（API 类型） |
| Sort | int | 排序 |

#### 3.2.4 AdminRoles（管理员 - 角色关联表）

| 字段 | 类型 | 说明 |
|------|------|------|
| AdminId | int | 管理员 ID，外键 |
| RoleId | int | 角色 ID，外键 |

#### 3.2.5 RolePermissions（角色 - 权限关联表）

| 字段 | 类型 | 说明 |
|------|------|------|
| RoleId | int | 角色 ID，外键 |
| PermissionId | int | 权限 ID，外键 |

#### 3.2.6 OperationLogs（操作日志表）

| 字段 | 类型 | 说明 |
|------|------|------|
| Id | long | 主键，自增 |
| AdminId | int | 管理员 ID，外键 |
| Action | varchar(50) | 操作类型（Create/Update/Delete/Login） |
| Module | varchar(50) | 模块（Admin/Role/Permission/User） |
| Details | text | 详细信息（JSON 格式） |
| IpAddress | varchar(50) | IP 地址 |
| CreatedAt | datetime | 创建时间 |

#### 3.2.7 Users（C 端用户表 - 复用现有）

| 字段 | 类型 | 说明 |
|------|------|------|
| Id | int | 主键，自增 |
| Email | varchar(255) | 邮箱 |
| PasswordHash | varchar(255) | 密码哈希 |
| PhoneNumber | varchar(20) | 手机号 |
| CreatedAt | datetime | 创建时间 |
| UpdatedAt | datetime | 更新时间 |
| LastLoginAt | datetime? | 最后登录时间 |

---

## 4. API 设计

### 4.1 认证模块（Auth）

| 方法 | 路径 | 描述 | 权限 |
|------|------|------|------|
| POST | /api/auth/login | 管理员登录 | 公开 |
| POST | /api/auth/logout | 退出登录 | Bearer |
| POST | /api/auth/refresh | 刷新 Token | Bearer |
| GET | /api/auth/me | 获取当前管理员信息 | Bearer |

### 4.2 管理员管理（Admins）

| 方法 | 路径 | 描述 | 权限 |
|------|------|------|------|
| GET | /api/admins | 管理员列表（分页） | admin:list |
| GET | /api/admins/{id} | 管理员详情 | admin:detail |
| POST | /api/admins | 创建管理员 | admin:create |
| PUT | /api/admins/{id} | 更新管理员 | admin:update |
| DELETE | /api/admins/{id} | 删除管理员 | admin:delete |
| PUT | /api/admins/{id}/status | 启用/禁用 | admin:status |
| POST | /api/admins/{id}/reset-password | 重置密码 | admin:reset-pwd |

### 4.3 角色管理（Roles）

| 方法 | 路径 | 描述 | 权限 |
|------|------|------|------|
| GET | /api/roles | 角色列表 | role:list |
| GET | /api/roles/{id} | 角色详情 | role:detail |
| POST | /api/roles | 创建角色 | role:create |
| PUT | /api/roles/{id} | 更新角色 | role:update |
| DELETE | /api/roles/{id} | 删除角色 | role:delete |
| PUT | /api/roles/{id}/permissions | 分配权限 | role:assign |

### 4.4 权限管理（Permissions）

| 方法 | 路径 | 描述 | 权限 |
|------|------|------|------|
| GET | /api/permissions/tree | 权限树 | permission:list |
| GET | /api/permissions | 权限列表 | permission:list |
| POST | /api/permissions | 创建权限 | permission:create |
| PUT | /api/permissions/{id} | 更新权限 | permission:update |
| DELETE | /api/permissions/{id} | 删除权限 | permission:delete |

### 4.5 用户管理（Users）

| 方法 | 路径 | 描述 | 权限 |
|------|------|------|------|
| GET | /api/users | 用户列表（分页） | user:list |
| GET | /api/users/{id} | 用户详情 | user:detail |
| PUT | /api/users/{id}/status | 禁用/启用用户 | user:status |
| POST | /api/users/{id}/reset-password | 重置用户密码 | user:reset-pwd |
| GET | /api/users/{id}/logs | 用户行为日志 | user:logs |

### 4.6 操作日志（Logs）

| 方法 | 路径 | 描述 | 权限 |
|------|------|------|------|
| GET | /api/logs | 日志列表（分页） | log:list |
| GET | /api/logs/{id} | 日志详情 | log:detail |

---

## 5. 前端设计

### 5.1 页面结构

```
src/views/
├── Login.vue                    # 登录页 /login
├── Dashboard.vue                # 仪表盘 /dashboard
├── system/
│   ├── AdminList.vue            # 管理员列表 /system/admins
│   ├── RoleList.vue             # 角色列表 /system/roles
│   └── PermissionTree.vue       # 权限树 /system/permissions
├── user/
│   └── UserList.vue             # 用户列表 /users
└── settings/
    └── SystemSettings.vue       # 系统设置 /settings
```

### 5.2 布局结构

```
<AdminLayout>
├── Sidebar (左侧导航菜单)
│   ├── Dashboard
│   ├── 系统管理 (展开)
│   │   ├── 管理员管理
│   │   ├── 角色管理
│   │   └── 权限管理
│   ├── 用户管理
│   └── 系统设置
│
├── Header (顶部栏)
│   ├── 面包屑
│   ├── 搜索
│   └── 用户头像/退出
│
└── MainContent (内容区)
    └── <router-view />
```

### 5.3 路由配置（示例）

```typescript
const routes = [
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/views/Login.vue'),
    meta: { requiresAuth: false }
  },
  {
    path: '/',
    component: AdminLayout,
    redirect: '/dashboard',
    children: [
      {
        path: 'dashboard',
        name: 'Dashboard',
        component: () => import('@/views/Dashboard.vue'),
        meta: { requiresAuth: true, permission: 'dashboard:view' }
      },
      {
        path: 'system/admins',
        name: 'AdminList',
        component: () => import('@/views/system/AdminList.vue'),
        meta: { requiresAuth: true, permission: 'admin:list' }
      }
    ]
  }
]
```

### 5.4 状态管理（Pinia）

**stores/auth.ts** - 认证状态
```typescript
interface AuthState {
  token: string | null
  refreshToken: string | null
  admin: AdminUser | null
  permissions: string[]
}
```

**stores/app.ts** - 应用状态
```typescript
interface AppState {
  sidebarCollapsed: boolean
  activeMenu: string
  theme: 'light' | 'dark'
}
```

---

## 6. 权限控制设计

### 6.1 认证流程

```
1. 用户输入账号密码 → POST /api/auth/login
2. 后端验证 → 返回 { token, refreshToken, admin, permissions }
3. 前端存储 token 到 localStorage
4. 前端存储 permissions 到 Pinia store
5. 后续请求携带 Authorization: Bearer <token>
```

### 6.2 前端权限控制

**路由守卫**:
```typescript
router.beforeEach((to, from, next) => {
  const auth = useAuthStore()
  if (to.meta.requiresAuth && !auth.token) {
    next('/login')
  } else if (to.meta.permission && !auth.hasPermission(to.meta.permission)) {
    next('/403')
  } else {
    next()
  }
})
```

**菜单渲染**:
```vue
<template>
  <el-menu>
    <el-menu-item
      v-for="menu in menus"
      v-if="hasPermission(menu.permission)"
      :key="menu.path"
    >
      {{ menu.name }}
    </el-menu-item>
  </el-menu>
</template>
```

**按钮权限**:
```vue
<template>
  <el-button
    v-permission="['admin:create']"
    @click="handleCreate"
  >
    新增
  </el-button>
</template>
```

### 6.3 后端权限中间件

```csharp
public class PermissionAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _permission;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var admin = context.HttpContext.Items["Admin"] as AdminUser;
        if (!admin.HasPermission(_permission))
        {
            context.Result = new ForbidResult();
        }
    }
}

// 使用
[HttpPost]
[Permission("admin:create")]
public async Task<IActionResult> CreateAdmin(...) { }
```

---

## 7. 安全设计

### 7.1 密码安全

- 使用 BCrypt 加密存储密码
- 密码长度要求：8-20 位
- 登录失败 5 次锁定账号 30 分钟

### 7.2 Token 安全

- Access Token: 有效期 2 小时
- Refresh Token: 有效期 7 天
- Token 绑定 IP 地址（可选）

### 7.3 操作审计

- 所有写操作（Create/Update/Delete）记录日志
- 日志包含：操作人、操作类型、模块、详情、IP、时间

---

## 8. 项目计划

### Phase 1: 基础架构（P0）
- [ ] 创建 AdminAPI 项目骨架
- [ ] 数据库迁移脚本
- [ ] 管理员登录认证
- [ ] 管理员 CRUD

### Phase 2: 权限系统（P0）
- [ ] 角色 CRUD
- [ ] 权限树管理
- [ ] 角色 - 权限分配
- [ ] 前端权限控制

### Phase 3: 用户管理（P1）
- [ ] C 端用户列表
- [ ] 用户详情
- [ ] 用户禁用/启用

### Phase 4: 日志与设置（P2）
- [ ] 操作日志列表
- [ ] 系统设置页面

---

## 9. 待决策事项

1. **是否支持多语言**：暂不支持，仅中文
2. **是否需要数据导出**：后续考虑 Excel 导出
3. **是否需要通知系统**：暂不需要

---

## 10. 参考资料

- Element Plus 文档：https://element-plus.org/
- .NET 8 WebAPI 最佳实践
- RBAC 权限模型设计
