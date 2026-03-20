# CLAUDE.md
# ============================================
# 核心规则入口
# ============================================

1. 启动与唤醒：
   - Claude 启动或重启时，必须加载以下规则：
     - AGENT_RULES.md
     - FIGMA_RULES.md
     - DESIGN_SYSTEM_RULES.md
     - CODING_RULES.md
   - 输出 [状态对齐] 报告。

2. 执行顺序：
   - Brainstorming
   - 用户确认方案
   - Figma MCP 调用
   - 代码生成与资源下载
   - Artifact 同步
   - 验证和提交

3. Artifact 同步：
   - 所有决策、任务拆解、分析和进度必须写入：
     - task_plan.md
     - findings.md
     - progress.md

- # 前端生成策略

  - 所有 Figma 页面生成 Vue 3 组件（<script setup> + Composition API）
  - 必须实现 Figma 设计稿 1:1 视觉还原（像素级还原 UI 结构、间距、字体、颜色）

  ## 🎯 Figma 还原规则（强约束）

  - 严格按照 Figma Auto Layout 还原为 Flex / Grid（禁止随意改布局）
  - 所有间距（padding / margin / gap）必须与 Figma 一致（优先使用 Token）
  - 字体（font-size / weight / line-height）必须完全匹配设计稿
  - 颜色必须使用设计 Token（禁止手写 hex 值）
  - 圆角 / 阴影 / 边框必须还原（包括细节参数）
  - 组件层级结构必须与 Figma 分组一致（避免扁平化乱写）
  - 禁止“视觉差不多”的实现（必须结构一致）

  ## 🎨 样式规范

  - 使用 Tailwind CSS / UnoCSS 映射 Figma Token
  - 禁止硬编码样式（如 style="margin: 13px"）
  - 优先使用原子类表达样式

  ## 🧩 组件规范

  - 自动识别并抽离公共组件（Button / Card / Input 等）
  - 所有组件必须支持 Props（text / state / variant）
  - 禁止页面中写死 UI（必须可复用）

  ## ⚙️ 工程规范

  - 优先复用已有组件（src/components, src/design-system）
  - 状态管理统一使用 Pinia
  - 页面路由使用 Vue Router
  - 复杂逻辑必须拆分到 composables

  ## 🧠 交互还原

  - 必须还原 Figma 中的交互状态（hover / active / disabled）
  - 动效（如有）需使用 CSS 或 Vue 动画实现