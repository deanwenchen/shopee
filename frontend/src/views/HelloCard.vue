<script setup lang="ts">
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'

const router = useRouter()

// 引导数据
interface GuidePage {
  title: string
  description: string
  image: string
  hasButton?: boolean
}

const guides: GuidePage[] = [
  {
    title: 'Hello',
    description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed non consectetur turpis. Morbi eu eleifend lacus.',
    image: 'https://www.figma.com/api/mcp/asset/82d6a24a-030a-489c-aef1-0eaf43fe097a'
  },
  {
    title: 'Explore',
    description: 'Discover amazing products from top brands. Find exactly what you need with our smart search.',
    image: 'https://www.figma.com/api/mcp/asset/c4c9a2ec-0c31-4001-89aa-72516f6d1195'
  },
  {
    title: 'Choose',
    description: 'Add items to your wishlist and cart. Compare products and find the best deals.',
    image: 'https://www.figma.com/api/mcp/asset/c4c9a2ec-0c31-4001-89aa-72516f6d1195'
  },
  {
    title: 'Ready',
    description: 'You are all set! Start shopping and enjoy exclusive deals.',
    image: 'https://www.figma.com/api/mcp/asset/c4c9a2ec-0c31-4001-89aa-72516f6d1195',
    hasButton: true
  }
]

const currentPage = ref(0)
const touchStartX = ref(0)
const touchEndX = ref(0)

const SWIPE_THRESHOLD = 50

// 计算当前图片（根据页码循环使用设计资源）
const currentImage = computed(() => {
  // 第 0 页使用 Figma 设计的图片，其他页使用占位图片
  if (currentPage.value === 0) {
    return guides[0].image
  }
  return guides[currentPage.value].image
})

// 滑动处理
const handleTouchStart = (e: TouchEvent) => {
  touchStartX.value = e.touches[0].clientX
}

const handleTouchEnd = (e: TouchEvent) => {
  touchEndX.value = e.changedTouches[0].clientX
  handleSwipe()
}

const handleSwipe = () => {
  const deltaX = touchEndX.value - touchStartX.value

  if (Math.abs(deltaX) < SWIPE_THRESHOLD) return

  if (deltaX > 0 && currentPage.value > 0) {
    // 向右滑动 → 上一页
    currentPage.value--
  } else if (deltaX < 0 && currentPage.value < guides.length - 1) {
    // 向左滑动 → 下一页
    currentPage.value++
  }
}

// Dots 点击
const handleDotClick = (index: number) => {
  currentPage.value = index
}

// "Let's Go" 按钮
const handleLetsGo = () => {
  localStorage.setItem('onboardingCompleted', 'true')
  router.push('/shop')
}

// 页面挂载时检查是否已完成引导
onMounted(() => {
  const completed = localStorage.getItem('onboardingCompleted')
  if (completed === 'true') {
    router.push('/shop')
  }
})
</script>

<template>
  <div
    class="bg-white relative w-[375px] h-[817px] mx-auto"
    data-name="11 Hello Card"
    data-node-id="0:12177"
    @touchstart="handleTouchStart"
    @touchend="handleTouchEnd"
  >
    <!-- Status Bar -->
    <StatusBar />

    <!-- Background Bubbles -->
    <div class="absolute left-[-147px] top-[-123px]" data-name="Bubbles">
      <!-- Bubble 02 - rotated 108deg -->
      <div class="absolute left-[-7px] top-[362px] w-[537px] h-[495px] rotate-[108deg]">
        <img
          src="https://www.figma.com/api/mcp/asset/955bc41c-ced6-4d61-89ad-112c380c8b64"
          alt="Bubble 02"
          class="w-full h-full object-contain"
        />
      </div>
      <!-- Bubble 01 -->
      <div class="absolute left-0 top-0 w-[403px] h-[443px]">
        <img
          src="https://www.figma.com/api/mcp/asset/0c005a5c-0525-42fa-b53f-082528108cea"
          alt="Bubble 01"
          class="w-full h-full object-contain"
        />
      </div>
    </div>

    <!-- White Card Container -->
    <div class="absolute inset-[9.98%_6.4%_14.41%_6.67%] bg-white rounded-[30px] shadow-[0px_10px_37px_0px_rgba(0,0,0,0.16)]" data-name="Card Shape" />

    <!-- Dynamic Image Section -->
    <div class="absolute inset-[9.98%_6.4%_48.4%_6.67%] overflow-hidden rounded-t-[30px]" data-name="Image">
      <img
        :src="currentImage"
        :alt="guides[currentPage].title"
        class="w-full h-full object-cover"
      />
    </div>

    <!-- Title -->
    <p class="absolute left-1/2 -translate-x-1/2 top-[calc(50%+59px)] font-raleway font-bold text-[28px] leading-[36px] text-[#202020] text-center tracking-[-0.28px]">
      {{ guides[currentPage].title }}
    </p>

    <!-- Description -->
    <p class="absolute left-[17.6%] right-[17.33%] top-[calc(50%+107px)] font-nunito font-light text-[19px] leading-[27px] text-black text-center h-[111px] overflow-hidden">
      {{ guides[currentPage].description }}
    </p>

    <!-- Dots Indicator -->
    <div class="absolute left-[118px] top-[725px] flex gap-[40px]" data-name="Dots">
      <div
        v-for="(_, index) in guides"
        :key="index"
        @click="handleDotClick(index)"
        class="w-[20px] h-[20px] cursor-pointer flex items-center justify-center"
      >
        <div class="absolute inset-[-150%]">
          <img
            :src="currentPage === index ? 'https://www.figma.com/api/mcp/asset/0879f193-3ae3-4c81-a09b-510b5a66e6f2' : 'https://www.figma.com/api/mcp/asset/267a7198-bd19-4e8d-a373-472caedc1dd2'"
            :alt="'dot ' + (index + 1)"
            class="w-full h-full"
          />
        </div>
      </div>
    </div>

    <!-- Let's Go Button (最后一页显示) -->
    <button
      v-if="guides[currentPage].hasButton"
      @click="handleLetsGo"
      class="absolute left-[87px] top-[640px] w-[201px] h-[50px] bg-[#202020] rounded-[16px] flex items-center justify-center cursor-pointer border-none"
    >
      <span class="font-nunito font-light text-[22px] leading-[31px] text-[#f3f3f3]">
        Let's Go
      </span>
    </button>

    <!-- Home Indicator -->
    <HomeIndicator />
  </div>
</template>

<style scoped>
</style>
