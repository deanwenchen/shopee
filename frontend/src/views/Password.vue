<script setup lang="ts">
import { ref, watch, onMounted, nextTick, computed } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth'
import StatusBar from '@/components/StatusBar.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'

// 导入本地图片
import emptyDotImg from '@/assets/figma/25bd7827-642a-4eb0-8971-e28624a121b9.svg'
import blueDotImg from '@/assets/figma/blue-dot.svg'
import redDotImg from '@/assets/figma/red-dot.svg'

const router = useRouter()
const authStore = useAuthStore()
const inputRef = ref<HTMLInputElement | null>(null)

// 密码输入状态
const password = ref('')
const isLoggingIn = ref(false)

// Figma asset URLs
const emptyDot = emptyDotImg
const blueDotFilled = blueDotImg
const redDot = redDotImg

// 状态控制
// inputMode: '4-digit' | '8-digit' | 'error'
const inputMode = ref<'4-digit' | '8-digit' | 'error'>('4-digit')
// 是否已经退格过（用于错误状态后重新输入）
const hasBackspaced = ref(false)

// 获取邮箱（从 login page 传过来）
const loginEmail = sessionStorage.getItem('loginEmail') || ''

// 如果没有 loginEmail，说明是直接访问 /password，跳转回登录页
if (!loginEmail) {
  router.push('/login')
}

// 监听密码输入和退格操作
watch(password, async (newValue, oldValue) => {
  // 4 位密码输入模式 - 只允许输入 1 个字符
  if (inputMode.value === '4-digit') {
    if (newValue.length >= 1) {
      inputMode.value = '8-digit'
      nextTick(() => {
        inputRef.value?.focus()
      })
    }
    return
  }

  // 8 位密码输入模式
  if (inputMode.value === '8-digit') {
    if (newValue.length === 8) {
      // 调用 API 登录
      if (!isLoggingIn.value) {
        isLoggingIn.value = true
        const result = await authStore.login(loginEmail, newValue)
        isLoggingIn.value = false

        if (result.success) {
          // 登录成功，跳转到首页
          router.push('/hello-card')
        } else {
          // 密码错误，显示错误状态
          inputMode.value = 'error'
          hasBackspaced.value = false
        }
      }
    }
    return
  }

  // 错误状态模式
  if (inputMode.value === 'error') {
    // 检测退格操作 - 从 8 个字符减少
    if (oldValue && oldValue.length === 8 && newValue.length < 8) {
      hasBackspaced.value = true
      return
    }

    // 当输入达到 8 个字符时验证
    if (newValue.length === 8 && oldValue.length === 7 && !isLoggingIn.value) {
      isLoggingIn.value = true
      const result = await authStore.login(loginEmail, newValue)
      isLoggingIn.value = false

      if (result.success) {
        router.push('/hello-card')
      } else {
        // 密码错误，重置 hasBackspaced，保持错误状态
        hasBackspaced.value = false
      }
    }
  }
})

onMounted(() => {
  nextTick(() => {
    inputRef.value?.focus()
  })
})

// 处理输入框点击，确保聚焦
const handleInputClick = () => {
  inputRef.value?.focus()
}

// 处理键盘退格
const handleKeydown = (event: KeyboardEvent) => {
  if (event.key === 'Backspace' && inputMode.value === 'error') {
    hasBackspaced.value = true
  }
}

// 使用 beforeinput 事件，在输入前触发，响应更快
const handleBeforeInput = (event: InputEvent) => {
  if (event.inputType === 'deleteContentBackward' && inputMode.value === 'error') {
    hasBackspaced.value = true
  }
}

// 动态 dots 图片 - computed 属性（用于 8 位密码和错误状态）
const dotImages = computed(() => {
  const images = []

  for (let i = 0; i < 8; i++) {
    if (password.value.length >= i + 1) {
      // 已输入的格子
      if (inputMode.value === 'error' && !hasBackspaced.value) {
        // 错误确认状态：红色实心点
        images.push(redDot)
      } else {
        // 正在输入状态：蓝色实心点
        images.push(blueDotFilled)
      }
    } else {
      // 未输入的格子
      if (inputMode.value === 'error' && hasBackspaced.value) {
        // 退格后：空心点
        images.push(emptyDot)
      } else if (inputMode.value === 'error') {
        // 错误初始状态：红点
        images.push(redDot)
      } else {
        // 8 位输入状态：空心点
        images.push(emptyDot)
      }
    }
  }
  return images
})

const handleNotYou = () => {
  // 清除 sessionStorage 中的 email
  sessionStorage.removeItem('loginEmail')
  router.push('/login')
}

const handleForgotPassword = () => {
  router.push('/password-recovery')
}
</script>

<template>
  <div class="bg-white relative w-[375px] h-[817px] mx-auto overflow-hidden" data-name="04 Password" data-node-id="0:12649">
    <!-- Status Bar -->
    <StatusBar />

    <!-- Background Bubbles - Order matters for z-index -->
    <div class="absolute top-[-172px] left-[-158px]" data-name="Bubbles">
      <!-- Bubble 02 - 浅蓝色大气泡（旋转 158 度） -->
      <div class="absolute top-[0px] left-[22px] w-[512px] h-[550px] rotate-[158deg]">
        <img
          src="../assets/figma/090d6144-e1da-4b62-8d46-af67a8b62405.svg"
          alt="Bubble 02"
          class="w-full h-full object-contain"
        />
      </div>

      <!-- Bubble 01 - 深蓝色大气泡 -->
      <div class="absolute top-[0px] left-[0px] w-[403px] h-[443px]">
        <img
          src="../assets/figma/9acc6ed4-83e1-4c99-b930-5279f1abf2b7.svg"
          alt="Bubble 01"
          class="w-full h-full object-contain"
        />
      </div>
    </div>

    <!-- Avatar Circle -->
    <div class="absolute top-[149px] left-[135px] w-[105px] h-[105px]" data-name="ellipse">
      <div class="absolute inset-[-4.76%] rounded-full overflow-hidden bg-[#ffb6c9]">
        <img
          src="@/assets/icons/artist.png"
          alt="Avatar frame"
          class="w-full h-full object-cover"
        />
      </div>
      <!-- Avatar Image -->
      <div class="absolute inset-[19.21%_37.87%_69.58%_37.87%] rounded-full overflow-hidden">
        <img
          src="../assets/figma/55897da1-4abd-446d-9448-8ea2bb86b45d.svg"
          alt="Avatar"
          class="w-full h-full object-cover"
        />
      </div>
    </div>

    <!-- Greeting Text -->
    <h1 class="absolute left-1/2 -translate-x-1/2 top-[282px] font-raleway font-bold text-[28px] leading-[36px] text-brand-black text-center tracking-[-0.28px] whitespace-nowrap">
      Hello, Romina!!
    </h1>

    <!-- Instruction Text -->
    <p class="absolute left-1/2 -translate-x-1/2 top-[348px] font-nunito font-light text-[19px] leading-[35px] text-black text-center">
      Type your password
    </p>

    <!-- Hidden Password Input (唤起系统键盘) -->
    <input
      ref="inputRef"
      v-model="password"
      type="password"
      maxlength="8"
      class="absolute opacity-0 left-[78px] top-[390px] w-[261px] h-[17px]"
      autofocus
      @click="handleInputClick"
      @keydown="handleKeydown"
      @beforeinput="handleBeforeInput"
    />

    <!-- 4 位密码输入框（初始状态） -->
    <div @click="handleInputClick" v-if="inputMode === '4-digit'" class="absolute left-[81px] top-[406px] flex gap-[5.056px] cursor-text" data-name="Password Inputs">
      <div class="w-[49.3px] h-[50.564px] rounded-[10.113px] bg-[#f8f8f8]" />
      <div class="w-[49.3px] h-[50.564px] rounded-[10.113px] bg-[#f8f8f8]" />
      <div class="w-[49.3px] h-[50.564px] rounded-[10.113px] bg-[#f8f8f8]" />
      <div class="w-[49.932px] h-[50.564px] rounded-[10.113px] bg-[#f8f8f8]" />
    </div>

    <!-- 8 位密码点（8 位输入状态和错误状态） -->
    <div @click="handleInputClick" v-else class="absolute left-[78px] top-[390px] flex gap-[12px] cursor-text" data-name="Dots">
      <div v-for="(dot, index) in dotImages" :key="index" class="w-[17px] h-[17px]">
        <img :src="dot" :alt="'Dot ' + (index + 1)" class="w-full h-full" />
      </div>
    </div>

    <!-- Not You? 按钮（仅在 4 位和 8 位输入模式显示） -->
    <div v-if="inputMode !== 'error'" class="absolute left-[135px] top-[713px] flex items-center gap-[10px]" data-name="Not You?">
      <span class="font-nunito font-light text-[15px] leading-[26px] text-brand-black opacity-90">
        Not you?
      </span>
      <button
        @click="handleNotYou"
        class="w-[30px] h-[30px] rounded-full bg-brand-blue flex items-center justify-center border-none cursor-pointer"
      >
        <svg width="14" height="14" viewBox="0 0 14 14" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M10.5 7H3.5M3.5 7L6.41667 4.08333M3.5 7L6.41667 9.91667" stroke="white" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
        </svg>
      </button>
    </div>

    <!-- Forgot Password 链接（仅在错误状态显示） -->
    <p
      v-else
      @click="handleForgotPassword"
      class="absolute left-1/2 -translate-x-1/2 top-[445px] font-nunito font-light text-[15px] leading-[26px] text-brand-black text-center opacity-90 cursor-pointer"
    >
      Forgot your password?
    </p>

    <!-- Home Indicator -->
    <HomeIndicator />
  </div>
</template>

<style scoped>
</style>
