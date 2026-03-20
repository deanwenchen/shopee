# 🧠 Claude Operating System（COS）

---

# 🚀 1. 启动与上下文恢复（Boot & Context Recovery）

Claude 在以下情况 **必须执行初始化流程**：

- 对话开始
- 上下文丢失
- 会话重启

## 📥 强制加载文件

claude.md
docs/pedagogy.md
task_plan.md
findings.md
progress.md

# 📌 文件更新策略（强制）

Claude 必须遵循以下规则：

## task_plan.md（计划）
- 仅在需求新增 / 修改时更新
- 禁止在执行阶段频繁修改

## findings.md（决策）
- 仅记录关键技术决策
- 禁止记录执行日志

## progress.md（进度）
- 每完成一个执行步骤必须更新
- 必须记录：
  - 已完成内容
  - 当前状态
  - 下一步行动

❗禁止行为：
- ❌ 三个文件同时无差别重写
- ❌ 不更新 progress.md
- ❌ 把 progress 写进 findings
## 📥 强制加载规则

AGENT_RULES.md
FIGMA_RULES.md
DESIGN_SYSTEM_RULES.md
CODING_RULES.md

---

# 🧠 2. 系统能力定义（System Core）

你是一个具备 **规划 + 推理 + 执行 + 记录能力** 的工程代理系统：

## 📁 Planning Layer（规划层）

使用 planning-with-files：

- task_plan.md → 任务拆解
- findings.md → 分析与决策
- progress.md → 执行记录

👉 形成闭环：

Plan → Findings → Code → Progress

---

## ⚙️ Execution Layer（执行层）

使用 superpowers：

- /brainstorming → 方案设计 / 架构分析
- /execute-plan → 按计划执行

---

## 📊 输出要求（强制）

每次响应必须包含：

[状态对齐报告]

- 当前任务：
- 已完成进度：
- 下一步行动：

---

# 🔄 3. 标准执行流程（强制顺序）

所有任务必须严格按以下顺序执行：

1. Brainstorming（方案设计）
2. 用户确认方案
3. 调用 Figma MCP（如涉及 UI）
4. 代码生成 + 资源下载
5. Artifact 落地（文件写入）
6. 验证（运行 / UI 校验）
7. 更新 progress.md

❗禁止跳步骤执行

---

# 🧠 4. 思考与落盘机制（强制）

以下内容 **必须写入文件**：

- 决策过程
- 技术分析
- 任务拆解
- 执行过程

## 📄 写入目标文件

task_plan.md
findings.md
progress.md
prd.md

---

## 🧾 写入格式（强制）

=== WRITE_FILE: <filename> ===
<完整文件内容>
=== END ===

---

# 🎨 5. 前端生成策略（Figma → Vue3 强约束）

## ⚙️ 技术栈（固定）

- Vue 3 + <script setup> + Composition API
- Tailwind CSS / UnoCSS
- Pinia
- Vue Router

---

## 🎯 5.1 Figma 还原规则（最高优先级）

### 📐 布局

- 严格遵循 Auto Layout
- 转换为 Flex / Grid
- ❌ 禁止修改结构

### 📏 间距

- padding / margin / gap 必须一致
- 必须使用 Design Token

### 🔤 字体

- font-size / weight / line-height 完全一致

### 🎨 颜色

- 必须使用 Design Token
- ❌ 禁止手写 hex

### 🟦 视觉细节

- 圆角 / 阴影 / 边框完全一致

### 🧱 结构

- 组件层级必须与 Figma 分组一致
- ❌ 禁止扁平化

---

## 🎨 5.2 样式规范

- 使用 Tailwind / UnoCSS
- ❌ 禁止 inline style
- 样式必须可复用

---

## 🧩 5.3 组件规范

必须自动抽离公共组件：

- Button
- Card
- Input

### 要求：

- 支持 Props（text / state / variant）
- 可复用
- ❌ 禁止写死 UI

---

## ⚙️ 5.4 工程规范

- 优先复用：
  - src/components
  - src/design-system
- 状态管理：Pinia（强制）
- 路由：Vue Router（强制）
- 复杂逻辑：必须拆到 composables

---

## 🧠 5.5 交互还原

必须实现：

- hover
- active
- disabled

### 动效：

- CSS 或 Vue 动画

---

# ✅ 6. 核心原则（最高优先级）

- ❌ 不允许“差不多实现”
- ✅ 必须像素级还原
- ✅ 结构必须一致
- ✅ 所有行为可追溯（写入 md）
- ✅ 所有组件必须可复用

---

# 📄 7. PRD 自动沉淀机制（强制）

## 🎯 目标

自动将需求与实现沉淀为 PRD 文档。

---

## 🧠 触发时机（任意满足即触发）

- Brainstorming 完成
- 代码生成完成
- Figma 页面生成完成
- 用户提出新需求 / 修改需求

---

## ✍️ 必须执行

更新：

prd.md

---

## 📄 PRD 内容结构（强制）

每个功能必须包含：

- 功能名称
- 功能目标（Why）
- 功能描述（What）
- 业务规则（Logic）
- UI说明（Figma / 页面结构）
- 技术实现（接口 / 状态 / 数据）

---

## 🔄 版本管理

每次更新必须包含：

- 时间
- 修改内容

并维护：

变更记录（Changelog）

---

## ⚠️ 强制约束

- ❌ 不允许只写代码不更新 PRD
- ❌ 不允许 PRD 与代码不一致
- ❌ 不允许自动提交 Git
- ❌ 写完代码禁止自动 commit
- ✅ 必须人工提交
- ✅ PRD 必须反映真实系统状态

---

# 🧩 8. 多 Agent 协作

## Agent 分工

- Planner → task_plan.md
- Analyst → findings.md
- Executor → Code + 文件落地
- Recorder → progress.md + prd.md

---

## 执行原则

- ❌ 不允许一个 Agent 跳角色
- ✅ 必须按职责写入对应文件