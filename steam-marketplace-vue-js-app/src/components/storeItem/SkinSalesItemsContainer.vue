<template>
  <div id="skinSales">
    <layout-content-spinner :loading="loading">
      <items-grid-container :items="items">
        <template v-slot:item="{ item }">
          <skin-sale-item :sale="item" />
        </template>
        <template slot="pagination">
          <pagination
            v-model="page"
            class="pagination"
            :page-size="limit"
            :total-items="totalItems"
            @limit-changed="onLimitChanged"
          />
        </template>
      </items-grid-container>
    </layout-content-spinner>
  </div>
</template>

<script>
  import API from '@/api'
  import itemsContainer from '@/services/mixins/itemsContainer'
  import Pagination from '@/components/common/Pagination'
  import SkinSaleItem from '@/components/storeItem/SkinSaleItem'
  import ItemsGridContainer from '@/components/common/ItemsGridContainer'
  import LayoutContentSpinner from '@/components/common/layout/layoutContent/LayoutContentSpinner'

  export default {
    name: 'SkinSalesItemsContainer',

    mixins: [itemsContainer],

    components: {
      Pagination,
      SkinSaleItem,
      ItemsGridContainer,
      LayoutContentSpinner,
    },

    computed: {
      fullName() {
        return this.$route.params.fullName
      },
    },

    methods: {
      async getItems() {
        const filters = {
          fullName: this.fullName,
          pagination: {
            page: this.page,
            limit: this.limit,
          },
        }
        return await API.sales.getSalesItem(filters)
      },
    },
  }
</script>

<style scoped>

</style>
