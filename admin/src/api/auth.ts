import { request } from '@/utils/request'

export interface LoginRequest {
  username: string
  password: string
}

export interface LoginResponse {
  adminId: number
  username: string
  email?: string
  token: string
  refreshToken: string
  permissions: string[]
}

export interface AdminInfo {
  id: number
  username: string
  email?: string
  permissions: string[]
}

export function loginApi(data: LoginRequest) {
  return request<LoginResponse>({
    url: '/auth/login',
    method: 'post',
    data
  })
}

export function logoutApi() {
  return request({
    url: '/auth/logout',
    method: 'post'
  })
}

export function getCurrentAdminApi() {
  return request<AdminInfo>({
    url: '/auth/me',
    method: 'get'
  })
}
