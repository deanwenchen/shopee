# Password Recovery Flow Implementation Plan

> **For agentic workers:** REQUIRED: Use superpowers:subagent-driven-development (if subagents available) or superpowers:executing-plans to implement this plan. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Implement 3 password recovery pages from Figma designs with complete navigation flow

**Architecture:**
- Create 3 new Vue components following existing patterns (StatusBar, HomeIndicator, bubble backgrounds)
- Add routes for password recovery flow
- Connect navigation from existing WrongPassword page "Forgot password?" link

**Tech Stack:** Vue 3 + TypeScript + Tailwind CSS + Vue Router

---

## Chunk 1: Password Recovery Page (07)

### Task 1: Create PasswordRecovery.vue component

**Files:**
- Create: `frontend/src/views/PasswordRecovery.vue`
- Modify: `frontend/src/router/index.ts` (add route)

- [ ] **Step 1: Create PasswordRecovery.vue with template structure**

```vue
<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'

const router = useRouter()
const recoveryMethod = ref<'sms' | 'email'>('sms')

const handleNext = () => {
  router.push('/password-recovery-code')
}

const handleCancel = () => {
  router.push('/login')
}

const selectMethod = (method: 'sms' | 'email') => {
  recoveryMethod.value = method
}
</script>

<template>
  <div class="bg-white relative w-[375px] h-[817px] mx-auto" data-name="07 Password Recovery" data-node-id="0:12449">
    <!-- Status Bar -->
    <StatusBar />

    <!-- Background Bubbles -->
    <div class="absolute top-[-273px] left-[20px]" data-name="Bubbles">
      <!-- Bubble 02 - rotated -110deg -->
      <div class="absolute top-[32px] left-0 w-[544px] h-[502px] -rotate-[110deg]">
        <img
          src="https://www.figma.com/api/mcp/asset/9c19b644-cb14-4fb2-a71e-ad837174465c"
          alt="Bubble 02"
          class="w-full h-full object-contain"
        />
      </div>
      <!-- Bubble 01 - rotated 92deg -->
      <div class="absolute top-0 left-[100px] w-[456px] h-[418px] rotate-[92deg]">
        <img
          src="https://www.figma.com/api/mcp/asset/d436c9f7-b996-4171-b868-5db5b8c4387c"
          alt="Bubble 01"
          class="w-full h-full object-contain"
        />
      </div>
    </div>

    <!-- Avatar Circle -->
    <div class="absolute top-[149px] left-[135px] w-[105px] h-[105px]" data-name="ellispse">
      <div class="absolute inset-[-4.76%] rounded-full overflow-hidden bg-[#ffb6c9]">
        <img
          src="https://www.figma.com/api/mcp/asset/8d1c66de-50de-4ff3-8131-916ed876edd2"
          alt="Avatar frame"
          class="w-full h-full object-cover"
        />
      </div>
      <!-- Avatar Image -->
      <div class="absolute inset-[19.21%_37.87%_69.58%_37.87%] rounded-full overflow-hidden">
        <img
          src="https://www.figma.com/api/mcp/asset/4da3e66c-ab19-482d-bb34-1aecc9bf42c0"
          alt="Avatar"
          class="w-full h-full object-cover"
        />
      </div>
    </div>

    <!-- Page Title -->
    <h1 class="absolute left-1/2 -translate-x-1/2 top-[266px] font-raleway font-bold text-[21px] leading-[30px] text-brand-black text-center tracking-[-0.21px]">
      Password Recovery
    </h1>

    <!-- Instruction Text -->
    <p class="absolute left-1/2 -translate-x-1/2 top-[301px] font-nunito font-light text-[19px] leading-[27px] text-black text-center w-[290px]">
      How you would like to restore your password?
    </p>

    <!-- SMS Option (selected) -->
    <button
      @click="selectMethod('sms')"
      class="absolute left-[88px] top-[386px] w-[199px] h-[40px] rounded-[18px] flex items-center justify-between px-[16px]"
      :class="recoveryMethod === 'sms' ? 'bg-[#e5ebfc]' : 'bg-[#f5f5f5]'"
    >
      <span class="font-raleway" :class="recoveryMethod === 'sms' ? 'font-bold text-[#004cff]' : 'font-medium text-black'">
        SMS
      </span>
      <div class="w-[20px] h-[20px] rounded-full" :class="recoveryMethod === 'sms' ? 'bg-[#004cff] flex items-center justify-center' : 'bg-[#e0e0e0]'">
        <svg v-if="recoveryMethod === 'sms'" width="12" height="10" viewBox="0 0 12 10" fill="none">
          <path d="M1 5L4 8L11 1" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        </svg>
      </div>
    </button>

    <!-- Email Option -->
    <button
      @click="selectMethod('email')"
      class="absolute left-[89px] top-[436px] w-[198px] h-[40px] rounded-[18px] flex items-center justify-between px-[16px]"
      :class="recoveryMethod === 'email' ? 'bg-[#ffebeb]' : 'bg-[#f5f5f5]'"
    >
      <span class="font-raleway" :class="recoveryMethod === 'email' ? 'font-bold text-black' : 'font-medium text-black'">
        Email
      </span>
      <div class="w-[20px] h-[20px] rounded-full" :class="recoveryMethod === 'email' ? 'bg-[#ff5790] flex items-center justify-center' : 'bg-[#e0e0e0]'">
        <svg v-if="recoveryMethod === 'email'" width="12" height="10" viewBox="0 0 12 10" fill="none">
          <path d="M1 5L4 8L11 1" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        </svg>
      </div>
    </button>

    <!-- Next Button -->
    <button
      @click="handleNext"
      class="absolute left-[20px] top-[634px] w-[335px] h-[61px] bg-[#004cff] rounded-[16px] flex items-center justify-center cursor-pointer border-none"
    >
      <span class="font-nunito font-light text-[22px] leading-[31px] text-[#f3f3f3]">
        Next
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
```

- [ ] **Step 2: Add route to router**

Modify `frontend/src/router/index.ts`:

```typescript
import PasswordRecovery from '@/views/PasswordRecovery.vue'

const routes = [
  // ... existing routes
  {
    path: '/password-recovery',
    name: 'password-recovery',
    component: PasswordRecovery
  }
]
```

- [ ] **Step 3: Commit**

```bash
git add frontend/src/views/PasswordRecovery.vue frontend/src/router/index.ts
git commit -m "feat: add Password Recovery page with SMS/Email selection"
```

---

## Chunk 2: Password Recovery Code Page (08)

### Task 2: Create PasswordRecoveryCode.vue component

**Files:**
- Create: `frontend/src/views/PasswordRecoveryCode.vue`
- Modify: `frontend/src/router/index.ts`

- [ ] **Step 1: Create PasswordRecoveryCode.vue**

```vue
<script setup lang="ts">
import { ref, watch, onMounted, nextTick } from 'vue'
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'

const router = useRouter()
const code = ref('')
const inputRef = ref<HTMLInputElement | null>(null)

onMounted(() => {
  nextTick(() => {
    inputRef.value?.focus()
  })
})

watch(code, (newValue) => {
  if (newValue.length === 4) {
    setTimeout(() => {
      router.push('/new-password')
    }, 500)
  }
})

const handleCancel = () => {
  router.push('/login')
}

const handleSendAgain = () => {
  console.log('Send code again')
  // Reset code
  code.value = ''
  nextTick(() => {
    inputRef.value?.focus()
  })
}
</script>

<template>
  <div class="bg-white relative w-[375px] h-[817px] mx-auto" data-name="08 Password Recovery — Code" data-node-id="0:12382">
    <!-- Status Bar -->
    <StatusBar />

    <!-- Background Bubbles -->
    <div class="absolute top-[-273px] left-[20px]" data-name="Bubbles">
      <!-- Bubble 02 - rotated -110deg -->
      <div class="absolute top-[32px] left-0 w-[544px] h-[502px] -rotate-[110deg]">
        <img
          src="https://www.figma.com/api/mcp/asset/c2dd1527-31ae-47f5-9051-91a5c687f22a"
          alt="Bubble 02"
          class="w-full h-full object-contain"
        />
      </div>
      <!-- Bubble 01 - rotated 92deg -->
      <div class="absolute top-0 left-[100px] w-[456px] h-[418px] rotate-[92deg]">
        <img
          src="https://www.figma.com/api/mcp/asset/9e822c92-18e2-479d-ac5d-b34d8f15cf4e"
          alt="Bubble 01"
          class="w-full h-full object-contain"
        />
      </div>
    </div>

    <!-- Avatar Circle -->
    <div class="absolute top-[149px] left-[135px] w-[105px] h-[105px]" data-name="ellispse">
      <div class="absolute inset-[-4.76%] rounded-full overflow-hidden bg-[#ffb6c9]">
        <img
          src="https://www.figma.com/api/mcp/asset/e362e4a2-f6a0-4fb4-b3b5-3bb27f04206b"
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
      Password Recovery
    </h1>

    <!-- Instruction Text -->
    <p class="absolute left-1/2 -translate-x-1/2 top-[301px] font-nunito font-light text-[19px] leading-[27px] text-black text-center w-[290px]">
      Enter 4-digits code we sent you on your phone number
    </p>

    <!-- Phone Number -->
    <p class="absolute left-1/2 -translate-x-1/2 top-[371px] font-nunito font-bold text-[16px] leading-[25px] text-black text-center tracking-[1.6px]">
      +98*******00
    </p>

    <!-- Hidden Input -->
    <input
      ref="inputRef"
      v-model="code"
      type="text"
      inputmode="numeric"
      maxlength="4"
      class="absolute opacity-0 left-[136px] top-[424px] w-[136px] h-[17px]"
      autofocus
    />

    <!-- Code Dots (4 dots at left: 136px, 165px, 194px, 223px) -->
    <div class="absolute left-[136px] top-[424px]" data-name="dots">
      <div class="absolute left-[0px] w-[17px] h-[17px]">
        <img
          v-if="code.length >= 1"
          src="https://www.figma.com/api/mcp/asset/e7e83f8b-5728-4e4c-89fd-187bf2dc4270"
          alt="Filled dot"
          class="w-full h-full"
        />
        <img
          v-else
          src="https://www.figma.com/api/mcp/asset/25bd7827-642a-4eb0-8971-e28624a121b9"
          alt="Empty dot"
          class="w-full h-full"
        />
      </div>
      <div class="absolute left-[29px] w-[17px] h-[17px]">
        <img
          v-if="code.length >= 2"
          src="https://www.figma.com/api/mcp/asset/e7e83f8b-5728-4e4c-89fd-187bf2dc4270"
          alt="Filled dot"
          class="w-full h-full"
        />
        <img
          v-else
          src="https://www.figma.com/api/mcp/asset/25bd7827-642a-4eb0-8971-e28624a121b9"
          alt="Empty dot"
          class="w-full h-full"
        />
      </div>
      <div class="absolute left-[58px] w-[17px] h-[17px]">
        <img
          v-if="code.length >= 3"
          src="https://www.figma.com/api/mcp/asset/e7e83f8b-5728-4e4c-89fd-187bf2dc4270"
          alt="Filled dot"
          class="w-full h-full"
        />
        <img
          v-else
          src="https://www.figma.com/api/mcp/asset/25bd7827-642a-4eb0-8971-e28624a121b9"
          alt="Empty dot"
          class="w-full h-full"
        />
      </div>
      <div class="absolute left-[87px] w-[17px] h-[17px]">
        <img
          v-if="code.length === 4"
          src="https://www.figma.com/api/mcp/asset/e7e83f8b-5728-4e4c-89fd-187bf2dc4270"
          alt="Filled dot"
          class="w-full h-full"
        />
        <img
          v-else
          src="https://www.figma.com/api/mcp/asset/25bd7827-642a-4eb0-8971-e28624a121b9"
          alt="Empty dot"
          class="w-full h-full"
        />
      </div>
    </div>

    <!-- Send Again Button -->
    <button
      @click="handleSendAgain"
      class="absolute left-[87px] top-[640px] w-[201px] h-[50px] bg-[#ff5790] rounded-[16px] flex items-center justify-center cursor-pointer border-none"
    >
      <span class="font-nunito font-light text-[22px] leading-[31px] text-[#f3f3f3]">
        Send Again
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
```

- [ ] **Step 2: Add route to router**

```typescript
import PasswordRecoveryCode from '@/views/PasswordRecoveryCode.vue'

const routes = [
  // ... existing routes
  {
    path: '/password-recovery-code',
    name: 'password-recovery-code',
    component: PasswordRecoveryCode
  }
]
```

- [ ] **Step 3: Commit**

```bash
git add frontend/src/views/PasswordRecoveryCode.vue frontend/src/router/index.ts
git commit -m "feat: add Password Recovery Code entry page"
```

---

## Chunk 3: New Password Page (09)

### Task 3: Create NewPassword.vue component

**Files:**
- Create: `frontend/src/views/NewPassword.vue`
- Modify: `frontend/src/router/index.ts`
- Modify: `frontend/src/views/WrongPassword.vue` (update Forgot password link)

- [ ] **Step 1: Create NewPassword.vue**

```vue
<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import StatusBar from '@/components/StatusBar.vue'
import HomeIndicator from '@/components/HomeIndicator.vue'

const router = useRouter()
const newPassword = ref('')
const repeatPassword = ref('')

const handleSave = () => {
  console.log('Save new password')
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
```

- [ ] **Step 2: Add route to router**

```typescript
import NewPassword from '@/views/NewPassword.vue'

const routes = [
  // ... existing routes
  {
    path: '/new-password',
    name: 'new-password',
    component: NewPassword
  }
]
```

- [ ] **Step 3: Update WrongPassword.vue Forgot password link**

Modify `frontend/src/views/WrongPassword.vue`:

```typescript
const handleForgotPassword = () => {
  router.push('/password-recovery')
}
```

- [ ] **Step 4: Commit**

```bash
git add frontend/src/views/NewPassword.vue frontend/src/router/index.ts frontend/src/views/WrongPassword.vue
git commit -m "feat: add New Password page and connect Forgot password link"
```

---

## Complete Navigation Flow

```
WrongPassword → (Forgot password?) → PasswordRecovery → (Next) → PasswordRecoveryCode → (input 4 digits) → NewPassword → (Save) → Login
```
