<template>
  <div id="skinsImport">
    <a-row :gutter="[0,32]">
      <a-col :span="24">
        <a-page-header title="Прогресс" />
        <a-row :gutter="[8,8]">
          <a-col :sm="12" :md="8" :lg="6">
            <a-card title="Прошло времени">
              {{ elapsedTime }}
            </a-card>
          </a-col>
          <a-col :sm="12" :md="8" :lg="6">
            <a-card title="Найдено">
              {{ foundItems }}
            </a-card>
          </a-col>
          <a-col :sm="12" :md="8" :lg="6">
            <a-card title="Импортировано">
              {{ importedItems }}
            </a-card>
          </a-col>
        </a-row>
      </a-col>
      <a-col :span="24">
        <a-page-header title="Мониторинг">
          <a-spin slot="subTitle">
            <a-icon slot="indicator" type="loading" style="font-size: 24px" spin />
          </a-spin>
        </a-page-header>
        <cS-money-monitoring />
      </a-col>
    </a-row>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'
  import moment from 'moment'
  import CSMoneyMonitoring from '@/components/cSMoney/CSMoneyMonitoring'

  export default {
    name: 'SkinsImport',

    components: {
      CSMoneyMonitoring,
    },

    data: () => ({
      interval: null,
      elapsedTime: '',
      startedAt: moment(),
    }),

    computed: {
      ...mapGetters({
        importing: 'skins/importing',
        foundItems: 'skins/foundItems',
        importedItems: 'skins/importedItems',
      }),
    },

    watch: {
      importing: {
        async handler(newValue, oldValue) {
          if (!newValue && !oldValue) await this.startImport()
          else if (!newValue && oldValue) await this.stopImport()
        },
        immediate: true,
      },
    },

    async beforeDestroy() {
      await this.stopImport()
    },

    methods: {
      async startImport() {
        this.interval = setInterval(() => {
          this.elapsedTime = moment.utc(moment().diff(this.startedAt)).format('HH:mm:ss')
        }, 1000)
        await this.$store.dispatch('skins/import')
      },
      async stopImport() {
        clearInterval(this.interval)
        await this.$store.dispatch('skins/cancelImport')
      },
    },
  }
</script>

<style scoped>

</style>
