<template>
  <div class="forbidden-page">
    <h1>403</h1>
    <p>无权限访问</p>
  </div>
</template>

<script setup lang="ts">
</script>

<style scoped>
.forbidden-page {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100%;
  color: #606266;
}

.forbidden-page h1 {
  font-size: 72px;
  margin: 0;
}

.forbidden-page p {
  font-size: 18px;
}
</style>
