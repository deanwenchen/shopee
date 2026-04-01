# Shoppe 项目发现与分析

## 技术栈分析

### 核心技术
- **框架**: Vue 3 + Composition API (`<script setup>`)
- **语言**: TypeScript
- **构建工具**: Vite
- **样式**: Tailwind CSS
- **路由**: Vue Router
- **状态管理**: Pinia（待使用）

### 项目结构
```
frontend/
├── src/
│   ├── assets/          # 静态资源
│   │   ├── icons/       # SVG 图标
│   │   └── images/      # 图片资源
│   ├── components/      # 可复用组件
│   │   ├── StatusBar.vue
│   │   ├── HomeIndicator.vue
│   │   ├── FormInput.vue
│   │   ├── PasswordInput.vue
│   │   ├── PrimaryButton.vue
│   │   ├── SecondaryAction.vue
│   │   ├── NextButton.vue
│   │   └── Keyboard.vue (未使用)
│   ├── router/          # 路由配置
│   ├── views/           # 页面组件
│   │   ├── StartPage.vue
│   │   ├── LoginPage.vue
│   │   ├── CreateAccount.vue
│   │   ├── Password.vue
│   │   ├── PasswordTyping.vue
│   │   └── WrongPassword.vue
│   ├── App.vue
│   └── main.ts
└── ...
```

## 设计系统分析

### 字体
- **Raleway**: 标题字体（Bold）
- **Nunito Sans**: 正文字体（Light）
- **Poppins**: 表单字体（Medium）

### 颜色 Token
- `text-brand-black`: #202020 (主要文字)
- `bg-brand-blue`: 蓝色按钮背景
- `bg-[#f8f8f8]`: 表单输入背景
- `bg-[#ffb6c9]`: 头像边框

### 布局规范
- 画布尺寸：375px × 817px (移动端)
- 状态栏高度：44px
- Home Indicator：底部 24px

## 页面导航流程

```
┌─────────────┐
│ StartPage   │
│     /       │
└──────┬──────┘
       │
   ┌───┴───┐
   ▼       ▼
┌─────────┐ ┌─────────────┐
│ Create  │ │   Login     │
│ Account │ │   Page      │
└────┬────┘ └──────┬──────┘
     │             │
     │             ▼
     │      ┌─────────────┐
     │      │  Password   │
     │      │  (4 位输入)  │
     │      └──────┬──────┘
     │             │
     └─────────────┘
           │
           ▼
    ┌──────────────┐
    │ Password     │
    │ Typing       │
    │ (8 位蓝点)    │
    └──────┬───────┘
           │
           ▼
    ┌──────────────┐
    │ Wrong        │
    │ Password     │
    │ (8 位红点)    │
    └──────┬───────┘
           │
           ▼
    ┌──────────────┐
    │   Login      │
    │   (返回)     │
    └──────────────┘
```

## 关键发现

### 1. 输入框实现
- 使用隐藏 `<input>` 元素唤起系统键盘
- `opacity-0` + `autofocus` + `focus()` 确保键盘自动弹出
- 密码点使用绝对定位精确对齐（间隔 29px）

### 2. 页面跳转时机
- **Password 页面**: 输入第 1 个字符立即跳转
- **PasswordTyping 页面**: 输入满 8 位跳转
- **WrongPassword 页面**: 输入满 8 位返回登录

### 3. 参数传递
- 使用 `route.query.password` 传递密码值
- 实现跨页面状态保持（密码点填充状态）

### 4. 商品详情页布局策略（2026-04-01）
- **问题**: 大屏幕下 BottomBar 随内容下移，超出可视区域
- **根本原因**: `position: fixed` 元素在内容超过视口时会相对于文档底部定位
- **解决方案**:
  - `html, body` 使用 `overflow: hidden` 阻止 body 滚动
  - `#app` 使用 `height: 100%` 固定为视口高度
  - `.product-detail-page` 使用 `height: 100vh` 固定高度
  - `.scroll-container` 使用 `flex: 1` 和 `overflow-y: auto` 实现内部滚动
  - BottomBar 改为 `position: absolute; bottom: 0` 固定在容器底部
  - Bottom Sheet 改为 `position: absolute` 相对于容器定位
  - `.scroll-container` 添加 `padding-bottom: 100px` 确保内容可以滚动到不被按钮遮挡的位置
  - `.bottom-sheet-content` 添加 `padding-bottom: calc(24px + 84px)` 避免被 BottomBar 遮挡
- **层级关系**:
  - BottomBar: `z-index: 100`（最上层）
  - Bottom Sheet Overlay: `z-index: 99`（低于 BottomBar）

### 5. SKU 选择器交互流程
- 用户点击 Variations 展开按钮 → Bottom Sheet 弹出
- 实时选择颜色/尺寸/数量（无需 Apply 按钮）
- 点击遮罩或关闭按钮关闭弹窗
- 关闭弹窗后点击 BottomBar 的 Add to Cart / Buy Now 提交订单

## 待确认事项

### 1. Figma 设计稿待实现节点
- 主应用界面
- 商品列表/详情页
- 购物车页面
- 个人中心页面

### 2. 组件待优化
- Keyboard.vue 和 KeyboardKey.vue 未使用，考虑删除
- PasswordInput.vue 需要验证显示/隐藏切换功能

### 3. 功能待补充
- 表单验证逻辑
- API 调用集成
- Pinia 状态管理
- 密码重置流程

## 设计还原度检查清单

- [x] 布局结构（Flex/Grid）
- [x] 间距（padding/margin/gap）
- [x] 字体大小/粗细/行高
- [x] 颜色（使用 Design Token）
- [x] 圆角/阴影/边框
- [x] 组件层级
- [x] 交互状态（hover/active）
