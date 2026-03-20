<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'

const router = useRouter()
const recoveryMethod = ref<'sms' | 'email'>('sms')

const handleNext = () => {
  console.log('Recovery method selected:', recoveryMethod.value)
  router.push('/password-recovery-code')
}

const handleCancel = () => {
  router.push('/login')
}

const selectMethod = (method: 'sms' | 'email') => {
  recoveryMethod.value = method
}
</script>

<template>
  <div class="bg-white relative w-[375px] h-[817px] mx-auto" data-name="07 Password Recovery" data-node-id="0:12449">
    <!-- Status Bar -->
    <StatusBar />

    <!-- Background Bubbles -->
    <div class="absolute top-[-273px] left-[20px]" data-name="Bubbles">
      <!-- Bubble 02 - rotated -110deg -->
      <div class="absolute top-[32px] left-0 w-[544px] h-[502px] -rotate-[110deg]">
        <img
          src="https://www.figma.com/api/mcp/asset/9c19b644-cb14-4fb2-a71e-ad837174465c"
          alt="Bubble 02"
          class="w-full h-full object-contain"
        />
      </div>
      <!-- Bubble 01 - rotated 92deg -->
      <div class="absolute top-0 left-[100px] w-[456px] h-[418px] rotate-[92deg]">
        <img
          src="https://www.figma.com/api/mcp/asset/d436c9f7-b996-4171-b868-5db5b8c4387c"
          alt="Bubble 01"
          class="w-full h-full object-contain"
        />
      </div>
    </div>

    <!-- Avatar Circle -->
    <div class="absolute top-[149px] left-[135px] w-[105px] h-[105px]" data-name="ellispse">
      <div class="absolute inset-[-4.76%] rounded-full overflow-hidden bg-[#ffb6c9]">
        <img
          src="https://www.figma.com/api/mcp/asset/8d1c66de-50de-4ff3-8131-916ed876edd2"
          alt="Avatar frame"
          class="w-full h-full object-cover"
        />
      </div>
      <!-- Avatar Image -->
      <div class="absolute inset-[19.21%_37.87%_69.58%_37.87%] rounded-full overflow-hidden">
        <img
          src="https://www.figma.com/api/mcp/asset/4da3e66c-ab19-482d-bb34-1aecc9bf42c0"
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
      How you would like to restore your password?
    </p>

    <!-- SMS Option (selected) -->
    <button
      @click="selectMethod('sms')"
      class="absolute left-[88px] top-[386px] w-[199px] h-[40px] rounded-[18px] flex items-center justify-between px-[16px] transition-colors"
      :class="recoveryMethod === 'sms' ? 'bg-[#e5ebfc]' : 'bg-[#f5f5f5]'"
    >
      <span class="font-raleway" :class="recoveryMethod === 'sms' ? 'font-bold text-[#004cff]' : 'font-medium text-black'">
        SMS
      </span>
      <div class="w-[20px] h-[20px] rounded-full flex items-center justify-center" :class="recoveryMethod === 'sms' ? 'bg-[#004cff]' : 'bg-[#e0e0e0]'">
        <svg v-if="recoveryMethod === 'sms'" width="12" height="10" viewBox="0 0 12 10" fill="none">
          <path d="M1 5L4 8L11 1" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        </svg>
      </div>
    </button>

    <!-- Email Option -->
    <button
      @click="selectMethod('email')"
      class="absolute left-[89px] top-[436px] w-[198px] h-[40px] rounded-[18px] flex items-center justify-between px-[16px] transition-colors"
      :class="recoveryMethod === 'email' ? 'bg-[#ffebeb]' : 'bg-[#f5f5f5]'"
    >
      <span class="font-raleway" :class="recoveryMethod === 'email' ? 'font-bold text-black' : 'font-medium text-black'">
        Email
      </span>
      <div class="w-[20px] h-[20px] rounded-full flex items-center justify-center" :class="recoveryMethod === 'email' ? 'bg-[#ff5790]' : 'bg-[#e0e0e0]'">
        <svg v-if="recoveryMethod === 'email'" width="12" height="10" viewBox="0 0 12 10" fill="none">
          <path d="M1 5L4 8L11 1" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        </svg>
      </div>
    </button>

    <!-- Next Button -->
    <button
      @click="handleNext"
      class="absolute left-[20px] top-[634px] w-[335px] h-[61px] bg-[#004cff] rounded-[16px] flex items-center justify-center cursor-pointer border-none"
    >
      <span class="font-nunito font-light text-[22px] leading-[31px] text-[#f3f3f3]">
        Next
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
  </div>
</template>

<style scoped>
</style>
