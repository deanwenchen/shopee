<script setup lang="ts">
import { ref, watch, onMounted, nextTick, computed } from 'vue'
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'

const router = useRouter()
const code = ref('')
const inputRef = ref<HTMLInputElement | null>(null)

// 最大尝试次数和当前计数
const maxAttempts = 3
const attemptCount = ref(0)
const showMaxAttemptsPopup = ref(false)
const isErrorState = ref(false) // 错误状态标记

// Figma asset URLs
const bubble01 = 'https://www.figma.com/api/mcp/asset/4f3439f4-62ab-4010-a149-fa9a7d441c99'
const bubble02 = 'https://www.figma.com/api/mcp/asset/0a65a368-1b3f-46f0-ac21-a12833a0e3e7'
const ellipse = 'https://www.figma.com/api/mcp/asset/b6068eeb-70af-498c-9980-3dffc3091251'
const avatarBD2B = 'https://www.figma.com/api/mcp/asset/b4b25371-b629-4555-975e-26e544aaa638'
const avatar6959 = 'https://www.figma.com/api/mcp/asset/332d8b03-bf20-4595-9f49-76a62e79acf3'
const avatarMask = 'https://www.figma.com/api/mcp/asset/48bed544-77b8-49a0-a821-837efbcff322'
const avatarArtist2 = 'https://www.figma.com/api/mcp/asset/c0f59a89-b825-4bcc-8849-8df1cc7f7e9d'
const emptyDot = 'https://www.figma.com/api/mcp/asset/83ba2ab2-92b5-4d1e-9c94-e321540274a4' // 空心点
const warningIcon = 'https://www.figma.com/api/mcp/asset/cf9766c4-dba1-439a-9506-9618ca76549a'

// SVG components for dots
const BlueDotSvg = `<svg width="17" height="17" viewBox="0 0 17 17" fill="none" xmlns="http://www.w3.org/2000/svg"><path d="M8.5 17C13.1944 17 17 13.1944 17 8.5C17 3.80558 13.1944 0 8.5 0C3.80558 0 0 3.80558 0 8.5C0 13.1944 3.80558 17 8.5 17Z" fill="#004BFE"/></svg>`
const RedDotSvg = `<svg width="17" height="17" viewBox="0 0 17 17" fill="none" xmlns="http://www.w3.org/2000/svg"><path d="M8.5 17C13.1944 17 17 13.1944 17 8.5C17 3.80558 13.1944 0 8.5 0C3.80558 0 0 3.80558 0 8.5C0 13.1944 3.80558 17 8.5 17Z" fill="#EC4E4E"/></svg>`

// Create blob URLs for SVGs
const blueDotBlob = URL.createObjectURL(new Blob([BlueDotSvg], { type: 'image/svg+xml' }))
const redDotBlob = URL.createObjectURL(new Blob([RedDotSvg], { type: 'image/svg+xml' }))

const blueDotFilled = blueDotBlob // 蓝色实心点
const redDotFilled = redDotBlob // 红色实心点

// 剩余尝试次数
const remainingAttempts = computed(() => maxAttempts - attemptCount.value)

// 动态 dots 图片
const dotImages = computed(() => {
  const images = []
  for (let i = 0; i < 4; i++) {
    if (code.value.length >= i + 1) {
      // 已输入的格子
      if (isErrorState.value) {
        images.push(redDotFilled) // 错误状态：红点
      } else {
        images.push(blueDotFilled) // 正常输入：蓝点
      }
    } else {
      // 未输入的格子
      if (isErrorState.value) {
        images.push(redDotFilled) // 错误状态：红点
      } else {
        images.push(emptyDot) // 未输入：空心点
      }
    }
  }
  return images
})

// 模拟验证码验证（实际应该调用 API）
const validateCode = (inputCode: string): boolean => {
  // TODO: 替换为真实 API 验证
  const correctCode = '1234'
  return inputCode === correctCode
}

onMounted(() => {
  // 页面加载时自动聚焦输入框
  nextTick(() => {
    setTimeout(() => {
      inputRef.value?.focus()
    }, 100)
  })
})

watch(code, (newValue) => {
  if (newValue.length === 4) {
    // 验证验证码
    if (!validateCode(newValue)) {
      attemptCount.value++
      code.value = '' // 清空输入
      isErrorState.value = true // 设置错误状态

      if (attemptCount.value >= maxAttempts) {
        // 达到最大尝试次数，显示弹窗
        showMaxAttemptsPopup.value = true
      } else {
        // 重置错误状态，允许重新输入
        setTimeout(() => {
          isErrorState.value = false
        }, 500)
      }
    } else {
      // 验证码正确，跳转到新密码页面
      setTimeout(() => {
        router.push('/new-password')
      }, 500)
    }
  }
})

const handleCancel = () => {
  router.push('/login')
}

const handleInput = (event: Event) => {
  const target = event.target as HTMLInputElement
  code.value = target.value
}

const handleDotAreaClick = () => {
  inputRef.value?.focus()
}

const handleSendAgain = () => {
  console.log('Send code again')
  attemptCount.value = 0 // 重置计数
  isErrorState.value = false // 重置错误状态
  code.value = ''
  nextTick(() => {
    inputRef.value?.focus()
  })
}

const handleOkay = () => {
  showMaxAttemptsPopup.value = false
  router.push('/login')
}
</script>

<template>
  <div class="bg-white relative w-[375px] h-[817px] mx-auto overflow-hidden" data-name="08 Password Recovery — Code" data-node-id="0:12382">
    <!-- Status Bar -->
    <StatusBar />

    <!-- Background Bubbles -->
    <div class="absolute top-[-273.16px] left-[20.28px] w-[354.72px] h-[780px] overflow-hidden pointer-events-none" data-name="Bubbles" data-node-id="0:12383">
      <!-- Bubble 02 - rotated -110deg -->
      <div class="absolute top-[-240.83px] left-0 flex h-[502.4px] w-[543.71px] items-center justify-center">
        <div class="-rotate-[110deg] flex-none h-[442.65px] w-[373.531px] relative">
          <img
            :src="bubble02"
            alt="Bubble 02"
            class="absolute block size-full max-w-none"
          />
        </div>
      </div>
      <!-- Bubble 01 - rotated 92deg -->
      <div class="absolute top-0 left-[99.45px] flex h-[418.074px] w-[456.44px] items-center justify-center">
        <div class="rotate-[92deg] flex-none h-[442.65px] w-[402.871px] relative">
          <img
            :src="bubble01"
            alt="Bubble 01"
            class="absolute block size-full max-w-none"
          />
        </div>
      </div>
    </div>

    <!-- Ellipse background -->
    <div class="absolute left-[187.5px] top-[149px] -translate-x-1/2 w-[105px] h-[110px] z-10" data-name="ellispse" data-node-id="0:12397">
      <div class="absolute inset-[-4.76%]">
        <img
          :src="ellipse"
          alt="Ellipse"
          class="block size-full max-w-none"
        />
      </div>
    </div>

    <!-- Avatar Image Group -->
    <div class="absolute left-[187.5px] top-[149px] -translate-x-1/2 w-[105px] h-[110px] z-10" data-name="image" data-node-id="0:12398">
      <!-- BD2B4EE3-3A6F-4C47-B114-F152B95B18D0 -->
      <div class="absolute inset-[19.21%_37.87%_69.58%_37.87%]">
        <img
          :src="avatarBD2B"
          alt="Avatar decoration"
          class="absolute block size-full max-w-none"
        />
      </div>
      <!-- 69598E49-7F05-4DDB-9911-66FFA208CAFD - masked avatar -->
      <div class="absolute inset-[12.42%_37.87%_62.79%_37.87%]" style="mask-image: url('https://www.figma.com/api/mcp/asset/48bed544-77b8-49a0-a821-837efbcff322'); mask-size: 91px 91px; mask-position: 0px 55.146px; mask-repeat: no-repeat; -webkit-mask-image: url('https://www.figma.com/api/mcp/asset/48bed544-77b8-49a0-a821-837efbcff322'); -webkit-mask-size: 91px 91px; -webkit-mask-position: 0px 55.146px; -webkit-mask-repeat: no-repeat;">
        <div class="absolute inset-0 overflow-hidden pointer-events-none">
          <img
            :src="avatar6959"
            alt="Avatar"
            class="absolute left-0 top-0 size-full max-w-none"
          />
        </div>
      </div>
      <!-- artist-2 1 -->
      <div class="absolute inset-0" style="mask-image: url('https://www.figma.com/api/mcp/asset/48bed544-77b8-49a0-a821-837efbcff322'); mask-size: 91px 91px; mask-position: 7px 8px; mask-repeat: no-repeat; -webkit-mask-image: url('https://www.figma.com/api/mcp/asset/48bed544-77b8-49a0-a821-837efbcff322'); -webkit-mask-size: 91px 91px; -webkit-mask-position: 7px 8px; -webkit-mask-repeat: no-repeat;">
        <img
          :src="avatarArtist2"
          alt="Avatar main"
          class="absolute inset-0 size-full max-w-none object-cover pointer-events-none"
        />
      </div>
    </div>

    <!-- Page Title -->
    <h1 class="absolute left-[188.5px] top-[266px] -translate-x-1/2 font-raleway font-bold text-[21px] leading-[30px] text-[#202020] text-center tracking-[-0.21px] whitespace-nowrap" data-node-id="0:12394">
      Password Recovery
    </h1>

    <!-- Instruction Text -->
    <p class="absolute left-[188px] top-[301px] -translate-x-1/2 font-nunito font-light text-[19px] leading-[27px] text-black text-center w-[290px] h-[57px]" data-node-id="0:12393">
      Enter 4-digits code we sent you on your phone number
    </p>

    <!-- Phone Number -->
    <p class="absolute left-[188.5px] top-[371px] -translate-x-1/2 font-nunito font-bold text-[16px] leading-[25px] text-black text-center tracking-[1.6px] whitespace-nowrap" data-node-id="0:12392">
      +98*******00
    </p>

    <!-- Hidden Input - Fully accessible -->
    <input
      ref="inputRef"
      :value="code"
      type="text"
      inputmode="numeric"
      maxlength="4"
      autocomplete="one-time-code"
      class="absolute left-[136px] top-[424px] w-[201px] h-[50px] z-10"
      style="opacity: 0.01; pointer-events: auto;"
      autofocus
      @input="handleInput"
    />

    <!-- Click capture layer for dots area -->
    <div
      class="absolute left-[136px] top-[424px] w-[201px] h-[50px] z-20"
      @click="handleDotAreaClick"
    />

    <!-- Code Dots -->
    <div class="absolute left-[136px] top-[424px]" data-name="dots" data-node-id="0:12387">
      <div class="absolute left-0 size-[17px]" data-name="ellispse 01" data-node-id="0:12388">
        <img :src="dotImages[0]" alt="Dot 1" class="block size-full" />
      </div>
      <div class="absolute left-[29px] size-[17px]" data-name="ellispse 01" data-node-id="0:12389">
        <img :src="dotImages[1]" alt="Dot 2" class="block size-full" />
      </div>
      <div class="absolute left-[58px] size-[17px]" data-name="ellispse 01" data-node-id="0:12390">
        <img :src="dotImages[2]" alt="Dot 3" class="block size-full" />
      </div>
      <div class="absolute left-[87px] size-[17px]" data-name="ellispse 01" data-node-id="0:12391">
        <img :src="dotImages[3]" alt="Dot 4" class="block size-full" />
      </div>
    </div>

    <!-- Remaining Attempts Text -->
    <p
      v-if="attemptCount > 0 && attemptCount < maxAttempts"
      class="absolute left-[188.5px] top-[460px] -translate-x-1/2 font-nunito font-light text-[14px] leading-[22px] text-[#ff5790] text-center"
    >
      {{ remainingAttempts }} attempts remaining
    </p>

    <!-- Send Again Button -->
    <button
      @click="handleSendAgain"
      class="absolute left-[87px] top-[640px] w-[201px] h-[50px] bg-[#ff5790] rounded-[16px] flex items-center justify-center cursor-pointer border-none"
      data-name="Button" data-node-id="0:12446"
    >
      <span class="font-nunito font-light text-[22px] leading-[31px] text-[#f3f3f3]" data-node-id="0:12448">
        Send Again
      </span>
    </button>

    <!-- Cancel Link -->
    <button
      @click="handleCancel"
      class="absolute left-[188.5px] top-[719px] -translate-x-1/2 font-nunito font-light text-[15px] leading-[26px] text-[#202020] opacity-90 bg-transparent border-none cursor-pointer whitespace-nowrap"
      data-node-id="0:12386"
    >
      Cancel
    </button>

    <!-- Home Indicator -->
    <HomeIndicator />

    <!-- Maximum Attempts Popup Overlay -->
    <div
      v-if="showMaxAttemptsPopup"
      class="absolute inset-0 bg-[#0e0e0e] opacity-78 z-50"
      data-name="cover-blue"
    />

    <!-- Maximum Attempts Popup -->
    <div
      v-if="showMaxAttemptsPopup"
      class="absolute left-[14px] top-[295px] w-[347px] h-[263px] z-50"
      data-name="Pop-up"
    >
      <!-- Popup Background -->
      <div class="absolute left-0 right-0 top-[38px] h-[225px] bg-[#f8f8f8] rounded-[19px]" />

      <!-- Warning Icon Circle -->
      <div class="absolute left-1/2 -translate-x-1/2 top-[-72px] w-[80px] h-[80px]">
        <div class="absolute inset-[-6.25%_-10%_-13.75%_-10%] rounded-full bg-[#ffb6c9] flex items-center justify-center">
          <!-- Exclamation Icon -->
          <div class="relative w-[24px] h-[24px]">
            <img
              :src="warningIcon"
              alt="Warning"
              class="w-full h-full object-contain"
            />
          </div>
        </div>
      </div>

      <!-- Message Text -->
      <p class="absolute left-[17%] right-[16.71%] top-[64px] font-raleway font-medium text-[18px] leading-[26px] text-[#202020] text-center tracking-[-0.18px]">
        You reached out maximum amount of attempts. Please, try later.
      </p>

      <!-- Okay Button -->
      <button
        @click="handleOkay"
        class="absolute left-[21%] right-[21%] top-[140px] h-[50px] bg-[#202020] rounded-[16px] flex items-center justify-center cursor-pointer border-none"
      >
        <span class="font-nunito font-light text-[22px] leading-[31px] text-[#f3f3f3]">
          Okay
        </span>
      </button>
    </div>
  </div>
</template>

<style scoped>
</style>
