<template>
  <div id="inventoryItemsContainer">
    <layout-content-spinner :loading="loading">
      <items-grid-container :items="items">
        <template v-slot:item="{ item }">
          <inventory-item :item="item.item" />
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
  import InventoryItem from '@/components/myInventory/InventoryItem'
  import ItemsGridContainer from '@/components/common/ItemsGridContainer'
  import LayoutContentSpinner from '@/components/common/layout/layoutContent/LayoutContentSpinner'

  export default {
    name: 'InventoryItemsContainer',

    mixins: [itemsContainer],

    components: {
      Pagination,
      InventoryItem,
      ItemsGridContainer,
      LayoutContentSpinner,
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
          userId: this.currentUser.id,
          pagination: {
            page: this.page,
            limit: this.limit,
          },
        }
        return await API.userInventories.getMyInventory(filters)
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
