<template>
  <login-form />
</template>

<script>
  import store from '@/store'
  import LoginForm from '@/components/login/LoginForm'

  export default {
    name: 'Login',

    components: {
      LoginForm,
    },

    async beforeRouteEnter(to, from, next) {
      await store.dispatch('common/init')
      await store.dispatch('exchangeRates/init')
      next()
    },
  }
</script>
