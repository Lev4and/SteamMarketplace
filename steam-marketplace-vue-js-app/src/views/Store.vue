<template>
  <div id="store">
    <grouped-items />
  </div>
</template>

<script>
  import GroupedItems from '@/components/store/GroupedItems'

  export default {
    name: 'Store',

    components: {
      GroupedItems,
    },
  }
</script>

<style scoped>

</style>
