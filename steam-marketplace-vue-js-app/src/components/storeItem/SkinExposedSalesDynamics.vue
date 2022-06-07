<template>
  <div id="skinExposedSalesDynamics">
    <apexchart type="line" height="500" :options="chartOptions" :series="series" />
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'
  import { map as _map, findLast as _findLast } from 'lodash'
  import moment from 'moment'
  import API from '@/api'
  import { getCurrencyFormat } from '@/services/utils/formatUtils'

  export default {
    name: 'SkinExposedSalesDynamics',

    data: () => ({
      exposedSalesDynamics: [],
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
        return _map(this.exposedSalesDynamics, (exposedSales) => {
          return moment.parseZone(exposedSales.date).utc(true).local().format('LLLL')
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
            text: 'Динамика выставлений на продажу'
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
                text: 'Выставлено',
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
      countExposedSeries() {
        return {
          name: 'Выставлено',
          type: 'column',
          data: _map(this.exposedSalesDynamics, (exposedSales) => exposedSales.countExposed) 
        }
      },
      minPriceSeries() {
        return {
          name: 'Цена',
          type: 'line',
          data: _map(this.exposedSalesDynamics, (exposedSales) => exposedSales.minPriceUsd * this.getExchangeRateByDate(exposedSales.date)) 
        }
      },
      series() {
        return [this.countExposedSeries, this.minPriceSeries]
      },
    },

    watch: {
      fullName: {
        async handler() {
          await this.loadExposedSalesDynamics()
        },
        immediate: true,
      },
    },

    methods: {
      async loadExposedSalesDynamics() {
        try {
          const response = await API.sales.getExposedSalesDynamics(this.fullName)
          if (response.status.isSuccessful()) {
            this.exposedSalesDynamics = response.result
          }
        } catch (exception) {
          this.$error(exception.message, 'Ошибка при загрузке')
        }
      },
      getExchangeRateByDate(date) {
        return _findLast(this.currentUser?.currency?.rates, (rate) => moment(date).isAfter(rate.dateTime))?.rate || 1
      },
    },
  }
</script>

<style scoped>

</style>
