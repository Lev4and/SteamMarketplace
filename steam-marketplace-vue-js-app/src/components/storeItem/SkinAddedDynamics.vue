<template>
  <div id="skinAddedDynamics">
    <apexchart type="line" height="500" :options="chartOptions" :series="series" />
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'
  import { map as _map, orderBy as _orderBy } from 'lodash'
  import moment from 'moment'
  import API from '@/api'

  export default {
    name: 'SkinAddedDynamics',

    data: () => ({
      addedDynamics: [],
    }),

    computed: {
      ...mapGetters({
        currentUser: 'auth/currentUser',
      }),
      cultureInfoName() {
        return this.currentUser?.currency?.cultureInfoName || 'us-US'
      },
      fullName() {
        return this.$route.params.fullName
      },
      dates() {
        moment.locale(this.cultureInfoName)
        return _map(this.addedDynamics, (addedDynamic) => {
          return moment.parseZone(addedDynamic.date).utc(true).local().format('LLLL')
        })
      },
      chartOptions() {
        return {
          chart: {
            height: 500,
            type: 'line',
          },
          title: {
            text: 'Динамика добавлений'
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
                text: 'Добавлено',
              },
              labels: {
                formatter: (value) => { return value.toFixed(0) }
              },
            }, 
          ],
        }
      },
      countAddedSeries() {
        return {
          name: 'Добавлено',
          type: 'line',
          data: _map(this.addedDynamics, (addedDynamic) => addedDynamic.countAdded) 
        }
      },
      series() {
        return [this.countAddedSeries]
      },
    },

    watch: {
      fullName: {
        async handler() {
          await this.loadAddedDynamics()
        },
        immediate: true,
      },
    },

    methods: {
      async loadAddedDynamics() {
        try {
          const response = await API.items.getAddedDynamics(this.fullName)
          if (response.status.isSuccessful) {
            this.addedDynamics = _orderBy(response.result, ['date'], ['asc'])
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
