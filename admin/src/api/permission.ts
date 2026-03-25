import { request } from '@/utils/request'

export interface Permission {
  id: number
  name: string
  code: string
  type: number // 1=Directory, 2=Menu, 3=Button, 4=API
  parentId?: number
  path?: string
  icon?: string
  apiPath?: string
  sort: number
  createdAt: string
  children?: Permission[]
}

export interface CreatePermissionRequest {
  name: string
  code: string
  type: number
  parentId?: number
  path?: string
  icon?: string
  apiPath?: string
  sort?: number
}

export interface UpdatePermissionRequest {
  name?: string
  code?: string
  type?: number
  parentId?: number
  path?: string
  icon?: string
  apiPath?: string
  sort?: number
}

export interface PermissionListParams {
  page: number
  pageSize: number
}

export interface PermissionListResponse {
  items: Permission[]
  total: number
}

export interface PermissionTreeResponse {
  trees: Permission[]
}

/**
 * Get permission tree
 */
export function getPermissionTree() {
  return request<PermissionTreeResponse>({
    url: '/permission/tree',
    method: 'get'
  })
}

/**
 * Get permissions list with pagination
 */
export function getPermissions(params: PermissionListParams) {
  return request<PermissionListResponse>({
    url: '/permission',
    method: 'get',
    params
  })
}

/**
 * Get single permission by ID
 */
export function getPermission(id: number) {
  return request<Permission>({
    url: `/permission/${id}`,
    method: 'get'
  })
}

/**
 * Create a new permission
 */
export function createPermission(data: CreatePermissionRequest) {
  return request<Permission>({
    url: '/permission',
    method: 'post',
    data
  })
}

/**
 * Update an existing permission
 */
export function updatePermission(id: number, data: UpdatePermissionRequest) {
  return request<Permission>({
    url: `/permission/${id}`,
    method: 'put',
    data
  })
}

/**
 * Delete a permission
 */
export function deletePermission(id: number) {
  return request({
    url: `/permission/${id}`,
    method: 'delete'
  })
}
