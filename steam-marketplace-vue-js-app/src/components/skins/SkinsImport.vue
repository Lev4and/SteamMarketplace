<template>
  <div id="skinsImport">
    <a-row :gutter="[0,0]">
      <a-col :span="24">
        <a-row :gutter="[8,8]">
          <a-col :sm="12" :md="8" :lg="6" :xl="4" :xxl="4">
            <a-card title="Прошло времени">
              {{ elapsedTime }}
            </a-card>
          </a-col>
          <a-col :sm="12" :md="8" :lg="6" :xl="4" :xxl="4">
            <a-card title="Найдено">
              {{ items.length }}
            </a-card>
          </a-col>
          <a-col :sm="12" :md="8" :lg="6" :xl="4" :xxl="4">
            <a-card title="Импортировано">
              {{ importedItems.length }}
            </a-card>
          </a-col>
          <a-col :sm="12" :md="8" :lg="6" :xl="4" :xxl="4">
            <a-card title="Скорость импорта">
              0
            </a-card>
          </a-col>
        </a-row>
      </a-col>
      <a-col :span="24">

      </a-col>
    </a-row>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'
  import moment from 'moment'

  export default {
    name: 'SkinsImport',

    data: () => ({
      interval: null,
      elapsedTime: '',
      startedAt: moment(),
    }),

    computed: {
      ...mapGetters({
        items: 'skins/items',
        importedItems: 'skins/importedItems',
      }),
    },

    mounted() {
      this.interval = setInterval(() => {
        this.elapsedTime = moment.utc(moment().diff(this.startedAt)).format('HH:mm:ss')
      }, 1000)
      this.import()
    },

    beforeDestroy() {
      clearInterval(this.interval)
    },

    methods: {
      async import() {
        await this.$store.dispatch('skins/import')
      },
    },
  }
</script>

<style scoped>

</style>
