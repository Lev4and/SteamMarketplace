<template>
  <a-layout-header class="header">
    <router-link :to="{ name: 'Home' }">
      <div class="logo">
        <img :src="require('@/assets/logo.svg')" />
      </div>
    </router-link>
    <a-row class="row" type="flex">
      <a-col flex="8">
        
      </a-col>
      <a-col v-if="isAuthorized" flex="2">
        <account-drop-down />
      </a-col>
    </a-row>
  </a-layout-header>
</template>

<script>
  import { mapGetters } from 'vuex'
  import AccountDropDown from '@/components/common/layout/layoutHeader/AccountDropdown'

  export default {
    name: 'LayoutHeader',

    components: { 
      AccountDropDown, 
    },

    computed: {
      ...mapGetters({
        isAuthorized: 'auth/isAuthorized',
      }),
    },
  }
</script>
