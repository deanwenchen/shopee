# Shoppe Start Page Implementation Plan

> **For agentic workers:** REQUIRED: Use superpowers:subagent-driven-development (if subagents available) or superpowers:executing-plans to implement this plan. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** 实现 Shoppe Start 页面 (Figma node-id: 0:12855)，创建 Vue 3 + TypeScript + Tailwind CSS 项目，1:1 像素级还原设计稿

**Architecture:** 采用 Vue 3 Composition API (`<script setup>`) + TypeScript + Tailwind CSS 技术栈，设计 Token 驱动样式，组件化架构

**Tech Stack:**
- Vue 3.4+ (Vite 5 构建)
- TypeScript 5.x
- Tailwind CSS 3.x
- Pinia (状态管理)
- Vue Router 4.x

---

## Chunk 1: 项目初始化

### Task 1: 创建 Vite + Vue 3 + TypeScript 项目

**Files:**
- Create: `package.json`
- Create: `vite.config.ts`
- Create: `tsconfig.json`
- Create: `index.html`

- [ ] **Step 1: 初始化 npm 项目**

```bash
cd D:\Claude\Figma\Shoppe
npm init -y
```

- [ ] **Step 2: 安装核心依赖**

```bash
npm install vue@latest vue-router@latest pinia@latest
```
Expected: 安装 Vue 3, Vue Router, Pinia

- [ ] **Step 3: 安装开发依赖**

```bash
npm install -D vite@latest @vitejs/plugin-vue typescript tailwindcss postcss autoprefixer
```
Expected: 安装 Vite, Vue 插件，TypeScript, Tailwind CSS

- [ ] **Step 4: 初始化 Tailwind CSS**

```bash
npx tailwindcss init -p
```
Expected: 创建 tailwind.config.js 和 postcss.config.js

- [ ] **Step 5: 提交**

```bash
git init
git add .
git commit -m "chore: initialize Vue 3 + TypeScript + Tailwind project"
```

---

## Chunk 2: 配置文件

### Task 2: 配置 Vite 和 TypeScript

**Files:**
- Create: `vite.config.ts`
- Create: `tsconfig.json`
- Create: `tsconfig.node.json`

- [ ] **Step 1: 创建 vite.config.ts**

```typescript
import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import { fileURLToPath, URL } from 'node:url'

export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    port: 3000,
    open: true
  }
})
```

- [ ] **Step 2: 创建 tsconfig.json**

```json
{
  "compilerOptions": {
    "target": "ES2020",
    "useDefineForClassFields": true,
    "module": "ESNext",
    "lib": ["ES2020", "DOM", "DOM.Iterable"],
    "skipLibCheck": true,
    "moduleResolution": "bundler",
    "allowImportingTsExtensions": true,
    "resolveJsonModule": true,
    "isolatedModules": true,
    "noEmit": true,
    "jsx": "preserve",
    "strict": true,
    "noUnusedLocals": true,
    "noUnusedParameters": true,
    "noFallthroughCasesInSwitch": true,
    "baseUrl": ".",
    "paths": {
      "@/*": ["./src/*"]
    }
  },
  "include": ["src/**/*.ts", "src/**/*.tsx", "src/**/*.vue"],
  "references": [{ "path": "./tsconfig.node.json" }]
}
```

- [ ] **Step 3: 创建 tsconfig.node.json**

```json
{
  "compilerOptions": {
    "composite": true,
    "skipLibCheck": true,
    "module": "ESNext",
    "moduleResolution": "bundler",
    "allowSyntheticDefaultImports": true
  },
  "include": ["vite.config.ts"]
}
```

- [ ] **Step 4: 提交**

```bash
git add vite.config.ts tsconfig.json tsconfig.node.json
git commit -m "chore: add TypeScript and Vite configuration"
```

---

### Task 3: 配置 Design Tokens (Tailwind)

**Files:**
- Modify: `tailwind.config.js`

- [ ] **Step 1: 创建 tailwind.config.js 完整配置**

```javascript
/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        // Figma Design Tokens - Shoppe Brand Colors
        brand: {
          blue: '#004cff',      // Primary button background
          black: '#202020',     // Primary text color
          white: '#f3f3f3',     // Button text color
        }
      },
      fontFamily: {
        // Figma Font Families
        raleway: ['Raleway', 'sans-serif'],
        nunito: ['Nunito Sans', 'sans-serif'],
      },
      fontSize: {
        // Figma Font Sizes
        '52': ['52px', '1'],
        '22': ['22px', '31px'],
        '19': ['19px', '33px'],
        '15': ['15px', '26px'],
        '14': ['14px', '1'],
      },
      letterSpacing: {
        'shoppe': '-0.52px',
      },
      borderRadius: {
        'shoppe-btn': '16px',
        'shoppe-xl': '34px',
      }
    },
  },
  plugins: [],
}
```

- [ ] **Step 2: 提交**

```bash
git add tailwind.config.js
git commit -m "chore: add Figma design tokens to Tailwind config"
```

---

## Chunk 3: 项目骨架

### Task 4: 创建项目入口文件

**Files:**
- Create: `index.html`
- Create: `src/main.ts`
- Create: `src/App.vue`
- Create: `src/style.css`

- [ ] **Step 1: 创建 index.html**

```html
<!DOCTYPE html>
<html lang="zh-CN">
  <head>
    <meta charset="UTF-8" />
    <link rel="icon" type="image/svg+xml" href="/vite.svg" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Shoppe - Beautiful eCommerce UI Kit</title>
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Raleway:wght@700&family=Nunito+Sans:wght@300;400;600&display=swap" rel="stylesheet">
  </head>
  <body>
    <div id="app"></div>
    <script type="module" src="/src/main.ts"></script>
  </body>
</html>
```

- [ ] **Step 2: 创建 src/style.css**

```css
@tailwind base;
@tailwind components;
@tailwind utilities;

* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

body {
  font-family: 'Nunito Sans', sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}

#app {
  width: 100%;
  min-height: 100vh;
}
```

- [ ] **Step 3: 创建 src/main.ts**

```typescript
import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import './style.css'

const app = createApp(App)
const pinia = createPinia()

app.use(pinia)
app.mount('#app')
```

- [ ] **Step 4: 创建 src/App.vue**

```vue
<script setup lang="ts">
import { RouterView } from 'vue-router'
</script>

<template>
  <RouterView />
</template>

<style scoped>
</style>
```

- [ ] **Step 5: 提交**

```bash
git add index.html src/main.ts src/App.vue src/style.css
git commit -m "chore: add application entry point files"
```

---

### Task 5: 配置 Vue Router

**Files:**
- Create: `src/router/index.ts`

- [ ] **Step 1: 创建 router 配置**

```typescript
import { createRouter, createWebHistory } from 'vue-router'
import StartPage from '@/views/StartPage.vue'

const routes = [
  {
    path: '/',
    name: 'start',
    component: StartPage
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
```

- [ ] **Step 2: 更新 src/main.ts 添加 router**

```typescript
// 添加 import
import router from './router'

// 添加 use
app.use(router)
```

- [ ] **Step 3: 提交**

```bash
git add src/router/index.ts src/main.ts
git commit -m "feat: configure Vue Router with StartPage route"
```

---

## Chunk 4: 组件实现

### Task 6: 创建基础组件

**Files:**
- Create: `src/components/StatusBar.vue`
- Create: `src/components/PrimaryButton.vue`
- Create: `src/components/SecondaryAction.vue`
- Create: `src/components/HomeIndicator.vue`

- [ ] **Step 1: 创建 StatusBar 组件**

```vue
<script setup lang="ts">
import wifiIcon from '@/assets/icons/wifi.svg'
import cellularIcon from '@/assets/icons/cellular.svg'
import batteryIcon from '@/assets/icons/battery.svg'
</script>

<template>
  <div class="absolute top-0 left-0 w-[375px] h-[44px] flex items-center justify-between px-[20px]" data-name="Status Bar">
    <span class="text-[14px] font-semibold text-brand-black">9:41</span>
    <div class="flex items-center gap-[5px]">
      <img :src="cellularIcon" alt="Cellular signal" class="w-[17px] h-[10.667px]" />
      <img :src="wifiIcon" alt="WiFi" class="w-[15.272px] h-[10.966px]" />
      <img :src="batteryIcon" alt="Battery" class="w-[24.328px] h-[11.333px]" />
    </div>
  </div>
</template>

<style scoped>
</style>
```

- [ ] **Step 2: 创建 PrimaryButton 组件**

```vue
<script setup lang="ts">
interface Props {
  text: string
  onClick?: () => void
}

const props = withDefaults(defineProps<Props>(), {
  text: 'Button'
})

defineEmits<{
  click: []
}>()
</script>

<template>
  <button
    class="w-[335px] h-[61px] bg-brand-blue rounded-shoppe-btn flex items-center justify-center cursor-pointer border-none"
    @click="$emit('click')"
    :data-name="text"
  >
    <span class="font-light text-[22px] leading-[31px] text-brand-white">
      {{ text }}
    </span>
  </button>
</template>

<style scoped>
</style>
```

- [ ] **Step 3: 创建 SecondaryAction 组件**

```vue
<script setup lang="ts">
import arrowIcon from '@/assets/icons/arrow.svg'

interface Props {
  text: string
  onClick?: () => void
}

const props = withDefaults(defineProps<Props>(), {
  text: 'Secondary Action'
})

defineEmits<{
  click: []
}>()
</script>

<template>
  <div class="flex items-center justify-center gap-[10px]" data-name="Secondary Action">
    <span class="font-light text-[15px] leading-[26px] text-brand-black opacity-90">
      {{ text }}
    </span>
    <button
      class="w-[30px] h-[30px] bg-brand-blue rounded-full flex items-center justify-center cursor-pointer border-none"
      @click="$emit('click')"
      data-name="Arrow Button"
    >
      <img :src="arrowIcon" alt="Arrow" class="w-[14.457px] h-[11.387px]" />
    </button>
  </div>
</template>

<style scoped>
</style>
```

- [ ] **Step 4: 创建 HomeIndicator 组件**

```vue
<script setup lang="ts">
</script>

<template>
  <div
    class="absolute bottom-[24px] left-[121px] w-[134px] h-[5px] bg-black rounded-shoppe-xl"
    data-name="Home Indicator"
  />
</template>

<style scoped>
</style>
```

- [ ] **Step 5: 提交**

```bash
git add src/components/*.vue
git commit -m "feat: create reusable UI components"
```

---

### Task 7: 准备资源文件

**Files:**
- Create: `src/assets/icons/` 目录
- Create: `src/assets/images/` 目录

- [ ] **Step 1: 创建资源目录**

```bash
mkdir -p src/assets/icons src/assets/images
```

- [ ] **Step 2: 下载 Figma 资源**

从 Figma MCP 获取的资源 URL:
- `imgButton` = https://www.figma.com/api/mcp/asset/bf4dc18f-6094-451f-8756-9e8c641cb59c
- `imgArrow` = https://www.figma.com/api/mcp/asset/dbcf1e1f-5683-4631-afb8-c8cda7a0960e
- `imgBarsStatusBarLightStatusBar` = https://www.figma.com/api/mcp/asset/f4cedf9e-0f9c-422d-8693-c7f83fc0d4ed
- `imgBorder` = https://www.figma.com/api/mcp/asset/c8cd50e6-afa9-428e-85f5-148c82177940
- `imgCap` = https://www.figma.com/api/mcp/asset/365e2c7b-97b4-4158-9983-f22e1b9292a8
- `imgCapacity` = https://www.figma.com/api/mcp/asset/98491d8c-8547-4940-800e-f8aba3691053
- `imgWifi` = https://www.figma.com/api/mcp/asset/3eee7ca5-911a-46f5-bfc2-d2dfb64bb9db
- `imgCellularConnection` = https://www.figma.com/api/mcp/asset/068778bf-15cd-4f61-b3a8-5ed5c6651a27
- `imgBackground` = https://www.figma.com/api/mcp/asset/cedd961a-27e6-4422-b1ff-7d3cee708af6
- `imgEllipse` = https://www.figma.com/api/mcp/asset/43a2ac67-ef29-47cd-8b60-274b980903fe
- `imgGroup1000004649` = https://www.figma.com/api/mcp/asset/a346784d-8af3-4e21-a784-6d630d75a65a

```bash
# 下载资源 (使用 PowerShell 或 curl)
curl -o src/assets/images/shopping-bag.svg "https://www.figma.com/api/mcp/asset/a346784d-8af3-4e21-a784-6d630d75a65a"
curl -o src/assets/icons/arrow.svg "https://www.figma.com/api/mcp/asset/dbcf1e1f-5683-4631-afb8-c8cda7a0960e"
curl -o src/assets/icons/wifi.svg "https://www.figma.com/api/mcp/asset/3eee7ca5-911a-46f5-bfc2-d2dfb64bb9db"
curl -o src/assets/icons/cellular.svg "https://www.figma.com/api/mcp/asset/068778bf-15cd-4f61-b3a8-5ed5c6651a27"
curl -o src/assets/icons/battery.svg "https://www.figma.com/api/mcp/asset/f4cedf9e-0f9c-422d-8693-c7f83fc0d4ed"
```

- [ ] **Step 3: 提交**

```bash
git add src/assets/
git commit -m "chore: add design assets from Figma"
```

---

## Chunk 5: 页面实现

### Task 8: 创建 StartPage 视图

**Files:**
- Create: `src/views/StartPage.vue`

- [ ] **Step 1: 创建完整的 StartPage 组件**

```vue
<script setup lang="ts">
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import PrimaryButton from '@/components/PrimaryButton.vue'
import SecondaryAction from '@/components/SecondaryAction.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'
import logoImage from '@/assets/images/shopping-bag.svg'

const router = useRouter()

const handleGetStarted = () => {
  console.log('Get Started clicked')
  // TODO: Navigate to next page
}

const handleLogin = () => {
  console.log('Login clicked')
  // TODO: Navigate to login page
}
</script>

<template>
  <div class="bg-white relative w-[375px] h-[817px] mx-auto" data-name="01 Start" data-node-id="0:12855">
    <!-- Status Bar -->
    <StatusBar />

    <!-- Logo Container - Centered -->
    <div class="absolute top-[232px] left-[121px] w-[133px] h-[133px]" data-name="Logo Container">
      <img :src="logoImage" alt="Shoppe Logo" class="w-full h-full object-contain" />
    </div>

    <!-- Brand Title -->
    <h1
      class="absolute left-[188px] top-[390px] font-raleway font-bold text-[52px] text-brand-black text-center tracking-[-0.52px] whitespace-nowrap -translate-x-1/2"
      data-node-id="0:12858"
    >
      Shoppe
    </h1>

    <!-- Subtitle -->
    <p
      class="absolute left-[187.5px] top-[469px] w-[249px] font-nunito font-light text-[19px] leading-[33px] text-brand-black text-center -translate-x-1/2"
      data-node-id="0:12859"
    >
      Beautiful eCommerce UI Kit for your online store
    </p>

    <!-- Primary Action Button -->
    <div class="absolute left-[20px] top-[634px]" data-node-id="0:12908">
      <PrimaryButton
        text="Let's get started"
        @click="handleGetStarted"
      />
    </div>

    <!-- Secondary Action -->
    <div class="absolute left-[81px] top-[713px]" data-node-id="0:12860">
      <SecondaryAction
        text="I already have an account"
        @click="handleLogin"
      />
    </div>

    <!-- Home Indicator -->
    <HomeIndicator />
  </div>
</template>

<style scoped>
/* Figma exact positioning */
</style>
```

- [ ] **Step 2: 运行开发服务器测试视觉效果**

```bash
npm run dev
```
Expected: 开发服务器启动，浏览器打开 http://localhost:3000

- [ ] **Step 3: 提交**

```bash
git add src/views/StartPage.vue
git commit -m "feat: implement StartPage with Figma 1:1 design"
```

---

## Chunk 6: 验证和交付

### Task 9: 视觉验证

**Files:**
- N/A (验证任务)

- [ ] **Step 1: 获取页面截图进行验证**

打开浏览器访问 http://localhost:3000，对比 Figma 设计稿验证以下元素:
- [ ] 状态栏位置和样式正确
- [ ] Logo 图标位置正确 (top-[232px])
- [ ] "Shoppe" 标题字体和大小正确 (Raleway Bold, 52px)
- [ ] 副标题行高和宽度正确 (19px, leading-[33px], w-[249px])
- [ ] 主按钮颜色、圆角、尺寸正确 (#004cff, 16px, 335x61)
- [ ] 次要操作文字和箭头按钮正确
- [ ] 底部 Home Indicator 位置正确

- [ ] **Step 2: 提交最终验证**

```bash
git add .
git commit -m "chore: final verification and cleanup"
```

---

## Chunk 7: 文档

### Task 10: 更新项目文档

**Files:**
- Create: `README.md`

- [ ] **Step 1: 创建 README.md**

```markdown
# Shoppe - Beautiful eCommerce UI Kit

基于 Figma 设计稿实现的 Vue 3 电商平台 UI Kit.

## 技术栈

- Vue 3.4+ (Composition API, `<script setup>`)
- TypeScript 5.x
- Tailwind CSS 3.x
- Vite 5.x
- Vue Router 4.x
- Pinia

## 快速开始

```bash
# 安装依赖
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

## 项目结构

```
src/
├── assets/          # 静态资源
├── components/      # 可复用组件
├── router/          # 路由配置
├── views/           # 页面组件
├── App.vue          # 根组件
├── main.ts          # 入口文件
└── style.css        # 全局样式
```

## Figma 设计

设计稿来源: https://www.figma.com/design/HPtpuBt4RrvXdzDLy4vUN1/Shoppe?node-id=0-12855
```

- [ ] **Step 2: 提交**

```bash
git add README.md
git commit -m "docs: add project README"
```

---

## 完成标准

- [ ] 所有 Task 完成
- [ ] 所有测试通过
- [ ] 视觉验证与 Figma 设计稿一致
- [ ] 代码已提交到版本控制

---

## 参考资源

- Figma 设计稿：https://www.figma.com/design/HPtpuBt4RrvXdzDLy4vUN1/Shoppe?node-id=0-12855
- 设计文档：`docs/superpowers/specs/2026-03-20-shoppe-start-page-design.md`
- Vue 3 文档：https://vuejs.org/
- Tailwind CSS 文档：https://tailwindcss.com/
