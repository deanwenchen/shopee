# 商品详情页设计文档

**创建时间**: 2026-04-01
**状态**: 待确认

---

## 需求分析

Figma Page 35-39 是商品详情页的 5 个不同状态/变体：

| 页面 | 名称 | 使用场景 | 核心内容 |
|------|------|----------|----------|
| 35 | Product | 默认商品页 | 主图 + 基本信息 |
| 36 | Product Sale | 促销商品页 | 促销 Banner + 倒计时 + 双价格 |
| 37 | Product Full | 完整详情页 | Tabs + 商品描述 + 规格参数 |
| 38 | Product Variations | 多规格商品页 | SKU 选择器（颜色/尺寸） |
| 39 | Reviews | 评价详情页 | 评分汇总 + 评价列表 + 晒图 |

---

## 组件架构设计

### 推荐方案：1 个主组件 + 6 个子组件

```
ProductDetail.vue (主组件)
├── ProductGallery.vue      ← 图片轮播（4-6 张）
├── ProductInfo.vue         ← 基本信息（名称/价格/评分/收藏）
├── FlashSaleBanner.vue     ← Page 36 专用（促销 Banner + 倒计时）
├── SKUSelector.vue         ← Page 38 专用（规格选择器）
├── ProductDetails.vue      ← Page 37 专用（详情 Tabs）
├── ReviewsSection.vue      ← Page 39 专用（评价模块）
└── BottomBar.vue           ← 底部操作栏（购物车/购买按钮）
```

### 路由设计

```ts
// router/index.ts
{
  path: '/product/:id',
  component: ProductDetail,
  children: [
    { path: 'reviews', component: ReviewsSection },
    { path: 'details', component: ProductDetails },
  ]
}
```

### 条件渲染逻辑

| 页面 | 触发条件 |
|------|----------|
| Page 35 (基础) | 默认状态 |
| Page 36 (促销) | `product.isOnSale === true` |
| Page 37 (完整) | 路由 `/product/:id/details` 或 用户点击展开 |
| Page 38 (规格) | `product.hasVariations === true` |
| Page 39 (评价) | 路由 `/product/:id/reviews` 或 锚点 `#reviews` |

---

## 组件详细设计

### 1. ProductGallery.vue
**功能**: 商品图片轮播
- 支持 4-6 张图片
- 指示器 Dots
- 手势滑动支持
- 收藏按钮（浮动右上角）

### 2. ProductInfo.vue
**功能**: 商品基本信息
- 商品名称
- 价格（支持原价 + 促销价双显示）
- 评分（星星 + 评论数）
- 已售货量

### 3. FlashSaleBanner.vue
**功能**: 促销 Banner（Page 36）
- 促销标题
- 倒计时器（分：秒：毫秒）
- 进度条（已售百分比）

### 4. SKUSelector.vue
**功能**: 规格选择器（Page 38）
- 颜色选择（可切换商品图片）
- 尺寸选择（S/M/L/XL）
- 库存状态
- 已选规格显示

### 5. ProductDetails.vue
**功能**: 商品详情（Page 37）
- Tabs 切换（Description / Specs / Reviews）
- 富文本描述
- 规格参数表
- 物流/售后信息

### 6. ReviewsSection.vue
**功能**: 评价模块（Page 39）
- 评分汇总（平均分 + 分布条）
- 评价筛选（全部/有图/追评）
- 评价列表（头像 + 评分 + 内容 + 晒图）
- 写评价按钮

### 7. BottomBar.vue
**功能**: 底部操作栏
- 客服按钮
- 收藏按钮
- 加入购物车
- 立即购买

---

## 数据结构设计

```typescript
// types/product.ts
interface Product {
  id: number
  name: string
  description: string
  price: number
  originalPrice?: number  // 促销原价
  isOnSale: boolean       // 是否促销
  saleEndTime?: string    // 促销结束时间
  images: string[]
  rating: number
  reviewCount: number
  salesCount: number
  hasVariations: boolean
  variations: Variation[]
  details?: ProductDetails
}

interface Variation {
  id: number
  type: 'color' | 'size'
  options: string[]
  selected?: string
}

interface ProductDetails {
  description: string
  specifications: Record<string, string>
  shipping: string
  afterSales: string
}

interface Review {
  id: number
  userName: string
  userAvatar: string
  rating: number
  content: string
  images?: string[]
  date: string
  isFollowUp?: boolean
}
```

---

## 实现优先级

### Phase 1: 基础框架（P0）
1. ProductDetail.vue 主组件
2. ProductGallery.vue
3. ProductInfo.vue
4. BottomBar.vue

### Phase 2: 核心功能（P1）
5. SKUSelector.vue（电商核心）
6. FlashSaleBanner.vue（营销功能）

### Phase 3: 扩展功能（P2）
7. ReviewsSection.vue
8. ProductDetails.vue

---

## Figma 资源需求

需要获取以下页面的设计上下文：
- Page 35: Product (nodeId: 0:8785) - 基础布局
- Page 36: Product Sale (nodeId: 0:8689) - 促销元素
- Page 37: Product Full (nodeId: 0:8438) - 详情布局
- Page 38: Product Variations (nodeId: 0:8314) - SKU 选择器
- Page 39: Reviews (nodeId: 0:8192) - 评价模块

---

## 验收标准

- [ ] 5 个页面状态全部还原
- [ ] 组件可复用，支持 Props 配置
- [ ] 响应式布局（375px 宽度）
- [ ] 与 Figma 设计像素级对齐
- [ ] 所有图片使用本地资源
