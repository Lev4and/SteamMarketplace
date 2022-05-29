<template>
  <div id="pricesDynamics">

  </div>
</template>

<script>
  import API from '@/api'

  export default {
    name: 'SkinPricesDynamics',

    data: () => ({
      pricesDynamics: [],
    }),

    computed: {
      fullName() {
        return this.$route.params.fullName
      },
    },

    watch: {
      fullName: {
        async handler() {
          await this.loadPricesDynamics()
        },
        immediate: true,
      },
    },

    methods: {
      async loadPricesDynamics() {
        try {
          const response = await API.sales.getPricesDynamicsItem(this.fullName)
          if (response.status.isSuccessful()) {
            this.pricesDynamics = response.result
            console.log(this.pricesDynamics)
          }
        } catch (exception) {
          this.$error(exception.message, 'Ошибка при загрузке')
        }
      },
    },
  }
</script>

<style scoped>

</style>
