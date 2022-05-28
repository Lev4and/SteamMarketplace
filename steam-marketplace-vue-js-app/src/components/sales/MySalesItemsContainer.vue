<template>
  <div id="mySalesItemsContainer">
    <layout-content-spinner :loading="loading">
      <items-grid-container :items="items">
        <template v-slot:item="{ item }">
          <my-sale-item :sale="item" />
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
  import MySaleItem from '@/components/sales/MySaleItem'
  import Pagination from '@/components/common/Pagination'
  import ItemsGridContainer from '@/components/common/ItemsGridContainer'
  import LayoutContentSpinner from '@/components/common/layout/layoutContent/LayoutContentSpinner'

  export default {
    name: 'MySalesItemsContainer',

    mixins: [itemsContainer],

    components: {
      MySaleItem,
      Pagination,
      ItemsGridContainer,
      LayoutContentSpinner,
    },

    methods: {
      async getItems() {
        const filters = {
          userId: this.currentUser.id,
          pagination: {
            page: this.page,
            limit: this.limit,
          },
        }
        return await API.sales.getMySales(filters)
      },
    },
  }
</script>
