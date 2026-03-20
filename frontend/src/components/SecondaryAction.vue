<script setup lang="ts">
interface Props {
  text: string
  onClick?: () => void
}

withDefaults(defineProps<Props>(), {
  text: 'Secondary Action'
})

defineEmits<{
  click: []
}>()
</script>

<template>
  <div class="flex items-center justify-center gap-[10px]" data-name="Secondary Action">
    <span class="font-nunito font-light text-[15px] leading-[26px] text-brand-black opacity-90">
      {{ text }}
    </span>
    <button
      class="w-[30px] h-[30px] bg-brand-blue rounded-full flex items-center justify-center cursor-pointer border-none"
      @click="$emit('click')"
      data-name="Arrow Button"
    >
      <img src="@/assets/icons/arrow.svg" alt="Arrow" class="w-[14.457px] h-[11.387px]" />
    </button>
  </div>
</template>

<style scoped>
</style>
