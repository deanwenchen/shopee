<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'

const router = useRouter()
const newPassword = ref('')
const repeatPassword = ref('')
const showPassword1 = ref(false)
const showPassword2 = ref(false)

// Figma asset URLs
const bubble01 = 'https://www.figma.com/api/mcp/asset/4a99ade5-6b24-4cc2-933d-d8f31b84065d'
const bubble02 = 'https://www.figma.com/api/mcp/asset/af2e68eb-5dd2-4033-98d6-07c69a9baff9'
const ellipse = 'https://www.figma.com/api/mcp/asset/107f6677-203f-4995-b90e-614e0b603eee'
const avatarMain = 'https://www.figma.com/api/mcp/asset/a5052054-69de-49d5-8ed5-6d4bfabd85ad'

const handleSave = () => {
  console.log('Save new password:', newPassword.value)
  // TODO: Call API to save new password
  router.push('/login')
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
    <div class="absolute top-[-273.16px] left-[20.28px]" data-name="Bubbles" data-node-id="0:12316">
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
    <div class="absolute top-[156px] left-[142px]" data-name="image" data-node-id="0:12323">
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
          src="@/assets/icons/artist.png"
          alt="Avatar frame"
          class="absolute block size-full max-w-none"
        />
      </div>
      <!-- Main avatar -->
      <div class="absolute top-[26.52px] left-[26.26px] w-[52.48px] h-[52.48px] mask-avatar">
        <img
          :src="avatarMain"
          alt="Avatar"
          class="absolute inset-0 size-full max-w-none object-cover"
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
          src="https://www.figma.com/api/mcp/asset/313428fc-ea69-4094-b8ea-56a00278eb22"
          alt="Show password"
          class="w-[16px] h-[16px]"
        />
        <img
          v-else
          src="https://www.figma.com/api/mcp/asset/313428fc-ea69-4094-b8ea-56a00278eb22"
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
          src="https://www.figma.com/api/mcp/asset/313428fc-ea69-4094-b8ea-56a00278eb22"
          alt="Show password"
          class="w-[16px] h-[16px]"
        />
        <img
          v-else
          src="https://www.figma.com/api/mcp/asset/313428fc-ea69-4094-b8ea-56a00278eb22"
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
  mask-image: url('https://www.figma.com/api/mcp/asset/63769f61-3a5a-4dd7-99c7-15f122fb5217');
  mask-size: 91px 91px;
  mask-position: 0px 55.146px;
  mask-repeat: no-repeat;
  -webkit-mask-image: url('https://www.figma.com/api/mcp/asset/63769f61-3a5a-4dd7-99c7-15f122fb5217');
  -webkit-mask-size: 91px 91px;
  -webkit-mask-position: 0px 55.146px;
  -webkit-mask-repeat: no-repeat;
}
</style>
