# Hello Card + Maximum Attempts 实现设计文档

**创建日期**: 2026-03-20
**Figma 节点**:
- 0:12238 (10 Maximum Attempts - 错误弹窗)
- 0:12177 (11 Hello Card - 欢迎轮播第 1 页)
**状态**: 已批准

---

## 1. 概述

### 1.1 功能目标
1. **Maximum Attempts (第 10 页)**: 密码恢复验证码错误达到最大次数时的错误提示弹窗
2. **Hello Card 轮播 (第 11 页开始)**: 登录后的首次引导轮播页面，共 4 页

### 1.2 页面关系
```
密码恢复流程:
WrongPassword → PasswordRecovery → PasswordRecoveryCode → (错误 3 次) → Maximum Attempts 弹窗

登录成功流程:
Login → Password → PasswordTyping → (成功) → Hello Card 轮播 → Shop 主页
```

### 1.2 成功标准
- ✅ 4 页引导内容完整展示
- ✅ 支持左右滑动切换页面
- ✅ 支持点击底部 dots 跳转
- ✅ 最后一页显示进入主应用按钮
- ✅ 像素级还原 Figma 设计

---

## 2. Maximum Attempts 弹窗设计

### 2.1 功能描述
当用户在 PasswordRecoveryCode 页面输入错误验证码达到 3 次时，弹出此提示框。

### 2.2 UI 元素
| 元素 | 描述 | 样式 |
|------|------|------|
| 遮罩层 | 半透明黑色背景 | `opacity-78`, `bg-[#0e0e0e]` |
| 弹窗容器 | 白色圆角卡片 | `bg-[#f8f8f8]`, `rounded-[19px]` |
| 警告图标 | 顶部圆形感叹号 | 粉色背景 + 感叹号 |
| 标题文本 | 错误提示 | "You reached out maximum amount of attempts. Please, try later." |
| Okay 按钮 | 关闭弹窗 | `bg-[#202020]`, `rounded-[16px]` |

### 2.3 交互逻辑
```typescript
// PasswordRecoveryCode.vue 添加
const maxAttempts = 3
const attemptCount = ref(0)

// 错误时增加计数
const handleWrongCode = () => {
  attemptCount.value++
  if (attemptCount.value >= maxAttempts) {
    showMaxAttemptsPopup.value = true
  }
}

// Okay 按钮行为
const handleOkay = () => {
  showMaxAttemptsPopup.value = false
  router.push('/login') // 返回登录页
}
```

---

## 3. Hello Card 轮播设计

### 3.1 架构设计

**组件结构：**
```
HelloCard.vue (主页面)
├── StatusBar.vue (状态栏)
├── BackgroundBubbles (背景装饰)
├── CardContainer (白色圆角卡片)
│   ├── ImageSection (动态图片)
│   ├── Title (动态标题)
│   └── Description (动态描述)
├── DotsIndicator (4 个点指示器)
└── HomeIndicator.vue (底部指示条)
```

### 3.2 数据流

```
引导数据数组 (guides)
    ↓
当前页索引 (currentPage)
    ↓
条件渲染 → 显示对应图片/标题/描述
    ↓
用户滑动/点击 → 更新 currentPage
    ↓
最后一页显示按钮 → 跳转主应用
```

### 3.3 4 页内容规划

| 索引 | 标题 | 描述 | 图片 | 按钮 |
|------|------|------|------|------|
| 0 | Hello | 欢迎介绍 | 购物女性 | - |
| 1 | Explore | 探索精选商品 | 商品展示 | - |
| 2 | Choose | 收藏心仪好物 | 购物车 | - |
| 3 | Ready | 准备好了吗？ | - | "Let's Go" |

**注意**: 当前 Figma 节点 0:12177 显示的是第 2 页 (索引 1) 的设计，但我们需要实现完整的 4 页轮播。

---

## 4. 交互设计

### 4.1 滑动切换

```typescript
// 滑动阈值
const SWIPE_THRESHOLD = 50 // px

// 触摸事件
touchstart → 记录 startX
touchend   → 计算 deltaX
  - deltaX > SWIPE_THRESHOLD  → 上一页
  - deltaX < -SWIPE_THRESHOLD → 下一页
```

### 4.2 Dots 点击

```typescript
// 点击第 n 个点
const handleDotClick = (index: number) => {
  currentPage.value = index
}
```

### 4.3 按钮行为

最后一页的 "Let's Go" 按钮:
```typescript
const handleLet'sGo = () => {
  // 标记引导已完成 (localStorage)
  localStorage.setItem('onboardingCompleted', 'true')
  // 跳转主应用
  router.push('/shop')
}
```

---

## 5. 视觉规范

### 5.1 卡片样式
- 圆角：`rounded-[30px]`
- 阴影：`shadow-[0px_10px_37px_0px_rgba(0,0,0,0.16)]`
- 背景：`bg-white`
- 位置：`inset-[9.98%_6.4%_14.41%_6.67%]`

### 5.2 字体规范
| 元素 | 字体系列 | 字重 | 字号 | 行高 |
|------|----------|------|------|------|
| 标题 | Raleway | Bold | 28px | 36px |
| 描述 | Nunito Sans | Light | 19px | 27px |

### 5.3 Dots 样式
- 未激活：浅蓝色 `#C9D9F9` (dot 01)
- 激活：蓝色 `#3B82F6` (dot 02)
- 尺寸：`20px × 20px`
- 间距：`40px` (118px, 158px, 198px, 238px)

---

## 6. 技术实现

### 6.1 核心代码结构

```vue
<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'

const router = useRouter()
const currentPage = ref(0)

// 引导数据
const guides: GuidePage[] = [
  { title: 'Hello', description: '...', image: '...' },
  { title: 'Explore', description: '...', image: '...' },
  { title: 'Choose', description: '...', image: '...' },
  { title: 'Ready', description: '...', image: '...', hasButton: true },
]

// 滑动处理
const handleTouchStart = (e: TouchEvent) => { /* ... */ }
const handleTouchEnd = (e: TouchEvent) => { /* ... */ }
</script>
```

### 6.2 依赖组件
- `StatusBar.vue` - 已有
- `HomeIndicator.vue` - 已有
- 气泡背景 - 内联实现

---

## 7. 边界情况

### 7.1 第一页 (索引 0)
- 无法继续向左滑动
- 不显示上一页指示

### 7.2 最后一页 (索引 3)
- 无法继续向右滑动
- 显示 "Let's Go" 按钮
- 禁止自动循环

### 7.3 引导完成标记
```typescript
// 首次加载时检查
onMounted(() => {
  const completed = localStorage.getItem('onboardingCompleted')
  if (completed === 'true') {
    router.push('/shop')
  }
})
```

---

## 8. 文件清单

| 文件 | 路径 | 状态 |
|------|------|------|
| HelloCard.vue | frontend/src/views/HelloCard.vue | 待创建 |
| 路由配置 | frontend/src/router/index.ts | 待更新 |

---

## 9. 测试要点

- [ ] 滑动切换流畅
- [ ] Dots 点击响应正确
- [ ] 最后一页按钮显示
- [ ] 跳转主应用正常
- [ ] 视觉还原 Figma 设计

---

## 10. 后续扩展

- 图片资源本地化
- 添加页面切换动画
- 支持跳过引导
- 自动播放功能 (可选)
