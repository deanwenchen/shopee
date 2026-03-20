<script setup lang="ts">
import { ref, watch, onMounted, nextTick } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'

const router = useRouter()
const route = useRoute()
const inputRef = ref<HTMLInputElement | null>(null)

// 使用真实密码输入框（隐藏），唤起系统键盘
const password = ref('')

// 监听来自 Password 页面的密码参数并聚焦输入框
onMounted(() => {
  if (route.query.password) {
    password.value = route.query.password as string
  }
  // 聚焦隐藏输入框，唤起系统键盘
  nextTick(() => {
    inputRef.value?.focus()
  })
})

// 监听密码输入，当密码长度达到 8 位时自动跳转
watch(password, (newValue) => {
  if (newValue.length === 8) {
    // 密码输入完成，跳转到错误密码页面
    setTimeout(() => {
      router.push({ path: '/wrong-password', query: { password: password.value } })
    }, 500)
  }
})

const handleNotYou = () => {
  router.push('/login')
}
</script>

<template>
  <div class="bg-white relative w-[375px] h-[817px] mx-auto" data-name="05 Password Typing" data-node-id="0:12584">
    <!-- Status Bar -->
    <StatusBar />

    <!-- Background Bubbles -->
    <div class="absolute top-[-172px] left-[-158px]" data-name="Bubbles">
      <!-- Bubble 02 - 浅蓝色大气泡（旋转 158 度） -->
      <div class="absolute top-[0px] left-[22px] w-[512px] h-[550px] rotate-[158deg]">
        <img
          src="https://www.figma.com/api/mcp/asset/87207947-76cf-492f-8dc8-efe4d8998496"
          alt="Bubble 02"
          class="w-full h-full object-contain"
        />
      </div>

      <!-- Bubble 01 - 深蓝色大气泡 -->
      <div class="absolute top-[0px] left-[0px] w-[403px] h-[443px]">
        <img
          src="https://www.figma.com/api/mcp/asset/7d0d20c9-158b-4fb1-a334-a562ea492074"
          alt="Bubble 01"
          class="w-full h-full object-contain"
        />
      </div>
    </div>

    <!-- Avatar Circle -->
    <div class="absolute top-[149px] left-[135px] w-[105px] h-[105px]" data-name="ellipse">
      <div class="absolute inset-[-4.76%] rounded-full overflow-hidden bg-[#ffb6c9]">
        <img
          src="https://www.figma.com/api/mcp/asset/e21ef7ee-77ef-4d49-9190-a8ae9b2b66c6"
          alt="Avatar frame"
          class="w-full h-full object-cover"
        />
      </div>
      <!-- Avatar Image -->
      <div class="absolute inset-[19.21%_37.87%_69.58%_37.87%] rounded-full overflow-hidden">
        <img
          src="https://www.figma.com/api/mcp/asset/cd35377c-39a6-4f27-9804-fee221b3e91b"
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

    <!-- Hidden Password Input (唤起系统键盘) -->
    <input
      ref="inputRef"
      v-model="password"
      type="password"
      maxlength="8"
      class="absolute opacity-0 left-[78px] top-[390px] w-[261px] h-[17px]"
      autofocus
    />

    <!-- Password Dots - 根据输入动态显示 -->
    <div class="absolute left-[78px] top-[390px]" data-name="Dots">
      <!-- Dot 1 -->
      <div class="absolute left-[0px] w-[17px] h-[17px]">
        <img
          v-if="password.length >= 1"
          src="https://www.figma.com/api/mcp/asset/b324fa17-8d12-4309-a9d3-da82a6394522"
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
      <!-- Dot 2 -->
      <div class="absolute left-[29px] w-[17px] h-[17px]">
        <img
          v-if="password.length >= 2"
          src="https://www.figma.com/api/mcp/asset/b324fa17-8d12-4309-a9d3-da82a6394522"
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
      <!-- Dot 3 -->
      <div class="absolute left-[58px] w-[17px] h-[17px]">
        <img
          v-if="password.length >= 3"
          src="https://www.figma.com/api/mcp/asset/b324fa17-8d12-4309-a9d3-da82a6394522"
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
      <!-- Dot 4 -->
      <div class="absolute left-[87px] w-[17px] h-[17px]">
        <img
          v-if="password.length >= 4"
          src="https://www.figma.com/api/mcp/asset/b324fa17-8d12-4309-a9d3-da82a6394522"
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
      <!-- Dot 5 -->
      <div class="absolute left-[116px] w-[17px] h-[17px]">
        <img
          v-if="password.length >= 5"
          src="https://www.figma.com/api/mcp/asset/b324fa17-8d12-4309-a9d3-da82a6394522"
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
      <!-- Dot 6 -->
      <div class="absolute left-[145px] w-[17px] h-[17px]">
        <img
          v-if="password.length >= 6"
          src="https://www.figma.com/api/mcp/asset/b324fa17-8d12-4309-a9d3-da82a6394522"
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
      <!-- Dot 7 -->
      <div class="absolute left-[174px] w-[17px] h-[17px]">
        <img
          v-if="password.length >= 7"
          src="https://www.figma.com/api/mcp/asset/b324fa17-8d12-4309-a9d3-da82a6394522"
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
      <!-- Dot 8 -->
      <div class="absolute left-[203px] w-[17px] h-[17px]">
        <img
          v-if="password.length === 8"
          src="https://www.figma.com/api/mcp/asset/b324fa17-8d12-4309-a9d3-da82a6394522"
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

    <!-- Home Indicator -->
    <HomeIndicator />
  </div>
</template>

<style scoped>
</style>
