<template>
  <div class="permission-tree-page">
    <el-card class="permission-card" shadow="hover">
      <div class="card-header">
        <h3 class="card-title">Permission Management</h3>
      </div>

      <div class="permission-content">
        <!-- Left Panel: Form -->
        <div class="left-panel">
          <h4 class="panel-title">{{ isEdit ? 'Edit Permission' : 'Create Permission' }}</h4>
          <el-form
            ref="formRef"
            :model="formData"
            :rules="formRules"
            label-width="100px"
            size="default"
          >
            <el-form-item label="Name" prop="name">
              <el-input
                v-model="formData.name"
                placeholder="Enter permission name"
              />
            </el-form-item>

            <el-form-item label="Code" prop="code">
              <el-input
                v-model="formData.code"
                placeholder="Enter permission code (e.g., admin:list)"
              />
            </el-form-item>

            <el-form-item label="Type" prop="type">
              <el-select v-model="formData.type" placeholder="Select type" style="width: 100%">
                <el-option
                  v-for="item in typeOptions"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                >
                  <div class="type-option">
                    <el-icon :size="18">
                      <component :is="item.icon" />
                    </el-icon>
                    <span>{{ item.label }}</span>
                  </div>
                </el-option>
              </el-select>
            </el-form-item>

            <el-form-item label="Parent" prop="parentId">
              <el-cascader
                v-model="formData.parentId"
                :options="cascaderOptions"
                :props="{ label: 'name', value: 'id', children: 'children', checkStrictly: true, emitPath: false }"
                placeholder="Select parent permission (optional)"
                clearable
                style="width: 100%"
              />
            </el-form-item>

            <el-form-item
              v-if="formData.type === 2"
              label="Path"
              prop="path"
            >
              <el-input
                v-model="formData.path"
                placeholder="Enter route path (e.g., /system/users)"
              />
            </el-form-item>

            <el-form-item
              v-if="formData.type === 2"
              label="Icon"
              prop="icon"
            >
              <el-input
                v-model="formData.icon"
                placeholder="Enter icon name (e.g., User, Setting)"
              />
            </el-form-item>

            <el-form-item
              v-if="formData.type === 4"
              label="API Path"
              prop="apiPath"
            >
              <el-input
                v-model="formData.apiPath"
                placeholder="Enter API path (e.g., GET /api/users)"
              />
            </el-form-item>

            <el-form-item label="Sort Order" prop="sort">
              <el-input-number
                v-model="formData.sort"
                :min="0"
                :max="9999"
                controls-position="right"
                style="width: 100%"
              />
            </el-form-item>

            <el-form-item>
              <el-button
                type="primary"
                :loading="submitLoading"
                @click="handleSubmit"
                style="width: 100%"
              >
                {{ isEdit ? 'Update' : 'Create' }}
              </el-button>
              <el-button @click="handleReset" style="width: 100%; margin-top: 10px">
                Reset
              </el-button>
            </el-form-item>
          </el-form>
        </div>

        <!-- Right Panel: Tree -->
        <div class="right-panel">
          <div class="tree-toolbar">
            <div class="tree-actions">
              <el-button size="small" @click="handleExpandAll">
                <el-icon><FolderOpened /></el-icon>
                Expand All
              </el-button>
              <el-button size="small" @click="handleCollapseAll">
                <el-icon><Folder /></el-icon>
                Collapse All
              </el-button>
            </div>
            <el-button
              v-if="selectedNode"
              type="primary"
              size="small"
              @click="handleAddChild"
            >
              <el-icon><Plus /></el-icon>
              Add Child
            </el-button>
          </div>

          <div class="tree-container">
            <el-tree
              ref="treeRef"
              :data="treeData"
              :props="{ children: 'children', label: 'name' }"
              node-key="id"
              default-expand-all
              :expand-on-click-node="false"
              @node-click="handleNodeClick"
            >
              <template #default="{ node, data }">
                <div class="tree-node">
                  <el-icon :size="18" :class="['node-icon', getTypeClass(data.type)]">
                    <component :is="getTypeIcon(data.type)" />
                  </el-icon>
                  <span class="node-name">{{ node.label }}</span>
                  <span class="node-code">{{ data.code }}</span>
                  <el-tag :type="getTypeTagType(data.type)" size="small">
                    {{ getTypeLabel(data.type) }}
                  </el-tag>
                  <div class="node-actions">
                    <el-button link type="primary" size="small" @click.stop="handleEditNode(data)">
                      Edit
                    </el-button>
                    <el-button link type="danger" size="small" @click.stop="handleDeleteNode(data)">
                      Delete
                    </el-button>
                  </div>
                </div>
              </template>
            </el-tree>
          </div>
        </div>
      </div>
    </el-card>

    <!-- Delete Confirmation Dialog -->
    <el-dialog
      v-model="deleteDialog.visible"
      title="Confirm Delete"
      width="400px"
    >
      <p>{{ deleteDialog.message }}</p>
      <template #footer>
        <el-button @click="deleteDialog.visible = false">Cancel</el-button>
        <el-button type="danger" :loading="deleteLoading" @click="confirmDelete">
          Delete
        </el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted, computed } from 'vue'
import { ElMessage } from 'element-plus'
import {
  Folder,
  FolderOpened,
  Plus,
  Document,
  Connection,
  Grid
} from '@element-plus/icons-vue'
import type { FormInstance, FormRules } from 'element-plus'
import type { ElTree } from 'element-plus'
import {
  getPermissionTree,
  createPermission,
  updatePermission,
  deletePermission,
  type Permission,
  type CreatePermissionRequest,
  type UpdatePermissionRequest
} from '@/api/permission'

// Type options
const typeOptions = [
  { label: 'Directory', value: 1, icon: 'Folder' },
  { label: 'Menu', value: 2, icon: 'Document' },
  { label: 'Button', value: 3, icon: 'Grid' },
  { label: 'API', value: 4, icon: 'Connection' }
]

// Form ref
const formRef = ref<FormInstance>()
const treeRef = ref<InstanceType<typeof ElTree>>()
const submitLoading = ref(false)
const deleteLoading = ref(false)

// Form data
const formData = reactive<CreatePermissionRequest & { id?: number }>({
  name: '',
  code: '',
  type: 1,
  parentId: undefined,
  path: '',
  icon: '',
  apiPath: '',
  sort: 0
})

// Form rules
const formRules: FormRules = {
  name: [
    { required: true, message: 'Permission name is required', trigger: 'blur' },
    { min: 2, max: 100, message: 'Name must be 2-100 characters', trigger: 'blur' }
  ],
  code: [
    { required: true, message: 'Permission code is required', trigger: 'blur' },
    { min: 2, max: 100, message: 'Code must be 2-100 characters', trigger: 'blur' },
    {
      pattern: /^[a-zA-Z0-9:_-]+$/,
      message: 'Code can only contain letters, numbers, colon, underscore and hyphen',
      trigger: 'blur'
    }
  ],
  type: [
    { required: true, message: 'Permission type is required', trigger: 'change' }
  ],
  path: [
    { max: 255, message: 'Path must be less than 255 characters', trigger: 'blur' }
  ],
  icon: [
    { max: 100, message: 'Icon must be less than 100 characters', trigger: 'blur' }
  ],
  apiPath: [
    { max: 255, message: 'API Path must be less than 255 characters', trigger: 'blur' }
  ]
}

// Tree data
const treeData = ref<Permission[]>([])
const selectedNode = ref<Permission | null>(null)

// Delete dialog
const deleteDialog = reactive({
  visible: false,
  permissionId: 0,
  message: ''
})

// isEdit flag
const isEdit = computed(() => formData.id !== undefined)

// Cascader options (flat tree for parent selection)
const cascaderOptions = computed(() => {
  const flattenTree = (nodes: Permission[], includeDisabled = false): any[] => {
    return nodes
      .filter(node => includeDisabled || node.id !== formData.id)
      .map(node => ({
        id: node.id,
        name: `${node.name} (${node.code})`,
        children: node.children ? flattenTree(node.children, includeDisabled) : []
      }))
  }
  return flattenTree(treeData.value, true)
})

// Get type icon
function getTypeIcon(type: number) {
  switch (type) {
    case 1:
      return Folder
    case 2:
      return Document
    case 3:
      return Grid
    case 4:
      return Connection
    default:
      return Folder
  }
}

// Get type class
function getTypeClass(type: number) {
  switch (type) {
    case 1:
      return 'icon-directory'
    case 2:
      return 'icon-menu'
    case 3:
      return 'icon-button'
    case 4:
      return 'icon-api'
    default:
      return ''
  }
}

// Get type label
function getTypeLabel(type: number) {
  return typeOptions.find(t => t.value === type)?.label || 'Unknown'
}

// Get type tag type
function getTypeTagType(type: number) {
  switch (type) {
    case 1:
      return 'warning'
    case 2:
      return 'success'
    case 3:
      return 'primary'
    case 4:
      return 'danger'
    default:
      return 'info'
  }
}

// Fetch tree data
async function fetchTreeData() {
  try {
    const res = await getPermissionTree()
    treeData.value = res.trees
  } catch (error: any) {
    ElMessage.error(error.response?.data?.message || 'Failed to fetch permission tree')
  }
}

// Handle node click
function handleNodeClick(data: Permission) {
  selectedNode.value = data
}

// Handle expand all
function handleExpandAll() {
  if (treeRef.value) {
    const nodes = treeRef.value.store.nodesMap
    for (const node in nodes) {
      nodes[node].expanded = true
    }
  }
}

// Handle collapse all
function handleCollapseAll() {
  if (treeRef.value) {
    const nodes = treeRef.value.store.nodesMap
    for (const node in nodes) {
      nodes[node].expanded = false
    }
  }
}

// Handle add child
function handleAddChild() {
  if (!selectedNode.value) {
    ElMessage.warning('Please select a parent node first')
    return
  }
  handleReset()
  formData.parentId = selectedNode.value.id
  formData.type = selectedNode.value.type === 1 ? 1 : selectedNode.value.type
  // Focus on name input
  setTimeout(() => {
    const nameInput = document.querySelector('input[placeholder="Enter permission name"]') as HTMLInputElement
    nameInput?.focus()
  }, 100)
}

// Handle edit node
function handleEditNode(data: Permission) {
  formData.id = data.id
  formData.name = data.name
  formData.code = data.code
  formData.type = data.type
  formData.parentId = data.parentId
  formData.path = data.path || ''
  formData.icon = data.icon || ''
  formData.apiPath = data.apiPath || ''
  formData.sort = data.sort
  selectedNode.value = data
  // Scroll to top of form
  document.querySelector('.left-panel')?.scrollTo({ top: 0, behavior: 'smooth' })
}

// Handle delete node
function handleDeleteNode(data: Permission) {
  deleteDialog.permissionId = data.id
  deleteDialog.message = `Are you sure you want to delete permission "${data.name}"?`
  if (data.children && data.children.length > 0) {
    deleteDialog.message += `\n\nWarning: This permission has ${data.children.length} child item(s). Deleting will also remove all children.`
  }
  deleteDialog.visible = true
}

// Confirm delete
async function confirmDelete() {
  deleteLoading.value = true
  try {
    await deletePermission(deleteDialog.permissionId)
    ElMessage.success('Permission deleted successfully')
    deleteDialog.visible = false
    fetchTreeData()
    handleReset()
    selectedNode.value = null
  } catch (error: any) {
    ElMessage.error(error.response?.data?.message || 'Failed to delete permission')
  } finally {
    deleteLoading.value = false
  }
}

// Handle submit
async function handleSubmit() {
  if (!formRef.value) return

  await formRef.value.validate(async (valid: boolean) => {
    if (!valid) return

    submitLoading.value = true
    try {
      const data: CreatePermissionRequest = {
        name: formData.name,
        code: formData.code,
        type: formData.type,
        parentId: formData.parentId || undefined,
        path: formData.type === 2 ? formData.path : undefined,
        icon: formData.type === 2 ? formData.icon : undefined,
        apiPath: formData.type === 4 ? formData.apiPath : undefined,
        sort: formData.sort
      }

      if (formData.id) {
        const updateData: UpdatePermissionRequest = { ...data }
        await updatePermission(formData.id, updateData)
        ElMessage.success('Permission updated successfully')
      } else {
        await createPermission(data)
        ElMessage.success('Permission created successfully')
      }

      handleReset()
      fetchTreeData()
    } catch (error: any) {
      ElMessage.error(error.response?.data?.message || 'Operation failed')
    } finally {
      submitLoading.value = false
    }
  })
}

// Handle reset
function handleReset() {
  formRef.value?.resetFields()
  formData.id = undefined
  formData.name = ''
  formData.code = ''
  formData.type = 1
  formData.parentId = undefined
  formData.path = ''
  formData.icon = ''
  formData.apiPath = ''
  formData.sort = 0
  selectedNode.value = null
}

// Lifecycle
onMounted(() => {
  fetchTreeData()
})
</script>

<style scoped>
.permission-tree-page {
  padding: 20px;
}

.permission-card {
  min-height: calc(100vh - 104px);
}

.card-header {
  padding-bottom: 20px;
  border-bottom: 1px solid #e4e7ed;
  margin-bottom: 20px;
}

.card-title {
  font-size: 20px;
  font-weight: 600;
  color: #303133;
  margin: 0;
}

.permission-content {
  display: flex;
  gap: 20px;
}

.left-panel {
  width: 400px;
  flex-shrink: 0;
  padding-right: 20px;
  border-right: 1px solid #e4e7ed;
  max-height: calc(100vh - 180px);
  overflow-y: auto;
}

.right-panel {
  flex: 1;
  min-width: 0;
}

.panel-title {
  font-size: 16px;
  font-weight: 600;
  color: #303133;
  margin: 0 0 20px 0;
}

.tree-toolbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 15px;
  padding: 10px;
  background-color: #f5f7fa;
  border-radius: 4px;
}

.tree-actions {
  display: flex;
  gap: 10px;
}

.tree-container {
  border: 1px solid #e4e7ed;
  border-radius: 4px;
  padding: 10px;
  max-height: calc(100vh - 260px);
  overflow-y: auto;
}

.tree-node {
  display: flex;
  align-items: center;
  gap: 8px;
  flex: 1;
}

.node-icon {
  flex-shrink: 0;
}

.icon-directory {
  color: #e6a23c;
}

.icon-menu {
  color: #67c23a;
}

.icon-button {
  color: #409eff;
}

.icon-api {
  color: #f56c6c;
}

.node-name {
  font-weight: 500;
  color: #303133;
}

.node-code {
  font-size: 12px;
  color: #909399;
  font-family: monospace;
}

.node-actions {
  display: flex;
  gap: 5px;
  margin-left: auto;
  opacity: 0;
  transition: opacity 0.2s;
}

:deep(.el-tree-node__content:hover) .node-actions {
  opacity: 1;
}

:deep(.el-tree-node__content) {
  height: auto;
  padding: 8px 0;
  border-radius: 4px;
}

:deep(.el-tree-node__content:hover) {
  background-color: #f5f7fa;
}

:deep(.el-tree-node.is-current > .el-tree-node__content) {
  background-color: #ecf5ff;
}

.type-option {
  display: flex;
  align-items: center;
  gap: 8px;
}

:deep(.el-form-item) {
  margin-bottom: 18px;
}

:deep(.el-input-number) {
  width: 100%;
}

/* Scrollbar styling */
.left-panel::-webkit-scrollbar,
.tree-container::-webkit-scrollbar {
  width: 6px;
}

.left-panel::-webkit-scrollbar-thumb,
.tree-container::-webkit-scrollbar-thumb {
  background-color: #c1c1c1;
  border-radius: 3px;
}

.left-panel::-webkit-scrollbar-thumb:hover,
.tree-container::-webkit-scrollbar-thumb:hover {
  background-color: #a8a8a8;
}
</style>
