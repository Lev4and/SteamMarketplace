<template>
  <div id="logout">

  </div>
</template>

<script>
  import store from '@/store'

  export default {
    name: 'Logout',

    async beforeRouteEnter(to, from, next) {
      await store.dispatch('auth/logout')
      next({ name: 'Login' })
    },
  }
</script>

<style scoped>

</style>
