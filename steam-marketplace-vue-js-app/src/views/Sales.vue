<template>
  <div id="sales">
    <my-sales-items-container />
  </div>
</template>

<script>
  import MySalesItemsContainer from '@/components/sales/MySalesItemsContainer'

  export default {
    name: 'Sales',

    components: {
      MySalesItemsContainer,
    },
  }
</script>

<style scoped>

</style>
