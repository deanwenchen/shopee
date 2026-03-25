<template>
  <div class="role-list-page">
    <!-- Table Card -->
    <el-card class="table-card" shadow="hover">
      <div class="table-header">
        <h3 class="table-title">Roles</h3>
        <el-button type="primary" @click="handleCreate">
          <el-icon><Plus /></el-icon>
          Create Role
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
        <el-table-column prop="name" label="Role Name" min-width="150" />
        <el-table-column prop="description" label="Description" min-width="200" show-overflow-tooltip />
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
            <el-button link type="warning" @click="handleAssignPermissions(row)">
              Assign Permissions
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
      :title="dialog.isEdit ? 'Edit Role' : 'Create Role'"
      width="500px"
      @close="handleDialogClose"
    >
      <el-form
        ref="formRef"
        :model="formData"
        :rules="formRules"
        label-width="100px"
      >
        <el-form-item label="Role Name" prop="name">
          <el-input
            v-model="formData.name"
            placeholder="Enter role name"
          />
        </el-form-item>
        <el-form-item label="Description" prop="description">
          <el-input
            v-model="formData.description"
            type="textarea"
            :rows="4"
            placeholder="Enter description (optional)"
          />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="dialog.visible = false">Cancel</el-button>
        <el-button type="primary" :loading="submitLoading" @click="handleSubmit">
          Confirm
        </el-button>
      </template>
    </el-dialog>

    <!-- Assign Permissions Dialog -->
    <el-dialog
      v-model="permissionDialog.visible"
      title="Assign Permissions"
      width="600px"
      @close="handlePermissionDialogClose"
    >
      <div class="permission-toolbar">
        <el-checkbox
          v-model="permissionDialog.expandAll"
          @change="handleExpandAllChange"
        >
          Expand All
        </el-checkbox>
        <div class="permission-actions">
          <el-button size="small" @click="handleCheckAll">Check All</el-button>
          <el-button size="small" @click="handleUncheckAll">Uncheck All</el-button>
        </div>
      </div>
      <el-tree
        ref="treeRef"
        :data="permissionTree"
        :props="{ children: 'children', label: 'name', disabled: 'disabled' }"
        show-checkbox
        node-key="id"
        :default-checked-keys="permissionDialog.checkedKeys"
        :expand-on-click-node="false"
      />
      <template #footer>
        <el-button @click="permissionDialog.visible = false">Cancel</el-button>
        <el-button type="primary" :loading="permissionLoading" @click="handlePermissionSubmit">
          Confirm
        </el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { Plus } from '@element-plus/icons-vue'
import type { ElTree } from 'element-plus'
import {
  getRoles,
  createRole,
  updateRole,
  deleteRole,
  assignPermissions,
  getPermissionTree,
  type Role,
  type CreateRoleRequest,
  type UpdateRoleRequest,
  type Permission
} from '@/api/role'

// Table data
const loading = ref(false)
const tableData = ref<Role[]>([])

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
const formData = reactive<CreateRoleRequest & { id?: number }>({
  name: '',
  description: '',
  permissionIds: []
})

const formRules = {
  name: [
    { required: true, message: 'Role name is required', trigger: 'blur' },
    { min: 2, max: 50, message: 'Role name must be 2-50 characters', trigger: 'blur' }
  ]
}

// Permission dialog
const permissionDialog = reactive({
  visible: false,
  roleId: 0,
  checkedKeys: [] as number[],
  expandAll: false
})

const treeRef = ref<InstanceType<typeof ElTree>>()
const permissionLoading = ref(false)
const permissionTree = ref<Permission[]>([])

// Fetch data
async function fetchData() {
  loading.value = true
  try {
    const res = await getRoles({
      page: pagination.page,
      pageSize: pagination.pageSize
    })
    tableData.value = res.items
    pagination.total = res.total
  } catch (error) {
    ElMessage.error('Failed to fetch role list')
  } finally {
    loading.value = false
  }
}

// Fetch permission tree
async function fetchPermissionTree() {
  try {
    const res = await getPermissionTree()
    permissionTree.value = res.trees
  } catch (error) {
    ElMessage.error('Failed to fetch permission tree')
  }
}

// Create
function handleCreate() {
  dialog.isEdit = false
  dialog.visible = true
}

// Edit
function handleEdit(row: Role) {
  dialog.isEdit = true
  formData.id = row.id
  formData.name = row.name
  formData.description = row.description || ''
  formData.permissionIds = row.permissionIds || []
  dialog.visible = true
}

// Dialog close
function handleDialogClose() {
  formRef.value?.resetFields()
  formData.name = ''
  formData.description = ''
  formData.permissionIds = []
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
        const data: UpdateRoleRequest = {
          name: formData.name,
          description: formData.description || undefined
        }
        await updateRole(formData.id!, data)
        ElMessage.success('Role updated successfully')
      } else {
        const data: CreateRoleRequest = {
          name: formData.name,
          description: formData.description || undefined
        }
        await createRole(data)
        ElMessage.success('Role created successfully')
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

// Assign permissions
async function handleAssignPermissions(row: Role) {
  permissionDialog.roleId = row.id
  permissionDialog.checkedKeys = row.permissionIds || []
  permissionDialog.visible = true

  if (permissionTree.value.length === 0) {
    await fetchPermissionTree()
  }
}

function handlePermissionDialogClose() {
  permissionDialog.checkedKeys = []
  permissionDialog.expandAll = false
}

function handleExpandAllChange() {
  if (treeRef.value) {
    const nodes = treeRef.value.store.nodesMap
    for (const node in nodes) {
      if (nodes[node].expanded === permissionDialog.expandAll) {
        nodes[node].expanded = permissionDialog.expandAll
      }
    }
  }
}

function handleCheckAll() {
  if (treeRef.value) {
    const allKeys = getAllNodeIds(permissionTree.value)
    treeRef.value.setCheckedKeys(allKeys)
  }
}

function handleUncheckAll() {
  if (treeRef.value) {
    treeRef.value.setCheckedKeys([])
  }
}

function getAllNodeIds(tree: Permission[]): number[] {
  const ids: number[] = []
  function traverse(nodes: Permission[]) {
    nodes.forEach(node => {
      ids.push(node.id)
      if (node.children && node.children.length > 0) {
        traverse(node.children)
      }
    })
  }
  traverse(tree)
  return ids
}

async function handlePermissionSubmit() {
  if (!treeRef.value) return

  permissionLoading.value = true
  try {
    const checkedKeys = treeRef.value.getCheckedKeys() as number[]
    const halfCheckedKeys = treeRef.value.getHalfCheckedKeys() as number[]
    const allCheckedKeys = [...checkedKeys, ...halfCheckedKeys]

    await assignPermissions(permissionDialog.roleId, allCheckedKeys)
    ElMessage.success('Permissions assigned successfully')
    permissionDialog.visible = false
    fetchData()
  } catch (error: any) {
    ElMessage.error(error.response?.data?.message || 'Failed to assign permissions')
  } finally {
    permissionLoading.value = false
  }
}

// Delete
async function handleDelete(row: Role) {
  try {
    await ElMessageBox.confirm(
      `Are you sure you want to delete role "${row.name}"?`,
      'Confirm Delete',
      { type: 'warning' }
    )
    await deleteRole(row.id)
    ElMessage.success('Role deleted successfully')
    fetchData()
  } catch (error: any) {
    if (error !== 'cancel') {
      ElMessage.error('Failed to delete role')
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
})
</script>

<style scoped>
.role-list-page {
  padding: 20px;
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

.permission-toolbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 15px;
  padding: 10px;
  background-color: #f5f7fa;
  border-radius: 4px;
}

.permission-actions {
  display: flex;
  gap: 10px;
}

:deep(.el-dialog__body) {
  padding-top: 10px;
}

:deep(.el-tree) {
  max-height: 400px;
  overflow-y: auto;
  border: 1px solid #e4e7ed;
  border-radius: 4px;
  padding: 10px;
}

:deep(.el-tree-node__content) {
  height: 32px;
}
</style>
