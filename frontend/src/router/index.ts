import { createRouter, createWebHistory } from 'vue-router'
import StartPage from '@/views/StartPage.vue'
import LoginPage from '@/views/LoginPage.vue'
import CreateAccount from '@/views/CreateAccount.vue'
import Password from '@/views/Password.vue'
import PasswordRecovery from '@/views/PasswordRecovery.vue'
import PasswordRecoveryCode from '@/views/PasswordRecoveryCode.vue'
import NewPassword from '@/views/NewPassword.vue'
import HelloCard from '@/views/HelloCard.vue'
import ShopPage from '@/views/ShopPage.vue'
import { useAuthStore } from '@/stores/auth'

const routes = [
  {
    path: '/',
    name: 'start',
    component: StartPage,
    meta: { guest: true }
  },
  {
    path: '/login',
    name: 'login',
    component: LoginPage,
    meta: { guest: true }
  },
  {
    path: '/create-account',
    name: 'create-account',
    component: CreateAccount,
    meta: { guest: true }
  },
  {
    path: '/password',
    name: 'password',
    component: Password,
    meta: { guest: true }
  },
  {
    path: '/password-recovery',
    name: 'password-recovery',
    component: PasswordRecovery,
    meta: { guest: true }
  },
  {
    path: '/password-recovery-code',
    name: 'password-recovery-code',
    component: PasswordRecoveryCode,
    meta: { guest: true }
  },
  {
    path: '/new-password',
    name: 'new-password',
    component: NewPassword,
    meta: { guest: true }
  },
  {
    path: '/hello-card',
    name: 'hello-card',
    component: HelloCard,
    meta: { requiresAuth: true }
  },
  {
    path: '/shop',
    name: 'shop',
    component: ShopPage,
    meta: { requiresAuth: true }
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

// Global navigation guard
router.beforeEach(async (to, from, next) => {
  const authStore = useAuthStore()

  // Always initialize auth check
  if (!authStore.accessToken && !authStore.isLoading) {
    await authStore.initAuth()
  }

  // Wait for loading to complete before making decisions
  if (authStore.isLoading) {
    await new Promise<void>(resolve => {
      const checkLoading = setInterval(() => {
        if (!authStore.isLoading) {
          clearInterval(checkLoading)
          resolve()
        }
      }, 50)
    })
  }

  // Protected routes - require authentication
  if (to.meta.requiresAuth) {
    if (!authStore.isAuthenticated) {
      next({ name: 'start' })
      return
    }
  }

  // Guest routes - redirect to shop if already authenticated
  if (to.meta.guest && authStore.isAuthenticated) {
    next({ name: 'shop' })
    return
  }

  next()
})

export default router
