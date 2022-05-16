<template>
  <div id="app">
    <a-layout>
      <layout-header />
      <a-layout :style="{ height: 'calc(100vh - 64px)' }">
        <layout-sider />
        <layout-content />
      </a-layout>
    </a-layout>
  </div>
</template>

<script>
  import LayoutContent from '@/components/common/layout/LayoutContent'
  import LayoutHeader from '@/components/common/layout/LayoutHeader'
  import LayoutSider from '@/components/common/layout/LayoutSider'

  export default {
    name: 'App',

    components: {
      LayoutContent,
      LayoutHeader,
      LayoutSider,
    },
  }
</script>
