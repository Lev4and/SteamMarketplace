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
        <a-menu-item>
          <router-link :to="{ name: 'Logout' }">
            <a-icon type="close-circle" class="mr-[8px]" /> 
            <span v-text="'Выход'" />
          </router-link>
        </a-menu-item>
      </a-menu>
    </a-dropdown>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'
  import { getCurrencyFormat } from '@/services/utils/formatUtils'

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
        return getCurrencyFormat(this.walletBalance, this.cultureInfoName, this.currency)
      },
    },
  }
</script>
