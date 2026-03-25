import axios from 'axios'
import type { AxiosInstance, InternalAxiosRequestConfig, AxiosResponse, AxiosRequestConfig } from 'axios'
import { useAuthStore } from '@/stores/auth'
import { ElMessage } from 'element-plus'

const service: AxiosInstance = axios.create({
  baseURL: '/api',
  timeout: 15000
})

// Request interceptor
service.interceptors.request.use(
  (config: InternalAxiosRequestConfig) => {
    const authStore = useAuthStore()
    if (authStore.token) {
      config.headers.Authorization = `Bearer ${authStore.token}`
    }
    return config
  },
  (error) => {
    return Promise.reject(error)
  }
)

// Response interceptor - unwrap response to return data directly
service.interceptors.response.use(
  (response: AxiosResponse) => {
    return response.data
  },
  (error) => {
    const authStore = useAuthStore()

    if (error.response?.status === 401) {
      // Token 过期，跳转登录
      authStore.logout()
      window.location.href = '/login'
    } else if (error.response?.status === 403) {
      // 无权限
      ElMessage.error('无权限访问')
    } else if (error.response?.status === 500) {
      // 服务器错误
      ElMessage.error(error.response.data?.message || '服务器错误')
    }

    return Promise.reject(error)
  }
)

export function request<T>(config: AxiosRequestConfig): Promise<T> {
  return service.request(config)
}

export default service
