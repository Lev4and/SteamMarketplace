<template>
  <a-layout-header class="header">
    <router-link :to="{ name: 'Home' }">
      <div class="logo">
        <img :src="require('@/assets/logo.svg')" />
      </div>
    </router-link>
  </a-layout-header>
</template>

<script>
  export default {
    name: 'LayoutHeader',
  }
</script>

<style scoped>
  .header {
    display: flex;
    padding: 0 24px;
    align-items: center;
  }
  .header .logo {
    display: flex;
    align-items: center;
    float: left;
  }
</style>
