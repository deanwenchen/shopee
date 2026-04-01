# Shoppe 电商应用 - 通用产品规格文档

**版本**: 5.0 (Platform-Agnostic)
**最后更新**: 2026-04-01
**文档类型**: 跨平台代码生成规格 (Cross-Platform Code Generation Spec)
**适用目标**: Web / Android / iOS / Backend

---

## 📋 文档说明

本文档采用 **Markdown + 结构化数据** 格式：
- ✅ 人类可读 - 产品/设计/开发可阅读
- ✅ 机器可解析 - 支持代码生成工具提取数据
- ✅ 平台无关 - 不绑定任何技术栈

### 结构化数据块说明

| 类型 | 用途 | 生成目标 |
|------|------|----------|
| `JSON Schema` | 数据模型定义 | 后端 Entity / 前端 Type / 移动端 Model |
| `OpenAPI 3.0` | API 接口定义 | 后端 Controller / 前端 Service / 移动端 API Client |
| `Design Token` | 设计系统定义 | CSS Variables / SwiftUI Token / Android Compose Theme |
| `State Machine` | 状态流转定义 | 各端路由守卫 / 状态管理 |

---

## 1. 产品概述

### 1.1 产品定位

Shoppe 是一个移动端优先的电商应用，提供完整的用户认证流程和购物体验。

### 1.2 目标平台

| 平台 | 优先级 | 说明 |
|------|--------|------|
| Mobile Web | P0 | 响应式 H5，iPhone 尺寸优先 |
| Android | P1 | 原生或 Compose |
| iOS | P1 | SwiftUI 优先 |
| Backend API | P0 | RESTful API |

### 1.3 功能模块总览

```json
{
  "modules": [
    {
      "id": "auth",
      "name": "用户认证模块",
      "priority": "P0",
      "status": "completed",
      "features": [
        "start_page",
        "register",
        "login_two_step",
        "password_recovery",
        "onboarding"
      ]
    },
    {
      "id": "commerce",
      "name": "电商核心模块",
      "priority": "P0",
      "status": "partial",
      "features": [
        "shop_home",
        "product_detail",
        "cart",
        "category",
        "wishlist"
      ]
    },
    {
      "id": "user",
      "name": "用户中心模块",
      "priority": "P1",
      "status": "pending",
      "features": [
        "profile",
        "orders",
        "settings"
      ]
    }
  ]
}
```

---

## 2. 设计系统 (Design System)

### 2.1 画布规格

```json
{
  "canvas": {
    "platform": "mobile",
    "baseWidth": 375,
    "baseHeight": 817,
    "unit": "dp",
    "safeArea": {
      "top": 44,
      "bottom": 34
    },
    "statusBarHeight": 44,
    "homeIndicatorHeight": 34
  }
}
```

### 2.2 颜色 Design Token

```json
{
  "colors": {
    "brand": {
      "black": {
        "value": "#202020",
        "description": "主要文字颜色",
        "usage": ["text-primary", "titles"]
      },
      "blue": {
        "value": "#004bff",
        "description": "品牌主色/行动色",
        "usage": ["buttons", "links", "accents"]
      }
    },
    "background": {
      "white": {
        "value": "#ffffff",
        "description": "背景色"
      },
      "input": {
        "value": "#f8f8f8",
        "description": "表单输入背景"
      },
      "overlay": {
        "value": "rgba(0, 0, 0, 0.5)",
        "description": "遮罩层"
      }
    },
    "semantic": {
      "error": {
        "value": "#ff0000",
        "description": "错误状态"
      },
      "success": {
        "value": "#00aa00",
        "description": "成功状态"
      },
      "disabled": {
        "value": "#e0e0e0",
        "description": "禁用状态"
      }
    },
    "gradient": {
      "tag-new": {
        "value": "linear-gradient(135deg, #ff9a9e, #fecfef)",
        "description": "新品标签"
      },
      "tag-sale": {
        "value": "linear-gradient(135deg, #ff5790, #f81140)",
        "description": "促销标签"
      },
      "tag-hot": {
        "value": "linear-gradient(135deg, #ff6b6b, #ee5a5a)",
        "description": "热门标签"
      }
    }
  }
}
```

### 2.3 字体 Design Token

```json
{
  "typography": {
    "families": {
      "headings": {
        "value": "Raleway",
        "fallback": "sans-serif",
        "platforms": {
          "web": "'Raleway', system-ui, sans-serif",
          "android": "resources/font/raleway.ttf",
          "ios": "Raleway"
        }
      },
      "body": {
        "value": "Nunito Sans",
        "fallback": "sans-serif"
      },
      "input": {
        "value": "Poppins",
        "fallback": "sans-serif"
      }
    },
    "sizes": {
      "xs": { "value": 11, "lineHeight": 16 },
      "sm": { "value": 12, "lineHeight": 18 },
      "base": { "value": 14, "lineHeight": 20 },
      "lg": { "value": 15, "lineHeight": 22 },
      "xl": { "value": 17, "lineHeight": 24 },
      "2xl": { "value": 20, "lineHeight": 28 },
      "3xl": { "value": 26, "lineHeight": 32 },
      "4xl": { "value": 28, "lineHeight": 36 }
    },
    "weights": {
      "light": { "value": 300 },
      "regular": { "value": 400 },
      "medium": { "value": 500 },
      "semibold": { "value": 600 },
      "bold": { "value": 700 },
      "extrabold": { "value": 800 }
    }
  }
}
```

### 2.4 间距 Design Token

```json
{
  "spacing": {
    "xs": { "value": 4, "description": "最小间距" },
    "sm": { "value": 8, "description": "小间距" },
    "base": { "value": 12, "description": "基础间距" },
    "md": { "value": 16, "description": "中等间距" },
    "lg": { "value": 20, "description": "大间距" },
    "xl": { "value": 24, "description": "超大间距" },
    "2xl": { "value": 32, "description": "页边距" }
  },
  "borderRadius": {
    "sm": { "value": 4 },
    "base": { "value": 8 },
    "md": { "value": 9 },
    "lg": { "value": 11 },
    "xl": { "value": 18 },
    "2xl": { "value": 20 },
    "full": { "value": 9999 }
  }
}
```

### 2.5 阴影 Design Token

```json
{
  "shadows": {
    "card": {
      "value": {
        "offsetX": 0,
        "offsetY": 5,
        "blur": 10,
        "spread": 0,
        "color": "rgba(0, 0, 0, 0.1)"
      },
      "description": "卡片阴影"
    },
    "bottomNav": {
      "value": {
        "offsetX": 0,
        "offsetY": -4,
        "blur": 10,
        "spread": 0,
        "color": "rgba(0, 0, 0, 0.05)"
      },
      "description": "底部导航阴影"
    }
  }
}
```

---

## 3. 数据模型 (Data Models)

### 3.1 用户模型 (User)

```json
{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "$id": "User",
  "title": "User",
  "description": "用户实体",
  "type": "object",
  "properties": {
    "id": {
      "type": "integer",
      "description": "用户唯一标识",
      "readOnly": true
    },
    "email": {
      "type": "string",
      "format": "email",
      "description": "邮箱地址"
    },
    "phoneNumber": {
      "type": "string",
      "pattern": "^\\+[0-9]{10,15}$",
      "description": "国际格式电话号码"
    },
    "avatar": {
      "type": "string",
      "format": "uri",
      "description": "头像 URL"
    },
    "createdAt": {
      "type": "string",
      "format": "date-time",
      "readOnly": true
    }
  },
  "required": ["id", "email"]
}
```

### 3.2 商品模型 (Product)

```json
{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "$id": "Product",
  "title": "Product",
  "description": "商品实体",
  "type": "object",
  "properties": {
    "id": {
      "type": ["string", "integer"],
      "description": "商品唯一标识"
    },
    "name": {
      "type": "string",
      "description": "商品名称"
    },
    "description": {
      "type": "string",
      "description": "商品描述"
    },
    "price": {
      "type": "number",
      "minimum": 0,
      "description": "当前价格"
    },
    "originalPrice": {
      "type": "number",
      "minimum": 0,
      "description": "原价（促销时使用）"
    },
    "images": {
      "type": "array",
      "items": {
        "type": "string",
        "format": "uri"
      },
      "minItems": 1,
      "description": "商品图片列表"
    },
    "category": {
      "type": "object",
      "properties": {
        "id": { "type": "integer" },
        "name": { "type": "string" }
      }
    },
    "tags": {
      "type": "array",
      "items": {
        "type": "string",
        "enum": ["new", "sale", "hot"]
      }
    },
    "rating": {
      "type": "number",
      "minimum": 0,
      "maximum": 5,
      "description": "评分 (0-5)"
    },
    "salesCount": {
      "type": "integer",
      "minimum": 0,
      "description": "销量"
    },
    "variations": {
      "type": "array",
      "items": { "$ref": "#/definitions/Variation" }
    }
  },
  "required": ["id", "name", "price", "images"],
  "definitions": {
    "Variation": {
      "type": "object",
      "properties": {
        "id": { "type": "integer" },
        "type": { "type": "string", "enum": ["color", "size"] },
        "name": { "type": "string" },
        "image": { "type": "string", "format": "uri" },
        "disabled": { "type": "boolean", "default": false }
      },
      "required": ["id", "type", "name"]
    }
  }
}
```

### 3.3 购物车模型 (Cart)

```json
{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "$id": "Cart",
  "title": "Cart",
  "description": "购物车实体",
  "type": "object",
  "properties": {
    "id": {
      "type": "string",
      "format": "uuid",
      "description": "购物车 ID"
    },
    "userId": {
      "type": "integer",
      "description": "所属用户 ID"
    },
    "items": {
      "type": "array",
      "items": { "$ref": "#/definitions/CartItem" }
    },
    "totalPrice": {
      "type": "number",
      "description": "总价"
    },
    "updatedAt": {
      "type": "string",
      "format": "date-time"
    }
  },
  "required": ["id", "userId", "items", "totalPrice"],
  "definitions": {
    "CartItem": {
      "type": "object",
      "properties": {
        "productId": { "type": ["string", "integer"] },
        "quantity": { "type": "integer", "minimum": 1 },
        "selectedVariations": {
          "type": "array",
          "items": {
            "type": "object",
            "properties": {
              "type": { "type": "string" },
              "value": { "type": "string" }
            }
          }
        },
        "price": { "type": "number" }
      },
      "required": ["productId", "quantity", "price"]
    }
  }
}
```

### 3.4 认证 Token 模型

```json
{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "$id": "AuthToken",
  "title": "AuthToken",
  "type": "object",
  "properties": {
    "accessToken": {
      "type": "string",
      "description": "访问令牌 (JWT)"
    },
    "refreshToken": {
      "type": "string",
      "description": "刷新令牌",
      "httpOnly": true
    },
    "expiresIn": {
      "type": "integer",
      "description": "过期时间 (秒)"
    },
    "tokenType": {
      "type": "string",
      "default": "Bearer"
    }
  },
  "required": ["accessToken", "refreshToken", "expiresIn"]
}
```

---

## 4. API 规格 (OpenAPI 3.0)

### 4.1 服务信息

```yaml
openapi: 3.0.3
info:
  title: Shoppe API
  description: Shoppe 电商应用后端服务
  version: 1.0.0
  contact:
    name: Shoppe Team
servers:
  - url: http://localhost:9000/api
    description: 本地开发环境
  - url: https://api.shoppe.com/v1
    description: 生产环境
```

### 4.2 认证模块 API

#### 4.2.1 用户注册

```yaml
/register:
  post:
    summary: 用户注册
    operationId: register
    tags:
      - 认证
    requestBody:
      required: true
      content:
        application/json:
          schema:
            type: object
            properties:
              email:
                type: string
                format: email
                example: "user@example.com"
              password:
                type: string
                minLength: 8
                example: "12345678"
              phoneNumber:
                type: string
                pattern: "^\+[0-9]{10,15}$"
                example: "+44123456789"
            required:
              - email
              - password
    responses:
      '201':
        description: 注册成功
        content:
          application/json:
            schema:
              type: object
              properties:
                success:
                  type: boolean
                  example: true
                userId:
                  type: integer
                  example: 1
                message:
                  type: string
                  example: "Registration successful"
      '400':
        description: 请求参数错误
      '409':
        description: 邮箱已存在
```

#### 4.2.2 登录第一步（邮箱验证）

```yaml
/login-step1:
  post:
    summary: 登录第一步 - 邮箱验证
    operationId: loginStep1
    tags:
      - 认证
    requestBody:
      required: true
      content:
        application/json:
          schema:
            type: object
            properties:
              email:
                type: string
                format: email
            required:
              - email
    responses:
      '200':
        description: 邮箱验证通过，请输入密码
        content:
          application/json:
            schema:
              type: object
              properties:
                success:
                  type: boolean
                requiresPassword:
                  type: boolean
                  example: true
                message:
                  type: string
      '404':
        description: 账号不存在
        content:
          application/json:
            schema:
              type: object
              properties:
                success:
                  type: boolean
                  example: false
                message:
                  type: string
                  example: "Account not found"
```

#### 4.2.3 登录第二步（密码验证）

```yaml
/login:
  post:
    summary: 登录第二步 - 密码验证
    operationId: login
    tags:
      - 认证
    requestBody:
      required: true
      content:
        application/json:
          schema:
            type: object
            properties:
              email:
                type: string
                format: email
              password:
                type: string
            required:
              - email
              - password
    responses:
      '200':
        description: 登录成功
        content:
          application/json:
            schema:
              allOf:
                - $ref: '#/components/schemas/AuthToken'
                - type: object
                  properties:
                    success:
                      type: boolean
                    user:
                      $ref: '#/components/schemas/User'
      '401':
        description: 密码错误
```

#### 4.2.4 密码恢复请求

```yaml
/password-recovery:
  post:
    summary: 密码恢复请求
    operationId: passwordRecovery
    tags:
      - 认证
    requestBody:
      required: true
      content:
        application/json:
          schema:
            type: object
            properties:
              email:
                type: string
                format: email
              method:
                type: string
                enum:
                  - sms
                  - email
            required:
              - email
              - method
    responses:
      '200':
        description: 验证码已发送
        content:
          application/json:
            schema:
              type: object
              properties:
                success:
                  type: boolean
                codeId:
                  type: string
                  description: "验证码会话 ID"
                message:
                  type: string
```

#### 4.2.5 验证码验证

```yaml
/verify-code:
  post:
    summary: 验证恢复验证码
    operationId: verifyCode
    tags:
      - 认证
    requestBody:
      required: true
      content:
        application/json:
          schema:
            type: object
            properties:
              codeId:
                type: string
              code:
                type: string
                pattern: '^\d{4}$'
            required:
              - codeId
              - code
    responses:
      '200':
        description: 验证成功
        content:
          application/json:
            schema:
              type: object
              properties:
                success:
                  type: boolean
                resetToken:
                  type: string
                  description: "密码重置令牌"
      '400':
        description: 验证码错误或已过期
      '429':
        description: 超过最大尝试次数
```

#### 4.2.6 重置密码

```yaml
/reset-password:
  post:
    summary: 重置密码
    operationId: resetPassword
    tags:
      - 认证
    requestBody:
      required: true
      content:
        application/json:
          schema:
            type: object
            properties:
              resetToken:
                type: string
              newPassword:
                type: string
                minLength: 8
            required:
              - resetToken
              - newPassword
    responses:
      '200':
        description: 密码重置成功
```

#### 4.2.7 Token 验证

```yaml
/verify:
  get:
    summary: 验证访问 Token
    operationId: verifyToken
    tags:
      - 认证
    security:
      - bearerAuth: []
    responses:
      '200':
        description: Token 有效
        content:
          application/json:
            schema:
              type: object
              properties:
                valid:
                  type: boolean
                  example: true
                user:
                  $ref: '#/components/schemas/User'
      '401':
        description: Token 无效或已过期
```

#### 4.2.8 刷新 Token

```yaml
/refresh:
  post:
    summary: 刷新访问 Token
    operationId: refreshToken
    tags:
      - 认证
    security:
      - cookieAuth: []
    responses:
      '200':
        description: 刷新成功
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/AuthToken'
```

#### 4.2.9 登出

```yaml
/logout:
  post:
    summary: 用户登出
    operationId: logout
    tags:
      - 认证
    security:
      - bearerAuth: []
    responses:
      '200':
        description: 登出成功
```

### 4.3 商品模块 API

#### 4.3.1 获取商品列表

```yaml
/products:
  get:
    summary: 获取商品列表
    operationId: getProducts
    tags:
      - 商品
    parameters:
      - name: category
        in: query
        schema:
          type: integer
      - name: page
        in: query
        schema:
          type: integer
          default: 1
      - name: limit
        in: query
        schema:
          type: integer
          default: 20
      - name: sort
        in: query
        schema:
          type: string
          enum: [popular, price_asc, price_desc, newest]
    responses:
      '200':
        description: 商品列表
        content:
          application/json:
            schema:
              type: object
              properties:
                items:
                  type: array
                  items:
                    $ref: '#/components/schemas/Product'
                total:
                  type: integer
                page:
                  type: integer
                hasMore:
                  type: boolean
```

#### 4.3.2 获取商品详情

```yaml
/products/{id}:
  get:
    summary: 获取商品详情
    operationId: getProductById
    tags:
      - 商品
    parameters:
      - name: id
        in: path
        required: true
        schema:
          type: string
    responses:
      '200':
        description: 商品详情
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/Product'
      '404':
        description: 商品不存在
```

### 4.4 组件 Schemas

```yaml
components:
  schemas:
    User:
      type: object
      properties:
        id:
          type: integer
        email:
          type: string
          format: email
        phoneNumber:
          type: string
        avatar:
          type: string
          format: uri
    AuthToken:
      type: object
      properties:
        accessToken:
          type: string
        refreshToken:
          type: string
        expiresIn:
          type: integer
        tokenType:
          type: string
          default: Bearer
  securitySchemes:
    bearerAuth:
      type: http
      scheme: bearer
      bearerFormat: JWT
    cookieAuth:
      type: apiKey
      in: cookie
      name: refreshToken
```

---

## 5. 页面规格 (Page Specifications)

### 5.1 路由配置

```json
{
  "routes": [
    {
      "path": "/",
      "name": "start",
      "component": "StartPage",
      "meta": { "guest": true, "auth": false },
      "description": "启动页面"
    },
    {
      "path": "/register",
      "name": "register",
      "component": "RegisterPage",
      "meta": { "guest": true, "auth": false },
      "description": "注册页面"
    },
    {
      "path": "/login",
      "name": "login",
      "component": "LoginPage",
      "meta": { "guest": true, "auth": false },
      "description": "登录页面"
    },
    {
      "path": "/password",
      "name": "password",
      "component": "PasswordPage",
      "meta": { "guest": true, "auth": false, "flow": "login" },
      "description": "密码输入页面"
    },
    {
      "path": "/password-recovery",
      "name": "password-recovery",
      "component": "PasswordRecoveryPage",
      "meta": { "guest": true, "auth": false },
      "description": "密码恢复方式选择"
    },
    {
      "path": "/password-recovery-code",
      "name": "password-recovery-code",
      "component": "PasswordRecoveryCodePage",
      "meta": { "guest": true, "auth": false },
      "description": "验证码输入"
    },
    {
      "path": "/reset-password",
      "name": "reset-password",
      "component": "ResetPasswordPage",
      "meta": { "guest": true, "auth": false },
      "description": "设置新密码"
    },
    {
      "path": "/onboarding",
      "name": "onboarding",
      "component": "OnboardingPage",
      "meta": { "auth": true },
      "description": "引导轮播"
    },
    {
      "path": "/shop",
      "name": "shop",
      "component": "ShopPage",
      "meta": { "auth": true, "tab": "home" },
      "description": "商城主页"
    },
    {
      "path": "/product/:id",
      "name": "product-detail",
      "component": "ProductDetailPage",
      "meta": { "auth": true },
      "description": "商品详情页"
    },
    {
      "path": "/cart",
      "name": "cart",
      "component": "CartPage",
      "meta": { "auth": true, "tab": "cart" },
      "description": "购物车"
    },
    {
      "path": "/category/:id",
      "name": "category",
      "component": "CategoryPage",
      "meta": { "auth": true },
      "description": "分类页面"
    },
    {
      "path": "/wishlist",
      "name": "wishlist",
      "component": "WishlistPage",
      "meta": { "auth": true, "tab": "wishlist" },
      "description": "心愿单"
    },
    {
      "path": "/profile",
      "name": "profile",
      "component": "ProfilePage",
      "meta": { "auth": true, "tab": "profile" },
      "description": "个人中心"
    }
  ]
}
```

### 5.2 路由守卫逻辑

```json
{
  "navigationGuard": {
    "description": "全局路由守卫逻辑",
    "steps": [
      {
        "step": 1,
        "action": "checkAccessToken",
        "description": "检查本地是否存在 accessToken"
      },
      {
        "step": 2,
        "action": "verifyToken",
        "condition": "accessToken exists",
        "description": "调用 API 验证 Token 有效性"
      },
      {
        "step": 3,
        "action": "handleAuthRoute",
        "condition": "route.meta.auth === true",
        "logic": {
          "if": "isAuthenticated",
          "then": "proceed",
          "else": "redirect('/')"
        }
      },
      {
        "step": 4,
        "action": "handleGuestRoute",
        "condition": "route.meta.guest === true",
        "logic": {
          "if": "isAuthenticated AND route.path !== '/password'",
          "then": "redirect('/shop')",
          "else": "proceed"
        },
        "exception": {
          "path": "/password",
          "reason": "密码页面是登录流程一部分，允许已认证用户访问"
        }
      }
    ]
  }
}
```

---

## 6. 页面详细规格

### 6.1 StartPage (启动页面)

**路由**: `/`
**认证要求**: Guest Only

#### 页面结构

```
┌─────────────────────────┐
│     Status Bar          │
├─────────────────────────┤
│                         │
│      [Logo]             │
│      Shoppe             │
│                         │
│   "Your style, your     │
│      way"               │
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

#### 交互行为

| 元素 | 事件 | 行为 | 目标路由 |
|------|------|------|----------|
| "Let's get started" 按钮 | Click/Tap | 导航 | `/register` |
| "I already have an account" 链接 | Click/Tap | 导航 | `/login` |

#### 业务规则

- 已认证用户访问此页时，若从外部链接进入，保持当前页；若从 App 内部返回，重定向到 `/shop`

---

### 6.2 RegisterPage (注册页面)

**路由**: `/register`
**认证要求**: Guest Only

#### 页面结构

```
┌─────────────────────────┐
│     Status Bar          │
├─────────────────────────┤
│  ← Cancel    Create     │
│             Account     │
├─────────────────────────┤
│                         │
│  Email                  │
│  ┌─────────────────┐   │
│  │                 │   │
│  └─────────────────┘   │
│                         │
│  Password               │
│  ┌───────────────┐ [👁] │
│  │               │       │
│  └───────────────┘       │
│                         │
│  Phone Number           │
│  ┌──────────────┐       │
│  │ +44          │       │
│  └──────────────┘       │
│                         │
│  ┌─────────────────┐   │
│  │      Done       │   │
│  └─────────────────┘   │
│                         │
└─────────────────────────┘
```

#### 表单字段

```json
{
  "fields": [
    {
      "name": "email",
      "type": "email",
      "label": "Email",
      "placeholder": "Enter your email",
      "required": true,
      "validation": {
        "pattern": "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$",
        "messages": {
          "required": "Please enter your email",
          "pattern": "Please enter a valid email address"
        }
      }
    },
    {
      "name": "password",
      "type": "password",
      "label": "Password",
      "placeholder": "Enter password",
      "required": true,
      "minLength": 8,
      "showPasswordToggle": true,
      "validation": {
        "messages": {
          "required": "Please enter a password",
          "minLength": "Password must be at least 8 characters"
        }
      }
    },
    {
      "name": "phoneNumber",
      "type": "tel",
      "label": "Phone Number",
      "placeholder": "Enter phone number",
      "required": false,
      "countryCodePicker": {
        "default": "GB",
        "defaultCode": "+44"
      }
    }
  ]
}
```

#### 操作按钮

| 按钮 | 位置 | 行为 |
|------|------|------|
| Cancel | 左上角 | 导航到 `/` |
| Done | 底部 | 提交表单，成功后导航到 `/login` |

#### API 调用

```json
{
  "endpoint": "POST /api/register",
  "request": {
    "email": "string",
    "password": "string",
    "phoneNumber": "string (optional)"
  },
  "success": {
    "action": "navigate",
    "target": "/login"
  },
  "error": {
    "action": "showToast",
    "message": "from response"
  }
}
```

---

### 6.3 LoginPage (登录页面)

**路由**: `/login`
**认证要求**: Guest Only

#### 页面结构

```
┌─────────────────────────┐
│     Status Bar          │
├─────────────────────────┤
│  ← Cancel    Login      │
├─────────────────────────┤
│                         │
│  Email                  │
│  ┌─────────────────┐   │
│  │                 │   │
│  └─────────────────┘   │
│  [Error message]        │
│                         │
│  ┌─────────────────┐   │
│  │      Next       │   │
│  └─────────────────┘   │
│                         │
└─────────────────────────┘
```

#### 表单字段

```json
{
  "fields": [
    {
      "name": "email",
      "type": "email",
      "label": "Email",
      "placeholder": "Enter your email",
      "required": true,
      "validation": {
        "pattern": "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$",
        "messages": {
          "required": "Please enter your email",
          "pattern": "Please enter a valid email address",
          "notFound": "Account not found"
        }
      }
    }
  ]
}
```

#### 操作按钮

| 按钮 | 位置 | 行为 |
|------|------|------|
| Cancel | 左上角 | 导航到 `/` |
| Next | 底部 | 提交邮箱，成功后导航到 `/password` |

#### API 调用

```json
{
  "endpoint": "POST /api/login-step1",
  "request": { "email": "string" },
  "success": {
    "action": "navigate",
    "target": "/password"
  },
  "error": {
    "action": "showError",
    "displayLocation": "below-input"
  }
}
```

#### 测试账号

```json
{
  "testCredentials": {
    "email": "deanwen@gmail.com",
    "password": "12345678"
  }
}
```

---

### 6.4 PasswordPage (密码输入页面)

**路由**: `/password`
**认证要求**: Guest Only

#### 状态机

```json
{
  "machine": {
    "id": "passwordInput",
    "initial": "fourDigit",
    "states": {
      "fourDigit": {
        "description": "4 位空框占位状态",
        "on": {
          "INPUT_ANY": {
            "target": "eightDigit",
            "action": "switchToEightDigit"
          }
        }
      },
      "eightDigit": {
        "description": "8 位密码点状态",
        "on": {
          "INPUT_COMPLETE": {
            "target": "verifying",
            "action": "verifyPassword"
          }
        }
      },
      "verifying": {
        "type": "parallel",
        "on": {
          "SUCCESS": { "target": "success" },
          "FAILURE": { "target": "error" }
        }
      },
      "success": {
        "description": "密码正确",
        "onEntry": { "navigate": "/onboarding" }
      },
      "error": {
        "description": "密码错误状态（红色实心点）",
        "on": {
          "BACKSPACE": { "target": "eightDigit" },
          "FORGOT_PASSWORD": { "navigate": "/password-recovery" }
        }
      }
    }
  }
}
```

#### UI 状态映射

| 状态 | UI 表现 |
|------|--------|
| fourDigit | 4 个空心方框 |
| eightDigit | 8 个密码点（蓝色实心=已输入，灰色空心=未输入） |
| error | 8 个红色实心点 + "Forgot your password?" 链接 |

#### 操作

| 元素 | 事件 | 行为 |
|------|------|------|
| Not you? 链接 | Click (非 error 状态) | 导航到 `/login` |
| Forgot your password? | Click (error 状态) | 导航到 `/password-recovery` |

#### API 调用

```json
{
  "endpoint": "POST /api/login",
  "request": {
    "email": "from previous step",
    "password": "string"
  },
  "success": {
    "action": "storeToken",
    "then": "navigate",
    "target": "/onboarding"
  },
  "error": {
    "action": "setState",
    "state": "error"
  }
}
```

---

### 6.5 PasswordRecoveryPage (密码恢复)

**路由**: `/password-recovery`
**认证要求**: Guest Only

#### 页面结构

```
┌─────────────────────────┐
│     Status Bar          │
├─────────────────────────┤
│  ← Cancel    Recover    │
│             Password    │
├─────────────────────────┤
│                         │
│  How would you like to  │
│  receive your code?     │
│                         │
│  ┌─────────────────┐   │
│  │ ✓ SMS           │   │
│  └─────────────────┘   │
│  ┌─────────────────┐   │
│  │   Email         │   │
│  └─────────────────┘   │
│                         │
│  ┌─────────────────┐   │
│  │      Next       │   │
│  └─────────────────┘   │
│                         │
└─────────────────────────┘
```

#### 选项

```json
{
  "options": [
    {
      "id": "sms",
      "label": "SMS",
      "description": "Send code to phone number",
      "default": true,
      "icon": "check-circle"
    },
    {
      "id": "email",
      "label": "Email",
      "description": "Send code to email address",
      "default": false,
      "icon": "check-circle"
    }
  ]
}
```

#### 交互

- 单选模式，点击切换选中状态
- 选中样式：蓝色背景 + check 图标

#### API 调用

```json
{
  "endpoint": "POST /api/password-recovery",
  "request": {
    "email": "from login context",
    "method": "sms | email"
  },
  "success": {
    "action": "store",
    "data": "codeId",
    "then": "navigate",
    "target": "/password-recovery-code"
  }
}
```

---

### 6.6 PasswordRecoveryCodePage (验证码输入)

**路由**: `/password-recovery-code`
**认证要求**: Guest Only

#### 状态机

```json
{
  "machine": {
    "id": "codeVerification",
    "initial": "input",
    "context": {
      "attemptCount": 0,
      "maxAttempts": 3
    },
    "states": {
      "input": {
        "on": {
          "SUBMIT": { "target": "verifying" }
        }
      },
      "verifying": {
        "on": {
          "SUCCESS": { "target": "success" },
          "FAILURE": {
            "target": "input",
            "actions": ["incrementAttempts", "clearInput"]
          }
        }
      },
      "success": {
        "onEntry": { "navigate": "/reset-password" }
      },
      "locked": {
        "condition": "attemptCount >= maxAttempts",
        "onEntry": { "showModal": "maximumAttempts" }
      }
    }
  }
}
```

#### 表单配置

```json
{
  "codeInput": {
    "type": "numeric",
    "length": 4,
    "autoFocus": true,
    "correctValue": "1234"
  },
  "attempts": {
    "max": 3,
    "onExceed": {
      "action": "showModal",
      "modal": "maximumAttempts",
      "then": "navigate",
      "target": "/login"
    }
  }
}
```

#### 操作

| 元素 | 事件 | 行为 |
|------|------|------|
| Send Again 链接 | Click | 重置计数 + 清空输入 |

#### API 调用

```json
{
  "endpoint": "POST /api/verify-code",
  "request": {
    "codeId": "from previous step",
    "code": "string (4 digits)"
  },
  "success": {
    "action": "store",
    "data": "resetToken",
    "then": "navigate",
    "target": "/reset-password"
  },
  "error": {
    "action": "incrementAttemptCount",
    "onMaxReached": {
      "showModal": "maximumAttempts",
      "then": "navigate",
      "target": "/login"
    }
  }
}
```

---

### 6.7 ResetPasswordPage (重置密码)

**路由**: `/reset-password`
**认证要求**: Guest Only

#### 表单字段

```json
{
  "fields": [
    {
      "name": "newPassword",
      "type": "password",
      "label": "New Password",
      "placeholder": "Enter new password",
      "required": true,
      "minLength": 8,
      "showPasswordToggle": true
    },
    {
      "name": "confirmPassword",
      "type": "password",
      "label": "Confirm Password",
      "placeholder": "Confirm new password",
      "required": true,
      "minLength": 8,
      "showPasswordToggle": true,
      "validation": {
        "match": "newPassword",
        "message": "Passwords do not match"
      }
    }
  ]
}
```

#### 操作按钮

| 按钮 | 位置 | 行为 |
|------|------|------|
| Cancel | 左上角 | 导航到 `/login` |
| Save | 底部 | 提交表单，成功后导航到 `/login` |

#### API 调用

```json
{
  "endpoint": "POST /api/reset-password",
  "request": {
    "resetToken": "from previous step",
    "newPassword": "string"
  },
  "success": {
    "action": "navigate",
    "target": "/login"
  }
}
```

---

### 6.8 OnboardingPage (引导轮播)

**路由**: `/onboarding`
**认证要求**: Auth Required

#### 页面结构

```
┌─────────────────────────┐
│     Status Bar          │
├─────────────────────────┤
│                         │
│     [Illustration]      │
│                         │
│     Title Text          │
│   Description text...   │
│                         │
│  ○  ○  ●  ○            │  ← Dots 指示器
│                         │
│      Let's Go           │  ← 仅第 4 页显示
│                         │
└─────────────────────────┘
```

#### 配置

```json
{
  "pages": [
    {
      "index": 0,
      "title": "Welcome to Shoppe",
      "description": "Discover the latest fashion trends",
      "illustration": "onboarding-1.svg"
    },
    {
      "index": 1,
      "title": "Personalized Picks",
      "description": "Get recommendations just for you",
      "illustration": "onboarding-2.svg"
    },
    {
      "index": 2,
      "title": "Easy Shopping",
      "description": "Shop with just a few taps",
      "illustration": "onboarding-3.svg"
    },
    {
      "index": 3,
      "title": "Let's Get Started",
      "description": "Your style journey begins here",
      "illustration": "onboarding-4.svg",
      "showButton": true,
      "buttonText": "Let's Go"
    }
  ],
  "swipe": {
    "threshold": 50,
    "direction": "horizontal"
  },
  "dots": {
    "clickable": true,
    "activeStyle": "filled",
    "inactiveStyle": "outlined"
  },
  "persistence": {
    "key": "onboardingCompleted",
    "storage": "localStorage"
  }
}
```

#### 状态流转

```json
{
  "on": {
    "SWIPE_LEFT": { "action": "nextPage" },
    "SWIPE_RIGHT": { "action": "prevPage" },
    "DOT_CLICK": { "action": "goToPage" },
    "BUTTON_CLICK": {
      "action": ["markCompleted", "navigate"],
      "target": "/shop"
    }
  }
}
```

---

### 6.9 ShopPage (商城主页)

**路由**: `/shop`
**认证要求**: Auth Required
**Tab**: Home

#### 页面结构

```
┌─────────────────────────┐
│     Status Bar          │ (固定)
├─────────────────────────┤
│  Shop              🔍   │
├─────────────────────────┤
│  ┌─────────────────┐   │
│  │  Big Sale       │   │ ← 轮播 (4 组)
│  │  Banner         │   │
│  └─────────────────┘   │
├─────────────────────────┤
│  Categories      SeeAll │
│  ┌─────┐ ┌─────┐       │
│  │     │ │     │       │ ← 2 列网格
│  └─────┘ └─────┘       │
├─────────────────────────┤
│  Top Products           │
│  ○ ○ ○ ○ ○             │ ← 5 个横向排列
├─────────────────────────┤
│  New Items       SeeAll │
│  ┌───┬───┬───┐         │ ← 3 列网格
│  └───┴───┴───┘         │
├─────────────────────────┤
│  Flash Sale    ⏱️      │
│  ┌───┬───┬───┐         │ ← 3 列网格，带倒计时
│  └───┴───┴───┘         │
├─────────────────────────┤
│  Most Popular   SeeAll │
│  ┌─────┬─────┐         │ ← 2 列网格
│  └─────┴─────┘         │
├─────────────────────────┤
│  Just For You   SeeAll │
│  ┌─────┬─────┐         │ ← 2 列网格
│  └─────┴─────┘         │
├─────────────────────────┤
│  🏠   🛒   📂   ❤️   👤 │ ← Bottom Nav
├─────────────────────────┤
│     Home Indicator      │
└─────────────────────────┘
```

#### 内容区块配置

```json
{
  "sections": [
    {
      "id": "banner",
      "type": "carousel",
      "title": null,
      "itemCount": 4,
      "autoPlay": { "enabled": true, "interval": 3000 },
      "indicators": { "type": "dots", "clickable": true }
    },
    {
      "id": "categories",
      "type": "grid",
      "title": "Categories",
      "columns": 2,
      "showSeeAll": true,
      "itemCount": 8,
      "clickAction": { "navigate": "/category/:id" }
    },
    {
      "id": "topProducts",
      "type": "horizontal-list",
      "title": "Top Products",
      "itemCount": 5,
      "showDots": false
    },
    {
      "id": "newItems",
      "type": "grid",
      "title": "New Items",
      "columns": 3,
      "showSeeAll": true,
      "clickAction": { "navigate": "/product/:id" }
    },
    {
      "id": "flashSale",
      "type": "grid",
      "title": "Flash Sale",
      "columns": 3,
      "countdown": { "enabled": true, "format": "MM:SS:ms" },
      "clickAction": { "navigate": "/product/:id" }
    },
    {
      "id": "mostPopular",
      "type": "grid",
      "title": "Most Popular",
      "columns": 2,
      "showSeeAll": true,
      "itemBadges": ["new", "sale", "hot"],
      "clickAction": { "navigate": "/product/:id" }
    },
    {
      "id": "justForYou",
      "type": "grid",
      "title": "Just For You",
      "columns": 2,
      "showSeeAll": true,
      "showRating": true,
      "clickAction": { "navigate": "/product/:id" }
    }
  ]
}
```

#### Bottom Navigation

```json
{
  "bottomNavigation": {
    "height": 84,
    "tabs": [
      { "id": "home", "icon": "home.svg", "label": "Home", "route": "/shop" },
      { "id": "cart", "icon": "shopping-bag.svg", "label": "Cart", "route": "/cart" },
      { "id": "categories", "icon": "menu.svg", "label": "Categories", "route": "/categories" },
      { "id": "wishlist", "icon": "heart.svg", "label": "Wishlist", "route": "/wishlist" },
      { "id": "profile", "icon": "profile.svg", "label": "Profile", "route": "/profile" }
    ],
    "activeState": {
      "iconTint": "#004bff",
      "labelColor": "#004bff"
    },
    "inactiveState": {
      "iconTint": "#202020",
      "labelColor": "#202020"
    }
  }
}
```

---

### 6.10 ProductDetailPage (商品详情页)

**路由**: `/product/:id`
**认证要求**: Auth Required

#### 页面结构

```
┌─────────────────────────┐
│     Status Bar          │ (固定，z-index: 1000)
├─────────────────────────┤
│  ┌─────────────────┐   │
│  │                 │   │
│  │  Image Gallery  │   │ ← 可滚动
│  │  (Swipeable)    │   │
│  │                 │   │
│  │    ○●○○○        │   │ ← 圆点指示器
│  └─────────────────┘   │
├─────────────────────────┤
│  $17,00                 │
│  Product description... │
├─────────────────────────┤
│  Variations             │
│  [Pink] [M]   [Expand] │
├─────────────────────────┤
│  Specifications         │
│  Material: [Cotton 95%] │
│  Origin:   [EU]         │
│  Size guide [→]         │
├─────────────────────────┤
│  Delivery               │
│  Standart  $3,00  5-7d │
│  Express   $12,00  1-2d│
├─────────────────────────┤
│  Rating & Reviews       │
│  ★★★★☆ 4/5            │
│  [Avatar] Veronika...  │
│  [View All Reviews]     │
├─────────────────────────┤
│  Most Popular →         │
│  [横向滚动商品列表]      │
├─────────────────────────┤
│  You Might Like         │
│  [2 列网格商品]          │
├─────────────────────────┤
│  [♡] [Add to Cart] [Buy]│ ← BottomBar (固定)
└─────────────────────────┘
```

#### 组件配置

```json
{
  "components": [
    {
      "name": "ProductGallery",
      "type": "carousel",
      "properties": {
        "images": "array of URIs",
        "autoPlay": { "enabled": true, "interval": 3000 },
        "indicators": {
          "type": "dots",
          "activeShape": "elongated",
          "activeLength": 40,
          "inactiveShape": "circle",
          "inactiveDiameter": 10,
          "activeColor": "#004bff",
          "inactiveColor": "#e0e0e0"
        },
        "shareButton": {
          "visible": true,
          "position": "top-right"
        },
        "gestures": {
          "swipe": true,
          "drag": true,
          "tapIndicator": true
        }
      }
    },
    {
      "name": "BottomBar",
      "type": "fixed-action-bar",
      "position": "bottom",
      "height": 84,
      "zIndex": 100,
      "buttons": [
        {
          "id": "wishlist",
          "icon": "heart.svg",
          "width": 48,
          "backgroundColor": "#f5f5f5"
        },
        {
          "id": "addToCart",
          "label": "Add to Cart",
          "flex": 1,
          "backgroundColor": "#202020",
          "textColor": "#ffffff"
        },
        {
          "id": "buyNow",
          "label": "Buy Now",
          "flex": 1.5,
          "backgroundColor": "#004bff",
          "textColor": "#ffffff"
        }
      ]
    },
    {
      "name": "SKUSelector",
      "type": "bottom-sheet",
      "trigger": "expand button in Variations",
      "zIndex": 99,
      "maxHeight": "calc(100vh - 84px)",
      "sections": [
        {
          "id": "color",
          "title": "Color",
          "type": "image-grid",
          "selectionMode": "single"
        },
        {
          "id": "size",
          "title": "Size",
          "type": "button-grid",
          "options": ["S", "M", "L", "XL", "XXL", "XXXL"],
          "disabledOptions": ["XXL", "XXXL"],
          "selectionMode": "single"
        },
        {
          "id": "quantity",
          "title": "Quantity",
          "type": "stepper",
          "min": 1,
          "max": 99,
          "initial": 1
        }
      ],
      "closeMethods": ["close-button", "overlay-tap"],
      "confirmBehavior": "real-time-update"
    }
  ]
}
```

#### 布局约束

```json
{
  "layout": {
    "container": {
      "type": "flex",
      "direction": "column",
      "height": "100vh"
    },
    "scrollableArea": {
      "flex": 1,
      "overflowY": "auto",
      "paddingBottom": "100px"
    },
    "fixedElements": [
      {
        "element": "StatusBar",
        "position": "absolute",
        "top": 0,
        "zIndex": 1000
      },
      {
        "element": "BottomBar",
        "position": "absolute",
        "bottom": 0,
        "zIndex": 100
      }
    ]
  }
}
```

---

## 7. 业务规则 (Business Rules)

### 7.1 认证规则

```json
{
  "authentication": {
    "passwordPolicy": {
      "minLength": 8,
      "requireUppercase": false,
      "requireNumber": false,
      "requireSpecial": false
    },
    "loginAttempts": {
      "maxAttempts": 5,
      "lockoutDuration": 900
    },
    "recoveryCode": {
      "length": 4,
      "type": "numeric",
      "expiresIn": 300,
      "maxAttempts": 3,
      "onMaxReached": "showModalAndRedirectToLogin"
    },
    "tokenPolicy": {
      "accessTokenExpiry": 900,
      "refreshTokenExpiry": 604800,
      "refreshEnabled": true
    }
  }
}
```

### 7.2 商品展示规则

```json
{
  "productDisplay": {
    "priceFormat": {
      "currency": "USD",
      "symbol": "$",
      "decimalSeparator": ",",
      "thousandsSeparator": "",
      "decimalPlaces": 2
    },
    "imageAspectRatio": {
      "productCard": "1:1",
      "detailMain": "3:4",
      "thumbnail": "1:1"
    },
    "badgeRules": {
      "new": {
        "condition": "createdAt < now - 7 days",
        "gradient": "linear-gradient(135deg, #ff9a9e, #fecfef)"
      },
      "sale": {
        "condition": "originalPrice > price",
        "gradient": "linear-gradient(135deg, #ff5790, #f81140)",
        "discountCalculation": "((originalPrice - price) / originalPrice * 100).toFixed(0) + '%'"
      },
      "hot": {
        "condition": "salesCount > 1000",
        "gradient": "linear-gradient(135deg, #ff6b6b, #ee5a5a)"
      }
    }
  }
}
```

### 7.3 购物车规则

```json
{
  "cart": {
    "itemRules": {
      "minQuantity": 1,
      "maxQuantity": 99,
      "uniqueVariation": true
    },
    "priceCalculation": {
      "subtotal": "sum(items.price * items.quantity)",
      "shipping": "selectedDeliveryOption.price",
      "total": "subtotal + shipping"
    },
    "deliveryOptions": [
      {
        "id": "standart",
        "name": "Standart",
        "price": 3.00,
        "estimatedDays": "5-7"
      },
      {
        "id": "express",
        "name": "Express",
        "price": 12.00,
        "estimatedDays": "1-2"
      }
    ]
  }
}
```

---

## 8. 错误处理 (Error Handling)

### 8.1 错误码定义

```json
{
  "errorCodes": {
    "AUTH": {
      "001": { "message": "Invalid email format", "httpStatus": 400 },
      "002": { "message": "Account not found", "httpStatus": 404 },
      "003": { "message": "Invalid password", "httpStatus": 401 },
      "004": { "message": "Token expired", "httpStatus": 401 },
      "005": { "message": "Invalid token", "httpStatus": 401 },
      "006": { "message": "Maximum attempts exceeded", "httpStatus": 429 }
    },
    "PRODUCT": {
      "001": { "message": "Product not found", "httpStatus": 404 },
      "002": { "message": "Out of stock", "httpStatus": 400 }
    },
    "CART": {
      "001": { "message": "Cart not found", "httpStatus": 404 },
      "002": { "message": "Invalid quantity", "httpStatus": 400 }
    },
    "SERVER": {
      "001": { "message": "Internal server error", "httpStatus": 500 },
      "002": { "message": "Service unavailable", "httpStatus": 503 }
    }
  }
}
```

### 8.2 错误显示策略

```json
{
  "errorDisplay": {
    "inline": {
      "适用场景": "表单验证错误",
      "显示位置": "字段下方",
      "持续时间": "直到用户修正"
    },
    "toast": {
      "适用场景": "操作反馈",
      "显示位置": "底部",
      "持续时间": "3 秒"
    },
    "modal": {
      "适用场景": "严重错误/需要用户确认",
      "显示位置": "屏幕中央",
      "操作": "必须用户点击关闭"
    },
    "fullScreen": {
      "适用场景": "网络错误/空状态",
      "显示位置": "占位整个内容区",
      "操作": "提供重试按钮"
    }
  }
}
```

---

## 9. 附录：平台特定映射

### 9.1 Web 前端 (Vue 3 + Tailwind)

```json
{
  "platformMapping": {
    "web": {
      "framework": "Vue 3",
      "styleSystem": "Tailwind CSS",
      "stateManagement": "Pinia",
      "routing": "Vue Router",
      "httpClient": "Axios",
      "componentMapping": {
        "StatusBar": "components/StatusBar.vue",
        "BottomBar": "components/BottomBar.vue",
        "ProductGallery": "components/ProductGallery.vue"
      }
    }
  }
}
```

### 9.2 Android (Kotlin + Jetpack Compose)

```json
{
  "platformMapping": {
    "android": {
      "framework": "Jetpack Compose",
      "styleSystem": "Material 3 Theme",
      "stateManagement": "ViewModel + StateFlow",
      "routing": "Navigation Compose",
      "httpClient": "Retrofit + OkHttp",
      "componentMapping": {
        "StatusBar": "SystemUiController",
        "BottomBar": "BottomNavigation",
        "ProductGallery": "HorizontalPager"
      }
    }
  }
}
```

### 9.3 iOS (SwiftUI)

```json
{
  "platformMapping": {
    "ios": {
      "framework": "SwiftUI",
      "styleSystem": "SF Symbols + Custom Design Tokens",
      "stateManagement": "@State + @ObservedObject",
      "routing": "NavigationStack",
      "httpClient": "URLSession + async/await",
      "componentMapping": {
        "StatusBar": "StatusBarManager",
        "BottomBar": "TabView / Custom BottomBar",
        "ProductGallery": "TabView + PageTabViewStyle"
      }
    }
  }
}
```

### 9.4 后端 (Node.js + Express)

```json
{
  "platformMapping": {
    "backend": {
      "framework": "Express",
      "apiStyle": "RESTful",
      "authMiddleware": "JWT + httpOnly cookies",
      "database": "Any SQL/NoSQL",
      "orm": "Any ORM"
    }
  }
}
```

---

## 10. 变更记录

| 版本 | 日期 | 变更内容 |
|------|------|----------|
| 5.0 | 2026-04-01 | **重构为平台无关通用规格**：新增 Design Token JSON、OpenAPI 3.0 规格、JSON Schema 数据模型、状态机定义、平台映射表 |
| 4.0 | 2026-03-23 | 基于已实现代码反向生成，整合 Vue 3 技术栈细节 |
| 3.3 | 2026-03-23 | 商品详情页完整实现 |

---

## 11. 使用指南

### 11.1 前端代码生成

```bash
# 从 PRD 生成 Web 代码
npx @skills/create-specification --target=vue3 --input=prd.md --output=frontend/

# 从 PRD 生成 Android 代码
npx @skills/create-specification --target=compose --input=prd.md --output=android/

# 从 PRD 生成 iOS 代码
npx @skills/create-specification --target=swiftui --input=prd.md --output=ios/
```

### 11.2 后端代码生成

```bash
# 从 OpenAPI 生成 Node.js 服务端
npx @skills/create-specification --target=express --input=prd.md --output=backend/
```

### 11.3 Figma 集成

```bash
# 从 Figma 导出 Design Token
npx @figma/mcp export-tokens --fileKey=xxx --output=design-tokens.json
```
