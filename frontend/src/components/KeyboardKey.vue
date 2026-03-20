<script setup lang="ts">
// 键盘单个按键组件
interface Props {
  letter?: string
  width?: string
  isSpecial?: boolean
}

withDefaults(defineProps<Props>(), {
  letter: '',
  width: '32px',
  isSpecial: false
})
</script>

<template>
  <button
    class="h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"
    :style="{ width }"
  >
    <span class="font-nunito font-normal text-[22.5px] text-black leading-[normal]">
      {{ letter }}
    </span>
  </button>
</template>

<style scoped>
</style>
