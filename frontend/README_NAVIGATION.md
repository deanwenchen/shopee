# Shoppe 前端页面导航关系

## 页面跳转流程图

```
┌─────────────────────────────────────────────────────────────────┐
│                         StartPage (/)                           │
│  - Logo: Shoppe                                                 │
│  - "Let's get started" button → /create-account                 │
│  - "I already have an account" button → /login                  │
└─────────────────────────────────────────────────────────────────┘
                              │
              ┌───────────────┴───────────────┐
              ▼                               ▼
┌─────────────────────────┐       ┌─────────────────────────┐
│  CreateAccount          │       │   LoginPage             │
│  (/create-account)      │       │   (/login)              │
│  - Email input          │       │  - Email input          │
│  - Password input       │       │  - Next button →        │
│  - Phone input          │       │    /password            │
│  - Done button →        │       │  - Cancel button → /    │
│    /password-typing     │       │                         │
│  - Cancel button → /    │       │                         │
└─────────────────────────┘       └─────────────────────────┘
        │                                   │
        │                                   ▼
        │                   ┌───────────────────────┐
        │                   │  Password             │
        │                   │  (/password)          │
        │                   │  - 4 password boxes   │
        │                   │  - Input →            │
        │                   │    /password-typing   │
        │                   │  - Not you? → /login  │
        │                   └───────────────────────┘
        │                                   │
        └───────────────────────────────────┘
                                        ▼
                            ┌───────────────────────┐
                            │  PasswordTyping       │
                            │  (/password-typing)   │
                            │  - 8 dots (blue)      │
                            │  - Input 8 chars →    │
                            │    /wrong-password    │
                            └───────────────────────┘
                                        │
                                        ▼
                            ┌───────────────────────┐
                            │  WrongPassword        │
                            │  (/wrong-password)    │
                            │  - 8 dots (red)       │
                            │  - Input 8 chars →    │
                            │    /login             │
                            │  - Forgot password?   │
                            └───────────────────────┘
```

## 页面列表

| 页面 | 路由 | 说明 |
|------|------|------|
| StartPage | `/` | 启动页面，品牌展示 |
| CreateAccount | `/create-account` | 创建账户页面 |
| LoginPage | `/login` | 登录页面（输入 email） |
| Password | `/password` | 密码输入页面（4 个输入框） |
| PasswordTyping | `/password-typing` | 密码输入页面（蓝色点） |
| WrongPassword | `/wrong-password` | 密码错误页面（红色点） |

## 跳转逻辑

### StartPage (`/`)
- **主按钮** "Let's get started" → `/create-account`
- **次按钮** "I already have an account" → `/login`

### LoginPage (`/login`)
- **Next 按钮** → `/password`（输入 email 后）
- **Cancel 按钮** → `/`

### CreateAccount (`/create-account`)
- **Done 按钮** → `/password-typing`（注册成功后）
- **Cancel 按钮** → `/`

### Password (`/password`)
- **Not you?** → `/login`
- **输入框满后（4 位）** → 自动跳转 `/password-typing`

### PasswordTyping (`/password-typing`)
- **键盘输入** → 动态更新密码点（蓝色）
- **Go 按钮** → `/wrong-password`（密码输完后）

### WrongPassword (`/wrong-password`)
- **Forgot password?** → （待实现密码重置流程）
- **键盘 Go 按钮** → `/login`（返回登录页）

## 组件复用

- **StatusBar** - 所有页面顶部状态栏
- **FormInput** - 表单输入框
- **PasswordInput** - 密码输入框（带显示/隐藏切换）
- **NextButton** - 主按钮
- **HomeIndicator** - 底部 Home 指示器
