<template>
  <div id="cSMoneyItemsContainer">
    <a-spin :spinning="isLoading">
      <a-icon
        slot="indicator"
        type="loading"
        style="font-size: 96px"
        spin
      />
      <a-row :gutter="[0,16]">
        <a-col :span="24" class="items-container">
          <a-row :gutter="[8,8]">
            <template v-for="item in items">
              <a-col :key="item.id" :sm="12" :md="8" :lg="6" :xl="4" :xxl="4">
                <cS-money-item :item="item" />
              </a-col>
            </template>
          </a-row>
        </a-col>
        <a-col :span="24">
          <a-pagination
            v-model="page"
            class="pagination"
            show-quick-jumper
            :page-size="limit"
            :total="total"
            :show-total="total => `Total ${total} items`"
          />
        </a-col>
      </a-row>
    </a-spin>
  </div>
</template>

<script>
  import API from '@/api'
  import CSMoneyItem from '@/components/cSMoney/CSMoneyItem'

  export default {
    name: 'CSMoneyItemsContainer',

    components: {
      CSMoneyItem,
    },

    data: () => ({
      limit: 50,
      total: 150000,
      result: {},
      isLoading: false,
    }),

    computed: {
      page: {
        get() {
          return parseInt(this.$route.query.p) || 1
        },
        set(value) {
          this.$router.push({ query: { p: value } })
        },
      },
      items() {
        return this.result?.items || []
      },
    },

    watch: {
      page: {
        async handler() {
          await this.loadItems()
        },
        immediate: true,
      },
    },

    methods: {
      async loadItems() {
        this.isLoading = true
        const response = await API.cSMoney.getInventory(this.limit, (this.page - 1) * this.limit)
        if (response.status.isSuccessful()) {
          this.result = response.result
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
  #cSMoneyItemsContainer .items-container {
    padding-right: 10px;
  }
  ul.pagination {
    display: flex;
    flex-direction: row;
    justify-content: center;
  }
</style>
