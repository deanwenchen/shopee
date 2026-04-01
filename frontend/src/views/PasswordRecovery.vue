<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth'
import StatusBar from '@/components/StatusBar.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'

const router = useRouter()
const authStore = useAuthStore()
const recoveryMethod = ref<'sms' | 'email'>('sms')
const isLoading = ref(false)
const showError = ref(false)
const errorMessage = ref('')

// Figma asset URLs
const bubble01 = '/assets/figma/recovery-bubble-01.svg'
const bubble02 = '/assets/figma/recovery-bubble-02.svg'
const ellipse = '/assets/figma/recovery-ellipse.svg'
const avatar3A06 = '/assets/figma/recovery-avatar-deco.svg'
const avatarC0A416 = '/assets/figma/recovery-avatar-photo.png'
const avatarMask = '/assets/figma/recovery-avatar-mask.svg'
const avatarArtist2 = '/assets/figma/recovery-avatar-main.jpg'
const checkIcon = '/assets/figma/recovery-check-icon.svg'
const checkEmptyIcon = '/assets/figma/recovery-check-empty.svg'

const handleNext = async () => {
  console.log('[PasswordRecovery] handleNext clicked')
  isLoading.value = true
  showError.value = false

  // Get email from sessionStorage (try loginEmail first, then recoveryEmail)
  const email = sessionStorage.getItem('loginEmail') || sessionStorage.getItem('recoveryEmail')
  console.log('[PasswordRecovery] email from sessionStorage:', email)

  if (!email) {
    // If no email in sessionStorage, prompt user or go back to login
    showError.value = true
    errorMessage.value = 'Please enter your email first'
    isLoading.value = false
    console.log('[PasswordRecovery] No email found, showing error:', errorMessage.value)
    return
  }

  console.log('[PasswordRecovery] Calling passwordRecovery with email:', email, 'method:', recoveryMethod.value)
  const result = await authStore.passwordRecovery(email, recoveryMethod.value)
  isLoading.value = false
  console.log('[PasswordRecovery] passwordRecovery result:', result)

  if (result.success && result.codeId) {
    // Store codeId for next step
    sessionStorage.setItem('recoveryCodeId', result.codeId)
    console.log('[PasswordRecovery] Navigating to /password-recovery-code')
    router.push('/password-recovery-code')
  } else {
    showError.value = true
    errorMessage.value = result.message || 'Failed to send verification code'
    console.log('[PasswordRecovery] Error:', errorMessage.value)
  }
}

const handleCancel = () => {
  router.push('/login')
}

const selectMethod = (method: 'sms' | 'email') => {
  recoveryMethod.value = method
}
</script>

<template>
  <div class="bg-white relative w-[375px] h-[817px] mx-auto overflow-hidden" data-name="07 Password Recovery" data-node-id="0:12449">
    <!-- Status Bar -->
    <StatusBar />

    <!-- Background Bubbles -->
    <div class="absolute top-[-273.16px] left-[20.28px] w-[354.72px] h-[780px] overflow-hidden pointer-events-none" data-name="Bubbles" data-node-id="0:12450" style="z-index: 0;">
      <!-- Bubble 02 - rotated -110deg -->
      <div class="absolute top-[-240.83px] left-[20.28px] flex h-[502.4px] w-[543.71px] items-center justify-center">
        <div class="-rotate-[110deg] flex-none h-[442.65px] w-[373.531px] relative">
          <img
            :src="bubble02"
            alt="Bubble 02"
            class="absolute block size-full max-w-none"
          />
        </div>
      </div>
      <!-- Bubble 01 - rotated 92deg -->
      <div class="absolute top-[-273.16px] left-[119.73px] flex h-[418.074px] w-[456.44px] items-center justify-center">
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
    <div class="absolute left-[187.5px] top-[149px] -translate-x-1/2 size-[106px] z-[2]" data-name="ellispse" data-node-id="0:12466">
      <div class="absolute inset-[-4.76%]">
        <img
          :src="ellipse"
          alt="Ellipse"
          class="block size-full max-w-none"
        />
      </div>
    </div>

    <!-- Avatar Image Group -->
    <div class="absolute left-[187.5px] top-[149px] -translate-x-1/2 size-[106px] z-[3]" data-name="image" data-node-id="0:12467">
      <!-- 3A06AFD8-BC44-46CC-8865-FDE3018427B6 -->
      <div class="absolute inset-[19.21%_37.87%_69.58%_37.87%]">
        <img
          :src="avatar3A06"
          alt="Avatar decoration"
          class="absolute block size-full max-w-none"
        />
      </div>
      <!-- C0A416F7-B1E5-4BD4-8929-2BEAA8633585 - masked avatar -->
      <div class="absolute inset-[12.42%_37.87%_62.79%_37.87%]" style="mask-image: url('/assets/figma/recovery-avatar-mask.svg'); mask-size: 91px 91px; mask-position: 0px 55.146px; mask-repeat: no-repeat; -webkit-mask-image: url('/assets/figma/recovery-avatar-mask.svg'); -webkit-mask-size: 91px 91px; -webkit-mask-position: 0px 55.146px; -webkit-mask-repeat: no-repeat;">
        <div class="absolute inset-0 overflow-hidden pointer-events-none">
          <img
            :src="avatarC0A416"
            alt="Avatar"
            class="absolute left-0 top-0 size-full max-w-none"
          />
        </div>
      </div>
      <!-- artist-2 1 -->
      <div class="absolute inset-0" style="mask-image: url('/assets/figma/recovery-avatar-mask.svg'); mask-size: 91px 91px; mask-position: 7px 8px; mask-repeat: no-repeat; -webkit-mask-image: url('/assets/figma/recovery-avatar-mask.svg'); -webkit-mask-size: 91px 91px; -webkit-mask-position: 7px 8px; -webkit-mask-repeat: no-repeat;">
        <img
          :src="avatarArtist2"
          alt="Avatar main"
          class="absolute inset-0 size-full max-w-none object-cover pointer-events-none"
        />
      </div>
    </div>

    <!-- Page Title -->
    <h1 class="absolute left-[188.5px] top-[266px] -translate-x-1/2 font-raleway font-bold text-[21px] leading-[30px] text-[#202020] text-center tracking-[-0.21px] whitespace-nowrap" data-node-id="0:12463">
      Password Recovery
    </h1>

    <!-- Instruction Text -->
    <p class="absolute left-[188px] top-[301px] -translate-x-1/2 font-nunito font-light text-[19px] leading-[27px] text-black text-center w-[290px] h-[57px]" data-node-id="0:12462" style="opacity: 0.9">
      How you would like to restore your password?
    </p>

    <!-- SMS Option (selected) -->
    <div class="absolute left-[88px] top-[386px] w-[199px] h-[40px] overflow-clip" data-name="SMS" data-node-id="0:12454">
      <div class="absolute inset-0 rounded-[18px]" :class="recoveryMethod === 'sms' ? 'bg-[#e5ebfc]' : 'bg-[#f5f5f5]'" data-name="Rectangle" data-node-id="0:12455" />
      <p class="absolute left-[42.21%] right-[41.71%] top-[calc(50%-9px)] text-[15px] text-center tracking-[-0.15px] whitespace-nowrap font-raleway" :class="recoveryMethod === 'sms' ? 'font-bold text-[#004cff] leading-[19px]' : 'font-medium text-black leading-normal'" data-node-id="0:12456">
        SMS
      </p>
      <div v-if="recoveryMethod === 'sms'" class="absolute top-[22.5%] right-[5.03%] bottom-[22.5%] left-[83.92%]" data-name="Check" data-node-id="0:12457">
        <div class="absolute inset-[-9.09%]">
          <img :src="checkIcon" alt="Check" class="block size-full max-w-none" />
        </div>
      </div>
    </div>

    <!-- Email Option -->
    <div class="absolute left-[89px] top-[436px] w-[198px] h-[40px] overflow-clip" data-name="Email" data-node-id="0:12458">
      <div class="absolute inset-0 rounded-[18px]" :class="recoveryMethod === 'email' ? 'bg-[#ffebeb]' : 'bg-[#f5f5f5]'" data-name="Rectangle" data-node-id="0:12459" />
      <p class="absolute left-[40.4%] right-[39.9%] top-[calc(50%-9px)] text-[15px] text-center tracking-[-0.15px] whitespace-nowrap font-raleway" :class="recoveryMethod === 'email' ? 'font-bold text-[#ff5790] leading-[19px]' : 'font-medium text-black leading-normal'" data-node-id="0:12460">
        Email
      </p>
      <div v-if="recoveryMethod === 'email'" class="absolute top-[22.5%] right-[5.05%] bottom-[22.5%] left-[83.84%]" data-name="Check Empty" data-node-id="0:12461">
        <div class="absolute inset-[-9.09%]">
          <img :src="checkEmptyIcon" alt="Check Empty" class="block size-full max-w-none" />
        </div>
      </div>
    </div>

    <!-- Clickable buttons overlay for SMS/Email selection -->
    <button
      @click="selectMethod('sms')"
      class="absolute left-[88px] top-[386px] w-[199px] h-[40px] bg-transparent border-none cursor-pointer z-[50]"
      data-node-id="0:12454-btn"
      type="button"
    />
    <button
      @click="selectMethod('email')"
      class="absolute left-[89px] top-[436px] w-[198px] h-[40px] bg-transparent border-none cursor-pointer z-[50]"
      data-node-id="0:12458-btn"
      type="button"
    />

    <!-- Next Button -->
    <button
      @click="handleNext"
      class="absolute left-[20px] top-[634px] w-[335px] h-[61px] bg-[#004cff] rounded-[16px] flex items-center justify-center cursor-pointer border-none z-[100]"
      data-name="Button" data-node-id="0:12515"
      type="button"
    >
      <span class="pointer-events-none font-nunito font-light text-[22px] leading-[31px] text-[#f3f3f3]" data-node-id="0:12517">
        Next
      </span>
    </button>

    <!-- Cancel Link -->
    <button
      @click="handleCancel"
      class="absolute left-[188.5px] top-[719px] -translate-x-1/2 font-nunito font-light text-[15px] leading-[26px] text-[#202020] opacity-90 bg-transparent border-none cursor-pointer whitespace-nowrap z-[100]"
      data-node-id="0:12453"
      type="button"
    >
      Cancel
    </button>

    <!-- Home Indicator -->
    <HomeIndicator />
  </div>
</template>

<style scoped>
</style>
