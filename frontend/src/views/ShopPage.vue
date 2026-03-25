<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'

const router = useRouter()
const activeTab = ref('shop')

// 使用 public 目录的资源，通过字符串路径直接引用，避免 Vite 预加载
const ASSET_BASE = '/assets/figma/'

// 分类数据 - 使用纯字符串路径
const categories = ref([
  { id: 1, name: 'Clothing', count: 109, images: ['cat-clothing-1.png', 'cat-clothing-2.png', 'cat-clothing-3.png', 'cat-clothing-main.png'] },
  { id: 2, name: 'Bags', count: 87, images: ['cat-bags-1.png', 'cat-bags-2.png', 'cat-bags-3.png', 'cat-bags-main.png'] },
  { id: 3, name: 'Shoes', count: 530, images: ['cat-shoes-1.png', 'cat-shoes-2.png', 'cat-shoes-3.png', 'cat-shoes-main.png'] },
  { id: 4, name: 'Lingerie', count: 218, images: ['cat-lingerie-1.png', 'cat-lingerie-2.png', 'cat-lingerie-3.png', 'cat-lingerie-main.png'] },
  { id: 5, name: 'Watch', count: 218, images: ['cat-watch-1.png', 'cat-watch-2.png', 'cat-watch-3.png', 'cat-watch-4.png'] },
  { id: 6, name: 'Hoodies', count: 218, images: ['cat-hoodies-1.png', 'cat-hoodies-2.png', 'cat-hoodies-3.png', 'cat-hoodies-4.png'] },
  { id: 7, name: 'Dresses', count: 156, images: ['cat-dresses-1.png', 'cat-dresses-2.png'] },
  { id: 8, name: 'T-Shirts', count: 243, images: ['top-tshirts.png'] },
])

// Top Products 分类 - 使用纯字符串路径 (根据 Figma 0-11214)
// 5 个商品从左到右：Bags, Watches, Hoodies, Shoes, Lingerie
const topProducts = ref([
  { id: 1, name: 'Bags', image: `${ASSET_BASE}top-products-fourth.png` },
  { id: 2, name: 'Watches', image: `${ASSET_BASE}top-products-tertiary.png` },
  { id: 3, name: 'Hoodies', image: `${ASSET_BASE}top-products-secondary.png` },
  { id: 4, name: 'Shoes', image: `${ASSET_BASE}top-products-fifth.png` },
  { id: 5, name: 'Lingerie', image: `${ASSET_BASE}top-products-sixth.png` },
])

const topEllipse = `${ASSET_BASE}top-ellipse.svg`

// 新品数据 - 使用纯字符串路径
const newItems = ref([
  { id: 1, name: 'Lorem ipsum dolor sit amet consectetur.', price: 17.00, image: `${ASSET_BASE}new-item-1.png` },
  { id: 2, name: 'Lorem ipsum dolor sit amet consectetur.', price: 32.00, image: `${ASSET_BASE}new-item-2.png` },
  { id: 3, name: 'Lorem ipsum dolor sit amet consectetur.', price: 21.00, image: `${ASSET_BASE}new-item-3.png` },
])

// Flash Sale 数据 - 使用纯字符串路径
const flashSaleItems = ref([
  { id: 1, name: 'Product 1', price: 17.00, originalPrice: 21.25, image: `${ASSET_BASE}flash-1.png`, discount: '-20%' },
  { id: 2, name: 'Product 2', price: 17.00, originalPrice: 21.25, image: `${ASSET_BASE}flash-2.png`, discount: '-20%' },
  { id: 3, name: 'Product 3', price: 17.00, originalPrice: 21.25, image: `${ASSET_BASE}flash-3.png`, discount: '-20%' },
  { id: 4, name: 'Product 4', price: 17.00, originalPrice: 21.25, image: `${ASSET_BASE}flash-4.png`, discount: '-20%' },
  { id: 5, name: 'Product 5', price: 17.00, originalPrice: 21.25, image: `${ASSET_BASE}flash-5.png`, discount: '-20%' },
  { id: 6, name: 'Product 6', price: 17.00, originalPrice: 21.25, image: `${ASSET_BASE}flash-6.png`, discount: '-20%' },
])

// Most Popular 数据 - 使用纯字符串路径 (根据 Figma 2-7020)
const mostPopularItems = ref([
  { id: 1, name: 'New', price: 17.00, sales: 1780, image: `${ASSET_BASE}popular-new.png`, tag: 'New' },
  { id: 2, name: 'Sale', price: 17.00, sales: 1780, image: `${ASSET_BASE}popular-sale.png`, tag: 'Sale' },
  { id: 3, name: 'Hot', price: 17.00, sales: 1780, image: `${ASSET_BASE}popular-hot-1.png`, tag: 'Hot' },
  { id: 4, name: 'Hot', price: 17.00, sales: 1780, image: `${ASSET_BASE}popular-hot-2.png`, tag: 'Hot' },
])

// Just For You 数据 - 使用纯字符串路径 (根据 Figma 2-7021)
const justForYouItems = ref([
  { id: 1, name: 'Lorem ipsum dolor sit amet consectetur', price: 17.00, image: `${ASSET_BASE}jfy-1.png` },
  { id: 2, name: 'Lorem ipsum dolor sit amet consectetur', price: 17.00, image: `${ASSET_BASE}jfy-2.png` },
  { id: 3, name: 'Lorem ipsum dolor sit amet consectetur', price: 17.00, image: `${ASSET_BASE}jfy-3.png` },
  { id: 4, name: 'Lorem ipsum dolor sit amet consectetur', price: 17.00, image: `${ASSET_BASE}jfy-4.png` },
])

// 倒计时
const timeLeft = ref({
  minutes: 0,
  seconds: 36,
  milliseconds: 58,
})

// Banner 轮播数据 - 4 组不同的促销 Banner
const banners = ref([
  {
    id: 1,
    title: 'Big Sale',
    subtitle: 'Up to 50%',
    description: 'Happening\nNow',
    gradient: 'from-[#ff6b6b] to-[#ee5a5a]',
    image: `${ASSET_BASE}banner-main.png`
  },
  {
    id: 2,
    title: 'New Arrival',
    subtitle: 'Spring Collection',
    description: 'Shop Now\n2026',
    gradient: 'from-[#4ecdc4] to-[#44a08d]',
    image: `${ASSET_BASE}cat-clothing-1.png`
  },
  {
    id: 3,
    title: 'Flash Deal',
    subtitle: 'Limited Time',
    description: '70% OFF\nToday',
    gradient: 'from-[#ff9a9e] to-[#fecfef]',
    image: `${ASSET_BASE}cat-shoes-1.svg`
  },
  {
    id: 4,
    title: 'Hot Deals',
    subtitle: 'Best Sellers',
    description: 'Don\'t Miss\nOut',
    gradient: 'from-[#a18cd1] to-[#fbc2eb]',
    image: `${ASSET_BASE}cat-watch-1.png`
  }
])

// Banner 轮播状态
const currentBannerIndex = ref(0)
let bannerTimer: ReturnType<typeof setInterval> | null = null

// 切换到下一个 Banner
const nextBanner = () => {
  currentBannerIndex.value = (currentBannerIndex.value + 1) % banners.value.length
}

// 启动自动轮播
const startBannerAutoPlay = () => {
  if (bannerTimer) clearInterval(bannerTimer)
  bannerTimer = setInterval(() => {
    nextBanner()
  }, 3000) // 每 3 秒切换一次
}

// 停止自动轮播
const stopBannerAutoPlay = () => {
  if (bannerTimer) {
    clearInterval(bannerTimer)
    bannerTimer = null
  }
}

onMounted(() => {
  startBannerAutoPlay()
})

// 格式化时间
const formatTime = (value: number) => {
  return value.toString().padStart(2, '0')
}

// 处理搜索
const handleSearch = () => {
  router.push('/search')
}

// 处理分类点击
const handleCategoryClick = (categoryId: number) => {
  router.push(`/category/${categoryId}`)
}

// 处理商品点击
const handleProductClick = (productId: number) => {
  router.push(`/product/${productId}`)
}

// 获取 Status Bar 和导航图标（这些是必需的，数量少）
const statusBarIcon = `${ASSET_BASE}bars-status-bar.svg`
const batteryBorder = `${ASSET_BASE}battery-border.svg`
const batteryCap = `${ASSET_BASE}battery-cap.svg`
const batteryCapacity = `${ASSET_BASE}battery-capacity.svg`
const wifiIcon = `${ASSET_BASE}wifi.svg`
const cellularIcon = `${ASSET_BASE}cellular.svg`
const timeBackground = `${ASSET_BASE}time-background.svg`
const searchIcon = `${ASSET_BASE}search-icon.svg`

// Bottom Navigation 图标
const navShoppingBag = `${ASSET_BASE}nav-shopping-bag.svg`
const navPath335 = `${ASSET_BASE}nav-path-335.svg`
const navPath336 = `${ASSET_BASE}nav-path-336.svg`
const navMark01 = `${ASSET_BASE}nav-mark-01.svg`
const navHeart = `${ASSET_BASE}nav-heart.svg`
const navRectangle = `${ASSET_BASE}nav-rectangle.svg`
const navMenu = `${ASSET_BASE}nav-menu.svg`
const navHome = `${ASSET_BASE}nav-home.svg`
const navPath328 = `${ASSET_BASE}nav-path-328.svg`
const navMark2 = `${ASSET_BASE}nav-mark-2.svg`
const navProfile = `${ASSET_BASE}nav-profile.svg`

// Banner 相关的资源（用于轮播背景）
const bannerControls = `${ASSET_BASE}banner-controls.svg`

// 其他必要的 SVG
const newItemImage1 = `${ASSET_BASE}new-item-mask.svg`
const newItemImage2 = `${ASSET_BASE}new-item-mask-2.svg`
const flashClock = `${ASSET_BASE}flash-clock.svg`
const popularLike = `${ASSET_BASE}popular-like.svg`
const popularStar = `${ASSET_BASE}popular-star.svg`
const popularArrow = `${ASSET_BASE}popular-arrow.svg`
const newItemsButton = `${ASSET_BASE}new-items-button-fc7972cc-e06e-4935-bcdf-d465906914fe.svg`
const newItemsArrow = `${ASSET_BASE}new-items-arrow-df2c4a56-56c0-4270-a7a8-e5cbde0cfc45.svg`
</script>

<template>
  <div class="bg-white relative w-[375px] h-[817px] mx-auto overflow-hidden" data-name="15 Shop" data-node-id="0:11012">
    <!-- Status Bar -->
    <StatusBar />

    <!-- Header -->
    <div class="absolute top-[56px] left-[20px] right-[20px]">
      <h1 class="font-raleway font-bold text-[28px] text-brand-black tracking-[-0.28px]">Shop</h1>

      <!-- Search Bar -->
      <div class="absolute top-[48px] right-0 w-[248px] h-[36px]">
        <div class="absolute inset-0 bg-[#f8f8f8] rounded-[18px]" />
        <p class="absolute font-raleway font-medium text-[16px] text-[#c7c7c7] left-[16px] top-1/2 -translate-y-1/2 tracking-[-0.16px]">
          Search
        </p>
        <div class="absolute right-[18px] top-1/2 -translate-y-1/2 w-[18px] h-[18px]">
          <img :src="searchIcon" alt="Search" class="w-full h-full object-contain" />
        </div>
      </div>
    </div>

    <!-- Main Content (Scrollable) -->
    <div class="absolute top-[109px] left-0 right-0 bottom-[84px] overflow-y-auto">

      <!-- Big Sale Banner (轮播) -->
      <div class="relative h-[150px] mx-[20px] mb-[31px]" data-name="Big Sale Banner" data-node-id="0:11382">
        <div class="absolute inset-0 rounded-[9px] overflow-hidden">
          <!-- Banner 轮播容器 -->
          <transition-group name="banner-fade" tag="div" class="relative h-full">
            <div
              v-for="(banner, index) in banners"
              :key="banner.id"
              v-show="index === currentBannerIndex"
              class="absolute inset-0 h-full"
            >
              <!-- Banner Background -->
              <div :class="`absolute inset-0 bg-gradient-to-r ${banner.gradient}`" />
              <!-- Banner Image (叠加效果) -->
              <img :src="banner.image" :alt="banner.title" class="absolute inset-0 w-full h-full object-cover opacity-30" />

              <!-- Banner Content -->
              <div class="absolute inset-0 p-4">
                <p class="font-raleway font-bold text-[29px] text-white tracking-[-0.29px]">{{ banner.title }}</p>
                <p class="font-nunito-sans font-bold text-[12px] text-white mt-1">{{ banner.subtitle }}</p>
                <p class="font-raleway font-bold text-[11px] text-white mt-1 tracking-[-0.11px] whitespace-pre">{{ banner.description }}</p>
              </div>
            </div>
          </transition-group>

          <!-- Carousel Dots (可点击切换) -->
          <div class="absolute bottom-[10px] left-1/2 -translate-x-1/2 flex items-center gap-2">
            <button
              v-for="(banner, index) in banners"
              :key="banner.id"
              @click="currentBannerIndex = index"
              @mouseenter="stopBannerAutoPlay"
              @mouseleave="startBannerAutoPlay"
              :class="[
                'w-2 h-2 rounded-full transition-all duration-300',
                index === currentBannerIndex ? 'bg-white w-4' : 'bg-white/50'
              ]"
              :aria-label="`Go to slide ${index + 1}`"
            />
          </div>
        </div>
      </div>

      <!-- Categories Section -->
      <div class="px-[20px] mb-[31px]" data-name="Categories" data-node-id="8:5190">
        <div class="flex items-center justify-between mb-4">
          <h2 class="font-raleway font-bold text-[21px] text-brand-black tracking-[-0.21px]">Categories</h2>
          <div class="flex items-center gap-1">
            <span class="font-raleway font-bold text-[15px] text-brand-black">See All</span>
            <div class="w-[27px] h-[27px] bg-brand-blue rounded-full flex items-center justify-center">
              <svg width="14" height="14" viewBox="0 0 24 24" fill="none">
                <path d="M9 18L15 12L9 6" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
              </svg>
            </div>
          </div>
        </div>

        <!-- Category Grid - First Row -->
        <div class="grid grid-cols-2 gap-4">
          <template v-for="category in categories" :key="category.id">
            <div class="bg-white rounded-[9px] shadow-[0px_5px_10px_0px_rgba(0,0,0,0.1)] p-3 cursor-pointer" @click="handleCategoryClick(category.id)">
              <div class="grid grid-cols-2 gap-2 mb-2">
                <div v-for="(img, idx) in category.images" :key="idx" class="w-full aspect-square rounded-[5px] overflow-hidden">
                  <img :src="`${ASSET_BASE}${img}`" :alt="`${category.name} ${idx + 1}`" class="w-full h-full object-cover" />
                </div>
              </div>
              <div class="flex items-center justify-between">
                <h3 class="font-raleway font-bold text-[17px] text-brand-black">{{ category.name }}</h3>
                <div class="bg-[#dfe9ff] px-2 py-0.5 rounded-[6px]">
                  <span class="font-raleway font-bold text-[12px] text-brand-black">{{ category.count }}</span>
                </div>
              </div>
            </div>
          </template>
        </div>
      </div>

      <!-- Top Products -->
      <div class="px-[20px] mb-[31px]" data-name="Top Products">
        <h2 class="font-raleway font-bold text-[21px] text-brand-black tracking-[-0.21px] mb-4">Top Products</h2>
        <!-- 5 个商品等分布局 - 每个约 60px 宽度 -->
        <div class="flex justify-between items-center">
          <div v-for="item in topProducts" :key="item.id" class="flex-shrink-0 w-[60px] text-center">
            <div class="relative w-[60px] h-[60px] mx-auto mb-2">
              <!-- 圆形阴影背景 -->
              <img :src="topEllipse" :alt="item.name" class="absolute inset-0 w-full h-full" />
              <!-- 商品图片 - 使用圆形裁剪 -->
              <div class="absolute inset-[5px] w-[50px] h-[50px] rounded-full overflow-hidden">
                <img :src="item.image" :alt="item.name" class="w-full h-full object-cover" />
              </div>
            </div>
            <p class="font-raleway font-medium text-[13px] text-brand-black text-center tracking-[-0.13px]">{{ item.name }}</p>
          </div>
        </div>
      </div>

      <!-- New Items -->
      <div class="px-[20px] mb-[31px]" data-name="New Items">
        <div class="flex items-center justify-between mb-4">
          <h2 class="font-raleway font-bold text-[21px] text-brand-black tracking-[-0.21px]">New Items</h2>
          <div class="flex items-center gap-1">
            <span class="font-raleway font-bold text-[15px] text-brand-black">See All</span>
            <div class="w-[27px] h-[27px] bg-brand-blue rounded-full flex items-center justify-center">
              <img :src="newItemsArrow" alt="Arrow" class="w-[14px] h-[14px]" />
            </div>
          </div>
        </div>
        <div class="grid grid-cols-3 gap-3">
          <div v-for="item in newItems" :key="item.id" class="bg-white rounded-[9px] shadow-[0px_5px_10px_0px_rgba(0,0,0,0.1)] p-3 cursor-pointer" @click="handleProductClick(item.id)">
            <div class="relative mb-2">
              <div class="aspect-square rounded-[5px] overflow-hidden bg-[#f8f8f8]">
                <img :src="item.image" :alt="item.name" class="w-full h-full object-cover" />
              </div>
            </div>
            <p class="font-nunito-sans font-normal text-[12px] text-brand-black h-[36px] leading-[16px]">{{ item.name }}</p>
            <p class="font-raleway font-bold text-[17px] text-brand-black mt-1">${{ item.price.toFixed(2).replace('.', ',') }}</p>
          </div>
        </div>
      </div>

      <!-- Flash Sale -->
      <div class="px-[20px] mb-[31px]" data-name="Flash Sale">
        <div class="flex items-center justify-between mb-4">
          <div class="flex items-center gap-2">
            <h2 class="font-raleway font-bold text-[21px] text-brand-black tracking-[-0.21px]">Flash Sale</h2>
            <div class="w-[82px] h-[30px] relative">
              <img :src="flashClock" alt="Clock" class="w-[24px] h-[24px] absolute top-[3px] left-[3px]" />
              <div class="absolute top-[6px] left-[32px] font-nunito font-light text-[17px] text-brand-black">
                {{ formatTime(timeLeft.minutes) }}:{{ formatTime(timeLeft.seconds) }}:{{ formatTime(timeLeft.milliseconds) }}
              </div>
            </div>
          </div>
        </div>
        <div class="grid grid-cols-3 gap-3">
          <div v-for="item in flashSaleItems" :key="item.id" class="bg-white rounded-[9px] shadow-[0px_5px_10px_0px_rgba(0,0,0,0.1)] p-3 cursor-pointer" @click="handleProductClick(item.id)">
            <div class="relative mb-2">
              <div class="aspect-square rounded-[5px] overflow-hidden bg-[#f8f8f8]">
                <img :src="item.image" :alt="item.name" class="w-full h-full object-cover" />
              </div>
              <!-- Discount Badge -->
              <div class="absolute top-2 right-2 w-[45px] h-[20px] bg-gradient-to-r from-[#ff5790] to-[#f81140] rounded-full flex items-center justify-center">
                <span class="font-raleway font-bold text-[11px] text-white -translate-x-[1px]">{{ item.discount }}</span>
              </div>
            </div>
            <p class="font-raleway font-medium text-[15px] text-brand-black truncate">${{ item.price.toFixed(2) }}</p>
            <p class="font-nunito font-light text-[12px] text-[#a0a0a0] line-through">${{ item.originalPrice.toFixed(2) }}</p>
          </div>
        </div>
      </div>

      <!-- Most Popular -->
      <div class="px-[20px] mb-[31px]" data-name="Most Popular">
        <h2 class="font-raleway font-bold text-[21px] text-brand-black tracking-[-0.21px] mb-4">Most Popular</h2>
        <div class="grid grid-cols-2 gap-4">
          <div v-for="item in mostPopularItems" :key="item.id" class="bg-white rounded-[9px] shadow-[0px_5px_10px_0px_rgba(0,0,0,0.1)] p-3 cursor-pointer" @click="handleProductClick(item.id)">
            <div class="relative mb-2">
              <div class="aspect-square rounded-[5px] overflow-hidden bg-[#f8f8f8]">
                <img :src="item.image" :alt="item.name" class="w-full h-full object-cover" />
              </div>
              <!-- Tag Badge -->
              <div class="absolute top-2 left-2 px-2 py-0.5 rounded-[4px] bg-[#ff5790]">
                <span class="font-raleway font-bold text-[10px] text-white">{{ item.tag }}</span>
              </div>
              <!-- Like Button -->
              <button class="absolute top-2 right-2 w-[28px] h-[28px] bg-white rounded-full flex items-center justify-center shadow-md">
                <img :src="popularLike" alt="Like" class="w-[16px] h-[16px]" />
              </button>
            </div>
            <p class="font-raleway font-medium text-[15px] text-brand-black truncate">${{ item.price.toFixed(2) }}</p>
            <div class="flex items-center justify-between mt-1">
              <div class="flex items-center gap-1">
                <img :src="popularStar" alt="Star" class="w-[12px] h-[12px]" />
                <span class="font-raleway font-bold text-[11px] text-brand-black">{{ item.sales }}</span>
              </div>
              <img :src="popularArrow" alt="Arrow" class="w-[16px] h-[16px]" />
            </div>
          </div>
        </div>
      </div>

      <!-- Just For You -->
      <div class="px-[20px] mb-[31px]" data-name="Just For You">
        <h2 class="font-raleway font-bold text-[21px] text-brand-black tracking-[-0.21px] mb-4">Just For You</h2>
        <div class="grid grid-cols-2 gap-4">
          <div v-for="item in justForYouItems" :key="item.id" class="bg-white rounded-[9px] shadow-[0px_5px_10px_0px_rgba(0,0,0,0.1)] p-3 cursor-pointer" @click="handleProductClick(item.id)">
            <div class="mb-2">
              <div class="aspect-square rounded-[5px] overflow-hidden bg-[#f8f8f8]">
                <img :src="item.image" :alt="item.name" class="w-full h-full object-cover" />
              </div>
            </div>
            <p class="font-raleway font-medium text-[15px] text-brand-black truncate">${{ item.price.toFixed(2) }}</p>
            <div class="flex items-center justify-between mt-1">
              <div class="flex items-center gap-1">
                <img :src="popularStar" alt="Star" class="w-[12px] h-[12px]" />
                <span class="font-raleway font-bold text-[11px] text-brand-black">4.5</span>
              </div>
              <img :src="popularArrow" alt="Arrow" class="w-[16px] h-[16px]" />
            </div>
          </div>
        </div>
      </div>

    </div>

    <!-- Bottom Navigation -->
    <div class="absolute bottom-0 left-0 right-0 h-[84px] bg-white shadow-[0px_-4px_10px_0px_rgba(0,0,0,0.05)] flex items-end pb-5 px-6" data-name="Bottom Navigation">
      <button
        v-for="tab in [{id: 'shop', icon: navHome, label: 'Home'}, {id: 'cart', icon: navShoppingBag, label: 'Cart'}, {id: 'categories', icon: navMenu, label: 'Categories'}, {id: 'wishlist', icon: navHeart, label: 'Wishlist'}, {id: 'profile', icon: navProfile, label: 'Profile'}]"
        :key="tab.id"
        @click="activeTab = tab.id"
        class="flex-1 flex flex-col items-center gap-1 cursor-pointer"
      >
        <img :src="tab.icon" :alt="tab.label" class="w-[24px] h-[24px]" />
        <span class="font-nunito font-light text-[11px] text-brand-black">{{ tab.label }}</span>
      </button>
    </div>

    <!-- Home Indicator -->
    <HomeIndicator />
  </div>
</template>

<style scoped>
/* Banner 轮播淡入淡出动画 */
.banner-fade-enter-active,
.banner-fade-leave-active {
  transition: opacity 0.5s ease;
}

.banner-fade-enter-from,
.banner-fade-leave-to {
  opacity: 0;
}
</style>
