<script setup lang="ts">
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'

const router = useRouter()

const handleForgotPassword = () => {
  router.push('/password-recovery')
}
</script>

<template>
  <div class="bg-white relative w-[375px] h-[817px] mx-auto" data-name="06 Wrong Password" data-node-id="0:12518">
    <!-- Status Bar -->
    <StatusBar />

    <!-- Background Bubbles -->
    <div class="absolute top-[-172px] left-[-158px]" data-name="Bubbles">
      <!-- Bubble 02 - 浅蓝色大气泡（旋转 158 度） -->
      <div class="absolute top-[0px] left-[22px] w-[512px] h-[550px] rotate-[158deg]">
        <img
          src="https://www.figma.com/api/mcp/asset/12e38b67-a437-4e8b-ac1c-de6894d1bebc"
          alt="Bubble 02"
          class="w-full h-full object-contain"
        />
      </div>

      <!-- Bubble 01 - 深蓝色大气泡 -->
      <div class="absolute top-[0px] left-[0px] w-[403px] h-[443px]">
        <img
          src="https://www.figma.com/api/mcp/asset/09020d3a-276c-45f4-b36f-41b896a8105e"
          alt="Bubble 01"
          class="w-full h-full object-contain"
        />
      </div>
    </div>

    <!-- Avatar Circle -->
    <div class="absolute top-[149px] left-[135px] w-[105px] h-[105px]" data-name="ellispse">
      <div class="absolute inset-[-4.76%] rounded-full overflow-hidden bg-[#ffb6c9]">
        <img
          src="https://www.figma.com/api/mcp/asset/3dbcd54d-ed55-4aaa-83b5-b54049d4716e"
          alt="Avatar frame"
          class="w-full h-full object-cover"
        />
      </div>
      <!-- Avatar Image -->
      <div class="absolute inset-[19.21%_37.87%_69.58%_37.87%] rounded-full overflow-hidden">
        <img
          src="https://www.figma.com/api/mcp/asset/106410b9-ffed-43f8-a633-1b15951ca7c8"
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
      Wrong password! Try again or reset password
    </p>

    <!-- Forgot Password Link -->
    <p
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
