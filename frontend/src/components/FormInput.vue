<script setup lang="ts">
interface Props {
  placeholder: string
  modelValue?: string
  type?: string
}

withDefaults(defineProps<Props>(), {
  placeholder: '',
  modelValue: '',
  type: 'text'
})

defineEmits<{
  'update:modelValue': [value: string]
}>()
</script>

<template>
  <div class="bg-[#f8f8f8] content-stretch flex h-[52px] items-center px-[20px] py-[16px] rounded-[60px] w-[335px]">
    <input
      :type="type"
      :value="modelValue"
      :placeholder="placeholder"
      class="flex-[1_0_0] font-['Poppins:Medium',sans-serif] leading-[1.4] border-none bg-transparent outline-none text-[14px] w-full text-[#333333] placeholder-[#d2d2d2]"
      @input="$emit('update:modelValue', ($event.target as HTMLInputElement).value)"
    />
    <slot name="append" />
  </div>
</template>

<style scoped>
</style>
