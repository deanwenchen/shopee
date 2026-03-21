<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'

const router = useRouter()
const recoveryMethod = ref<'sms' | 'email'>('sms')

// Figma asset URLs
const bubble01 = 'https://www.figma.com/api/mcp/asset/b8e325ee-b084-4f0d-a732-352c807a4bbe'
const bubble02 = 'https://www.figma.com/api/mcp/asset/49e82bd7-c14c-4f92-9292-1a8e642fcea3'
const ellipse = 'https://www.figma.com/api/mcp/asset/63769f61-3a5a-4dd7-99c7-15f122fb5217'
const avatarFrame = 'https://www.figma.com/api/mcp/asset/8170b441-8445-4132-aa6d-9be92cf35f8a'
const avatarMask = 'https://www.figma.com/api/mcp/asset/1c7eb3a3-abb7-4de5-803f-72fcd24efb34'
const avatarMain = 'https://www.figma.com/api/mcp/asset/82b28a0b-3852-45e3-a64d-542cea01ec12'
const checkIcon = 'https://www.figma.com/api/mcp/asset/b6486150-28cc-4da9-ac00-29f25b9ba411'
const checkEmptyIcon = 'https://www.figma.com/api/mcp/asset/e8ac7e5a-9a7f-4cb0-a18d-89906b83a0a7'

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
  <div class="bg-white relative w-[375px] h-[817px] mx-auto overflow-hidden" data-name="07 Password Recovery" data-node-id="0:12449">
    <!-- Status Bar -->
    <StatusBar />

    <!-- Background Bubbles -->
    <div class="absolute top-[-273.16px] left-[20.28px]" data-name="Bubbles" data-node-id="0:12450">
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

    <!-- Avatar Image Group -->
    <div class="absolute top-[156px] left-[142px]" data-name="image" data-node-id="0:12467">
      <!-- Background ellipse -->
      <div class="absolute top-[0px] left-[0px] w-[105px] h-[105px]">
        <img
          :src="ellipse"
          alt="Ellipse"
          class="absolute block size-full max-w-none"
        />
      </div>
      <!-- Avatar frame -->
      <div class="absolute top-[20.29px] left-[39.88px] w-[25.24px] h-[11.13px]">
        <img
          :src="avatarFrame"
          alt="Avatar frame"
          class="absolute block size-full max-w-none"
        />
      </div>
      <!-- Main avatar with mask -->
      <div class="absolute top-[26.52px] left-[26.26px] w-[52.48px] h-[52.48px] overflow-hidden" style="mask-image: url('https://www.figma.com/api/mcp/asset/1c7eb3a3-abb7-4de5-803f-72fcd24efb34'); mask-size: 91px 91px; mask-position: 0px 55.146px; mask-repeat: no-repeat; -webkit-mask-image: url('https://www.figma.com/api/mcp/asset/1c7eb3a3-abb7-4de5-803f-72fcd24efb34'); -webkit-mask-size: 91px 91px; -webkit-mask-position: 0px 55.146px; -webkit-mask-repeat: no-repeat;">
        <img
          :src="avatarMain"
          alt="Avatar"
          class="absolute inset-0 size-full max-w-none object-cover"
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
