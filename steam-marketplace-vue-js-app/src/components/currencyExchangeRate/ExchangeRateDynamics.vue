<template>
  <div id="exchangeRateDynamics">
    <apexchart type="line" height="500" :options="chartOptions" :series="series"></apexchart>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'
  import moment from 'moment'
  import { find as _find, map as _map } from 'lodash'
  import API from '@/api'
  import { getCurrencyFormat } from '@/services/utils/formatUtils'

  export default {
    name: 'ExchangeRateDynamics',

    data: () => ({
      exchangeRate: [],
    }),

    computed: {
      ...mapGetters({
        currencies: 'common/currencies',
        currentUser: 'auth/currentUser',
      }),
      cultureInfoName() {
        return this.currentUser?.currency?.cultureInfoName || 'us-US'
      },
      currency() {
        return _find(this.currencies, (currency) => currency.literal === this.$route.params.literal)
      },
      dates() {
        moment.locale(this.cultureInfoName)
        return _map(this.exchangeRate, (rate) => {
          return moment.parseZone(rate.dateTime).utc(true).local().format('LLLL')
        })
      },
      chartOptions() {
        return {
          chart: {
            height: 500,
            type: 'line',
          },
          title: {
            text: `${this.currency.literal}/USD`
          },
          labels: this.dates,
          xaxis: {
            type: 'string',
            labels: {
              show: false,
            },
          },
          yaxis: [
            {
              opposite: true,
              title: {
                text: 'Цена'
              },
              labels: {
                formatter: (value) => { return getCurrencyFormat(value, this.currency.cultureInfoName, this.currency.literal) }
              },
            },
          ],
        }
      },
      rateSeries() {
        return {
          name: 'Цена',
          type: 'line',
          data: _map(this.exchangeRate, (rate) => rate.rate) 
        }
      },
      series() {
        return [this.rateSeries]
      },
    },

    watch: {
      currency: {
        async handler(newValue) {
          if (newValue) await this.loadExchangeRate()
        },
        immediate: true,
      },
    },

    methods: {
      async loadExchangeRate() {
        try {
          const response = await API.exchangeRates.getExchangeRates(this.currency.id)
          if (response.status.isSuccessful) {
            this.exchangeRate = response.result
          }
        } catch (exception) {
          this.$error(exception.message, 'Ошибка при загрузке курсов валюты')
        }
      },
    },
  }
</script>
