# 产品需求文档（PRD）- Shoppe

**版本**: 1.0
**最后更新**: 2026-03-20
**项目**: Shoppe 移动端电商 UI 应用

---

## 产品概述

Shoppe 是一个基于 Vue 3 的移动端电商应用前端，从 Figma 设计稿 1:1 还原生成。项目采用现代前端技术栈，实现完整的用户认证流程和购物相关页面。

### 技术栈

| 类别 | 技术 |
|------|------|
| 框架 | Vue 3.5.30 + Composition API (`<script setup>`) |
| 语言 | TypeScript 5.9.3 |
| 构建工具 | Vite 6.2.0 |
| 样式 | Tailwind CSS 3.4.17 |
| 路由 | Vue Router 5.0.4 |
| 状态管理 | Pinia 3.0.4（已安装，待使用） |

### 设计规范

- **画布尺寸**: 375px × 817px（移动端 iPhone 尺寸）
- **字体**:
  - Raleway: 标题字体（Bold）
  - Nunito Sans: 正文/辅助文本（Light）
  - Poppins: 表单输入（Medium）
- **设计 Token**:
  - `text-brand-black`: #202020（主要文字）
  - `bg-brand-blue`: 蓝色按钮背景
  - `bg-[#f8f8f8]`: 表单输入背景
  - `rounded-shoppe-btn`: 按钮圆角
  - `rounded-shoppe-xl`: 大圆角

---

## 功能模块列表

### 模块 1：用户认证流程 ✅ 已完成
- 启动页面（品牌展示）
- 创建账户
- 登录
- 密码输入（4 位）
- 密码输入（8 位）
- 密码错误处理
- Hello Card 引导轮播（4 页）
- Maximum Attempts 弹窗

### 模块 2：主应用界面 ⏳ 待实现
- 主页（商品列表）
- 商品详情
- 购物车
- 分类浏览
- 个人中心

### 模块 3：通用组件库 ✅ 已完成
- StatusBar - 状态栏
- HomeIndicator - 底部 Home 指示器
- FormInput - 表单输入框
- PasswordInput - 密码输入框（带显示/隐藏）
- PrimaryButton - 主按钮
- SecondaryAction - 次要操作按钮
- NextButton - Next 按钮

---

## 功能详情

### 1. StartPage（启动页面）

#### 🎯 功能目标
作为应用入口，展示品牌形象，引导用户进行登录或注册。

#### 📝 功能描述
- 展示 Shoppe 品牌 Logo 和标语
- 提供两个操作选项：
  - "Let's get started" - 新用户注册
  - "I already have an account" - 现有用户登录

#### 📐 业务规则
- 点击主按钮 → 导航至 `/create-account`
- 点击次按钮 → 导航至 `/login`
- 无强制登录要求，用户可自由选择路径

#### 🎨 UI 结构
```
┌─────────────────────────────┐
│      StatusBar (9:41)       │
│                             │
│         [Logo Icon]         │
│                             │
│       Shoppe (标题)          │
│                             │
│  Beautiful eCommerce UI...  │
│                             │
│  ┌─────────────────────┐    │
│  │ Let's get started   │    │
│  └─────────────────────┘    │
│                             │
│    I already have an account│
│                             │
│      [Home Indicator]       │
└─────────────────────────────┘
```

#### ⚙️ 技术实现
- **组件**: `StartPage.vue`
- **路由**: `/` (name: 'start')
- **依赖组件**: StatusBar, PrimaryButton, SecondaryAction, HomeIndicator
- **状态**: 无（纯展示页面）
- **事件处理**:
  ```typescript
  const handleGetStarted = () => router.push('/create-account')
  const handleLogin = () => router.push('/login')
  ```

---

### 2. CreateAccount（创建账户）

#### 🎯 功能目标
允许新用户注册账户，收集基本信息。

#### 📝 功能描述
- 用户上传头像照片（UI 占位）
- 填写邮箱、密码、电话号码
- 支持密码显示/隐藏切换
- 选择国家代码（UI 占位，默认英国 +44）

#### 📐 业务规则
- 点击 Done → 验证表单 → 调用注册 API（TODO）→ 跳转至 `/password-typing`
- 点击 Cancel → 返回 `/`
- 密码输入框支持明文/密文切换
- 电话号码包含国家选择器

**表单验证规则:**
- 邮箱必填，格式验证：`/^[^\s@]+@[^\s@]+\.[^\s@]+$/`
- 密码必填，长度 ≥ 8 位
- 错误提示显示在表单下方

#### 🎨 UI 结构
```
┌─────────────────────────────┐
│      StatusBar              │
│                             │
│   Create                    │
│   Account (大标题)           │
│                             │
│   [📷 上传头像]              │
│                             │
│   ┌───────────────────┐     │
│   │ Email             │     │
│   └───────────────────┘     │
│   ┌───────────────────┐     │
│   │ Password      [👁] │     │
│   └───────────────────┘     │
│   ┌[🇬🇧]│[🇬🇧↓]│+44 你的号码┐   │
│   └───────────────────┘     │
│                             │
│   ┌─────────────────────┐   │
│   │       Done          │   │
│   └─────────────────────┘   │
│                             │
│         Cancel              │
│                             │
│      [Home Indicator]       │
└─────────────────────────────┘
```

#### ⚙️ 技术实现
- **组件**: `CreateAccount.vue`
- **路由**: `/create-account` (name: 'create-account')
- **依赖组件**: StatusBar, FormInput, PasswordInput, NextButton
- **状态管理**:
  ```typescript
  const email = ref('')
  const password = ref('')
  const phoneNumber = ref('')
  const showPassword = ref(false)
  ```
- **事件处理**:
  ```typescript
  const handleDone = () => {
    // TODO: 调用注册 API
    router.push('/password-typing')
  }
  const handleCancel = () => router.push('/')
  ```

---

### 3. LoginPage（登录页面）

#### 🎯 功能目标
允许现有用户通过邮箱登录。

#### 📝 功能描述
- 用户输入邮箱地址
- 点击 Next 进入密码输入流程

#### 📐 业务规则
- 点击 Next → 导航至 `/password`
- 点击 Cancel → 返回 `/`
- 邮箱格式验证（TODO）

#### 🎨 UI 结构
```
┌─────────────────────────────┐
│      StatusBar              │
│       💙 Bubbles BG         │
│                             │
│   Login                     │
│   Good to see you back! 💙  │
│                             │
│   ┌───────────────────┐     │
│   │ Email             │     │
│   └───────────────────┘     │
│                             │
│        ┌─────────┐          │
│        │  Next   │          │
│        └─────────┘          │
│                             │
│         Cancel              │
│                             │
│      [Home Indicator]       │
└─────────────────────────────┘
```

#### ⚙️ 技术实现
- **组件**: `LoginPage.vue`
- **路由**: `/login` (name: 'login')
- **依赖组件**: StatusBar, FormInput, NextButton
- **状态管理**:
  ```typescript
  const email = ref('')
  const showError = ref(false)
  const errorMessage = ref('')
  ```
- **验证规则**:
  ```typescript
  // 邮箱格式验证
  const validateEmail = (email: string): boolean => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
    return emailRegex.test(email)
  }

  const handleNext = () => {
    if (!email.value.trim()) {
      showError.value = true
      errorMessage.value = 'Please enter your email'
      return
    }
    if (!validateEmail(email.value)) {
      showError.value = true
      errorMessage.value = 'Please enter a valid email address'
      return
    }
    // 验证账号是否存在
    if (email.value === 'deanwen@gmail.com') {
      router.push('/password')  // 账号正确
    } else {
      showError.value = true
      errorMessage.value = 'Account not found. Please check your email.'
    }
  }
  ```
- **事件处理**:
  ```typescript
  const handleNext = () => { /* 验证并跳转 */ }
  const handleCancel = () => router.push('/')
  ```

---

### 4. Password（密码输入页面 - 4 位）

#### 🎯 功能目标
用户输入 4 位短密码，系统自动跳转。

#### 📝 功能描述
- 显示问候语 "Hello, Romina!!"
- 提示 "Type your password"
- 4 个空输入框占位
- 隐藏输入框接收系统键盘输入
- 输入任意字符后立即跳转至 PasswordTyping

#### 📐 业务规则
- 输入第 1 个字符 → 立即跳转至 `/password-typing`（携带密码值）
- 点击 "Not you?" → 返回 `/login`
- 输入框仅用于视觉占位，实际输入通过隐藏 input 处理

#### 🎨 UI 结构
```
┌─────────────────────────────┐
│      StatusBar              │
│       💙 Bubbles BG         │
│                             │
│      [Avatar Circle]        │
│                             │
│   Hello, Romina!!           │
│                             │
│   Type your password        │
│                             │
│   ┌───┐ ┌───┐ ┌───┐ ┌───┐  │
│   │   │ │   │ │   │ │   │  │
│   └───┘ └───┘ └───┘ └───┘  │
│                             │
│        Not you? →           │
│                             │
│      [Home Indicator]       │
└─────────────────────────────┘
```

#### ⚙️ 技术实现
- **组件**: `Password.vue`
- **路由**: `/password` (name: 'password')
- **依赖组件**: StatusBar, HomeIndicator
- **状态管理**:
  ```typescript
  const password = ref('')
  watch(password, (newValue) => {
    if (newValue.length >= 1) {
      router.push({ path: '/password-typing', query: { password: newValue } })
    }
  })
  ```
- **UI 结构**: 4 个空 div 占位 + 1 个隐藏 input 接收输入

---

### 5. PasswordTyping（密码输入页面 - 8 位蓝点）

#### 🎯 功能目标
用户输入 8 位密码，动态显示蓝色密码点。

#### 📝 功能描述
- 从 Password 页面接收初始密码值
- 显示 8 个密码点（已输入蓝色实心，未输入灰色空心）
- 系统键盘自动唤起
- 输满 8 位后自动跳转至 WrongPassword

#### 📐 业务规则
- 页面加载时聚焦隐藏输入框 → 唤起系统键盘
- 每个输入字符对应一个蓝色实心点
- 输满 8 位 → 验证密码
  - 密码正确 (`12345678`) → 跳转至 `/hello-card`
  - 密码错误 → 跳转至 `/wrong-password`
- 密码点位置：绝对定位，间隔 29px

#### 🎨 UI 结构
```
┌─────────────────────────────┐
│      StatusBar              │
│       💙 Bubbles BG         │
│                             │
│      [Avatar Circle]        │
│                             │
│   Hello, Romina!!           │
│                             │
│   Type your password        │
│                             │
│   ● ● ● ● ○ ○ ○ ○  (蓝点)   │
│                             │
│      [系统键盘]              │
│                             │
│      [Home Indicator]       │
└─────────────────────────────┘
```

#### ⚙️ 技术实现
- **组件**: `PasswordTyping.vue`
- **路由**: `/password-typing` (name: 'password-typing')
- **依赖组件**: StatusBar, HomeIndicator
- **状态管理**:
  ```typescript
  const password = ref('')
  const inputRef = ref<HTMLInputElement | null>(null)

  onMounted(() => {
    if (route.query.password) {
      password.value = route.query.password as string
    }
    nextTick(() => inputRef.value?.focus())
  })
  ```
- **动态点渲染**: 8 个绝对定位的 div，根据 `password.length` 切换图片

---

### 6. WrongPassword（密码错误页面 - 8 位红点）

#### 🎯 功能目标
模拟密码错误场景，显示红色错误状态。

#### 📝 功能描述
- 从 PasswordTyping 页面接收密码值
- 显示 8 个密码点（已输入红色实心，未输入灰色空心）
- 提供 "Forgot your password?" 链接
- 输满 8 位后返回登录页

#### 📐 业务规则
- 页面加载时聚焦隐藏输入框 → 唤起系统键盘
- 每个输入字符对应一个红色实心点
- 输满 8 位 → 延时 500ms 跳转至 `/login`
- 点击 "Forgot your password?" → TODO: 密码重置流程

#### 🎨 UI 结构
```
┌─────────────────────────────┐
│      StatusBar              │
│       💙 Bubbles BG         │
│                             │
│      [Avatar Circle]        │
│                             │
│   Hello, Romina!!           │
│                             │
│   Type your password        │
│                             │
│   ● ● ● ● ○ ○ ○ ○  (红点)   │
│                             │
│   Forgot your password?     │
│                             │
│      [系统键盘]              │
│                             │
│      [Home Indicator]       │
└─────────────────────────────┘
```

#### ⚙️ 技术实现
- **组件**: `WrongPassword.vue`
- **路由**: `/wrong-password` (name: 'wrong-password')
- **依赖组件**: StatusBar, HomeIndicator
- **状态管理**:
  ```typescript
  const password = ref('')
  const inputRef = ref<HTMLInputElement | null>(null)

  onMounted(() => {
    if (route.query.password) {
      password.value = route.query.password as string
    }
    nextTick(() => inputRef.value?.focus())
  })

  watch(password, (newValue) => {
    if (newValue.length === 8) {
      router.push('/login')
    }
  })
  ```

---

## 通用能力（可复用组件）

### 1. StatusBar
- **用途**: 所有页面顶部状态栏
- **内容**: 时间 (9:41)、信号、WiFi、电池图标
- **尺寸**: 375px × 44px

### 2. HomeIndicator
- **用途**: 所有页面底部 Home 指示器
- **尺寸**: 134px × 5px
- **位置**: bottom-[24px]

### 3. FormInput
- **Props**: `placeholder`, `modelValue`, `type`
- **Emits**: `update:modelValue`
- **样式**: 圆角 60px，背景 #f8f8f8

### 4. PasswordInput
- **Props**: `placeholder`, `modelValue`, `showPassword`
- **Emits**: `update:modelValue`, `update:showPassword`
- **功能**: 支持密码显示/隐藏切换

### 5. PrimaryButton / NextButton
- **Props**: `text`, `onClick`
- **Emits**: `click`
- **样式**: 335px × 61px，蓝色背景，白色文字

### 6. SecondaryAction
- **用途**: 次要操作链接（如 "I already have an account"）

---

### 7. HelloCard（欢迎引导轮播）

#### 🎯 功能目标
登录成功后展示的 4 页引导轮播，帮助用户了解应用功能。

#### 📝 功能描述
- 4 页引导内容，支持左右滑动切换
- 底部 dots 指示器，显示当前页码
- 点击 dots 可直接跳转对应页面
- 最后一页显示 "Let's Go" 按钮，进入主应用

#### 📐 业务规则
- 密码正确 (`12345678`) → 自动跳转至 `/hello-card`
- 滑动阈值：50px
  - 向右滑动 → 上一页
  - 向左滑动 → 下一页
- 点击 dots → 跳转至对应页面
- 第 4 页显示 "Let's Go" 按钮
- 点击 "Let's Go" → 设置 `onboardingCompleted = true` → 跳转至 `/shop`
- 页面加载时检查 `onboardingCompleted`，已完成则直接跳转

#### 🎨 UI 结构
```
┌─────────────────────────────┐
│      StatusBar              │
│       Bubbles BG            │
│                             │
│   ┌───────────────────┐     │
│   │                   │     │
│   │   [Image]         │     │
│   │                   │     │
│   └───────────────────┘     │
│                             │
│        Hello (标题)          │
│                             │
│   Lorem ipsum dolor sit...  │
│   (描述文本)                 │
│                             │
│   ● ○ ○ ○  (dots 指示器)    │
│                             │
│   ┌─────────────────────┐   │
│   │     Let's Go        │   │
│   └─────────────────────┘   │ (仅第 4 页)
│                             │
│      [Home Indicator]       │
└─────────────────────────────┘
```

#### ⚙️ 技术实现
- **组件**: `HelloCard.vue`
- **路由**: `/hello-card` (name: 'hello-card')
- **依赖组件**: StatusBar, HomeIndicator
- **状态管理**:
  ```typescript
  interface GuidePage {
    title: string
    description: string
    image: string
    hasButton?: boolean
  }

  const guides: GuidePage[] = [
    { title: 'Hello', description: '...', image: '...' },
    { title: 'Explore', description: '...', image: '...' },
    { title: 'Choose', description: '...', image: '...' },
    { title: 'Ready', description: '...', hasButton: true },
  ]

  const currentPage = ref(0)
  const touchStartX = ref(0)
  const touchEndX = ref(0)
  const SWIPE_THRESHOLD = 50

  const handleSwipe = () => {
    const deltaX = touchEndX.value - touchStartX.value
    if (deltaX > SWIPE_THRESHOLD && currentPage.value > 0) {
      currentPage.value--
    } else if (deltaX < -SWIPE_THRESHOLD && currentPage.value < guides.length - 1) {
      currentPage.value++
    }
  }

  const handleLetsGo = () => {
    localStorage.setItem('onboardingCompleted', 'true')
    router.push('/shop')
  }
  ```

---

### 8. Maximum Attempts Popup（最大尝试次数弹窗）

#### 🎯 功能目标
密码恢复验证码输入错误达到 3 次时，显示错误提示并阻止继续尝试。

#### 📝 功能描述
- 半透明黑色遮罩层
- 白色圆角弹窗容器
- 顶部警告图标（粉色圆形 + 感叹号）
- 错误提示文本
- "Okay" 按钮关闭弹窗并返回登录页

#### 📐 业务规则
- 验证码错误 → `attemptCount++`
- `attemptCount >= 3` → 显示弹窗
- 点击 "Okay" → 关闭弹窗 → 跳转至 `/login`
- 点击 "Send Again" → 重置计数 → 清空输入

#### 🎨 UI 结构
```
┌─────────────────────────────┐
│      半透明黑色遮罩          │
│                             │
│   ┌───────────────────┐     │
│   │    ⚠️ (警告图标)   │     │
│   │                   │     │
│   │ You reached out   │     │
│   │ maximum amount of │     │
│   │ attempts.         │     │
│   │ Please, try later.│     │
│   │                   │     │
│   │  ┌─────────────┐  │     │
│   │  │    Okay     │  │     │
│   │  └─────────────┘  │     │
│   └───────────────────┘     │
│                             │
└─────────────────────────────┘
```

#### ⚙️ 技术实现
- **集成组件**: `PasswordRecoveryCode.vue`
- **状态管理**:
  ```typescript
  const maxAttempts = 3
  const attemptCount = ref(0)
  const showMaxAttemptsPopup = ref(false)
  const correctCode = '1234'

  watch(code, (newValue) => {
    if (newValue.length === 4) {
      if (newValue !== correctCode) {
        attemptCount.value++
        code.value = ''
        if (attemptCount.value >= maxAttempts) {
          showMaxAttemptsPopup.value = true
        }
      } else {
        router.push('/new-password')
      }
    }
  })

  const handleOkay = () => {
    showMaxAttemptsPopup.value = false
    router.push('/login')
  }
  ```

---

## 待确认事项（TODO）

### 功能待完善
- [ ] **表单验证**: Email 格式、密码强度、手机号格式
- [ ] **API 集成**: 登录/注册接口调用
- [ ] **密码重置流程**: Forgot password 链接目标页面
- [ ] **头像上传**: CreateAccount 页面的头像上传功能

### 状态管理
- [ ] **Pinia Store**: 用户状态、购物车状态
- [ ] **认证持久化**: Token 存储、自动登录

### 待实现页面
- [ ] **主应用框架**: 底部导航栏、顶部导航
- [ ] **商品列表页**: 商品卡片、筛选、排序
- [ ] **商品详情页**: 图片轮播、规格选择、加入购物车
- [ ] **购物车页**: 数量调整、价格计算、结算
- [ ] **个人中心**: 订单列表、设置

### 设计还原
- [ ] **动效实现**: 页面切换动画、按钮点击反馈
- [ ] **暗色模式**: 主题切换支持

---

## 附录：完整导航流程图

```
┌─────────────────────────────────────────────────────────────┐
│                      StartPage (/)                          │
│  ┌─────────────────┐       ┌─────────────────────────┐      │
│  │ Let's get started│──────▶│  CreateAccount          │      │
│  │                 │       │  /create-account        │      │
│  └─────────────────┘       └────────────┬────────────┘      │
│                                         │                   │
│  ┌─────────────────┐       ┌────────────▼────────────┐      │
│  │ I already have  │──────▶│  LoginPage              │      │
│  │ an account      │       │  /login                 │      │
│  └─────────────────┘       └────────────┬────────────┘      │
│                                         │                   │
│                              ┌──────────▼──────────┐        │
│                              │  Password           │        │
│                              │  /password          │        │
│                              │  (4 位输入框)        │        │
│                              └──────────┬──────────┘        │
│                                         │                   │
│                              ┌──────────▼──────────┐        │
│                              │  PasswordTyping     │        │
│                              │  /password-typing   │        │
│                              │  (8 位蓝点)          │        │
│                              └──────────┬──────────┘        │
│                                         │                   │
│                              ┌──────────▼──────────┐        │
│                              │  WrongPassword      │        │
│                              │  /wrong-password    │        │
│                              │  (8 位红点)          │        │
│                              └──────────┬──────────┘        │
│                                         │                   │
│                                         ▼                   │
│                                  ┌─────────────┐            │
│                                  │   返回 Login │            │
│                                  └─────────────┘            │
└─────────────────────────────────────────────────────────────┘
```

---

## 变更记录

| 版本 | 日期 | 变更内容 |
|------|------|----------|
| 1.1 | 2026-03-20 | 添加表单验证逻辑、账号密码验证、Hello Card 轮播、密码恢复流程、Maximum Attempts 弹窗 |
| 1.0 | 2026-03-20 | 初始版本 - 基于已有代码反向整理 |
