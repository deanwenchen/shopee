<script setup lang="ts">
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'

const router = useRouter()
const activeTab = ref('shop')

// 分类数据
const categories = ref([
  { id: 1, name: 'Clothing', image: 'src/assets/figma/532C6Dcf29Ca4Fb0A2E404F0Ac1D802A.svg', count: 109 },
  { id: 2, name: 'Bags', image: 'src/assets/figma/0F26E045Aa024Eb783Bb94Ded23C9E45.svg', count: 87 },
  { id: 3, name: 'Shoes', image: 'src/assets/figma/A70864C81B1F401484A4450Cd75C9Cef.svg', count: 530 },
  { id: 4, name: 'Lingerie', image: 'src/assets/figma/F8382341Af5B48F9B40187Fe74Febc69.svg', count: 218 },
  { id: 5, name: 'Watch', image: 'src/assets/figma/F2E8E4C3E5F745Bc9Dfd34A479B8D2B7.svg', count: 218 },
  { id: 6, name: 'Hoodies', image: 'src/assets/figma/F8382341Af5B48F9B40187Fe74Febc70.svg', count: 218 },
  { id: 7, name: 'Dresses', image: 'src/assets/figma/83Ad853C1A81400B8Af38E08589Ab454.svg', count: 156 },
  { id: 8, name: 'T-Shirts', image: 'src/assets/figma/F2E8E4C3E5F745Bc9Dfd34A479B8D2B8.svg', count: 243 },
])

// Top Products 分类
const topProducts = ref([
  { id: 1, name: 'Dresses', image: 'src/assets/figma/657021186Be84555860C895D4D1Da870.svg' },
  { id: 2, name: 'T-Shirts', image: 'src/assets/figma/657021186Be84555860C895D4D1Da870.svg' },
  { id: 3, name: 'Skirts', image: 'src/assets/figma/657021186Be84555860C895D4D1Da870.svg' },
  { id: 4, name: 'Shoes', image: 'src/assets/figma/657021186Be84555860C895D4D1Da870.svg' },
  { id: 5, name: 'Bags', image: 'src/assets/figma/657021186Be84555860C895D4D1Da870.svg' },
])

// 新品数据
const newItems = ref([
  { id: 1, name: 'Lorem ipsum dolor sit amet consectetur.', price: 17.00, image: 'src/assets/figma/6D305338A2A1480EAebb80A9382518E9.svg' },
  { id: 2, name: 'Lorem ipsum dolor sit amet consectetur.', price: 32.00, image: 'src/assets/figma/A70864C81B1F401484A4450Cd75C9Cef.svg' },
  { id: 3, name: 'Lorem ipsum dolor sit amet consectetur.', price: 21.00, image: 'src/assets/figma/Ddf8E2Ef1A434C29Bb9C4548488D9378.svg' },
])

// Flash Sale 数据
const flashSaleItems = ref([
  { id: 1, name: 'Product 1', price: 17.00, originalPrice: 21.25, image: 'src/assets/figma/2289C231211F4850B7Af5Ef0F942B4F7.svg', discount: '-20%' },
  { id: 2, name: 'Product 2', price: 17.00, originalPrice: 21.25, image: 'src/assets/figma/098576F0C6F34E058E3F2D9Cd12D2574.svg', discount: '-20%' },
  { id: 3, name: 'Product 3', price: 17.00, originalPrice: 21.25, image: 'src/assets/figma/922654839E7E4Fc3A35516Cca677C11C.svg', discount: '-20%' },
  { id: 4, name: 'Product 4', price: 17.00, originalPrice: 21.25, image: 'src/assets/figma/45D808E05E004D2A9Bdc5Ed6F427010E.svg', discount: '-20%' },
  { id: 5, name: 'Product 5', price: 17.00, originalPrice: 21.25, image: 'src/assets/figma/Cc0Db45D73F84F6BA6D6B9E811283C39.svg', discount: '-20%' },
  { id: 6, name: 'Product 6', price: 17.00, originalPrice: 21.25, image: 'src/assets/figma/333Cbbca93904C5AA60A21E776Bf77D2.svg', discount: '-20%' },
])

// Most Popular 数据
const mostPopularItems = ref([
  { id: 1, name: 'New', price: 17.00, sales: 1780, image: 'src/assets/figma/098576F0C6F34E058E3F2D9Cd12D2574.svg', tag: 'New' },
  { id: 2, name: 'Sale', price: 17.00, sales: 1780, image: 'src/assets/figma/2289C231211F4850B7Af5Ef0F942B4F7.svg', tag: 'Sale' },
  { id: 3, name: 'Hot', price: 17.00, sales: 1780, image: 'src/assets/figma/878Cf2Fb31Fe4B3FBf9EF96Bb5F076Ce.svg', tag: 'Hot' },
  { id: 4, name: 'Hot', price: 17.00, sales: 1780, image: 'src/assets/figma/Cc0Db45D73F84F6BA6D6B9E811283C39.svg', tag: 'Hot' },
])

// Just For You 数据
const justForYouItems = ref([
  { id: 1, name: 'Lorem ipsum dolor sit amet consectetur', price: 17.00, image: 'src/assets/figma/Bd96C0A49A694373Ba737898758375Ef.svg' },
  { id: 2, name: 'Lorem ipsum dolor sit amet consectetur', price: 17.00, image: 'src/assets/figma/9C2B30Eb4D4C49318Faf06F95297D258.svg' },
  { id: 3, name: 'Lorem ipsum dolor sit amet consectetur', price: 17.00, image: 'src/assets/figma/333Cbbca93904C5AA60A21E776Bf77D2.svg' },
  { id: 4, name: 'Lorem ipsum dolor sit amet consectetur', price: 17.00, image: 'src/assets/figma/922654839E7E4Fc3A35516Cca677C11C.svg' },
])

// 倒计时
const timeLeft = ref({
  minutes: 0,
  seconds: 36,
  milliseconds: 58,
})

// Banner 轮播
const currentBannerIndex = ref(0)

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
          <svg width="18" height="18" viewBox="0 0 24 24" fill="none" class="text-gray-400">
            <path d="M11 19C15.4183 19 19 15.4183 19 11C19 6.58172 15.4183 3 11 3C6.58172 3 3 6.58172 3 11C3 15.4183 6.58172 19 11 19Z" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M21 21L16.65 16.65" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
          </svg>
        </div>
      </div>
    </div>

    <!-- Main Content (Scrollable) -->
    <div class="absolute top-[109px] left-0 right-0 bottom-[84px] overflow-y-auto">

      <!-- Big Sale Banner -->
      <div class="relative h-[150px] mx-[20px] mb-[31px]" data-name="Big Sale Banner" data-node-id="0:11382">
        <div class="absolute inset-0 rounded-[9px] overflow-hidden">
          <!-- Banner Background with Gradient -->
          <div class="absolute inset-0 bg-gradient-to-r from-orange-400 to-orange-300" />

          <!-- Bubble Decorations -->
          <div class="absolute inset-0 overflow-hidden">
            <div class="absolute top-[-50px] right-[-50px] w-[200px] h-[200px] bg-white/20 rounded-full" />
            <div class="absolute bottom-[-30px] left-[100px] w-[150px] h-[150px] bg-white/15 rounded-full" />
          </div>

          <!-- Banner Content -->
          <div class="absolute inset-0 p-4">
            <p class="font-raleway font-bold text-[29px] text-white tracking-[-0.29px]">Big Sale</p>
            <p class="font-nunito-sans font-bold text-[12px] text-white mt-1">Up to 50%</p>
            <p class="font-raleway font-bold text-[11px] text-white mt-1 tracking-[-0.11px]">Happening Now</p>

            <!-- Carousel Dots -->
            <div class="absolute bottom-[10px] left-1/2 -translate-x-1/2 flex gap-1">
              <div class="w-[6px] h-[6px] rounded-full bg-white" />
              <div class="w-[6px] h-[6px] rounded-full bg-white/40" />
              <div class="w-[6px] h-[6px] rounded-full bg-white/40" />
            </div>
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

        <!-- Category Grid -->
        <div class="grid grid-cols-2 gap-4">
          <!-- Clothing -->
          <div
            v-for="category in categories.slice(0, 4)"
            :key="category.id"
            class="bg-white rounded-[9px] shadow-[0px_5px_10px_0px_rgba(0,0,0,0.1)] p-3 cursor-pointer"
            @click="handleCategoryClick(category.id)"
          >
            <div class="grid grid-cols-2 gap-2 mb-2">
              <div v-for="i in 4" :key="i" class="w-full aspect-square rounded-[5px] bg-gray-100 overflow-hidden">
                <div class="w-full h-full bg-gradient-to-br from-pink-200 to-purple-200" />
              </div>
            </div>
            <div class="flex items-center justify-between">
              <h3 class="font-raleway font-bold text-[17px] text-brand-black">{{ category.name }}</h3>
              <div class="bg-[#dfe9ff] px-2 py-0.5 rounded-[6px]">
                <span class="font-raleway font-bold text-[12px] text-brand-black">{{ category.count }}</span>
              </div>
            </div>
          </div>
        </div>

        <!-- Second Row -->
        <div class="grid grid-cols-2 gap-4 mt-4">
          <div
            v-for="category in categories.slice(4, 8)"
            :key="category.id"
            class="bg-white rounded-[9px] shadow-[0px_5px_10px_0px_rgba(0,0,0,0.1)] p-3 cursor-pointer"
            @click="handleCategoryClick(category.id)"
          >
            <div class="grid grid-cols-2 gap-2 mb-2">
              <div v-for="i in 4" :key="i" class="w-full aspect-square rounded-[5px] bg-gray-100 overflow-hidden">
                <div class="w-full h-full bg-gradient-to-br from-blue-200 to-green-200" />
              </div>
            </div>
            <div class="flex items-center justify-between">
              <h3 class="font-raleway font-bold text-[17px] text-brand-black">{{ category.name }}</h3>
              <div class="bg-[#dfe9ff] px-2 py-0.5 rounded-[6px]">
                <span class="font-raleway font-bold text-[12px] text-brand-black">{{ category.count }}</span>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Top Products -->
      <div class="px-[20px] mb-[31px]" data-name="Top Products" data-node-id="0:11214">
        <h2 class="font-raleway font-bold text-[21px] text-brand-black tracking-[-0.21px] mb-4">Top Products</h2>

        <div class="flex gap-[20px] overflow-x-auto pb-2">
          <div
            v-for="(product, index) in topProducts"
            :key="product.id"
            class="flex flex-col items-center flex-shrink-0"
          >
            <div class="relative w-[60px] h-[60px] mb-1">
              <!-- Ellipse Background -->
              <div class="absolute inset-0">
                <svg viewBox="0 0 60 60" class="w-full h-full">
                  <ellipse cx="30" cy="35" rx="25" ry="20" fill="#FFE5E5" />
                </svg>
              </div>
              <!-- Product Image Placeholder -->
              <div class="absolute inset-[8px] rounded-full overflow-hidden bg-white">
                <div class="w-full h-full bg-gradient-to-br from-pink-300 to-orange-200" />
              </div>
            </div>
            <p class="font-raleway font-medium text-[13px] text-brand-black tracking-[-0.13px] text-center">{{ product.name }}</p>
          </div>
        </div>
      </div>

      <!-- New Items -->
      <div class="px-[20px] mb-[31px]" data-name="New Items" data-node-id="2:7017">
        <div class="flex items-center justify-between mb-4">
          <h2 class="font-raleway font-bold text-[21px] text-brand-black tracking-[-0.21px]">New Items</h2>
          <div class="flex items-center gap-1">
            <span class="font-raleway font-bold text-[15px] text-brand-black">See All</span>
            <div class="w-[27px] h-[27px] bg-brand-blue rounded-full flex items-center justify-center">
              <svg width="14" height="14" viewBox="0 0 24 24" fill="none">
                <path d="M9 18L15 12L9 6" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
              </svg>
            </div>
          </div>
        </div>

        <div class="grid grid-cols-3 gap-3">
          <div
            v-for="item in newItems"
            :key="item.id"
            class="bg-white rounded-[9px] shadow-[0px_5px_10px_0px_rgba(0,0,0,0.1)] p-3 cursor-pointer"
            @click="handleProductClick(item.id)"
          >
            <div class="w-full aspect-square rounded-[5px] bg-gray-100 mb-2 overflow-hidden">
              <div class="w-full h-full bg-gradient-to-br from-blue-300 to-cyan-200" />
            </div>
            <p class="font-nunito-sans text-[12px] text-black line-clamp-2 h-[32px] overflow-hidden">{{ item.name }}</p>
            <p class="font-raleway font-bold text-[17px] text-brand-black mt-1">${{ item.price.toFixed(2) }}</p>
          </div>
        </div>
      </div>

      <!-- Flash Sale -->
      <div class="px-[20px] mb-[31px]" data-name="Flash Sale" data-node-id="9:5463">
        <div class="flex items-center justify-between mb-4">
          <div class="flex items-center gap-2">
            <h2 class="font-raleway font-bold text-[21px] text-brand-black tracking-[-0.21px]">Flash Sale</h2>
            <div class="w-[27px] h-[27px]">
              <svg viewBox="0 0 24 24" fill="none">
                <circle cx="12" cy="12" r="10" stroke="#FF5790" stroke-width="2"/>
                <path d="M12 6V12L16 14" stroke="#FF5790" stroke-width="2" stroke-linecap="round"/>
              </svg>
            </div>
          </div>
          <!-- Timer -->
          <div class="flex gap-1">
            <div class="bg-[#f3f3f3] rounded-[7px] px-2 py-1">
              <span class="font-raleway font-bold text-[17px] text-brand-black">{{ formatTime(timeLeft.minutes) }}</span>
            </div>
            <div class="bg-[#f3f3f3] rounded-[7px] px-2 py-1">
              <span class="font-raleway font-bold text-[17px] text-brand-black">{{ formatTime(timeLeft.seconds) }}</span>
            </div>
            <div class="bg-[#f3f3f3] rounded-[7px] px-2 py-1">
              <span class="font-raleway font-bold text-[17px] text-brand-black">{{ formatTime(timeLeft.milliseconds) }}</span>
            </div>
          </div>
        </div>

        <div class="grid grid-cols-3 gap-3">
          <div
            v-for="item in flashSaleItems"
            :key="item.id"
            class="bg-white rounded-[9px] shadow-[0px_5px_10px_0px_rgba(0,0,0,0.1)] p-3 cursor-pointer relative"
            @click="handleProductClick(item.id)"
          >
            <!-- Discount Badge -->
            <div class="absolute top-2 right-2 bg-gradient-to-l from-[#ff5790] to-[#f81140] px-1.5 py-0.5 rounded-tl-[5px] rounded-bl-[5px] rounded-tr-[5px]">
              <span class="font-raleway font-bold text-[13px] text-white">{{ item.discount }}</span>
            </div>

            <div class="w-full aspect-square rounded-[5px] bg-gray-100 mb-2 overflow-hidden">
              <div class="w-full h-full bg-gradient-to-br from-pink-200 to-red-200" />
            </div>
            <p class="font-raleway font-bold text-[17px] text-brand-black">${{ item.price.toFixed(2) }}</p>
          </div>
        </div>
      </div>

      <!-- Most Popular -->
      <div class="px-[20px] mb-[31px]" data-name="Most Popular" data-node-id="2:7020">
        <div class="flex items-center justify-between mb-4">
          <h2 class="font-raleway font-bold text-[21px] text-brand-black tracking-[-0.21px]">Most Popular</h2>
          <div class="flex items-center gap-1">
            <span class="font-raleway font-bold text-[15px] text-brand-black">See All</span>
            <div class="w-[27px] h-[27px] bg-brand-blue rounded-full flex items-center justify-center">
              <svg width="14" height="14" viewBox="0 0 24 24" fill="none">
                <path d="M9 18L15 12L9 6" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
              </svg>
            </div>
          </div>
        </div>

        <div class="grid grid-cols-4 gap-2">
          <div
            v-for="item in mostPopularItems"
            :key="item.id"
            class="bg-white rounded-[9px] shadow-[0px_5px_10px_0px_rgba(0,0,0,0.1)] p-2 cursor-pointer"
            @click="handleProductClick(item.id)"
          >
            <div class="relative">
              <div class="w-full aspect-square rounded-[5px] bg-gray-100 mb-2 overflow-hidden">
                <div class="w-full h-full bg-gradient-to-br from-pink-200 to-purple-200" />
              </div>
              <!-- Tag -->
              <span class="absolute top-1 right-1 font-raleway font-medium text-[11px]" :class="item.tag === 'New' ? 'text-green-600' : item.tag === 'Sale' ? 'text-orange-600' : 'text-red-600'">
                {{ item.tag }}
              </span>
              <!-- Like Icon -->
              <div class="absolute top-1 left-1 w-[16px] h-[16px]">
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                  <path d="M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z"/>
                </svg>
              </div>
            </div>
            <div class="flex items-center justify-between">
              <span class="font-raleway font-bold text-[15px] text-black">{{ item.sales }}</span>
            </div>
            <p class="font-raleway font-bold text-[15px] text-brand-black mt-1">${{ item.price.toFixed(2) }}</p>
          </div>
        </div>
      </div>

      <!-- Just For You -->
      <div class="px-[20px] mb-[20px]" data-name="Just for You" data-node-id="2:7021">
        <div class="flex items-center gap-2 mb-4">
          <h2 class="font-raleway font-bold text-[21px] text-brand-black tracking-[-0.21px]">Just For You</h2>
          <svg width="16" height="16" viewBox="0 0 24 24" fill="#FFD700">
            <path d="M12 2L15.09 8.26L22 9.27L17 14.14L18.18 21.02L12 17.77L5.82 21.02L7 14.14L2 9.27L8.91 8.26L12 2Z"/>
          </svg>
        </div>

        <div class="grid grid-cols-2 gap-3">
          <div
            v-for="item in justForYouItems.slice(0, 2)"
            :key="item.id"
            class="bg-white rounded-[9px] shadow-[0px_5px_10px_0px_rgba(0,0,0,0.1)] p-3 cursor-pointer"
            @click="handleProductClick(item.id)"
          >
            <div class="w-full h-[120px] rounded-[5px] bg-gray-100 mb-2 overflow-hidden">
              <div class="w-full h-full bg-gradient-to-br from-purple-200 to-pink-200" />
            </div>
            <p class="font-nunito-sans text-[12px] text-black line-clamp-2 h-[32px] overflow-hidden">{{ item.name }}</p>
            <p class="font-raleway font-bold text-[17px] text-brand-black mt-1">${{ item.price.toFixed(2) }}</p>
          </div>
        </div>
      </div>
    </div>

    <!-- Bottom Navigation -->
    <div class="absolute bottom-0 left-0 right-0 h-[84px] bg-white shadow-[0px_-1px_1px_0px_rgba(0,0,0,0.16)]" data-name="Bottom Bar" data-node-id="2:5227">
      <!-- Navigation Icons -->
      <div class="absolute inset-[16px_0_20px_0] flex items-center justify-around">
        <!-- Home/Shop - Active -->
        <div class="flex flex-col items-center">
          <div class="w-[24px] h-[24px] relative">
            <svg viewBox="0 0 24 24" fill="none" class="w-full h-full">
              <path d="M3 9L12 2L21 9V20C21 20.5304 20.7893 21.0391 20.4142 21.4142C20.0391 21.7893 19.5304 22 19 22H5C4.46957 22 3.96086 21.7893 3.58579 21.4142C3.21071 21.0391 3 20.5304 3 20V9Z" stroke="#004CFF" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
              <path d="M9 22V12H15V22" stroke="#004CFF" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
          </div>
          <div class="w-[9px] h-[5px] mt-1">
            <svg viewBox="0 0 9 5" fill="black">
              <path d="M1 1L4.5 4L8 1" stroke="black" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
          </div>
        </div>

        <!-- Cart -->
        <div class="flex flex-col items-center cursor-pointer" @click="router.push('/cart')">
          <div class="w-[24px] h-[24px] relative">
            <svg viewBox="0 0 24 24" fill="none" class="w-full h-full text-gray-400">
              <path d="M6 2L3 6V20C3 20.5304 3.21071 21.0391 3.58579 21.4142C3.96086 21.7893 4.46957 22 5 22H19C19.5304 22 20.0391 21.7893 20.4142 21.4142C20.7893 21.0391 21 20.5304 21 20V6L18 2H6Z" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
              <path d="M3 6H21" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
              <path d="M16 10C16 11.0609 15.5786 12.0783 14.8284 12.8284C14.0783 13.5786 13.0609 14 12 14C10.9391 14 9.92172 13.5786 9.17157 12.8284C8.42143 12.0783 8 11.0609 8 10" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
          </div>
        </div>

        <!-- Categories -->
        <div class="flex flex-col items-center cursor-pointer" @click="router.push('/categories')">
          <div class="w-[24px] h-[24px] relative">
            <svg viewBox="0 0 24 24" fill="none" class="w-full h-full text-gray-400">
              <rect x="3" y="3" width="7" height="7" rx="1" stroke="currentColor" stroke-width="2"/>
              <rect x="14" y="3" width="7" height="7" rx="1" stroke="currentColor" stroke-width="2"/>
              <rect x="3" y="14" width="7" height="7" rx="1" stroke="currentColor" stroke-width="2"/>
              <rect x="14" y="14" width="7" height="7" rx="1" stroke="currentColor" stroke-width="2"/>
            </svg>
          </div>
        </div>

        <!-- Wishlist -->
        <div class="flex flex-col items-center cursor-pointer" @click="router.push('/wishlist')">
          <div class="w-[24px] h-[24px] relative">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" class="w-full h-full text-gray-400">
              <path d="M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z"/>
            </svg>
          </div>
        </div>

        <!-- Profile -->
        <div class="flex flex-col items-center cursor-pointer" @click="router.push('/profile')">
          <div class="w-[24px] h-[24px] relative">
            <svg viewBox="0 0 24 24" fill="none" class="w-full h-full text-gray-400">
              <path d="M20 21V19C20 17.9391 19.5786 16.9217 18.8284 16.1716C18.0783 15.4214 17.0609 15 16 15H8C6.93913 15 5.92172 15.4214 5.17157 16.1716C4.42143 16.9217 4 17.9391 4 19V21" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
              <circle cx="12" cy="7" r="4" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
          </div>
        </div>
      </div>

      <!-- Home Indicator -->
      <HomeIndicator />
    </div>
  </div>
</template>

<style scoped>
/* Custom scrollbar styles */
.overflow-y-auto::-webkit-scrollbar {
  width: 0px;
}

.line-clamp-2 {
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
}
</style>
