<script setup lang="ts">
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'

const router = useRouter()
const password = ref('') // 密码输入框绑定

// 密码输入时，立即跳转到 PasswordTyping 页面并传递密码值
watch(password, (newValue) => {
  if (newValue.length >= 1) {
    setTimeout(() => {
      router.push({ path: '/password-typing', query: { password: newValue } })
    }, 50)
  }
})

const handleNotYou = () => {
  console.log('Not you? clicked - Navigate to Login')
  router.push('/login')
}
</script>

<template>
  <div class="bg-white relative w-[375px] h-[817px] mx-auto" data-name="04 Password" data-node-id="0:12649">
    <!-- Status Bar -->
    <StatusBar />

    <!-- Background Bubbles - Order matters for z-index -->
    <div class="absolute top-[-172px] left-[-158px]" data-name="Bubbles">
      <!-- Bubble 02 - 浅蓝色大气泡（旋转 158 度）- 在底层，先渲染 -->
      <div class="absolute top-[0px] left-[22px] w-[512px] h-[550px] rotate-[158deg]">
        <img
          src="https://www.figma.com/api/mcp/asset/090d6144-e1da-4b62-8d46-af67a8b62405"
          alt="Bubble 02"
          class="w-full h-full object-contain"
        />
      </div>

      <!-- Bubble 01 - 深蓝色大气泡 - 在浅蓝色上面，后渲染 -->
      <div class="absolute top-[0px] left-[0px] w-[403px] h-[443px]">
        <img
          src="https://www.figma.com/api/mcp/asset/9acc6ed4-83e1-4c99-b930-5279f1abf2b7"
          alt="Bubble 01"
          class="w-full h-full object-contain"
        />
      </div>
    </div>

    <!-- Avatar Circle -->
    <div class="absolute top-[149px] left-[135px] w-[105px] h-[105px]" data-name="ellipse">
      <div class="absolute inset-[-4.76%] rounded-full overflow-hidden bg-[#ffb6c9]">
        <img
          src="https://www.figma.com/api/mcp/asset/4f9e289e-bb36-458c-bdd1-96efb8db3dd6"
          alt="Avatar frame"
          class="w-full h-full object-cover"
        />
      </div>
      <!-- Avatar Image -->
      <div class="absolute inset-[19.21%_37.87%_69.58%_37.87%] rounded-full overflow-hidden">
        <img
          src="https://www.figma.com/api/mcp/asset/55897da1-4abd-446d-9448-8ea2bb86b45d"
          alt="Avatar"
          class="w-full h-full object-cover"
        />
      </div>
    </div>

    <!-- Greeting Text -->
    <h1 class="absolute left-1/2 -translate-x-1/2 top-[282px] font-raleway font-bold text-[28px] leading-[36px] text-brand-black text-center tracking-[-0.28px]">
      Hello, Romina!!
    </h1>

    <!-- Instruction Text -->
    <p class="absolute left-1/2 -translate-x-1/2 top-[348px] font-nunito font-light text-[19px] leading-[35px] text-black text-center">
      Type your password
    </p>

    <!-- Password Input Boxes - 4 个空框占位 (使用隐藏输入框接收系统输入) -->
    <div class="absolute left-[81px] top-[406px] flex gap-[5.056px]" data-name="Password Inputs">
      <div class="w-[49.3px] h-[50.564px] rounded-[10.113px] bg-[#f8f8f8]" />
      <div class="w-[49.3px] h-[50.564px] rounded-[10.113px] bg-[#f8f8f8]" />
      <div class="w-[49.3px] h-[50.564px] rounded-[10.113px] bg-[#f8f8f8]" />
      <div class="w-[49.932px] h-[50.564px] rounded-[10.113px] bg-[#f8f8f8]" />
    </div>

    <!-- Hidden Password Input (唤起系统键盘) -->
    <input
      v-model="password"
      type="password"
      maxlength="1"
      class="absolute opacity-0 left-[81px] top-[406px] w-[227px] h-[50px]"
      autofocus
    />

    <!-- Not You? Button -->
    <div class="absolute left-[135px] top-[713px] flex items-center gap-[10px]" data-name="Not You?">
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

    <!-- Home Indicator -->
    <HomeIndicator />
  </div>
</template>

<style scoped>
</style>
