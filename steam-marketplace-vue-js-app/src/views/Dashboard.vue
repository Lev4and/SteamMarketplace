<template>
  <div id="dashboard">
    <a-row :gutter="[0,64]">
      <a-col :span="24">
        <a-page-header title="Курсы валют" />
        <exchange-rates-container />
      </a-col>
    </a-row>
  </div>
</template>

<script>
  import ExchangeRatesContainer from '@/components/dashboard/ExchangeRatesContainer'

  export default {
    name: 'Dashboard',
    
    components: {
      ExchangeRatesContainer,
    },
  }
</script>

<style scoped>

</style>
