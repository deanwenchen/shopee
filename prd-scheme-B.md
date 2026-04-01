# Shoppe 电商应用 - 产品需求文档（混合版）

**版本**: 2.0
**最后更新**: 2026-04-01
**文档类型**: 混合版（人类可读 + 关键数据结构）
**适用对象**: 产品经理、设计师、开发工程师

---

## 1. 产品概述

### 1.1 产品定位

Shoppe 是一个移动端电商应用，为用户提供完整的购物体验。

### 1.2 功能模块总览

```json
{
  "modules": [
    {
      "id": "auth",
      "name": "用户认证模块",
      "priority": "P0",
      "status": "已完成",
      "features": ["注册", "登录", "密码找回", "引导页"]
    },
    {
      "id": "commerce",
      "name": "电商核心模块",
      "priority": "P0",
      "status": "部分完成",
      "features": ["商城首页", "商品详情", "购物车", "分类"]
    },
    {
      "id": "user",
      "name": "用户中心模块",
      "priority": "P1",
      "status": "待实现",
      "features": ["个人中心", "订单", "设置"]
    }
  ]
}
```

---

## 2. 设计规范

### 2.1 画布尺寸

- **基准尺寸**: 375px × 817px（iPhone 尺寸）
- **状态栏高度**: 44px
- **底部 Home Indicator**: 34px

### 2.2 颜色规范

| 颜色名称 | 色值 | 用途说明 |
|---------|------|---------|
| 品牌黑 | #202020 | 主要文字、标题 |
| 品牌蓝 | #004bff | 按钮、链接、强调色 |
| 白色 | #ffffff | 背景色 |
| 输入背景 | #f8f8f8 | 表单输入背景 |
| 错误红 | #ff0000 | 错误状态 |
| 遮罩 | rgba(0,0,0,0.5) | 弹窗遮罩 |

### 2.3 字体规范

| 字体 | 用途 | 字重 |
|------|------|------|
| Raleway | 标题 | Bold/ExtraBold |
| Nunito Sans | 正文 | Light/Regular |
| Poppins | 表单输入 | Medium |

### 2.4 设计 Token（机器可读）

```json
{
  "colors": {
    "brand": { "black": "#202020", "blue": "#004bff" },
    "background": { "white": "#ffffff", "input": "#f8f8f8" },
    "error": "#ff0000",
    "overlay": "rgba(0, 0, 0, 0.5)"
  },
  "typography": {
    "headings": "Raleway",
    "body": "Nunito Sans",
    "input": "Poppins"
  },
  "spacing": {
    "xs": 4, "sm": 8, "base": 12, "md": 16, "lg": 20, "xl": 24
  },
  "borderRadius": {
    "sm": 4, "base": 8, "md": 9, "lg": 11, "xl": 18, "full": 9999
  }
}
```

---

## 3. 用户故事与验收标准

### 3.1 新用户注册

> **故事**: 作为一个新用户，我希望能够创建账户，以便使用 App 的完整功能。

**验收标准：**

| 标准编号 | 验收内容 | 验证方式 |
|---------|---------|---------|
| AC-001 | 用户可以输入邮箱、密码、手机号 | 手动测试 |
| AC-002 | 密码至少 8 位，可显示/隐藏 | 手动测试 |
| AC-003 | 邮箱格式验证 | 输入非法格式测试 |
| AC-004 | 注册成功后跳转登录页 | 流程测试 |

**表单验证规则：**

```json
{
  "email": {
    "required": true,
    "pattern": "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$",
    "messages": {
      "required": "Please enter your email",
      "pattern": "Please enter a valid email address"
    }
  },
  "password": {
    "required": true,
    "minLength": 8,
    "messages": {
      "required": "Please enter a password",
      "minLength": "Password must be at least 8 characters"
    }
  }
}
```

---

### 3.2 用户登录

> **故事**: 作为一个已有账户的用户，我希望输入邮箱和密码登录。

**验收标准：**

| 标准编号 | 验收内容 | 验证方式 |
|---------|---------|---------|
| AC-001 | 登录采用两步验证 | 流程测试 |
| AC-002 | 邮箱不存在时给出提示 | 输入未注册邮箱测试 |
| AC-003 | 密码错误可重新输入 | 输入错误密码测试 |
| AC-004 | 登录成功后进入首页 | 流程测试 |

**错误提示映射：**

```json
{
  "errors": {
    "empty_email": "Please enter your email",
    "invalid_email": "Please enter a valid email address",
    "account_not_found": "Account not found",
    "wrong_password": "错误状态（红色密码点）"
  }
}
```

---

### 3.3 密码找回

> **故事**: 作为一个忘记密码的用户，我希望通过验证码重置密码。

**验收标准：**

| 标准编号 | 验收内容 | 验证方式 |
|---------|---------|---------|
| AC-001 | 可选择短信或邮箱接收验证码 | 手动测试 |
| AC-002 | 验证码为 4 位数字 | 手动测试 |
| AC-003 | 错误 3 次后锁定并弹窗提示 | 测试 3 次错误 |
| AC-004 | 重置密码需两次输入一致 | 输入不一致测试 |

**验证码规则：**

```json
{
  "codeLength": 4,
  "codeType": "numeric",
  "maxAttempts": 3,
  "expiresIn": 300,
  "onMaxReached": "showModalAndRedirectToLogin"
}
```

---

## 4. 页面详细规格

### 4.1 启动页 (StartPage)

**路由**: `/`

**页面结构：**
```
┌─────────────────────────┐
│     Status Bar          │
├─────────────────────────┤
│                         │
│      [Logo]             │
│      Shoppe             │
│                         │
│  ┌─────────────────┐   │
│  │ Let's get       │   │
│  │    started      │   │
│  └─────────────────┘   │
│                         │
│  I already have an     │
│       account          │
│                         │
└─────────────────────────┘
```

**交互逻辑：**

| 元素 | 事件 | 目标路由 |
|------|------|---------|
| "Let's get started" | 点击 | `/register` |
| "I already have an account" | 点击 | `/login` |

---

### 4.2 注册页 (RegisterPage)

**路由**: `/register`

**表单配置：**

| 字段 | 类型 | 必填 | 验证规则 |
|------|------|------|---------|
| 邮箱 | Email | 是 | 合法邮箱格式 |
| 密码 | Password | 是 | ≥8 位，可显示/隐藏 |
| 手机号 | Tel | 否 | 国际格式，默认 +44 |

**操作按钮：**

| 按钮 | 位置 | 行为 |
|------|------|------|
| Cancel | 左上角 | 返回 `/` |
| Done | 底部 | 提交 → `/login` |

---

### 4.3 登录页 (LoginPage)

**路由**: `/login`

**表单配置：**

| 字段 | 类型 | 必填 | 验证规则 |
|------|------|------|---------|
| 邮箱 | Email | 是 | 合法格式 + 账号存在 |

**错误提示：**

| 场景 | 提示文案 |
|------|---------|
| 未输入 | "Please enter your email" |
| 格式错误 | "Please enter a valid email address" |
| 账号不存在 | "Account not found" |

---

### 4.4 密码输入页 (PasswordPage)

**路由**: `/password`

**状态流转：**

```
┌─────────────┐
│  4-digit   │ ← 初始状态：4 个空框
└──────┬──────┘
       │ 输入任意字符
       ▼
┌─────────────┐
│  8-digit   │ ← 8 个密码点
└──────┬──────┘
       │ 输满 8 位
       ▼
   ┌───┴───┐
   │       │
正确 ▼     ▼ 错误
┌─────────┐ ┌─────────┐
│ Hello   │ │  error  │
│  Card   │ │  状态   │
└─────────┘ └─────────┘
```

**UI 状态映射：**

| 状态 | UI 表现 |
|------|--------|
| 4-digit | 4 个空心方框 |
| 8-digit | 8 个密码点（蓝实/灰空） |
| error | 8 个红色实心点 + 忘记密码链接 |

---

### 4.5 商城首页 (ShopPage)

**路由**: `/shop`

**页面区块：**

| 区块 ID | 区块名称 | 布局方式 | 内容 |
|--------|---------|---------|------|
| banner | 促销轮播 | 轮播 | 4 组 Banner，3 秒自动切换 |
| categories | 分类导航 | 2 列网格 | 8 个分类 |
| topProducts | 热门商品 | 横向排列 | 5 个分类图标 |
| newItems | 新品 | 3 列网格 | 3 个商品 |
| flashSale | 限时特卖 | 3 列网格 | 6 个商品 + 倒计时 |
| mostPopular | 人气商品 | 2 列网格 | 4 个商品 |
| justForYou | 为你推荐 | 2 列网格 | 4 个商品 |

**底部导航：**

```json
{
  "tabs": [
    { "id": "home", "label": "Home", "icon": "home.svg" },
    { "id": "cart", "label": "Cart", "icon": "shopping-bag.svg" },
    { "id": "categories", "label": "Categories", "icon": "menu.svg" },
    { "id": "wishlist", "label": "Wishlist", "icon": "heart.svg" },
    { "id": "profile", "label": "Profile", "icon": "profile.svg" }
  ]
}
```

---

### 4.6 商品详情页 (ProductDetailPage)

**路由**: `/product/:id`

**页面区块：**

| 区块 | 说明 |
|------|------|
| 图片轮播 | 5+ 张图片，滑动切换，圆点指示器，3 秒自动轮播 |
| 价格描述 | 价格 ($17,00) + 商品描述 |
| 规格预览 | 已选颜色/尺寸芯片 + 展开按钮 |
| 商品规格 | 材质 (Cotton 95%、Nylon 5%)、产地 (EU)、尺码指南 |
| 配送方式 | Standart ($3,00/5-7 天)、Express ($12,00/1-2 天) |
| 评分评价 | 4/5 星 + 用户评价 (Veronika) |
| 热门商品 | 4 个商品横向滚动 |
| 推荐商品 | 2 列网格 |

**底部操作栏：**

| 按钮 | 宽度 | 背景色 |
|------|------|--------|
| 收藏 | 48px | #f5f5f5 |
| Add to Cart | flex: 1 | #202020 |
| Buy Now | flex: 1.5 | #004bff |

**规格选择器配置：**

```json
{
  "variations": {
    "colors": [
      { "id": 1, "name": "Pink", "image": "pink.png" },
      { "id": 2, "name": "Floral", "image": "floral.png" },
      { "id": 3, "name": "Red", "image": "red.png" }
    ],
    "sizes": ["S", "M", "L", "XL", "XXL", "XXXL"],
    "disabledSizes": ["XXL", "XXXL"],
    "quantity": { "min": 1, "max": 99, "initial": 1 }
  }
}
```

---

## 5. 数据模型

### 5.1 用户模型

| 字段 | 类型 | 说明 |
|------|------|------|
| id | integer | 用户唯一标识 |
| email | string | 邮箱地址 |
| phoneNumber | string | 电话号码（国际格式） |
| avatar | string | 头像 URL |

### 5.2 商品模型

| 字段 | 类型 | 说明 |
|------|------|------|
| id | string/integer | 商品 ID |
| name | string | 商品名称 |
| description | string | 商品描述 |
| price | number | 当前价格 |
| originalPrice | number | 原价 |
| images | array | 图片列表 |
| rating | number | 评分 (0-5) |
| salesCount | integer | 销量 |
| variations | array | 规格选项 |

### 5.3 购物车模型

| 字段 | 类型 | 说明 |
|------|------|------|
| id | string | 购物车 ID |
| userId | integer | 用户 ID |
| items | array | 购物车项列表 |
| totalPrice | number | 总价 |

---

## 6. API 接口概览

### 6.1 认证模块

| 接口 | 方法 | 说明 |
|------|------|------|
| /api/register | POST | 用户注册 |
| /api/login-step1 | POST | 登录第一步（邮箱验证） |
| /api/login | POST | 登录第二步（密码验证） |
| /api/logout | POST | 登出 |
| /api/refresh | POST | 刷新 Token |
| /api/verify | GET | 验证 Token |
| /api/password-recovery | POST | 密码恢复请求 |
| /api/verify-code | POST | 验证码验证 |
| /api/reset-password | POST | 重置密码 |

### 6.2 商品模块

| 接口 | 方法 | 说明 |
|------|------|------|
| /api/products | GET | 获取商品列表 |
| /api/products/{id} | GET | 获取商品详情 |

---

## 7. 业务规则

### 7.1 认证规则

| 规则名称 | 规则说明 |
|---------|---------|
| 密码最小长度 | 8 位 |
| 验证码长度 | 4 位数字 |
| 验证码有效期 | 5 分钟 |
| 最大尝试次数 | 3 次（超过后锁定） |
| Token 有效期 | 访问 Token 15 分钟，刷新 Token 7 天 |

### 7.2 商品展示规则

| 规则名称 | 规则说明 |
|---------|---------|
| 价格格式 | $XX,XX（2 位小数，逗号分隔） |
| 新品标签 | 上架 7 天内，粉色渐变背景 |
| 促销标签 | 原价>现价，红色渐变背景，显示折扣百分比 |
| 热门标签 | 销量>1000，橙色渐变背景 |

### 7.3 购物车规则

| 规则名称 | 规则说明 |
|---------|---------|
| 最小数量 | 1 |
| 最大数量 | 99 |
| 配送方式 | Standart: $3/5-7 天，Express: $12/1-2 天 |

---

## 8. 页面流转图

```
StartPage
├── Let's get started → RegisterPage → Done → LoginPage
└── I already have an account → LoginPage
                              ↓
                        PasswordPage
                              ↓
                    ┌─────────┴─────────┐
                    ↓                   ↓
               正确 → Onboarding      错误 → Forgot password?
                    ↓                      ↓
                Let's Go → ShopPage   PasswordRecovery
                                           ↓
                                    PasswordRecoveryCode
                                           ├── 正确 → ResetPassword
                                           └── 错误 3 次 → MaximumAttempts
```

---

## 9. 错误处理

### 8.1 错误提示映射表

| 错误场景 | 提示文案 | 显示方式 |
|---------|---------|---------|
| 邮箱为空 | Please enter your email | 字段下方 |
| 邮箱格式错误 | Please enter a valid email address | 字段下方 |
| 账号不存在 | Account not found | 字段下方 |
| 密码错误 | （红色密码点状态） | 密码点变红 |
| 验证码错误 | （计数 +1，清空输入） | Toast 提示 |
| 超过最大尝试次数 | Maximum Attempts Popup | 弹窗 |
| Token 过期 | 自动刷新或跳转登录 | 无提示 |

---

## 10. 待实现功能

| 功能模块 | 优先级 | 状态 | 说明 |
|---------|--------|------|------|
| 购物车页面 | P0 | 待实现 | 展示购物车商品 |
| 分类页面 | P0 | 待实现 | 按分类浏览 |
| 个人中心 | P1 | 待实现 | 订单、设置 |
| 搜索页面 | P1 | 待实现 | 搜索功能 |
| 订单页面 | P1 | 待实现 | 订单历史 |
| 支付流程 | P0 | 待实现 | 结算支付 |

---

## 11. 变更记录

| 版本 | 日期 | 变更内容 |
|------|------|---------|
| 2.0 | 2026-04-01 | 混合版：主体文字描述 + 关键 JSON 数据块 |
| 1.0 | 2026-03-20 | 初始版本 |

---

## 附录：测试账号

| 账号 | 密码 |
|------|------|
| deanwen@gmail.com | 12345678 |
