<template>
  <div id="online">
    <a-table
      :columns="columns"
      :row-key="record => record.id"
      :data-source="users"
    >
      <template slot="actions">
        <span>Профиль</span>
      </template>
    </a-table>
  </div>
</template>

<script>
  import API from '@/api'
  import { isEmpty as _isEmpty, some as _some, remove as _remove } from 'lodash'

  export default {
    name: 'Online',

    data: () => ({
      users: [],
      columns: [
        {
          title: 'Логин',
          dataIndex: 'userName',
        },
        {
          title: 'E-Mail',
          dataIndex: 'email',
        },
        {
          title: 'Действия',
          dataIndex: 'actions',
          scopedSlots: { customRender: 'actions' },
        },
      ],
    }),

    watch: {
      users: {
        async handler(newValue) {
          if (_isEmpty(newValue)) await this.loadUsers()
        },
        immediate: true,
      },
    },

    async mounted() {
      document.addEventListener('online-users', this.onReceivedOnlineUsers)
      document.addEventListener('user-connected', this.onUserConnected)
      document.addEventListener('user-disconnected', this.onUserDisconnected)
    },

    beforeDestroy() {
      document.removeEventListener('online-users', this.onReceivedOnlineUsers)
      document.removeEventListener('user-connected', this.onUserConnected)
      document.removeEventListener('user-disconnected', this.onUserDisconnected)
    },

    methods: {
      async loadUsers() {
        await API.online.sendRequestOnGetOnlineUsers()
      },
      onReceivedOnlineUsers(event) {
        this.users = event?.detail?.data?.() || []
      },
      onUserConnected(event) {
        if (!_some(this.users, (user) => user.id === event?.detail?.data?.()?.id)) {
          this.users.push(event?.detail?.data?.())
        }
      },
      onUserDisconnected(event) {
        this.users = _remove(this.users, (user) => user.id !== event?.detail?.data?.()?.id)
      },
    },
  }
</script>

<style scoped>

</style>
