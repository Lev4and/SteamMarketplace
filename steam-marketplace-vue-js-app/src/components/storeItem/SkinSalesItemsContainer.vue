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
  import { map as _map, groupBy as _groupBy, forEach as _forEach, find as _find, set as _set } from 'lodash'
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

    watch: {
      items: {
        async handler() {
          await this.loadItemNesteds()
        },
        immediate: true,
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
      async loadItemNesteds() {
        const itemIds = _map(this.items, (item) => item.itemId)
        const response = await API.itemNesteds.getItemNesteds(itemIds)
        if (response.status.isSuccessful()) {
          const itemNesteds = response.result
          const groupedItemNesteds = _groupBy(itemNesteds, 'itemId')
          _forEach(groupedItemNesteds, (groupItemNesteds, key) => {
            const item = _find(this.items, (item) => item.itemId === key)
            _set(item, 'item.itemNesteds', groupItemNesteds)
          })
        }
      },
    },
  }
</script>

<style scoped>

</style>
