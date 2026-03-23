<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import NextButton from '@/components/NextButton.vue'
import FormInput from '@/components/FormInput.vue'
import PasswordInput from '@/components/PasswordInput.vue'

const router = useRouter()

const email = ref('')
const password = ref('')
const phoneNumber = ref('')
const showPassword = ref(false)
const showError = ref(false)
const errorMessage = ref('')

// 验证邮箱格式
const validateEmail = (email: string): boolean => {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
  return emailRegex.test(email)
}

// 验证密码长度
const validatePassword = (password: string): boolean => {
  return password.length >= 8
}

const handleDone = () => {
  // 清空之前的错误
  showError.value = false
  errorMessage.value = ''

  // 验证邮箱
  if (!email.value.trim()) {
    showError.value = true
    errorMessage.value = 'Please enter your email'
    return
  }
  if (!validateEmail(email.value)) {
    showError.value = true
    errorMessage.value = 'Please enter a valid email address'
    return
  }

  // 验证密码
  if (!password.value) {
    showError.value = true
    errorMessage.value = 'Please enter a password'
    return
  }
  if (!validatePassword(password.value)) {
    showError.value = true
    errorMessage.value = 'Password must be at least 8 characters'
    return
  }

  console.log('Create Account:', { email: email.value, password: password.value, phoneNumber: phoneNumber.value })
  // TODO: 调用注册 API
  // 注册成功后跳转到登录页面，让用户重新登录
  router.push('/login')
}

const handleCancel = () => {
  console.log('Cancel clicked')
  router.push('/')
}
</script>

<template>
  <div class="bg-white relative w-[375px] h-[817px] mx-auto overflow-hidden" data-name="02 Create Account" data-node-id="0:12779">
    <!-- Status Bar -->
    <StatusBar />

    <!-- Background Bubbles -->
    <div class="absolute top-[-206px] left-[-132px] w-[659px] h-[513px]" data-name="Bubbles">
      <img
        src="../assets/figma/70ae7a39-be04-4867-96d7-bf710ace7dcb.svg"
        alt="Bubbles background"
        class="w-full h-full object-contain"
      />
    </div>

    <!-- Page Title -->
    <div class="absolute left-[30px] top-[122px]" data-node-id="0:12812">
      <h1 class="font-raleway font-bold text-[50px] leading-[54px] text-brand-black tracking-[-0.5px]">
        Create<br />Account
      </h1>
    </div>

    <!-- Upload Photo -->
    <div class="absolute left-[30px] top-[284px] w-[90px] h-[90px]" data-name="Upload Photo">
      <div class="relative w-full h-full">
        <img
          src="../assets/figma/5cf44006-dae9-4be9-ac93-3937d16a9be3.png"
          alt="Upload placeholder"
          class="w-full h-full object-cover rounded-full border-2 border-dashed border-brand-blue"
        />
        <div class="absolute right-[28px] top-[31px] w-[34px] h-[28px]" data-name="camera icon">
          <img
            src="../assets/figma/df477442-f952-447f-9e34-26b0260de677.svg"
            alt="Camera"
            class="w-full h-full"
          />
        </div>
      </div>
    </div>

    <!-- Form Fields -->
    <div class="absolute left-[20px] top-[406px] flex flex-col gap-[8px]" data-node-id="4:14078">
      <!-- Email Input -->
      <FormInput
        v-model="email"
        placeholder="Email"
        type="email"
      />

      <!-- Password Input -->
      <PasswordInput
        v-model="password"
        v-model:show-password="showPassword"
        placeholder="Password"
      />

      <!-- Phone Number with Country Flag -->
      <div class="bg-[#f8f8f8] content-stretch flex h-[55px] items-center px-[20px] py-[16px] rounded-[60px] w-[335px] gap-[16px]">
        <div class="flex items-center gap-[8px]">
          <div class="w-[24px] h-[18px] rounded-[4px] overflow-hidden relative">
            <img
              src="../assets/figma/d85b2ef1-423c-43fb-9a18-791e4f7189a5.png"
              alt="England flag"
              class="w-full h-full object-cover"
            />
          </div>
          <img
            src="../assets/figma/ecb20b03-74c8-439a-a583-1c3142b33d8e.svg"
            alt="Country dropdown"
            class="w-[16px] h-[16px]"
          />
        </div>
        <div class="w-[1px] h-[24px] bg-[#d2d2d2]" />
        <input
          v-model="phoneNumber"
          type="tel"
          placeholder="Your number"
          class="flex-[1_0_0] font-['Poppins:Medium',sans-serif] leading-[1.4] border-none bg-transparent outline-none text-[#333333] text-[14px] w-full placeholder-[#d2d2d2]"
        />
      </div>
    </div>

    <!-- Error Message -->
    <div
      v-if="showError"
      class="absolute left-[20px] top-[600px] font-nunito font-light text-[13px] leading-[20px] text-red-500"
    >
      {{ errorMessage }}
    </div>

    <!-- Done Button -->
    <div class="absolute left-[20px] top-[634px]" data-node-id="0:12809">
      <NextButton text="Done" @click="handleDone" />
    </div>

    <!-- Cancel Link -->
    <button
      @click="handleCancel"
      class="absolute left-[188px] top-[719px] -translate-x-1/2 font-nunito font-light text-[15px] leading-[26px] text-brand-black opacity-90 bg-transparent border-none cursor-pointer"
      data-node-id="0:12785"
    >
      Cancel
    </button>

    <!-- Home Indicator -->
    <div class="absolute bottom-[24px] left-[121px] w-[134px] h-[5px] bg-black rounded-shoppe-xl" data-name="Bar" />
  </div>
</template>

<style scoped>
</style>
