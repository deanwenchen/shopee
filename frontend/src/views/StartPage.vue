<script setup lang="ts">
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import PrimaryButton from '@/components/PrimaryButton.vue'
import SecondaryAction from '@/components/SecondaryAction.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'
import logoImage from '@/assets/images/shopping-bag.svg'

const router = useRouter()

const handleGetStarted = () => {
  console.log('Get Started clicked - Navigate to Create Account')
  router.push('/create-account')
}

const handleLogin = () => {
  console.log('Login clicked - Navigate to Login Page')
  router.push('/login')
}
</script>

<template>
  <div class="bg-white relative w-[375px] h-[817px] mx-auto" data-name="01 Start" data-node-id="0:12855">
    <!-- Status Bar -->
    <StatusBar />

    <!-- Logo Container - Centered -->
    <div class="absolute top-[232px] left-[121px] w-[133px] h-[133px]" data-name="Logo Container">
      <img :src="logoImage" alt="Shoppe Logo" class="w-full h-full object-contain" />
    </div>

    <!-- Brand Title -->
    <h1
      class="absolute left-[188px] top-[390px] font-raleway font-bold text-[52px] text-brand-black text-center tracking-[-0.52px] whitespace-nowrap -translate-x-1/2"
      data-node-id="0:12858"
    >
      Shoppe
    </h1>

    <!-- Subtitle -->
    <p
      class="absolute left-[187.5px] top-[469px] w-[249px] font-nunito font-light text-[19px] leading-[33px] text-brand-black text-center -translate-x-1/2"
      data-node-id="0:12859"
    >
      Beautiful eCommerce UI Kit for your online store
    </p>

    <!-- Primary Action Button -->
    <div class="absolute left-[20px] top-[634px]" data-node-id="0:12908">
      <PrimaryButton
        text="Let's get started"
        @click="handleGetStarted"
      />
    </div>

    <!-- Secondary Action -->
    <div class="absolute left-[81px] top-[713px]" data-node-id="0:12860">
      <SecondaryAction
        text="I already have an account"
        @click="handleLogin"
      />
    </div>

    <!-- Home Indicator -->
    <HomeIndicator />
  </div>
</template>

<style scoped>
</style>
