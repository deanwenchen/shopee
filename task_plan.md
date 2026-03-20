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
  - 输入满 8 位 → `/wrong-password`
- **组件**: StatusBar, HomeIndicator

### 6. WrongPassword (`/wrong-password`)
- **状态**: ✅ 完成
- **功能**: 密码错误页面（8 位，红色点）
- **导航**:
  - 输入满 8 位 → `/login`
  - Forgot password? → （待实现）
- **组件**: StatusBar, HomeIndicator

## 待实现页面

### 1. 主应用页面
- **状态**: ⏳ 待实现
- **Figma 节点**: 待补充
- **功能**: 主应用界面（商品展示/购物车等）

### 2. 密码重置页面
- **状态**: ⏳ 待实现
- **Figma 节点**: 待补充
- **功能**: 重置忘记密码

## 待办事项

### 组件优化
- [ ] 检查所有组件的可复用性
- [ ] 添加 Props 支持（text/state/variant）
- [ ] 确保所有组件符合设计系统规范

### 路由验证
- [ ] 测试所有页面跳转流程
- [ ] 验证 query 参数传递
- [ ] 添加路由守卫（如需要）

### 代码质量
- [ ] TypeScript 类型完善
- [ ] 添加单元测试
- [ ] 代码审查和优化

### 设计验证
- [ ] 与 Figma 设计稿对比验证
- [ ] 确保像素级还原
- [ ] 检查响应式适配

## 当前进度
- 已实现页面：6/8（基础认证流程完成）
- 整体进度：约 40%
