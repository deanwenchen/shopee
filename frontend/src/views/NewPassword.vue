<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth'
import StatusBar from '@/components/StatusBar.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'

const router = useRouter()
const authStore = useAuthStore()
const newPassword = ref('')
const repeatPassword = ref('')
const showPassword1 = ref(false)
const showPassword2 = ref(false)
const showError = ref(false)
const errorMessage = ref('')
const isLoading = ref(false)

// Figma asset URLs
const bubble01 = '../assets/figma/f04821bb-dcc4-46ca-b2a1-5b59f443bf5f.svg'
const bubble02 = '../assets/figma/90d3c347-98d9-4f72-aec6-2deB6070603F.svg'
const ellipse = '../assets/figma/58006e4b-da06-4d80-a4a0-b32cbb465954.svg'
const avatarEF6B = '../assets/figma/77e59ace-f77e-4a18-b27f-b1ae8617b340.svg'
const avatar8C7A = '../assets/figma/a86862a8-6a33-4c94-9b81-952d3960ae81.svg'
const avatarMask = '../assets/figma/a86862a8-6a33-4c94-9b81-952d3960ae81.svg'
const avatarArtist2 = '../assets/figma/159d3385-63e5-47de-b7ec-10f295efffbe.jpg'

const handleSave = async () => {
  showError.value = false
  errorMessage.value = ''

  // Validate passwords
  if (!newPassword.value) {
    showError.value = true
    errorMessage.value = 'Please enter a new password'
    return
  }

  if (newPassword.value.length < 8) {
    showError.value = true
    errorMessage.value = 'Password must be at least 8 characters'
    return
  }

  if (newPassword.value !== repeatPassword.value) {
    showError.value = true
    errorMessage.value = 'Passwords do not match'
    return
  }

  // Get reset token from sessionStorage
  const resetToken = sessionStorage.getItem('resetToken') || ''

  if (!resetToken) {
    showError.value = true
    errorMessage.value = 'Invalid reset token. Please start the recovery process again.'
    return
  }

  isLoading.value = true
  const result = await authStore.resetPassword(resetToken, newPassword.value)
  isLoading.value = false

  if (result.success) {
    // Password reset successful
    alert('Password reset successfully! Please login with your new password.')
    sessionStorage.removeItem('resetToken')
    router.push('/login')
  } else {
    showError.value = true
    errorMessage.value = result.message || 'Failed to reset password'
  }
}

const handleCancel = () => {
  router.push('/login')
}

const togglePassword1 = () => {
  showPassword1.value = !showPassword1.value
}

const togglePassword2 = () => {
  showPassword2.value = !showPassword2.value
}
</script>

<template>
  <div class="bg-white relative w-[375px] h-[817px] mx-auto overflow-hidden" data-name="09 New Password" data-node-id="0:12315">
    <!-- Status Bar -->
    <StatusBar />

    <!-- Background Bubbles -->
    <div class="absolute top-[-273.16px] left-[20.28px] w-[354.72px] h-[780px] overflow-hidden pointer-events-none" data-name="Bubbles" data-node-id="0:12316">
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
    <div class="absolute left-[187.5px] top-[149px] -translate-x-1/2 w-[105px] h-[110px] z-10" data-name="ellispse" data-node-id="0:12322">
      <div class="absolute inset-[-4.76%]">
        <img
          :src="ellipse"
          alt="Ellipse"
          class="block size-full max-w-none"
        />
      </div>
    </div>

    <!-- Avatar Image Group -->
    <div class="absolute left-[187.5px] top-[149px] -translate-x-1/2 w-[105px] h-[110px] z-10" data-name="image" data-node-id="0:12323">
      <!-- EF6B71E4-B0D6-4280-BBFB-2DEB6070603F -->
      <div class="absolute inset-[19.21%_37.87%_69.58%_37.87%]">
        <img
          :src="avatarEF6B"
          alt="Avatar decoration"
          class="absolute block size-full max-w-none"
        />
      </div>
      <!-- 8C7A435A-3E32-4D89-BCEB-E976B0F70AF3 - masked avatar -->
      <div class="absolute inset-[12.42%_37.87%_62.79%_37.87%]" style="mask-image: url('../assets/figma/a86862a8-6a33-4c94-9b81-952d3960ae81.svg'); mask-size: 91px 91px; mask-position: 0px 55.146px; mask-repeat: no-repeat; -webkit-mask-image: url('../assets/figma/a86862a8-6a33-4c94-9b81-952d3960ae81.svg'); -webkit-mask-size: 91px 91px; -webkit-mask-position: 0px 55.146px; -webkit-mask-repeat: no-repeat;">
        <div class="absolute inset-0 overflow-hidden pointer-events-none">
          <img
            :src="avatar8C7A"
            alt="Avatar"
            class="absolute left-0 top-0 size-full max-w-none"
          />
        </div>
      </div>
      <!-- artist-2 1 -->
      <div class="absolute inset-0" style="mask-image: url('../assets/figma/a86862a8-6a33-4c94-9b81-952d3960ae81.svg'); mask-size: 91px 91px; mask-position: 7px 8px; mask-repeat: no-repeat; -webkit-mask-image: url('../assets/figma/a86862a8-6a33-4c94-9b81-952d3960ae81.svg'); -webkit-mask-size: 91px 91px; -webkit-mask-position: 7px 8px; -webkit-mask-repeat: no-repeat;">
        <img
          :src="avatarArtist2"
          alt="Avatar main"
          class="absolute inset-0 size-full max-w-none object-cover pointer-events-none"
        />
      </div>
    </div>

    <!-- Page Title -->
    <h1 class="absolute left-[188.5px] top-[266px] -translate-x-1/2 font-raleway font-bold text-[21px] leading-[30px] text-[#202020] text-center tracking-[-0.21px] whitespace-nowrap" data-node-id="0:12319">
      Setup New Password
    </h1>

    <!-- Instruction Text -->
    <p class="absolute left-[188px] top-[301px] -translate-x-1/2 font-nunito font-light text-[19px] leading-[27px] text-black text-center w-[290px] h-[57px]" data-node-id="0:12378">
      Please, setup a new password for your account
    </p>

    <!-- New Password Input -->
    <div class="absolute left-[20px] top-[381px] w-[335px] h-[50px] bg-[#f8f8f8] rounded-[9px] flex items-center px-[20px]" data-name="Password" data-node-id="0:12374">
      <input
        ref="passwordInput1"
        v-model="newPassword"
        :type="showPassword1 ? 'text' : 'password'"
        placeholder="New Password"
        class="w-full h-full bg-transparent font-raleway font-medium text-[17px] text-[#333333] border-none outline-none placeholder-[#dcdcdc]"
      />
      <button @click="togglePassword1" class="ml-2">
        <img
          v-if="showPassword1"
          src="../assets/figma/313428fc-ea69-4094-b8ea-56a00278eb22.svg"
          alt="Show password"
          class="w-[16px] h-[16px]"
        />
        <img
          v-else
          src="../assets/figma/313428fc-ea69-4094-b8ea-56a00278eb22.svg"
          alt="Hide password"
          class="w-[16px] h-[16px]"
        />
      </button>
    </div>

    <!-- Repeat Password Input -->
    <div class="absolute left-[20px] top-[441px] w-[335px] h-[50px] bg-[#f8f8f8] rounded-[9px] flex items-center px-[20px]" data-name="Repeat Password" data-node-id="0:12371">
      <input
        ref="passwordInput2"
        v-model="repeatPassword"
        :type="showPassword2 ? 'text' : 'password'"
        placeholder="Repeat Password"
        class="w-full h-full bg-transparent font-raleway font-medium text-[17px] text-[#333333] border-none outline-none placeholder-[#dcdcdc]"
      />
      <button @click="togglePassword2" class="ml-2">
        <img
          v-if="showPassword2"
          src="../assets/figma/313428fc-ea69-4094-b8ea-56a00278eb22.svg"
          alt="Show password"
          class="w-[16px] h-[16px]"
        />
        <img
          v-else
          src="../assets/figma/313428fc-ea69-4094-b8ea-56a00278eb22.svg"
          alt="Hide password"
          class="w-[16px] h-[16px]"
        />
      </button>
    </div>

    <!-- Save Button -->
    <button
      @click="handleSave"
      class="absolute left-[20px] top-[634px] w-[335px] h-[61px] bg-[#004cff] rounded-[16px] flex items-center justify-center cursor-pointer border-none"
      data-name="Button" data-node-id="0:12379"
    >
      <span class="font-nunito font-light text-[22px] leading-[31px] text-[#f3f3f3]" data-node-id="0:12381">
        Save
      </span>
    </button>

    <!-- Cancel Link -->
    <button
      @click="handleCancel"
      class="absolute left-[188.5px] top-[719px] -translate-x-1/2 font-nunito font-light text-[15px] leading-[26px] text-[#202020] opacity-90 bg-transparent border-none cursor-pointer whitespace-nowrap"
      data-node-id="0:12377"
    >
      Cancel
    </button>

    <!-- Home Indicator -->
    <HomeIndicator />
  </div>
</template>

<style scoped>
.mask-avatar {
  mask-image: url('../assets/figma/63769f61-3a5a-4dd7-99c7-15f122fb5217.svg');
  mask-size: 91px 91px;
  mask-position: 0px 55.146px;
  mask-repeat: no-repeat;
  -webkit-mask-image: url('../assets/figma/63769f61-3a5a-4dd7-99c7-15f122fb5217.svg');
  -webkit-mask-size: 91px 91px;
  -webkit-mask-position: 0px 55.146px;
  -webkit-mask-repeat: no-repeat;
}
</style>
