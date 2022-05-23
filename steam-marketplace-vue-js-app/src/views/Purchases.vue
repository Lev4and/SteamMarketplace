<template>
  <div id="purchases">
    <my-purchases />
  </div>
</template>

<script>
  import MyPurchases from '@/components/purchases/MyPurchases'

  export default {
    name: 'Purchases',

    components: {
      MyPurchases,
    },
  }
</script>

<style scoped>

</style>
