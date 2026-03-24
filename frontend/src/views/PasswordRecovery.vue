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
const bubble01 = '../assets/figma/1afb7a0d-c11b-4a9a-8e61-5ead6fffec23.svg'
const bubble02 = '../assets/figma/d31e2712-7080-4907-9c35-e4baff6bdee5.svg'
const ellipse = '../assets/figma/05b73873-ad02-4a83-bbc3-947026b13eee.svg'
const avatar3A06 = '../assets/figma/aa0e35dd-f8fd-4072-b35f-834ff4a95a73.svg'
const avatarC0A416 = '../assets/figma/0c20e9cb-8161-4915-9fa3-6837f4bebe14.png'
const avatarMask = '../assets/figma/6cd29425-f045-434b-9bba-edf0f4f699df.svg'
const avatarArtist2 = '../assets/figma/877a4afd-bfed-49c8-90dc-5b7809c99b2e.jpg'
const checkIcon = '../assets/figma/8dfe51c7-d0b1-4bb8-b502-ed108389b3f5.svg'
const checkEmptyIcon = '../assets/figma/ad430095-ecd8-42b2-841c-8572415af049.svg'

const handleNext = async () => {
  isLoading.value = true
  showError.value = false

  // Get email from sessionStorage (set from login page or user needs to enter it)
  const email = sessionStorage.getItem('recoveryEmail')

  if (!email) {
    // If no email in sessionStorage, prompt user or go back to login
    showError.value = true
    errorMessage.value = 'Please enter your email first'
    isLoading.value = false
    return
  }

  const result = await authStore.passwordRecovery(email, recoveryMethod)
  isLoading.value = false

  if (result.success && result.codeId) {
    // Store codeId for next step
    sessionStorage.setItem('recoveryCodeId', result.codeId)
    router.push('/password-recovery-code')
  } else {
    showError.value = true
    errorMessage.value = result.message || 'Failed to send verification code'
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
    <div class="absolute top-[-273.16px] left-[20.28px] w-[354.72px] h-[780px] overflow-hidden pointer-events-none" data-name="Bubbles" data-node-id="0:12450">
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
    <div class="absolute left-[187.5px] top-[149px] -translate-x-1/2 size-[106px]" data-name="ellispse" data-node-id="0:12466">
      <div class="absolute inset-[-4.76%]">
        <img
          :src="ellipse"
          alt="Ellipse"
          class="block size-full max-w-none"
        />
      </div>
    </div>

    <!-- Avatar Image Group -->
    <div class="absolute left-[187.5px] top-[149px] -translate-x-1/2 size-[106px]" data-name="image" data-node-id="0:12467">
      <!-- 3A06AFD8-BC44-46CC-8865-FDE3018427B6 -->
      <div class="absolute inset-[19.21%_37.87%_69.58%_37.87%]">
        <img
          :src="avatar3A06"
          alt="Avatar decoration"
          class="absolute block size-full max-w-none"
        />
      </div>
      <!-- C0A416F7-B1E5-4BD4-8929-2BEAA8633585 - masked avatar -->
      <div class="absolute inset-[12.42%_37.87%_62.79%_37.87%]" style="mask-image: url('../assets/figma/6cd29425-f045-434b-9bba-edf0f4f699df.svg'); mask-size: 91px 91px; mask-position: 0px 55.146px; mask-repeat: no-repeat; -webkit-mask-image: url('../assets/figma/6cd29425-f045-434b-9bba-edf0f4f699df.svg'); -webkit-mask-size: 91px 91px; -webkit-mask-position: 0px 55.146px; -webkit-mask-repeat: no-repeat;">
        <div class="absolute inset-0 overflow-hidden pointer-events-none">
          <img
            :src="avatarC0A416"
            alt="Avatar"
            class="absolute left-0 top-0 size-full max-w-none"
          />
        </div>
      </div>
      <!-- artist-2 1 -->
      <div class="absolute inset-0" style="mask-image: url('../assets/figma/6cd29425-f045-434b-9bba-edf0f4f699df.svg'); mask-size: 91px 91px; mask-position: 7px 8px; mask-repeat: no-repeat; -webkit-mask-image: url('../assets/figma/6cd29425-f045-434b-9bba-edf0f4f699df.svg'); -webkit-mask-size: 91px 91px; -webkit-mask-position: 7px 8px; -webkit-mask-repeat: no-repeat;">
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
      <p class="absolute left-[40.4%] right-[39.9%] top-[calc(50%-9px)] text-[15px] text-center tracking-[-0.15px] whitespace-nowrap font-raleway font-medium text-black" data-node-id="0:12460">
        Email
      </p>
      <div v-if="recoveryMethod === 'email'" class="absolute top-[22.5%] right-[5.05%] bottom-[22.5%] left-[83.84%]" data-name="Check Empty" data-node-id="0:12461">
        <div class="absolute inset-[-9.09%]">
          <img :src="checkEmptyIcon" alt="Check Empty" class="block size-full max-w-none" />
        </div>
      </div>
    </div>

    <!-- Clickable buttons overlay -->
    <button
      @click="selectMethod('sms')"
      class="absolute left-[88px] top-[386px] w-[199px] h-[40px] bg-transparent border-none cursor-pointer"
      data-node-id="0:12454-btn"
    />
    <button
      @click="selectMethod('email')"
      class="absolute left-[89px] top-[436px] w-[198px] h-[40px] bg-transparent border-none cursor-pointer"
      data-node-id="0:12458-btn"
    />

    <!-- Next Button -->
    <button
      @click="handleNext"
      class="absolute left-[20px] top-[634px] w-[335px] h-[61px] bg-[#004cff] rounded-[16px] flex items-center justify-center cursor-pointer border-none"
      data-name="Button" data-node-id="0:12515"
    >
      <span class="font-nunito font-light text-[22px] leading-[31px] text-[#f3f3f3]" data-node-id="0:12517">
        Next
      </span>
    </button>

    <!-- Cancel Link -->
    <button
      @click="handleCancel"
      class="absolute left-[188.5px] top-[719px] -translate-x-1/2 font-nunito font-light text-[15px] leading-[26px] text-[#202020] opacity-90 bg-transparent border-none cursor-pointer whitespace-nowrap"
      data-node-id="0:12453"
    >
      Cancel
    </button>

    <!-- Home Indicator -->
    <HomeIndicator />
  </div>
</template>

<style scoped>
</style>
