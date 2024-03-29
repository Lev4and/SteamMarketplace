<template>
  <div id="pricesDynamics">
    <apexchart type="line" height="500" :options="chartOptions" :series="series" />
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'
  import { map as _map, find as _find, orderBy as _orderBy } from 'lodash'
  import moment from 'moment'
  import API from '@/api'
  import { getCurrencyFormat } from '@/services/utils/formatUtils'

  export default {
    name: 'SkinPricesDynamics',

    data: () => ({
      pricesDynamics: [],
    }),

    computed: {
      ...mapGetters({
        currentUser: 'auth/currentUser',
      }),
      currency() {
        return this.currentUser?.currency?.literal || 'USD'
      },
      cultureInfoName() {
        return this.currentUser?.currency?.cultureInfoName || 'us-US'
      },
      fullName() {
        return this.$route.params.fullName
      },
      dates() {
        moment.locale(this.cultureInfoName)
        return _map(this.pricesDynamics, (price) => {
          return moment.parseZone(price.date).utc(true).local().format('LLLL')
        })
      },
      chartOptions() {
        return {
          chart: {
            height: 500,
            type: 'line',
          },
          stroke: {
            width: [0, 4]
          },
          title: {
            text: 'Динамика цен и продаж'
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
              title: {
                text: 'Продано',
              },
              labels: {
                formatter: (value) => { return value.toFixed(0) }
              },
            }, 
            {
              opposite: true,
              title: {
                text: 'Цена'
              },
              labels: {
                formatter: (value) => { return getCurrencyFormat(value, this.cultureInfoName, this.currency) }
              },
            },
          ],
        }
      },
      countSoldSeries() {
        return {
          name: 'Продано',
          type: 'column',
          data: _map(this.pricesDynamics, (price) => price.countSold) 
        }
      },
      minPriceSeries() {
        return {
          name: 'Цена',
          type: 'line',
          data: _map(this.pricesDynamics, (price) => price.minPriceUsd * this.getExchangeRateByDate(price.date)) 
        }
      },
      series() {
        return [this.countSoldSeries, this.minPriceSeries]
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
          if (response.status.isSuccessful) {
            this.pricesDynamics = _orderBy(response.result, ['date'], ['asc'])
          }
        } catch (exception) {
          this.$error(exception.message, 'Ошибка при загрузке')
        }
      },
      getExchangeRateByDate(date) {
        return _find(this.currentUser?.currency?.rates, (rate) => moment(date).isAfter(rate.dateTime))?.rate || 1
      },
    },
  }
</script>

<style scoped>

</style>
