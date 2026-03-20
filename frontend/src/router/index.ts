import { createRouter, createWebHistory } from 'vue-router'
import StartPage from '@/views/StartPage.vue'
import LoginPage from '@/views/LoginPage.vue'
import CreateAccount from '@/views/CreateAccount.vue'
import PasswordTyping from '@/views/PasswordTyping.vue'
import WrongPassword from '@/views/WrongPassword.vue'
import Password from '@/views/Password.vue'
import PasswordRecovery from '@/views/PasswordRecovery.vue'
import PasswordRecoveryCode from '@/views/PasswordRecoveryCode.vue'
import NewPassword from '@/views/NewPassword.vue'

const routes = [
  {
    path: '/',
    name: 'start',
    component: StartPage
  },
  {
    path: '/login',
    name: 'login',
    component: LoginPage
  },
  {
    path: '/create-account',
    name: 'create-account',
    component: CreateAccount
  },
  {
    path: '/password-typing',
    name: 'password-typing',
    component: PasswordTyping
  },
  {
    path: '/wrong-password',
    name: 'wrong-password',
    component: WrongPassword
  },
  {
    path: '/password',
    name: 'password',
    component: Password
  },
  {
    path: '/password-recovery',
    name: 'password-recovery',
    component: PasswordRecovery
  },
  {
    path: '/password-recovery-code',
    name: 'password-recovery-code',
    component: PasswordRecoveryCode
  },
  {
    path: '/new-password',
    name: 'new-password',
    component: NewPassword
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
