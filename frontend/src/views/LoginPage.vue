<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import NextButton from '@/components/NextButton.vue'
import FormInput from '@/components/FormInput.vue'

const router = useRouter()

const email = ref('')
const showError = ref(false)
const errorMessage = ref('')

// 验证邮箱格式
const validateEmail = (email: string): boolean => {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
  return emailRegex.test(email)
}

const handleNext = () => {
  // 清空之前的错误
  showError.value = false
  errorMessage.value = ''

  // 验证邮箱是否为空
  if (!email.value.trim()) {
    showError.value = true
    errorMessage.value = 'Please enter your email'
    return
  }

  // 验证邮箱格式
  if (!validateEmail(email.value)) {
    showError.value = true
    errorMessage.value = 'Please enter a valid email address'
    return
  }

  // 验证是否为正确账号
  if (email.value === 'deanwen@gmail.com') {
    // 账号正确，跳转到密码输入页面
    router.push('/password')
  } else {
    // 账号错误，显示错误提示
    showError.value = true
    errorMessage.value = 'Account not found. Please check your email.'
  }
}

const handleCancel = () => {
  console.log('Cancel clicked')
  router.push('/')
}
</script>

<template>
  <div class="bg-white relative w-[375px] h-[817px] mx-auto overflow-hidden" data-name="03 Login" data-node-id="0:12718">
    <!-- Status Bar -->
    <StatusBar />

    <!-- Background Bubbles Container -->
    <div class="absolute top-[-172px] left-[-158px] w-[533px] h-[800px] overflow-hidden pointer-events-none" data-name="Bubbles">
      <!-- Bubble 02 - 浅蓝色大气泡（旋转 158 度）- 在底层，先渲染 -->
      <div class="absolute top-[0px] left-[22px] w-[512px] h-[550px] rotate-[158deg]" data-name="bubble 02">
        <img
          src="../assets/figma/56fc5330-5d3d-4979-a050-37d85c359820.svg"
          alt="Bubble 02"
          class="w-full h-full object-contain"
        />
      </div>

      <!-- Bubble 01 - 深蓝色大气泡 - 在浅蓝色上面，后渲染 -->
      <div class="absolute top-[0px] left-[0px] w-[403px] h-[443px]" data-name="bubble 01">
        <img
          src="../assets/figma/a007d904-91e7-4dab-92c2-975266a4496a.svg"
          alt="Bubble 01"
          class="w-full h-full object-contain"
        />
      </div>

      <!-- Bubble 03 - 右侧小气泡（旋转 -156 度） -->
      <div class="absolute top-[411px] left-[440px] w-[187px] h-[194px] -rotate-[156deg]" data-name="bubblle 03">
        <img
          src="../assets/figma/4047a2b8-7488-4f38-a0d0-fd665eeb8276.svg"
          alt="Bubble 03"
          class="w-full h-full object-contain"
        />
      </div>

      <!-- Bubble 04 - 底部大气泡（旋转 108 度） -->
      <div class="absolute top-[621px] left-[245px] w-[536px] h-[492px] rotate-[108deg]" data-name="bubble 04">
        <img
          src="../assets/figma/a8768779-5a96-411c-83de-55b550d454d1.svg"
          alt="Bubble 04"
          class="w-full h-full object-contain"
        />
      </div>
    </div>



    <!-- Page Title -->
    <div class="absolute left-[20px] top-[438px]" data-node-id="0:12735">
      <h1 class="font-raleway font-bold text-[52px] leading-[normal] text-brand-black tracking-[-0.52px]">
        Login
      </h1>
      <p class="font-nunito font-light text-[19px] leading-[35px] text-brand-black mt-[10px]">
        Good to see you back! <img src="../assets/figma/0e7c1044-1b0e-4025-ba9a-fd788bf676d4.svg" alt="" class="inline w-[20px] h-[20px] align-middle" />
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

    <!-- Error Message -->
    <div
      v-if="showError"
      class="absolute left-[21px] top-[605px] font-nunito font-light text-[13px] leading-[20px] text-red-500"
    >
      {{ errorMessage }}
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
