<template>
  <a-layout-sider class="sider">
    <a-menu
      mode="inline"
      :default-open-keys="['database']"
      :style="{ height: '100%', borderRight: 0 }"
    >
      <a-sub-menu key="database">
        <span slot="title"><a-icon type="database" /> База данных</span>
      </a-sub-menu>
    </a-menu>
  </a-layout-sider>
</template>

<script>
  export default {
    name: 'LayoutSider',
  }
</script>

<style scoped>
  .sider {
    width: 200px;
    background: #fff;
  }
</style>
