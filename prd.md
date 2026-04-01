# 产品需求文档（PRD）- Shoppe

**版本**: 3.0
**最后更新**: 2026-04-01
**项目**: Shoppe 移动端电商应用

---

## 产品概述

Shoppe 是一个移动端电商应用，提供完整的用户认证流程和购物体验。

### 设计规范

- **画布尺寸**: 375px × 817px（iPhone 尺寸）
- **字体**:
  - Raleway: 标题字体（Bold）
  - Nunito Sans: 正文/辅助文本（Light）
  - Poppins: 表单输入（Medium）
- **设计 Token**:
  - `text-brand-black`: #202020（主要文字）
  - 蓝色按钮背景
  - 表单输入背景 #f8f8f8
  - 按钮圆角样式

---

## 页面对照表

| 页面名称 | 路由 | 状态 |
|---------|------|------|
| StartPage | `/` | ✅ |
| CreateAccount | `/create-account` | ✅ |
| LoginPage | `/login` | ✅ |
| Password | `/password` | ✅ |
| PasswordRecovery | `/password-recovery` | ✅ |
| PasswordRecoveryCode | `/password-recovery-code` | ✅ |
| NewPassword | `/new-password` | ✅ |
| HelloCard | `/hello-card` | ✅ |
| ShopPage | `/shop` | ✅ |
| ProductDetail | `/product/:id` | ✅ |

---

## 功能模块

### 模块 1：用户认证流程 ✅ 已完成
- 启动页面、创建账户、登录
- 密码输入（4 位 → 8 位 → 错误处理）
- 密码恢复流程（SMS/Email → 验证码 → 新密码）
- Hello Card 引导轮播（4 页）
- Maximum Attempts 弹窗
- 后端认证服务（9 个 API 端点）

### 模块 2：主应用界面 ✅ Shop 页面完成 ⏳ 商品详情页进行中
- 主页（商品列表）✅
- 商品详情页 ✅（Phase 1-3 组件完成）
- 购物车 ⏳
- 分类浏览 ⏳
- 个人中心 ⏳

### 模块 3：通用组件库 ✅ 已完成
- StatusBar、HomeIndicator、FormInput、PasswordInput
- PrimaryButton、NextButton、SecondaryAction、MaximumAttemptsPopup

### 模块 4：商品详情页组件 ✅ 已完成
- ProductGallery - 图片轮播组件
- ProductInfo - 商品信息组件
- BottomBar - 底部操作栏组件
- FlashSaleBanner - 促销 Banner 组件
- SKUSelector - SKU 选择器组件
- ReviewsSection - 评价模块组件
- ProductDetails - 商品详情组件

---

## 功能详情

### 1. StartPage（启动页面）
- 展示 Shoppe 品牌 Logo 和标语
- "Let's get started" → 创建账户
- "I already have an account" → 登录

### 2. CreateAccount（创建账户）
- 上传头像（UI 占位）、邮箱、密码、电话号码
- 密码支持显示/隐藏切换
- 国家代码选择器（默认英国 +44）
- 表单验证：邮箱格式、密码≥8 位
- Done → 跳转登录页；Cancel → 返回启动页

### 3. LoginPage（登录页面）
- 邮箱输入，格式验证
- 错误提示："Please enter your email" / "Please enter a valid email address" / "Account not found"
- Next → 密码页面；Cancel → 返回启动页
- 测试账号：`deanwen@gmail.com`

### 4. Password（密码输入页面）
- **4-digit 状态**: 4 个空框占位，输入任意字符后切换到 8 位模式
- **8-digit 状态**: 8 个密码点（蓝色实心=已输入，灰色空心=未输入），输满 8 位后自动验证
- **error 状态**: 8 个红色实心点，显示 "Forgot your password?" 链接
- 正确密码：`12345678` → HelloCard
- "Not you?" → 返回登录页（仅非错误状态）
- 错误状态下退格 → 红点变蓝点，可重新输入

### 5. PasswordRecovery（密码恢复）
- SMS（默认选中，蓝色背景+check 图标）/ Email 选项
- 点击选项切换选中状态
- Next → 验证码输入；Cancel → 返回登录页

### 6. PasswordRecoveryCode（验证码输入）
- 4 位验证码输入，自动聚焦
- 正确验证码：`1234`
- 错误 → 计数 +1，清空输入；3 次错误 → Maximum Attempts 弹窗
- Send Again → 重置计数 + 清空输入
- 正确 → NewPassword

### 7. NewPassword（设置新密码）
- 两个密码框（新密码 + 重复密码），支持显示/隐藏切换
- Save → 保存并跳转登录页；Cancel → 返回登录页

### 8. HelloCard（引导轮播）
- 4 页引导内容，支持滑动切换（阈值 50px）
- Dots 指示器（4 个，实心=当前页，空心=其他页），点击可跳转
- 第 4 页显示 "Let's Go" 按钮 → 主应用
- localStorage 持久化引导完成状态

### 9. Maximum Attempts Popup（最大尝试次数弹窗）
- 验证码错误 3 次后显示
- 半透明黑色遮罩 + 白色圆角弹窗
- 顶部警告图标（粉色圆形 + 感叹号）
- Okay → 关闭弹窗并返回登录页

---

## 状态流转

```
StartPage
  ├── Let's get started → CreateAccount → Done → Password
  └── I already have an account → LoginPage → Next → Password
                                        ↓
                              Password (4 位→8 位)
                                        ↓
                              ┌─────────┴─────────┐
                              ↓                   ↓
                        正确 (12345678)       错误
                              ↓                   ↓
                        HelloCard          Password (error)
                              ↓                   ↓
                        Let's Go → 主应用    Forgot password?
                                                  ↓
                                         PasswordRecovery
                                                  ↓
                                         PasswordRecoveryCode
                                                  ├── 正确 → NewPassword
                                                  └── 错误 3 次 → MaximumAttempts
```

---

## 数据验证规则

| 页面 | 字段 | 规则 | 错误提示 |
|------|------|------|----------|
| LoginPage | Email | 必填，格式验证，账号存在检查 | "Please enter your email" / "Please enter a valid email address" / "Account not found" |
| CreateAccount | Email | 必填，格式验证 | "Please enter your email" / "Please enter a valid email address" |
| CreateAccount | Password | 必填，≥8 位 | "Please enter a password" / "Password must be at least 8 characters" |
| Password | 密码 | 8 位，固定值 `12345678` | 错误状态 |
| PasswordRecoveryCode | 验证码 | 4 位，固定值 `1234` | 计数 +1，3 次后弹窗 |

---

## 待实现功能

- [ ] 主应用框架（底部导航栏、顶部导航）
- [x] 商品详情页（Phase 1-3 完成）
- [ ] 商品列表页、购物车页
- [ ] 个人中心、订单页面
- [ ] Pinia 商品状态管理、购物车逻辑
- [ ] 商品数据 API 集成
- [ ] 动效实现、暗色模式

---

## 变更记录

| 版本 | 日期 | 变更内容 |
|------|------|----------|
| 3.1 | 2026-04-01 | 资源下载完成：执行 download-product-extras.js 下载 13 个额外资源 (yl-1~4.png, mp-1/3/4.png, review-avatar-1/2.png, star 图标)；跳转功能验证：ShopPage handleProductClick 已实现 |
| 3.0 | 2026-04-01 | 商品详情页组件完成（Phase 1-3）：ProductDetail、ProductGallery、ProductInfo、BottomBar、FlashSaleBanner、SKUSelector、ReviewsSection、ProductDetails；Figma 资源下载（19 个文件）；路由配置添加 `/product/:id` |
| 2.9 | 2026-03-23 | 后端认证服务完成（9 个 API 端点测试通过）；后端端口从 5000 改为 9000；后端目录重构为 `Shoppe/backend/ShoppeAPI` |
| 2.8 | 2026-03-23 | 修复 HelloCard 圆点显示问题（将 HelloCard.vue 中的 dots 指示器从字符串路径改为 Vite `import` 语法导入） |
| 2.7 | 2026-03-23 | 修复蓝点和红点显示问题（Password.vue 和 PasswordRecoveryCode.vue 都改为使用 Vite `import` 语法导入 SVG 文件） |
| 2.6 | 2026-03-23 | 创建 blue-dot.svg 和 red-dot.svg 本地文件，替换 Blob URL |
| 2.5 | 2026-03-23 | 修复 Figma 资源文件扩展名问题，创建缺失的 Keyboard 图标占位符 |
| 2.4 | 2026-03-23 | 下载所有 Figma 资源到本地，移除对 Figma CDN 的依赖 |
| 2.3 | 2026-03-23 | 修复所有页面水平滚动问题，优化 Password 页面退格动画延迟 |
| 2.2 | 2026-03-20 | 合并 PasswordTyping 和 WrongPassword 到 Password 页面 |
| 2.1 | 2026-03-20 | 清理技术实现细节，转化为纯产品交互文档 |
| 1.0 | 2026-03-20 | 初始版本 |
