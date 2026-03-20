<script setup lang="ts">
// iOS 风格字母键盘组件
// 包含 3 行字母键 + 1 行功能键
const emit = defineEmits<{
  keyInput: [key: string]
  keyDelete: []
  keyGo: []
}>()

// 处理字母键点击
const handleLetterKey = (letter: string) => {
  emit('keyInput', letter)
}

// 处理删除键
const handleKeyDelete = () => {
  emit('keyDelete')
}

// 处理 Go 键
const handleKeyGo = () => {
  emit('keyGo')
}
</script>

<template>
  <div class="absolute left-0 top-[521px] w-[375px] h-[291px]" data-name="Keyboard Alphabetic">
    <!-- 键盘背景 -->
    <div class="absolute inset-0 bg-[rgba(204,206,211,0.76)]" />

    <!-- 按键区域 -->
    <div class="absolute inset-[8px_3px_24px_3px] overflow-clip">
      <!-- 第一行 QWERTY 10 个键 -->
      <div class="absolute flex gap-[2px] left-0 top-0">
        <button @click="handleLetterKey('Q')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">Q</span></button>
        <button @click="handleLetterKey('W')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">W</span></button>
        <button @click="handleLetterKey('E')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">E</span></button>
        <button @click="handleLetterKey('R')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">R</span></button>
        <button @click="handleLetterKey('T')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">T</span></button>
        <button @click="handleLetterKey('Y')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">Y</span></button>
        <button @click="handleLetterKey('U')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">U</span></button>
        <button @click="handleLetterKey('I')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">I</span></button>
        <button @click="handleLetterKey('O')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">O</span></button>
        <button @click="handleLetterKey('P')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">P</span></button>
      </div>

      <!-- 第二行 ASDFGHJKL 9 个键 -->
      <div class="absolute flex gap-[2px] left-[19px] top-[42px]">
        <button @click="handleLetterKey('A')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">A</span></button>
        <button @click="handleLetterKey('S')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">S</span></button>
        <button @click="handleLetterKey('D')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">D</span></button>
        <button @click="handleLetterKey('F')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">F</span></button>
        <button @click="handleLetterKey('G')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">G</span></button>
        <button @click="handleLetterKey('H')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">H</span></button>
        <button @click="handleLetterKey('J')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">J</span></button>
        <button @click="handleLetterKey('K')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">K</span></button>
        <button @click="handleLetterKey('L')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">L</span></button>
      </div>

      <!-- 第三行 ZXCVBNM 7 个键 -->
      <div class="absolute flex gap-[2px] left-[47px] top-[84px]">
        <button @click="handleLetterKey('Z')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">Z</span></button>
        <button @click="handleLetterKey('X')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">X</span></button>
        <button @click="handleLetterKey('C')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">C</span></button>
        <button @click="handleLetterKey('V')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">V</span></button>
        <button @click="handleLetterKey('B')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">B</span></button>
        <button @click="handleLetterKey('N')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">N</span></button>
        <button @click="handleLetterKey('M')" class="w-[32px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center"><span class="font-nunito font-normal text-[22.5px] text-black">M</span></button>
      </div>

      <!-- 第四行功能键 -->
      <!-- Shift -->
      <button class="absolute left-[3px] top-[126px] w-[42px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center">
        <img src="https://www.figma.com/api/mcp/asset/0b568db6-0265-4ef6-a68b-d3c7fe45f5cb" alt="Shift" class="w-[18.88px] h-[16.456px]" />
      </button>

      <!-- 123 -->
      <button class="absolute left-[47px] top-[126px] w-[60px] h-[42px] bg-[#adb3bc] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center">
        <span class="font-nunito font-normal text-[16px] text-black">123</span>
      </button>

      <!-- Space -->
      <button @click="handleLetterKey(' ')" class="absolute left-1/2 -translate-x-1/2 top-[126px] w-[182px] h-[42px] bg-[#fcfcfe] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center">
        <span class="font-nunito font-normal text-[16px] text-black">space</span>
      </button>

      <!-- Go -->
      <button @click="handleKeyGo" class="absolute right-[3px] top-[126px] w-[88px] h-[42px] bg-[#adb3bc] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center">
        <span class="font-nunito font-normal text-[16px] text-black">Go</span>
      </button>

      <!-- Delete -->
      <button @click="handleKeyDelete" class="absolute right-[3px] top-[84px] w-[42px] h-[42px] bg-[#adb3bc] rounded-[4.6px] shadow-[0px_1px_0px_0px_#898a8d] flex items-center justify-center">
        <img src="https://www.figma.com/api/mcp/asset/cf7c3eb6-6a31-48e6-853f-926dcca5be80" alt="Delete" class="w-[18.88px] h-[16.456px]" />
      </button>

      <!-- Emoji -->
      <button class="absolute left-[20px] bottom-[24px] w-[30px] h-[30px] bg-transparent flex items-center justify-center">
        <img src="https://www.figma.com/api/mcp/asset/ee8cd16d-d3a1-49df-a1fd-9985348316d1" alt="Emoji" class="w-[24px] h-[24px]" />
      </button>

      <!-- Dictation -->
      <button class="absolute right-[20px] bottom-[24px] w-[30px] h-[30px] bg-transparent flex items-center justify-center">
        <img src="https://www.figma.com/api/mcp/asset/9a16895e-a05a-448c-a7be-51ce5b26abff" alt="Dictation" class="w-[24px] h-[24px]" />
      </button>
    </div>

    <!-- Home Indicator -->
    <div class="absolute bottom-[9px] left-1/2 -translate-x-1/2 w-[134px] h-[5px] bg-black rounded-[100px]" />
  </div>
</template>

<style scoped>
</style>
