import { ref } from 'vue'
import { defineStore } from 'pinia'
import { loginApi, logoutApi, getCurrentAdminApi } from '@/api/auth'
import type { LoginRequest, LoginResponse } from '@/api/auth'

export interface AdminUser {
  id: number
  username: string
  email?: string
  permissions: string[]
}

export const useAuthStore = defineStore('auth', () => {
  const token = ref<string>('')
  const refreshToken = ref<string>('')
  const admin = ref<AdminUser | null>(null)
  const permissions = ref<string[]>([])

  // Login
  async function login(request: LoginRequest): Promise<LoginResponse> {
    const data = await loginApi(request)
    token.value = data.token
    refreshToken.value = data.refreshToken
    admin.value = {
      id: data.adminId,
      username: data.username,
      email: data.email,
      permissions: data.permissions
    }
    permissions.value = data.permissions
    // Persist to localStorage
    localStorage.setItem('token', data.token)
    localStorage.setItem('refreshToken', data.refreshToken)
    return data
  }

  // Logout
  async function logout() {
    await logoutApi()
    token.value = ''
    refreshToken.value = ''
    admin.value = null
    permissions.value = []
    localStorage.removeItem('token')
    localStorage.removeItem('refreshToken')
  }

  // Get current admin
  async function getCurrentAdmin() {
    const data = await getCurrentAdminApi()
    admin.value = {
      id: data.id,
      username: data.username,
      email: data.email,
      permissions: data.permissions
    }
    permissions.value = data.permissions
  }

  // Check permission
  function hasPermission(code: string): boolean {
    return permissions.value.includes(code)
  }

  // Init from localStorage
  function initFromStorage() {
    const savedToken = localStorage.getItem('token')
    const savedRefreshToken = localStorage.getItem('refreshToken')
    if (savedToken) {
      token.value = savedToken
    }
    if (savedRefreshToken) {
      refreshToken.value = savedRefreshToken
    }
  }

  return {
    token,
    refreshToken,
    admin,
    permissions,
    login,
    logout,
    getCurrentAdmin,
    hasPermission,
    initFromStorage
  }
})
