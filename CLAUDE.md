# 🧠 Claude Operating System（COS）

---

# 🚀 1. 启动与上下文恢复（Boot & Context Recovery）

Claude 在以下情况 **必须执行初始化流程**：

- 对话开始
- 上下文丢失
- 会话重启

## 📥 强制加载文件

claude.md
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

---

# 🔌 外部工具接入规范

## OpenSpec 集成规则

OpenSpec 仅作为需求输入工具使用：

- 只允许生成：
  - 用户故事
  - 验收标准
  - 功能描述

- 禁止：
  - 写入 task_plan.md
  - 写入 progress.md
  - 直接生成代码

所有 OpenSpec 输出必须经过：

OpenSpec → Brainstorm → 用户确认 → 才能进入执行阶段

---

# 📁 Task Planning 触发规则

在以下情况必须生成或更新 task_plan.md：

- PRD 新增功能
- PRD 发生结构性变更

执行流程：

PRD 更新完成后 →

👉 必须进行任务拆解（Task Planning）

输出：

- task_plan.md（完整任务列表）

## ⚠️ Task Planning 强制约束

- ❌ 未生成 task_plan.md 时，禁止进入执行阶段
- ❌ 不允许跳过 Task Planning 直接写代码
- ✅ PRD 更新后必须触发 Task Planning

---

# 🧠 2. 系统能力定义

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

---

## 📊 输出要求（强制）

每次响应必须包含：

[状态对齐报告]

- 当前任务：
- 已完成进度：
- 下一步行动：

---

---
# 🗂️ 任务执行总流程

用户需求
   ↓
OpenSpec（可选）
   ↓
Brainstorm
   ↓
PRD.md ✅
   ↓
task_plan.md ✅
   ↓
execute-plan
   ↓
progress.md ✅

说明：
- ✅ 表示该步骤会落盘生成对应 md 文件
- OpenSpec 可选，但必须经过 Brainstorm → 用户确认
- Task Planning 必须在 PRD 更新后触发
- execute-plan 生成的执行结果必须同步更新 progress.md

---


# 🔄 3. 标准执行流程（强制顺序）

所有任务必须严格按以下顺序执行：

1. Brainstorming（方案设计）
2. 用户确认方案

3. 👉 更新 PRD（如有需求变更）

4. 👉 Task Planning（任务拆解，生成/更新 task_plan.md）

5. 调用 Figma MCP（如涉及 UI）
6. 代码生成 + 资源下载
7. Artifact 落地（文件写入）
8. 验证（运行 / UI 校验）
9. 更新 progress.md

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

PRD 更新完成后：

👉 必须触发 Task Planning，生成或更新 task_plan.md

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

## ⚠️ 强制约束

- ❌ 不允许只写代码不更新 PRD
- ❌ 不允许 PRD 与代码不一致
- ❌ 不允许自动提交 Git
- ❌ 写完代码禁止自动 commit
- ✅ 必须人工提交
- ✅ PRD 必须反映真实系统状态

