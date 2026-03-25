import { request } from '@/utils/request'

export interface AdminUser {
  id: number
  username: string
  email?: string
  phone?: string
  status: number  // 0=disabled, 1=enabled
  createdAt: string
  lastLoginAt?: string
  roleIds?: number[]
}

export interface CreateAdminRequest {
  username: string
  password: string
  email?: string
  phone?: string
  roleIds?: number[]
}

export interface UpdateAdminRequest {
  email?: string
  phone?: string
  roleIds?: number[]
}

export interface AdminListParams {
  page: number
  pageSize: number
  username?: string
  email?: string
  status?: number
}

export interface AdminListResponse {
  items: AdminUser[]
  total: number
}

/**
 * Get admins list with pagination and filters
 */
export function getAdmins(params: AdminListParams) {
  return request<AdminListResponse>({
    url: '/admin',
    method: 'get',
    params
  })
}

/**
 * Get single admin by ID
 */
export function getAdmin(id: number) {
  return request<AdminUser>({
    url: `/admin/${id}`,
    method: 'get'
  })
}

/**
 * Create a new admin
 */
export function createAdmin(data: CreateAdminRequest) {
  return request<AdminUser>({
    url: '/admin',
    method: 'post',
    data
  })
}

/**
 * Update an existing admin
 */
export function updateAdmin(id: number, data: UpdateAdminRequest) {
  return request<AdminUser>({
    url: `/admin/${id}`,
    method: 'put',
    data
  })
}

/**
 * Delete an admin
 */
export function deleteAdmin(id: number) {
  return request({
    url: `/admin/${id}`,
    method: 'delete'
  })
}

/**
 * Update admin status (enable/disable)
 */
export function updateAdminStatus(id: number, status: number) {
  return request({
    url: `/admin/${id}/status`,
    method: 'put',
    data: { status }
  })
}

/**
 * Reset admin password
 */
export function resetAdminPassword(id: number, password: string) {
  return request({
    url: `/admin/${id}/reset-password`,
    method: 'post',
    data: { newPassword: password }
  })
}
