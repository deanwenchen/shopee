<template>
  <div class="admin-list-page">
    <!-- Search Bar -->
    <el-card class="search-card" shadow="hover">
      <el-form :inline="true" :model="searchForm" class="search-form">
        <el-form-item label="Username">
          <el-input
            v-model="searchForm.username"
            placeholder="Search by username"
            clearable
            @keyup.enter="handleSearch"
          />
        </el-form-item>
        <el-form-item label="Email">
          <el-input
            v-model="searchForm.email"
            placeholder="Search by email"
            clearable
            @keyup.enter="handleSearch"
          />
        </el-form-item>
        <el-form-item label="Status">
          <el-select v-model="searchForm.status" placeholder="All Status" clearable>
            <el-option label="Enabled" :value="1" />
            <el-option label="Disabled" :value="0" />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleSearch">
            <el-icon><Search /></el-icon>
            Search
          </el-button>
          <el-button @click="handleReset">
            <el-icon><Refresh /></el-icon>
            Reset
          </el-button>
        </el-form-item>
      </el-form>
    </el-card>

    <!-- Table Card -->
    <el-card class="table-card" shadow="hover">
      <div class="table-header">
        <h3 class="table-title">Admin Users</h3>
        <el-button type="primary" @click="handleCreate">
          <el-icon><Plus /></el-icon>
          Create Admin
        </el-button>
      </div>

      <el-table
        v-loading="loading"
        :data="tableData"
        style="width: 100%"
        border
        stripe
      >
        <el-table-column prop="id" label="ID" width="80" />
        <el-table-column prop="username" label="Username" min-width="150" />
        <el-table-column prop="email" label="Email" min-width="200" />
        <el-table-column prop="phone" label="Phone" width="150" />
        <el-table-column prop="status" label="Status" width="100">
          <template #default="{ row }">
            <el-tag :type="row.status === 1 ? 'success' : 'danger'">
              {{ row.status === 1 ? 'Enabled' : 'Disabled' }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="createdAt" label="Created At" width="180">
          <template #default="{ row }">
            {{ formatDate(row.createdAt) }}
          </template>
        </el-table-column>
        <el-table-column label="Actions" width="280" fixed="right">
          <template #default="{ row }">
            <el-button link type="primary" @click="handleEdit(row)">
              Edit
            </el-button>
            <el-button link type="primary" @click="handleToggleStatus(row)">
              {{ row.status === 1 ? 'Disable' : 'Enable' }}
            </el-button>
            <el-button link type="warning" @click="handleResetPassword(row)">
              Reset Pwd
            </el-button>
            <el-button link type="danger" @click="handleDelete(row)">
              Delete
            </el-button>
          </template>
        </el-table-column>
      </el-table>

      <!-- Pagination -->
      <div class="pagination-container">
        <el-pagination
          v-model:current-page="pagination.page"
          v-model:page-size="pagination.pageSize"
          :page-sizes="[10, 20, 50]"
          :total="pagination.total"
          layout="total, sizes, prev, pager, next, jumper"
          @size-change="fetchData"
          @current-change="fetchData"
        />
      </div>
    </el-card>

    <!-- Create/Edit Dialog -->
    <el-dialog
      v-model="dialog.visible"
      :title="dialog.isEdit ? 'Edit Admin' : 'Create Admin'"
      width="500px"
      @close="handleDialogClose"
    >
      <el-form
        ref="formRef"
        :model="formData"
        :rules="formRules"
        label-width="100px"
      >
        <el-form-item label="Username" prop="username">
          <el-input
            v-model="formData.username"
            :disabled="dialog.isEdit"
            placeholder="Enter username"
          />
        </el-form-item>
        <el-form-item label="Password" prop="password" v-if="!dialog.isEdit">
          <el-input
            v-model="formData.password"
            type="password"
            show-password
            placeholder="Enter password"
          />
        </el-form-item>
        <el-form-item label="Email" prop="email">
          <el-input
            v-model="formData.email"
            placeholder="Enter email (optional)"
          />
        </el-form-item>
        <el-form-item label="Phone" prop="phone">
          <el-input
            v-model="formData.phone"
            placeholder="Enter phone (optional)"
          />
        </el-form-item>
        <el-form-item label="Roles" prop="roleIds">
          <el-select
            v-model="formData.roleIds"
            multiple
            placeholder="Select roles"
            style="width: 100%"
            :loading="rolesLoading"
          >
            <el-option
              v-for="role in roles"
              :key="role.id"
              :label="role.name"
              :value="role.id"
            />
          </el-select>
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="dialog.visible = false">Cancel</el-button>
        <el-button type="primary" :loading="submitLoading" @click="handleSubmit">
          Confirm
        </el-button>
      </template>
    </el-dialog>

    <!-- Reset Password Dialog -->
    <el-dialog
      v-model="passwordDialog.visible"
      title="Reset Password"
      width="400px"
      @close="handlePasswordDialogClose"
    >
      <el-form
        ref="passwordFormRef"
        :model="passwordData"
        :rules="passwordRules"
        label-width="100px"
      >
        <el-form-item label="Username">
          <el-input :value="passwordData.username" disabled />
        </el-form-item>
        <el-form-item label="New Password" prop="newPassword">
          <el-input
            v-model="passwordData.newPassword"
            type="password"
            show-password
            placeholder="Enter new password"
          />
        </el-form-item>
        <el-form-item label="Confirm" prop="confirmPassword">
          <el-input
            v-model="passwordData.confirmPassword"
            type="password"
            show-password
            placeholder="Confirm new password"
          />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="passwordDialog.visible = false">Cancel</el-button>
        <el-button type="primary" :loading="passwordLoading" @click="handlePasswordSubmit">
          Confirm
        </el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { Search, Refresh, Plus } from '@element-plus/icons-vue'
import {
  getAdmins,
  createAdmin,
  updateAdmin,
  deleteAdmin,
  updateAdminStatus,
  resetAdminPassword,
  type AdminUser,
  type CreateAdminRequest,
  type UpdateAdminRequest
} from '@/api/admin'
import { request } from '@/utils/request'

// Search form
const searchForm = reactive({
  username: '',
  email: '',
  status: undefined as number | undefined
})

// Table data
const loading = ref(false)
const tableData = ref<AdminUser[]>([])

// Pagination
const pagination = reactive({
  page: 1,
  pageSize: 10,
  total: 0
})

// Dialog
const dialog = reactive({
  visible: false,
  isEdit: false
})

const formRef = ref()
const submitLoading = ref(false)
const formData = reactive<CreateAdminRequest & { id?: number }>({
  username: '',
  password: '',
  email: '',
  phone: '',
  roleIds: []
})

const formRules = {
  username: [
    { required: true, message: 'Username is required', trigger: 'blur' },
    { min: 2, max: 50, message: 'Username must be 2-50 characters', trigger: 'blur' }
  ],
  password: [
    { required: true, message: 'Password is required', trigger: 'blur' },
    { min: 6, message: 'Password must be at least 6 characters', trigger: 'blur' }
  ],
  email: [
    { type: 'email', message: 'Invalid email format', trigger: 'blur' }
  ],
  phone: [
    { pattern: /^1[3-9]\d{9}$/, message: 'Invalid phone number', trigger: 'blur' }
  ]
}

// Roles
const rolesLoading = ref(false)
const roles = ref<{ id: number; name: string }[]>([])

// Password dialog
const passwordDialog = reactive({
  visible: false
})
const passwordFormRef = ref()
const passwordLoading = ref(false)
const passwordData = reactive({
  id: 0,
  username: '',
  newPassword: '',
  confirmPassword: ''
})

const passwordRules = {
  newPassword: [
    { required: true, message: 'New password is required', trigger: 'blur' },
    { min: 6, message: 'Password must be at least 6 characters', trigger: 'blur' }
  ],
  confirmPassword: [
    { required: true, message: 'Please confirm password', trigger: 'blur' },
    {
      validator: (_rule: any, value: string, callback: any) => {
        if (value !== passwordData.newPassword) {
          callback(new Error('Passwords do not match'))
        } else {
          callback()
        }
      },
      trigger: 'blur'
    }
  ]
}

// Fetch data
async function fetchData() {
  loading.value = true
  try {
    const res = await getAdmins({
      page: pagination.page,
      pageSize: pagination.pageSize,
      username: searchForm.username || undefined,
      email: searchForm.email || undefined,
      status: searchForm.status
    })
    tableData.value = res.items
    pagination.total = res.total
  } catch (error) {
    ElMessage.error('Failed to fetch admin list')
  } finally {
    loading.value = false
  }
}

// Fetch roles
async function fetchRoles() {
  rolesLoading.value = true
  try {
    const res = await request<{ items: { id: number; name: string }[] }>({
      url: '/role',
      method: 'get',
      params: { page: 1, pageSize: 100 }
    })
    roles.value = res.items
  } catch (error) {
    ElMessage.error('Failed to fetch roles')
  } finally {
    rolesLoading.value = false
  }
}

// Search
function handleSearch() {
  pagination.page = 1
  fetchData()
}

// Reset
function handleReset() {
  searchForm.username = ''
  searchForm.email = ''
  searchForm.status = undefined
  handleSearch()
}

// Create
function handleCreate() {
  dialog.isEdit = false
  dialog.visible = true
}

// Edit
function handleEdit(row: AdminUser) {
  dialog.isEdit = true
  formData.id = row.id
  formData.username = row.username
  formData.email = row.email || ''
  formData.phone = row.phone || ''
  formData.roleIds = row.roleIds || []
  dialog.visible = true
}

// Dialog close
function handleDialogClose() {
  formRef.value?.resetFields()
  formData.username = ''
  formData.password = ''
  formData.email = ''
  formData.phone = ''
  formData.roleIds = []
  formData.id = undefined
}

// Submit
async function handleSubmit() {
  if (!formRef.value) return

  await formRef.value.validate(async (valid: boolean) => {
    if (!valid) return

    submitLoading.value = true
    try {
      if (dialog.isEdit) {
        const data: UpdateAdminRequest = {
          email: formData.email || undefined,
          phone: formData.phone || undefined,
          roleIds: formData.roleIds
        }
        await updateAdmin(formData.id!, data)
        ElMessage.success('Admin updated successfully')
      } else {
        const data: CreateAdminRequest = {
          username: formData.username,
          password: formData.password,
          email: formData.email || undefined,
          phone: formData.phone || undefined,
          roleIds: formData.roleIds
        }
        await createAdmin(data)
        ElMessage.success('Admin created successfully')
      }
      dialog.visible = false
      fetchData()
    } catch (error: any) {
      ElMessage.error(error.response?.data?.message || 'Operation failed')
    } finally {
      submitLoading.value = false
    }
  })
}

// Toggle status
async function handleToggleStatus(row: AdminUser) {
  const newStatus = row.status === 1 ? 0 : 1
  const action = newStatus === 1 ? 'enable' : 'disable'

  try {
    await ElMessageBox.confirm(
      `Are you sure you want to ${action} this admin?`,
      'Confirm',
      { type: 'warning' }
    )
    await updateAdminStatus(row.id, newStatus)
    ElMessage.success(`Admin ${action}d successfully`)
    fetchData()
  } catch (error: any) {
    if (error !== 'cancel') {
      ElMessage.error('Failed to update status')
    }
  }
}

// Reset password
function handleResetPassword(row: AdminUser) {
  passwordData.id = row.id
  passwordData.username = row.username
  passwordData.newPassword = ''
  passwordData.confirmPassword = ''
  passwordDialog.visible = true
}

function handlePasswordDialogClose() {
  passwordFormRef.value?.resetFields()
}

async function handlePasswordSubmit() {
  if (!passwordFormRef.value) return

  await passwordFormRef.value.validate(async (valid: boolean) => {
    if (!valid) return

    passwordLoading.value = true
    try {
      await resetAdminPassword(passwordData.id, passwordData.newPassword)
      ElMessage.success('Password reset successfully')
      passwordDialog.visible = false
    } catch (error: any) {
      ElMessage.error(error.response?.data?.message || 'Failed to reset password')
    } finally {
      passwordLoading.value = false
    }
  })
}

// Delete
async function handleDelete(row: AdminUser) {
  try {
    await ElMessageBox.confirm(
      `Are you sure you want to delete admin "${row.username}"?`,
      'Confirm Delete',
      { type: 'warning' }
    )
    await deleteAdmin(row.id)
    ElMessage.success('Admin deleted successfully')
    fetchData()
  } catch (error: any) {
    if (error !== 'cancel') {
      ElMessage.error('Failed to delete admin')
    }
  }
}

// Format date
function formatDate(dateString: string) {
  return new Date(dateString).toLocaleString('zh-CN', {
    year: 'numeric',
    month: '2-digit',
    day: '2-digit',
    hour: '2-digit',
    minute: '2-digit'
  })
}

// Lifecycle
onMounted(() => {
  fetchData()
  fetchRoles()
})
</script>

<style scoped>
.admin-list-page {
  padding: 20px;
}

.search-card {
  margin-bottom: 20px;
}

.search-form {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
}

.table-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

.table-title {
  font-size: 18px;
  font-weight: 600;
  color: #303133;
  margin: 0;
}

.pagination-container {
  display: flex;
  justify-content: flex-end;
  margin-top: 20px;
}

:deep(.el-dialog__body) {
  padding-top: 10px;
}
</style>
