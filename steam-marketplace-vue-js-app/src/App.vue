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
  import { mapGetters } from 'vuex'
  import API from '@/api'
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

    computed: {
      ...mapGetters({
        currentUser: 'auth/currentUser',
      }),
    },

    watch: {
      currentUser: {
        async handler(newValue) {
          if (newValue) {
            await this.connect()
            await this.matchUser()
          } else await this.disconnect()
        },
        immediate: true,
      },
    },

    async beforeDestroy() {
      await this.disconnect()
    },

    methods: {
      async connect() {
        await API.online.connect()
      },
      async matchUser() {
        await API.online.matchUser(this.currentUser)
      },
      async disconnect() {
        await API.online.disconnect()
      },
    },
  }
</script>
