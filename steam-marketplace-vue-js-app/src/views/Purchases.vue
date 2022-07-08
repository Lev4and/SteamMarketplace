<template>
  <div id="purchases">
    <my-purchases-items-container />
  </div>
</template>

<script>
  import MyPurchasesItemsContainer from '@/components/purchases/MyPurchasesItemsContainer'

  export default {
    name: 'Purchases',

    components: {
      MyPurchasesItemsContainer,
    },
  }
</script>
