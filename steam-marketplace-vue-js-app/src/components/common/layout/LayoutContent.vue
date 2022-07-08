<template>
  <a-layout class="layout-content">
    <breadcrumb />
    <a-layout-content class="content">
      <router-view />
    </a-layout-content>
    <layout-footer />
  </a-layout>
</template>

<script>
  import LayoutFooter from '@/components/common/layout/LayoutFooter'
  import Breadcrumb from '@/components/common/layout/layoutContent/Breadcrumb.vue'

  export default {
    name: 'LayoutContent',

    components: {
      Breadcrumb,
      LayoutFooter,
    },
  }
</script>
