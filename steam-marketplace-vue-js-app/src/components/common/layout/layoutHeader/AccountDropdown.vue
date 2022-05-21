<template>
  <div id="accountDropdown">
    <a-dropdown :placement="'bottomCenter'">
      <div class="dropdown-header">
        <a-avatar class="user-avatar" size="large" icon="user" />
        <strong class="user-name" v-text="userName" />
      </div>
      <a-menu slot="overlay">
          <a-menu-item>
            <a-icon type="wallet" />
            <strong v-text="walletBalanceFormat" />
          </a-menu-item>
          <a-menu-item @click="logout">
            <a-icon type="close-circle" /> 
            <span v-text="'Выход'" />
          </a-menu-item>
      </a-menu>
    </a-dropdown>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'

  export default {
    name: 'AccountDropdown',

    computed: {
      ...mapGetters({
        currentUser: 'auth/currentUser',
      }),
      userName() {
        return this.currentUser?.userName || ''
      },
      currency() {
        return this.currentUser?.currency?.literal || 'USD'
      },
      cultureInfoName() {
        return this.currentUser?.currency?.cultureInfoName || 'us-US'
      },
      walletBalance() {
        return this.currentUser?.walletBalance || 0
      },
      walletBalanceFormat() {
        return new Intl.NumberFormat(this.cultureInfoName, { style: 'currency', currency: this.currency })
          .format(this.walletBalance)
      },
    },

    methods: {
      async logout() {
        await this.$store.dispatch('auth/logout')
        this.$router.push({ name: 'Login' })
      },
    },
  }
</script>

<style scoped>
  .dropdown-header {
    display: flex;
    align-items: center;
  }
  .dropdown-header .user-avatar {
    float: left;
  }
  .dropdown-header .user-name {
    color: white;
    margin: 0 auto;
    font-size: 18px;
  }
</style>
