# Shoppe - Beautiful eCommerce UI Kit

基于 Figma 设计稿实现的 Vue 3 电商平台 UI Kit.

## 技术栈

- Vue 3.5+ (Composition API, `<script setup>`)
- TypeScript 5.x
- Tailwind CSS 3.x
- Vite 6.x
- Vue Router 5.x
- Pinia 3.x

## 快速开始

```bash
# 进入 frontend 目录
cd frontend

# 安装依赖（如未安装）
npm install

# 启动开发服务器
npm run dev

# 构建生产版本
npm run build

# 预览生产构建
npm run preview
```

## 设计 Token

设计 Token 定义在 `tailwind.config.js` 中:

| Token | 值 | 用途 |
|-------|-----|------|
| colors.brand.blue | #004cff | 主按钮背景 |
| colors.brand.black | #202020 | 主文本颜色 |
| colors.brand.white | #f3f3f3 | 按钮文本颜色 |
| fontFamily.raleway | Raleway | 品牌标题 |
| fontFamily.nunito | Nunito Sans | 正文文本 |
| fontSize.52 | 52px | 品牌标题大小 |
| fontSize.22 | 22px | 按钮文本大小 |
| fontSize.19 | 19px | 副标题大小 |
| fontSize.15 | 15px | 次要文本大小 |
| letterSpacing.shoppe | -0.52px | 品牌标题字间距 |
| borderRadius.shoppe-btn | 16px | 按钮圆角 |
| borderRadius.shoppe-xl | 34px | 大圆角 |

## 项目结构

```
frontend/
├── src/
│   ├── assets/
│   │   ├── icons/           # 图标资源
│   │   │   ├── arrow.svg
│   │   │   ├── wifi.svg
│   │   │   ├── cellular.svg
│   │   │   └── battery.svg
│   │   └── images/          # 图片资源
│   │       └── shopping-bag.svg
│   ├── components/          # 可复用组件
│   │   ├── StatusBar.vue
│   │   ├── PrimaryButton.vue
│   │   ├── SecondaryAction.vue
│   │   └── HomeIndicator.vue
│   ├── router/
│   │   └── index.ts
│   ├── views/
│   │   └── StartPage.vue    # Start 页面
│   ├── App.vue
│   ├── main.ts
│   └── style.css
├── index.html
├── package.json
├── tailwind.config.js
├── vite.config.ts
└── tsconfig.json
```

## 已实现页面

### Start Page (启动页)

- 路由：`/`
- Figma 节点：`0:12855`
- 包含元素：
  - iOS 风格状态栏（时间、信号、WiFi、电池）
  - Shoppe Logo（购物袋图标）
  - 品牌标题 "Shoppe"
  - 副标题描述
  - "Let's get started" 主按钮
  - "I already have an account" 次要操作
  - 底部 Home Indicator

## Figma 设计

设计稿来源：https://www.figma.com/design/HPtpuBt4RrvXdzDLy4vUN1/Shoppe?node-id=0-12855

## 视觉还原度

- [x] 状态栏位置和样式正确
- [x] Logo 图标位置正确 (top-[232px])
- [x] "Shoppe" 标题字体和大小正确 (Raleway Bold, 52px, tracking-[-0.52px])
- [x] 副标题行高和宽度正确 (19px, leading-[33px], w-[249px])
- [x] 主按钮颜色、圆角、尺寸正确 (#004cff, 16px, 335x61)
- [x] 次要操作文字和箭头按钮正确
- [x] 底部 Home Indicator 位置正确
