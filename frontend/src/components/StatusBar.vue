<script setup lang="ts">
</script>

<template>
  <div class="absolute top-0 left-0 w-[375px] h-[44px] flex items-center justify-between px-[20px]" data-name="Status Bar">
    <span class="font-['Nunito_Sans:SemiBold',sans-serif] font-semibold text-[14px] text-black">9:41</span>
    <div class="flex items-center gap-[5px]">
      <!-- Cellular Signal -->
      <img src="@/assets/icons/cellular.svg" alt="Cellular signal" class="w-[17px] h-[10.667px]" />
      <!-- WiFi -->
      <img src="@/assets/icons/wifi.svg" alt="WiFi" class="w-[15.272px] h-[10.966px]" />
      <!-- Battery -->
      <img src="@/assets/icons/battery.svg" alt="Battery" class="w-[24.328px] h-[11.333px]" />
    </div>
  </div>
</template>

<style scoped>
</style>
