<template>
  <div id="store">
    <grouped-items-container />
  </div>
</template>

<script>
  import GroupedItemsContainer from '@/components/store/GroupedItemsContainer'

  export default {
    name: 'Store',

    components: {
      GroupedItemsContainer,
    },
  }
</script>

<style scoped>

</style>
