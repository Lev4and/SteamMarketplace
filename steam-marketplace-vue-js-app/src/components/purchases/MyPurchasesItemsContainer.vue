<template>
  <div id="myPurchasesItemsContainer">
    <layout-content-spinner :loading="loading">
      <items-grid-container :items="items">
        <template v-slot:item="{ item }">
          <my-purchase-item :purchase="item" />
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
  import MyPurchaseItem from '@/components/purchases/MyPurchaseItem'
  import ItemsGridContainer from '@/components/common/ItemsGridContainer'
  import LayoutContentSpinner from '@/components/common/layout/layoutContent/LayoutContentSpinner'

  export default {
    name: 'MyPurchasesItemsContainer',

    mixins: [itemsContainer],

    components: {
      Pagination,
      MyPurchaseItem,
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
        return await API.purchases.getMyPurchases(filters)
      },
    },
  }
</script>
