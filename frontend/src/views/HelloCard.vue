<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'

const router = useRouter()

// 导入本地图片
import dotFilled from '@/assets/figma/hello-dot-filled-f71bedda-e6dc-4e2e-b7cc-b5a3d8d7a968.svg'
import dotEmpty from '@/assets/figma/hello-dot-empty-594e48ee-e099-46ec-a7a9-1faacfc838ab.svg'
import guideImage from '@/assets/icons/guide-image.png'
import guideImageReady from '@/assets/icons/guide-image-ready.png'

// 引导数据 - 根据 Figma 设计更新
interface GuidePage {
  title: string
  description: string
  image: string
  hasButton?: boolean
  buttonText?: string
}

const guides: GuidePage[] = [
  {
    title: 'Hello',
    description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed non consectetur turpis. Morbi eu eleifend lacus.',
    image: guideImage
  },
  {
    title: 'Explore',
    description: 'Discover amazing products from top brands. Find exactly what you need with our smart search.',
    image: guideImage
  },
  {
    title: 'Choose',
    description: 'Add items to your wishlist and cart. Compare products and find the best deals.',
    image: guideImage
  },
  {
    title: 'Ready?',
    description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.',
    image: guideImageReady,
    hasButton: true,
    buttonText: "Let's Start"
  }
]

const currentPage = ref(0)
const touchStartX = ref(0)
const touchEndX = ref(0)
const touchStartTime = ref(0)

const SWIPE_THRESHOLD = 30 // 降低阈值，让滑动更灵敏
const SWIPE_TIME_THRESHOLD = 300 // 快速滑动时间阈值（毫秒）

// 滑动处理
const handleTouchStart = (e: TouchEvent) => {
  touchStartX.value = e.touches[0].clientX
  touchStartTime.value = Date.now()
}

const handleTouchEnd = (e: TouchEvent) => {
  touchEndX.value = e.changedTouches[0].clientX
  const touchDuration = Date.now() - touchStartTime.value

  // 如果是快速点击（不是滑动），不触发翻页
  if (touchDuration < SWIPE_TIME_THRESHOLD && Math.abs(touchEndX.value - touchStartX.value) < SWIPE_THRESHOLD) {
    return
  }

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

// "Let's Start" 按钮
const handleLetsStart = () => {
  localStorage.setItem('onboardingCompleted', 'true')
  router.push('/shop')
}

// 页面挂载时检查是否已完成引导（开发期间注释掉）
// onMounted(() => {
//   const completed = localStorage.getItem('onboardingCompleted')
//   if (completed === 'true') {
//     router.push('/shop')
//   }
// })
</script>

<template>
  <div
    class="bg-white relative w-[375px] h-[817px] mx-auto overflow-hidden"
    data-name="11 Hello Card"
    data-node-id="0:12177"
    @touchstart="handleTouchStart"
    @touchend="handleTouchEnd"
  >
    <!-- Status Bar -->
    <StatusBar />

    <!-- Background Bubbles - Order matters for z-index -->
    <div class="absolute left-[-147px] top-[-123px] w-[522px] h-[780px] overflow-hidden pointer-events-none" data-name="Bubbles">
      <!-- Bubble 02 - rotated 108deg -->
      <div class="absolute left-[66px] top-[463px] w-[537px] h-[495px] rotate-[108deg]">
        <img
          src="../assets/figma/hello-bubble-02-f18f3704-8a74-4131-9676-001a3c95bb7f.svg"
          alt="Bubble 02"
          class="w-full h-full object-contain"
        />
      </div>
      <!-- Bubble 01 -->
      <div class="absolute left-0 top-0 w-[403px] h-[443px]">
        <img
          src="../assets/figma/hello-bubble-01-13d94a9f-7c9e-4eb3-9e97-a51f4dc1e843.svg"
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
        :src="guides[currentPage].image"
        :alt="guides[currentPage].title"
        class="w-full h-full object-cover"
      />
    </div>

    <!-- Title -->
    <p class="absolute left-1/2 -translate-x-1/2 top-[calc(50%+59px)] font-raleway font-bold text-[28px] leading-[36px] text-[#202020] text-center tracking-[-0.28px] whitespace-nowrap">
      {{ guides[currentPage].title }}
    </p>

    <!-- Description -->
    <p class="absolute left-[17.6%] right-[17.33%] top-[calc(50%+107px)] font-nunito font-light text-[19px] leading-[27px] text-black text-center">
      {{ guides[currentPage].description }}
    </p>

    <!-- Dots Indicator - Position based on Figma node 0:12117 -->
    <div class="absolute left-[118px] top-[725px] flex" data-name="Dots">
      <div
        v-for="(_, index) in guides"
        :key="index"
        @click="handleDotClick(index)"
        class="w-[20px] h-[20px] cursor-pointer relative"
      >
        <img
          :src="currentPage === index ? dotFilled : dotEmpty"
          :alt="'dot ' + (index + 1)"
          class="w-full h-full"
        />
      </div>
    </div>

    <!-- Let's Start Button (最后一页显示) -->
    <button
      v-if="guides[currentPage].hasButton"
      @click="handleLetsStart"
      class="absolute left-[87px] top-[640px] w-[201px] h-[50px] bg-[#004cff] rounded-[16px] flex items-center justify-center cursor-pointer border-none"
    >
      <span class="font-nunito font-light text-[22px] leading-[31px] text-[#f3f3f3] whitespace-nowrap">
        {{ guides[currentPage].buttonText }}
      </span>
    </button>

    <!-- Home Indicator -->
    <HomeIndicator />
  </div>
</template>

<style scoped>
</style>
