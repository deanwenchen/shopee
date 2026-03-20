import { createRouter, createWebHistory } from 'vue-router'
import StartPage from '@/views/StartPage.vue'
import LoginPage from '@/views/LoginPage.vue'
import CreateAccount from '@/views/CreateAccount.vue'
import PasswordTyping from '@/views/PasswordTyping.vue'
import WrongPassword from '@/views/WrongPassword.vue'
import Password from '@/views/Password.vue'

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
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
