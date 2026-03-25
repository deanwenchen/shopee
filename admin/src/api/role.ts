import { request } from '@/utils/request'

export interface Role {
  id: number
  name: string
  description?: string
  createdAt: string
  permissionIds?: number[]
}

export interface CreateRoleRequest {
  name: string
  description?: string
  permissionIds?: number[]
}

export interface UpdateRoleRequest {
  name?: string
  description?: string
  permissionIds?: number[]
}

export interface RoleListParams {
  page: number
  pageSize: number
}

export interface RoleListResponse {
  items: Role[]
  total: number
}

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
  children: Permission[]
}

export interface PermissionTreeResponse {
  trees: Permission[]
}

/**
 * Get roles list with pagination
 */
export function getRoles(params: RoleListParams) {
  return request<RoleListResponse>({
    url: '/role',
    method: 'get',
    params
  })
}

/**
 * Get single role by ID
 */
export function getRole(id: number) {
  return request<Role>({
    url: `/role/${id}`,
    method: 'get'
  })
}

/**
 * Create a new role
 */
export function createRole(data: CreateRoleRequest) {
  return request<Role>({
    url: '/role',
    method: 'post',
    data
  })
}

/**
 * Update an existing role
 */
export function updateRole(id: number, data: UpdateRoleRequest) {
  return request<Role>({
    url: `/role/${id}`,
    method: 'put',
    data
  })
}

/**
 * Delete a role
 */
export function deleteRole(id: number) {
  return request({
    url: `/role/${id}`,
    method: 'delete'
  })
}

/**
 * Assign permissions to a role
 */
export function assignPermissions(id: number, permissionIds: number[]) {
  return request({
    url: `/role/${id}/permissions`,
    method: 'put',
    data: permissionIds
  })
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
