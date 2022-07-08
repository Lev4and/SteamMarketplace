<template>
  <div id="currencyExchangeRate">
    <exchange-rate-dynamics />
  </div>
</template>

<script>
  import ExchangeRateDynamics from '@/components/currencyExchangeRate/ExchangeRateDynamics'

  export default {
    name: 'CurrencyExchangeRate',

    components: {
      ExchangeRateDynamics,
    },
  }
</script>
