<template>
  <div id="myInventory">
    <inventory-items-container />
  </div>
</template>

<script>
  import InventoryItemsContainer from '@/components/myInventory/InventoryItemsContainer'

  export default {
    name: 'MyInventory',

    components: {
      InventoryItemsContainer,
    },
  }
</script>

<style scoped>

</style>
