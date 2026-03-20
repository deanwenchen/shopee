<script setup lang="ts">
interface Props {
  placeholder: string
  modelValue?: string
}

withDefaults(defineProps<Props>(), {
  placeholder: '',
  modelValue: ''
})

const showPassword = defineModel<boolean>('showPassword', { default: false })

defineEmits<{
  'update:modelValue': [value: string]
}>()

const togglePassword = () => {
  showPassword.value = !showPassword.value
}
</script>

<template>
  <div class="bg-[#f8f8f8] content-stretch flex h-[52px] items-center px-[20px] py-[16px] rounded-[60px] w-[335px]">
    <input
      :type="showPassword ? 'text' : 'password'"
      :value="modelValue"
      :placeholder="placeholder"
      class="flex-[1_0_0] font-['Poppins:Medium',sans-serif] leading-[1.4] border-none bg-transparent outline-none text-[14px] w-full text-[#333333] placeholder-[#d2d2d2]"
      @input="$emit('update:modelValue', ($event.target as HTMLInputElement).value)"
    />
    <button
      @click="togglePassword"
      class="ml-2 cursor-pointer border-none bg-transparent p-0"
    >
      <img
        :src="showPassword
          ? 'https://www.figma.com/api/mcp/asset/313428fc-ea69-4094-b8ea-56a00278eb22'
          : 'https://www.figma.com/api/mcp/asset/313428fc-ea69-4094-b8ea-56a00278eb22'"
        alt="Toggle password"
        class="w-[16px] h-[16px]"
      />
    </button>
  </div>
</template>

<style scoped>
</style>
