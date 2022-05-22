<template>
  <div id="cSMoneyItemsContainer">
    <a-spin :spinning="isLoading">
      <a-icon
        slot="indicator"
        type="loading"
        style="font-size: 96px"
        spin
      />
      <a-row :gutter="[8,16]">
        <template v-for="item in items">
          <a-col :key="item.id" :sm="12" :md="8" :lg="6" :xl="4" :xxl="4">
            <cS-money-item :item="item" />
          </a-col>
        </template>
      </a-row>
    </a-spin>
  </div>
</template>

<script>
  import API from '@/api'
  import EventBus from '@/services/eventBus'
  import CSMoneyItem from '@/components/cSMoney/CSMoneyItem'

  export default {
    name: 'CSMoneyItemsContainer',

    components: {
      CSMoneyItem,
    },

    data: () => ({
      limit: 50,
      items: [],
      offset: 0,
      isLoading: false,
    }),

    watch: {
      offset: {
        async handler() {
          await this.loadItems()
        },
        deep: true,
        immediate: true,
      },
    },

    mounted() {
      EventBus.$on('content-scrolled', this.onScroll)
    },

    beforeDestroy() {
      EventBus.$off('content-scrolled', this.onScroll)
    },

    methods: {
      onScroll(event) {
        if (!this.isLoading) {
          if (event.target.scrollTop / (event.target.scrollHeight - event.target.offsetHeight) * 100 > 95) {
            this.offset += this.limit
          }
        }
      },
      async loadItems() {
        this.isLoading = true
        const response = await API.cSMoney.getInventory(this.limit, this.offset)
        if (response.status.isSuccessful()) {
          this.items.push(...response.result.items || [])
        }
        this.isLoading = false
      },
    },
  }
</script>

<style>
  #cSMoneyItemsContainer .ant-spin.ant-spin-spinning {
    top: 50% !important;
    z-index: 4 !important;
    width: auto !important;
    height: auto !important;
    position: fixed !important;
    left: calc(50% + 48px) !important;
  }
</style>

<style scoped>
  #cSMoneyItemsContainer {
    padding-right: 10px;
  }
</style>
