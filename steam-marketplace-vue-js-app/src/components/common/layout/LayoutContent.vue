<template>
  <a-layout class="layout-content">
    <breadcrumb />
    <a-layout-content class="content" @scroll="onScroll">
      <router-view />
    </a-layout-content>
    <layout-footer />
  </a-layout>
</template>

<script>
  import EventBus from '@/services/eventBus'
  import LayoutFooter from '@/components/common/layout/LayoutFooter'
  import Breadcrumb from '@/components/common/layout/layoutContent/Breadcrumb.vue'

  export default {
    name: 'LayoutContent',

    components: {
      Breadcrumb,
      LayoutFooter,
    },

    methods: {
      onScroll(event) {
        EventBus.$emit('content-scrolled', event)
      },
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
    overflow: auto;
    min-height: 280px;
    background: #fff;
  }
</style>
