<template>
  <div id="home">

  </div>
</template>

<script>
  import store from '@/store'

  export default {
    name: 'Home',

    async beforeRouteEnter(to, from, next) {
      await store.dispatch('exchangeRates/init')
      next()
    },
  }
</script>
