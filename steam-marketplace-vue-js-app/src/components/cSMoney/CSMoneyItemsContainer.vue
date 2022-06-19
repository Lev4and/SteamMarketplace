<template>
  <div id="cSMoneyItemsContainer">
    <layout-content-spinner :loading="loading">
      <items-grid-container :items="items">
        <template v-slot:item="{ item }">
          <cS-money-item :item="item" />
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
  import CSMoneyItem from '@/components/cSMoney/CSMoneyItem'
  import ItemsGridContainer from '@/components/common/ItemsGridContainer'
  import LayoutContentSpinner from '@/components/common/layout/layoutContent/LayoutContentSpinner'

  export default {
    name: 'CSMoneyItemsContainer',

    mixins: [itemsContainer],

    components: {
      Pagination,
      CSMoneyItem,
      ItemsGridContainer,
      LayoutContentSpinner,
    },

    methods: {
      async getItems() {
        return await API.cSMoney.getInventory(this.limit, (this.page - 1) * this.limit)
      },
    },
  }
</script>
