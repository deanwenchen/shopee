<template>
  <div class="login-container">
    <el-card class="login-card">
      <template #header>
        <h2>管理后台登录</h2>
      </template>
      <div>Login Page</div>
    </el-card>
  </div>
</template>

<script setup lang="ts">
</script>

<style scoped>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #f5f7fa;
}

.login-card {
  width: 400px;
}

.login-card h2 {
  text-align: center;
  margin: 0;
}
</style>
