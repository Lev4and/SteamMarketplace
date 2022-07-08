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
      document.addEventListener('onlineusers', this.onReceivedOnlineUsers)
      document.addEventListener('userconnected', this.onUserConnected)
      document.addEventListener('userdisconnected', this.onUserDisconnected)
    },

    beforeDestroy() {
      document.removeEventListener('onlineusers', this.onReceivedOnlineUsers)
      document.removeEventListener('userconnected', this.onUserConnected)
      document.removeEventListener('userdisconnected', this.onUserDisconnected)
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
