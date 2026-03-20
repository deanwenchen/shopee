<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import NextButton from '@/components/NextButton.vue'
import FormInput from '@/components/FormInput.vue'

const router = useRouter()

const email = ref('')

const handleNext = () => {
  console.log('Login with email:', email.value)
  // TODO: 调用登录 API
  // 跳转到密码输入页面
  router.push('/password')
}

const handleCancel = () => {
  console.log('Cancel clicked')
  router.push('/')
}
</script>

<template>
  <div class="bg-white relative w-[375px] h-[817px] mx-auto" data-name="03 Login" data-node-id="0:12718">
    <!-- Status Bar -->
    <StatusBar />

    <!-- Background Bubbles Container -->
    <div class="absolute top-[-172px] left-[-158px]" data-name="Bubbles">
      <!-- Bubble 02 - 浅蓝色大气泡（旋转 158 度）- 在底层，先渲染 -->
      <div class="absolute top-[0px] left-[22px] w-[512px] h-[550px] rotate-[158deg]" data-name="bubble 02">
        <img
          src="https://www.figma.com/api/mcp/asset/56fc5330-5d3d-4979-a050-37d85c359820"
          alt="Bubble 02"
          class="w-full h-full object-contain"
        />
      </div>

      <!-- Bubble 01 - 深蓝色大气泡 - 在浅蓝色上面，后渲染 -->
      <div class="absolute top-[0px] left-[0px] w-[403px] h-[443px]" data-name="bubble 01">
        <img
          src="https://www.figma.com/api/mcp/asset/a007d904-91e7-4dab-92c2-975266a4496a"
          alt="Bubble 01"
          class="w-full h-full object-contain"
        />
      </div>

      <!-- Bubble 03 - 右侧小气泡（旋转 -156 度） -->
      <div class="absolute top-[411px] left-[440px] w-[187px] h-[194px] -rotate-[156deg]" data-name="bubblle 03">
        <img
          src="https://www.figma.com/api/mcp/asset/4047a2b8-7488-4f38-a0d0-fd665eeb8276"
          alt="Bubble 03"
          class="w-full h-full object-contain"
        />
      </div>

      <!-- Bubble 04 - 底部大气泡（旋转 108 度） -->
      <div class="absolute top-[621px] left-[245px] w-[536px] h-[492px] rotate-[108deg]" data-name="bubble 04">
        <img
          src="https://www.figma.com/api/mcp/asset/a8768779-5a96-411c-83de-55b550d454d1"
          alt="Bubble 04"
          class="w-full h-full object-contain"
        />
      </div>
    </div>

    <!-- Heart Icon -->
    <div class="absolute top-[535px] left-[215px] w-[24px] h-[24px]" data-name="heart">
      <img
        src="https://www.figma.com/api/mcp/asset/0e7c1044-1b0e-4025-ba9a-fd788bf676d4"
        alt="Heart"
        class="w-full h-full object-contain"
      />
    </div>

    <!-- Page Title -->
    <div class="absolute left-[20px] top-[438px]" data-node-id="0:12735">
      <h1 class="font-raleway font-bold text-[52px] leading-[normal] text-brand-black tracking-[-0.52px]">
        Login
      </h1>
      <p class="font-nunito font-light text-[19px] leading-[35px] text-brand-black mt-[10px]">
        Good to see you back! <img src="https://www.figma.com/api/mcp/asset/0e7c1044-1b0e-4025-ba9a-fd788bf676d4" alt="" class="inline w-[20px] h-[20px] align-middle" />
      </p>
    </div>

    <!-- Email Input -->
    <div class="absolute left-[21px] top-[555px]" data-node-id="4:14107">
      <FormInput
        v-model="email"
        placeholder="Email"
        type="email"
      />
    </div>

    <!-- Next Button -->
    <div class="absolute left-[187px] top-[644px] -translate-x-1/2" data-node-id="9:6550">
      <NextButton text="Next" @click="handleNext" />
    </div>

    <!-- Cancel Link -->
    <button
      @click="handleCancel"
      class="absolute left-[188px] top-[719px] -translate-x-1/2 font-nunito font-light text-[15px] leading-[26px] text-brand-black opacity-90 bg-transparent border-none cursor-pointer"
      data-node-id="0:12736"
    >
      Cancel
    </button>

    <!-- Home Indicator -->
    <div class="absolute bottom-[24px] left-[121px] w-[134px] h-[5px] bg-black rounded-shoppe-xl" data-name="Bar" />
  </div>
</template>

<style scoped>
</style>
