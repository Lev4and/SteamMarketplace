<template>
  <div id="myPurchases">
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
            <template v-for="purchase in myPurchases">
              <a-col :key="purchase.id" :sm="12" :md="8" :lg="6" :xl="4" :xxl="4">
                <my-purchase :purchase="purchase" />
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
            :total="totalItems"
            :show-total="total => `Total ${total} items`"
          />
        </a-col>
      </a-row>
    </a-spin>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'
  import API from '@/api'
  import MyPurchase from '@/components/purchases/MyPurchase'

  export default {
    name: 'MyPurchases',

    components: {
      MyPurchase
    },

    data: () => ({
      limit: 50,
      result: {},
      isLoading: false,
    }),

    computed: {
      ...mapGetters({
        currentUser: 'auth/currentUser',
      }),
      page: {
        get() {
          return parseInt(this.$route.query.p) || 1
        },
        set(value) {
          this.$router.push({ query: { p: value } })
        },
      },
      myPurchases() {
        return this.result?.items || []
      },
      pagination() {
        return this.result?.pageInfo
      },
      totalItems() {
        return this.pagination?.totalItems || 0
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
        const filters = {
          userId: this.currentUser.id,
          pagination: {
            page: this.page,
            limit: this.limit,
          },
        }
        const response = await API.purchases.getMyPurchases(filters)
        if (response.status.isSuccessful()) {
          this.result = response.result
        }
        this.isLoading = false
      },
    },
  }
</script>

<style>
  #myPurchases .ant-spin.ant-spin-spinning {
    top: 50% !important;
    z-index: 4 !important;
    width: auto !important;
    height: auto !important;
    position: fixed !important;
    left: calc(50% + 48px) !important;
  }
</style>

<style scoped>
  #myPurchases .items-container {
    padding-right: 10px;
  }
  ul.pagination {
    display: flex;
    flex-direction: row;
    justify-content: center;
  }
</style>
