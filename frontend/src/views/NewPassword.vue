<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'

const router = useRouter()
const newPassword = ref('')
const repeatPassword = ref('')

const handleSave = () => {
  console.log('Save new password:', newPassword.value)
  // TODO: Call API to save new password
  router.push('/login')
}

const handleCancel = () => {
  router.push('/login')
}
</script>

<template>
  <div class="bg-white relative w-[375px] h-[817px] mx-auto" data-name="09 New Password" data-node-id="0:12315">
    <!-- Status Bar -->
    <StatusBar />

    <!-- Background Bubbles -->
    <div class="absolute top-[-273px] left-[20px]" data-name="Bubbles">
      <!-- Bubble 02 - rotated -110deg -->
      <div class="absolute top-[32px] left-0 w-[544px] h-[502px] -rotate-[110deg]">
        <img
          src="https://www.figma.com/api/mcp/asset/e6db3fb1-1bed-4d08-9dbb-ddf2f97870a5"
          alt="Bubble 02"
          class="w-full h-full object-contain"
        />
      </div>
      <!-- Bubble 01 - rotated 92deg -->
      <div class="absolute top-0 left-[100px] w-[456px] h-[418px] rotate-[92deg]">
        <img
          src="https://www.figma.com/api/mcp/asset/e1f7b958-f4f4-4e35-8483-5527d5c4f457"
          alt="Bubble 01"
          class="w-full h-full object-contain"
        />
      </div>
    </div>

    <!-- Avatar Circle -->
    <div class="absolute top-[149px] left-[135px] w-[105px] h-[105px]" data-name="ellispse">
      <div class="absolute inset-[-4.76%] rounded-full overflow-hidden bg-[#ffb6c9]">
        <img
          src="https://www.figma.com/api/mcp/asset/c80d1bc4-1353-4ee5-beae-dadbf1ada485"
          alt="Avatar frame"
          class="w-full h-full object-cover"
        />
      </div>
      <!-- Avatar Image -->
      <div class="absolute inset-[19.21%_37.87%_69.58%_37.87%] rounded-full overflow-hidden">
        <img
          src="https://www.figma.com/api/mcp/asset/9cd9e73f-eaac-421d-a54e-5513c3cfb4df"
          alt="Avatar"
          class="w-full h-full object-cover"
        />
      </div>
    </div>

    <!-- Page Title -->
    <h1 class="absolute left-1/2 -translate-x-1/2 top-[266px] font-raleway font-bold text-[21px] leading-[30px] text-brand-black text-center tracking-[-0.21px]">
      Setup New Password
    </h1>

    <!-- Instruction Text -->
    <p class="absolute left-1/2 -translate-x-1/2 top-[301px] font-nunito font-light text-[19px] leading-[27px] text-black text-center w-[290px]">
      Please, setup a new password for your account
    </p>

    <!-- New Password Input -->
    <div class="absolute left-[20px] top-[381px] w-[335px] h-[50px] bg-[#f8f8f8] rounded-[9px] flex items-center justify-center">
      <input
        v-model="newPassword"
        type="password"
        placeholder="New Password"
        class="w-full h-full bg-transparent text-center font-raleway font-medium text-[17px] text-[#dcdcdc] border-none outline-none placeholder-[#dcdcdc]"
      />
    </div>

    <!-- Repeat Password Input -->
    <div class="absolute left-[20px] top-[441px] w-[335px] h-[50px] bg-[#f8f8f8] rounded-[9px] flex items-center justify-center">
      <input
        v-model="repeatPassword"
        type="password"
        placeholder="Repeat Password"
        class="w-full h-full bg-transparent text-center font-raleway font-medium text-[17px] text-[#dcdcdc] border-none outline-none placeholder-[#dcdcdc]"
      />
    </div>

    <!-- Save Button -->
    <button
      @click="handleSave"
      class="absolute left-[20px] top-[634px] w-[335px] h-[61px] bg-[#004cff] rounded-[16px] flex items-center justify-center cursor-pointer border-none"
    >
      <span class="font-nunito font-light text-[22px] leading-[31px] text-[#f3f3f3]">
        Save
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
