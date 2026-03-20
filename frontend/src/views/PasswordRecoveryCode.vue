<script setup lang="ts">
import { ref, watch, onMounted, nextTick } from 'vue'
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

// 模拟验证码验证（实际应该调用 API）
const validateCode = (inputCode: string): boolean => {
  // TODO: 替换为真实 API 验证
  const correctCode = '1234'
  return inputCode === correctCode
}

onMounted(() => {
  nextTick(() => {
    inputRef.value?.focus()
  })
})

watch(code, (newValue) => {
  if (newValue.length === 4) {
    // 验证验证码
    if (!validateCode(newValue)) {
      attemptCount.value++
      code.value = '' // 清空输入

      if (attemptCount.value >= maxAttempts) {
        // 达到最大尝试次数，显示弹窗
        showMaxAttemptsPopup.value = true
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

const handleSendAgain = () => {
  console.log('Send code again')
  attemptCount.value = 0 // 重置计数
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
  <div class="bg-white relative w-[375px] h-[817px] mx-auto" data-name="08 Password Recovery — Code" data-node-id="0:12382">
    <!-- Status Bar -->
    <StatusBar />

    <!-- Background Bubbles -->
    <div class="absolute top-[-273px] left-[20px]" data-name="Bubbles">
      <!-- Bubble 02 - rotated -110deg -->
      <div class="absolute top-[32px] left-0 w-[544px] h-[502px] -rotate-[110deg]">
        <img
          src="https://www.figma.com/api/mcp/asset/c2dd1527-31ae-47f5-9051-91a5c687f22a"
          alt="Bubble 02"
          class="w-full h-full object-contain"
        />
      </div>
      <!-- Bubble 01 - rotated 92deg -->
      <div class="absolute top-0 left-[100px] w-[456px] h-[418px] rotate-[92deg]">
        <img
          src="https://www.figma.com/api/mcp/asset/9e822c92-18e2-479d-ac5d-b34d8f15cf4e"
          alt="Bubble 01"
          class="w-full h-full object-contain"
        />
      </div>
    </div>

    <!-- Avatar Circle -->
    <div class="absolute top-[149px] left-[135px] w-[105px] h-[105px]" data-name="ellispse">
      <div class="absolute inset-[-4.76%] rounded-full overflow-hidden bg-[#ffb6c9]">
        <img
          src="https://www.figma.com/api/mcp/asset/e362e4a2-f6a0-4fb4-b3b5-3bb27f04206b"
          alt="Avatar frame"
          class="w-full h-full object-cover"
        />
      </div>
      <!-- Avatar Image -->
      <div class="absolute inset-[19.21%_37.87%_69.58%_37.87%] rounded-full overflow-hidden">
        <img
          src="https://www.figma.com/api/mcp/asset/f5a68064-653c-4120-9951-15bb4429b646"
          alt="Avatar"
          class="w-full h-full object-cover"
        />
      </div>
    </div>

    <!-- Page Title -->
    <h1 class="absolute left-1/2 -translate-x-1/2 top-[266px] font-raleway font-bold text-[21px] leading-[30px] text-brand-black text-center tracking-[-0.21px]">
      Password Recovery
    </h1>

    <!-- Instruction Text -->
    <p class="absolute left-1/2 -translate-x-1/2 top-[301px] font-nunito font-light text-[19px] leading-[27px] text-black text-center w-[290px]">
      Enter 4-digits code we sent you on your phone number
    </p>

    <!-- Phone Number -->
    <p class="absolute left-1/2 -translate-x-1/2 top-[371px] font-nunito font-bold text-[16px] leading-[25px] text-black text-center tracking-[1.6px]">
      +98*******00
    </p>

    <!-- Hidden Input -->
    <input
      ref="inputRef"
      v-model="code"
      type="text"
      inputmode="numeric"
      maxlength="4"
      class="absolute opacity-0 left-[136px] top-[424px] w-[136px] h-[17px]"
      autofocus
    />

    <!-- Code Dots -->
    <div class="absolute left-[136px] top-[424px]" data-name="dots">
      <div class="absolute left-[0px] w-[17px] h-[17px]">
        <img
          v-if="code.length >= 1"
          src="https://www.figma.com/api/mcp/asset/e7e83f8b-5728-4e4c-89fd-187bf2dc4270"
          alt="Filled dot"
          class="w-full h-full"
        />
        <img
          v-else
          src="https://www.figma.com/api/mcp/asset/25bd7827-642a-4eb0-8971-e28624a121b9"
          alt="Empty dot"
          class="w-full h-full"
        />
      </div>
      <div class="absolute left-[29px] w-[17px] h-[17px]">
        <img
          v-if="code.length >= 2"
          src="https://www.figma.com/api/mcp/asset/e7e83f8b-5728-4e4c-89fd-187bf2dc4270"
          alt="Filled dot"
          class="w-full h-full"
        />
        <img
          v-else
          src="https://www.figma.com/api/mcp/asset/25bd7827-642a-4eb0-8971-e28624a121b9"
          alt="Empty dot"
          class="w-full h-full"
        />
      </div>
      <div class="absolute left-[58px] w-[17px] h-[17px]">
        <img
          v-if="code.length >= 3"
          src="https://www.figma.com/api/mcp/asset/e7e83f8b-5728-4e4c-89fd-187bf2dc4270"
          alt="Filled dot"
          class="w-full h-full"
        />
        <img
          v-else
          src="https://www.figma.com/api/mcp/asset/25bd7827-642a-4eb0-8971-e28624a121b9"
          alt="Empty dot"
          class="w-full h-full"
        />
      </div>
      <div class="absolute left-[87px] w-[17px] h-[17px]">
        <img
          v-if="code.length === 4"
          src="https://www.figma.com/api/mcp/asset/e7e83f8b-5728-4e4c-89fd-187bf2dc4270"
          alt="Filled dot"
          class="w-full h-full"
        />
        <img
          v-else
          src="https://www.figma.com/api/mcp/asset/25bd7827-642a-4eb0-8971-e28624a121b9"
          alt="Empty dot"
          class="w-full h-full"
        />
      </div>
    </div>

    <!-- Send Again Button -->
    <button
      @click="handleSendAgain"
      class="absolute left-[87px] top-[640px] w-[201px] h-[50px] bg-[#ff5790] rounded-[16px] flex items-center justify-center cursor-pointer border-none"
    >
      <span class="font-nunito font-light text-[22px] leading-[31px] text-[#f3f3f3]">
        Send Again
      </span>
    </button>

    <!-- Cancel Link -->
    <button
      @click="handleCancel"
      class="absolute left-1/2 -translate-x-1/2 top-[719px] font-nunito font-light text-[15px] leading-[26px] text-brand-black opacity-90 bg-transparent border-none cursor-pointer"
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
              src="https://www.figma.com/api/mcp/asset/cf9766c4-dba1-439a-9506-9618ca76549a"
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
