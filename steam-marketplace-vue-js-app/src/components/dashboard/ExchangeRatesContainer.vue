<template>
  <div id="exchangeRates">
    <a-row :gutter="[8,8]">
      <template v-for="exchangeRate in exchangeRates">
        <a-col :key="exchangeRate.currency" :sm="12" :md="8" :lg="6" :xl="4" :xxl="4">
          <exchange-rate-item :exchangeRate="exchangeRate" />
        </a-col>
      </template>
    </a-row>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'
  import { isEmpty as _isEmpty, map as _map, forEach as _forEach, last as _last } from 'lodash'
  import API from '@/api'
  import ExchangeRateItem from '@/components/dashboard/ExchangeRateItem'

  export default {
    name: 'ExchangeRates',
    
    components: {
      ExchangeRateItem,
    },

    data: () => ({
      exchangeRates: [],
    }),

    computed: {
      ...mapGetters({
        currencies: 'common/currencies',
      }),
      last() {
        return _last
      },
    },

    watch: {
      currencies: {
        async handler(newValue) {
          if(!_isEmpty(newValue)) await this.loadExchangeRates()
        },
        immediate: true,
      },
    },

    methods: {
      async loadExchangeRates() {
        await Promise.all(_map(this.currencies, (currency) => {
          return API.exchangeRates.getExchangeRates(currency.id)
        })).then((responses) => {
          _forEach(responses, (response, index) => {
            if (response.status.isSuccessful()) {
              this.exchangeRates.push({
                currency: this.currencies[index].literal,
                rates: response.result || [],
              })
            }
          })
        }).catch((exception) => {
          this.$error(exception.message, 'Ошибка при загрузке курсов валют')
        })
      },
    },
  }
</script>

<style scoped>

</style>
