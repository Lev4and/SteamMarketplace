<template>
  <div id="groupedItemsContainer">
    <layout-content-spinner :loading="loading">
      <items-grid-container :items="items">
        <template v-slot:item="{ item }">
          <grouped-item :item="item" />
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
  import GroupedItem from '@/components/store/GroupedItem'
  import ItemsGridContainer from '@/components/common/ItemsGridContainer'
  import LayoutContentSpinner from '@/components/common/layout/layoutContent/LayoutContentSpinner'

  export default {
    name: 'GroupedItemsContainer',

    mixins: [itemsContainer],

    components: {
      Pagination,
      GroupedItem,
      ItemsGridContainer,
      LayoutContentSpinner,
    },

    data: () => ({
      searchString: '',
    }),

    methods: {
      async getItems() {
        const filters = {
          currencyId: this.currentUser.currencyId,
          searchString: this.searchString,
          pagination: {
            page: this.page,
            limit: this.limit,
          },
        }
        return await API.items.getGroupedItems(filters)
      },
    },
  }
</script>
