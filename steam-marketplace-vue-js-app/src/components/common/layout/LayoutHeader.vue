<template>
  <a-layout-header class="header">
    <router-link :to="{ name: 'Home' }">
      <div class="logo">
        <img :src="require('@/assets/logo.svg')" />
      </div>
    </router-link>
    <a-row class="row" type="flex">
      <a-col flex="8">
        <div class="title">
          <strong v-text="title" />
        </div>
      </a-col>
      <a-col v-if="isAuthorized" flex="2">
        <account-drop-down />
      </a-col>
    </a-row>
  </a-layout-header>
</template>

<script>
  import { mapGetters } from 'vuex'
  import AccountDropDown from './layoutHeader/AccountDropdown'

  export default {
    name: 'LayoutHeader',

    components: { 
      AccountDropDown, 
    },

    computed: {
      ...mapGetters({
        isAuthorized: 'auth/isAuthorized',
      }),
      title() {
        return this.$route.meta.title
      },
    },
  }
</script>

<style scoped>
  .header {
    display: flex;
    padding: 0 24px;
    align-items: center;
  }
  .header .logo {
    display: flex;
    align-items: center;
    float: left;
  }
  .header .title {
    display: flex;
    align-items: center;
    justify-items: center;
  }
  .header .row {
    width: 100%;
  }
  .title strong {
    margin: 0 auto;
    color: white;
    font-size: 24px;
    text-transform: uppercase;
  }
</style>
