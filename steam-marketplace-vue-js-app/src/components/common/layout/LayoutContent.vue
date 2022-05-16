<template>
  <a-layout class="layout-content">
    <a-breadcrumb style="margin: 16px 0">
      <a-breadcrumb-item>Главная</a-breadcrumb-item>
    </a-breadcrumb>
    <a-layout-content class="content" >
      <router-view />
    </a-layout-content>
    <layout-footer />
  </a-layout>
</template>

<script>
  import LayoutFooter from '@/components/common/layout/LayoutFooter'

  export default {
    name: 'LayoutContent',

    components: {
      LayoutFooter,
    },
  }
</script>

<style scoped>
  .layout-content {
    padding: 0 24px;
  }
  .layout-content .content {
    margin: 0;
    padding: 24px;
    min-height: 280px;
    background: #fff;
  }
</style>
