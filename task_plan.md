# Shoppe 项目任务规划

## 项目概述
Shoppe 是一个基于 Vue 3 的移动端电商 UI 应用，从 Figma 设计稿生成。

## 已完成页面

### 1. StartPage (`/`)
- **状态**: ✅ 完成
- **功能**: 启动页面，品牌展示
- **导航**:
  - "Let's get started" → `/create-account`
  - "I already have an account" → `/login`
- **组件**: StatusBar, PrimaryButton, SecondaryAction, HomeIndicator

### 2. CreateAccount (`/create-account`)
- **状态**: ✅ 完成
- **功能**: 用户注册页面
- **表单**: Email, Password, Phone Number
- **导航**:
  - Done → `/password-typing`
  - Cancel → `/`
- **组件**: StatusBar, FormInput, PasswordInput, NextButton, HomeIndicator

### 3. LoginPage (`/login`)
- **状态**: ✅ 完成
- **功能**: 用户登录页面
- **表单**: Email 输入
- **导航**:
  - Next → `/password`
  - Cancel → `/`
- **组件**: StatusBar, FormInput, NextButton, HomeIndicator

### 4. Password (`/password`)
- **状态**: ✅ 完成
- **功能**: 密码输入页面（4 位）
- **导航**:
  - 输入任意字符 → `/password-typing`
  - Not you? → `/login`
- **组件**: StatusBar, HomeIndicator

### 5. PasswordTyping (`/password-typing`)
- **状态**: ✅ 完成
- **功能**: 密码输入页面（8 位，蓝色点）
- **导航**:
  - 输入满 8 位 → 验证密码
    - 正确 (`12345678`) → `/hello-card`
    - 错误 → `/wrong-password`
- **组件**: StatusBar, HomeIndicator

### 6. WrongPassword (`/wrong-password`)
- **状态**: ✅ 完成
- **功能**: 密码错误页面（8 位，红色点）
- **导航**:
  - Forgot password? → `/password-recovery`
- **组件**: StatusBar, HomeIndicator

### 7. PasswordRecovery (`/password-recovery`)
- **状态**: ✅ 完成
- **功能**: 密码恢复方式选择（SMS/Email）
- **导航**:
  - Next → `/password-recovery-code`
  - Cancel → `/login`
- **组件**: StatusBar, HomeIndicator

### 8. PasswordRecoveryCode (`/password-recovery-code`)
- **状态**: ✅ 完成
- **功能**: 4 位验证码输入
- **导航**:
  - 输入满 4 位 → 验证
    - 正确 (`1234`) → `/new-password`
    - 错误 3 次 → Maximum Attempts 弹窗 → `/login`
  - Cancel → `/login`
  - Send Again → 重置计数
- **组件**: StatusBar, HomeIndicator, MaximumAttemptsPopup

### 9. NewPassword (`/new-password`)
- **状态**: ✅ 完成
- **功能**: 设置新密码（输入新密码和确认密码）
- **导航**:
  - Save → `/login`
  - Cancel → `/login`
- **组件**: StatusBar, HomeIndicator

### 10. HelloCard (`/hello-card`)
- **状态**: ✅ 完成
- **功能**: 登录成功后的 4 页引导轮播
- **导航**:
  - 滑动/点击 dots → 切换页面
  - "Let's Go" → `/shop` (设置 onboardingCompleted)
- **组件**: StatusBar, HomeIndicator

### 11. ShopPage (`/shop`)
- **状态**: ✅ 完成
- **功能**: 商城主页
- **模块**:
  - 顶部搜索栏
  - 分类导航（Women/Men/Kids）
  - Flash Sale 促销专区（倒计时）
  - Most Popular 热门商品轮播
  - You Might Like 推荐商品网格
- **导航**: 点击商品 → `/product/:id`
- **组件**: StatusBar, HomeIndicator, FlashSaleBanner

### 12. ProductDetail (`/product/:id`)
- **状态**: ✅ 完成
- **功能**: 商品详情页（完整 Figma 设计 node-id: 0:8438）
- **组件**:
  - StatusBar - 顶部状态栏（时间/电池/WiFi/信号）
  - ProductGallery - 图片轮播（5+ 张，圆点指示器，分享按钮，3 秒自动轮播）
  - Price Section - 价格 + 描述
  - Variations - 规格预览（已选芯片 + 缩略图 + 展开按钮）
  - Specifications - 商品规格（Material 标签、Origin 标签、Size guide）
  - Delivery - 配送选项（Standart/Express）
  - ReviewsSection - 评分与评价（评分汇总、用户评价、View All 按钮）
  - MostPopular - 热门商品轮播（4 个商品，New/Sale/Hot 标签）
  - YouMightLike - 推荐商品网格（2 列布局）
  - BottomBar - 底部操作栏（收藏/加入购物车/立即购买）
  - SKUSelector Bottom Sheet - 规格选择弹窗（颜色/尺寸/数量）
- **布局修复**:
  - 容器高度 100vh，内部滚动
  - BottomBar 固定在容器底部（z-index: 100）
  - Bottom Sheet 使用 `position: absolute`（z-index: 99）
  - 内容区域 `padding-bottom: 100px`
  - SKU 选择器弹窗 `max-height: calc(100vh - 84px)`，底部 `padding: calc(24px + 84px)`

## 待实现页面

### 1. 主应用页面
- **状态**: ⏳ 待实现
- **Figma 节点**: 待补充
- **功能**: 主应用界面（商品展示/购物车等）

## 待办事项

### 组件优化
- [x] 检查所有组件的可复用性
- [x] 添加 Props 支持（text/state/variant）
- [x] 确保所有组件符合设计系统规范

### 路由验证
- [x] 测试所有页面跳转流程
- [x] 验证 query 参数传递
- [ ] 添加路由守卫（如需要）

### 代码质量
- [ ] TypeScript 类型完善
- [ ] 添加单元测试
- [ ] 代码审查和优化

### 设计验证
- [x] 与 Figma 设计稿对比验证
- [x] 确保像素级还原
- [ ] 检查响应式适配

### 待实现功能
- [ ] 购物车页面 `/cart`
- [ ] 分类浏览页面 `/category/:id`
- [ ] 个人中心页面 `/profile`
- [ ] 订单列表/详情页面
- [ ] Pinia 状态管理（商品/购物车）
- [ ] 商品数据 API 集成
- [ ] 底部导航栏组件

## 当前进度
- 已实现页面：12/12（基础认证流程 + 密码重置流程 + 引导流程 + 商城主页 + 商品详情页（完整 Figma 设计））
- 整体进度：约 65%
