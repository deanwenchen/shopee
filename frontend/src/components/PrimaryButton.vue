<script setup lang="ts">
interface Props {
  text: string
  onClick?: () => void
}

withDefaults(defineProps<Props>(), {
  text: 'Button'
})

defineEmits<{
  click: []
}>()
</script>

<template>
  <button
    class="w-[335px] h-[61px] bg-brand-blue rounded-shoppe-btn flex items-center justify-center cursor-pointer border-none"
    @click="$emit('click')"
    :data-name="text"
  >
    <span class="font-nunito font-light text-[22px] leading-[31px] text-brand-white">
      {{ text }}
    </span>
  </button>
</template>

<style scoped>
</style>
