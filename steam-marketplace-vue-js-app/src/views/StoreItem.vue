<template>
  <div id="storeItem">
    <a-row :gutter="[0,64]">
      <a-col :span="24">
        <a-page-header title="Информация о скине" />
        <skin-info />
      </a-col>
      <a-col :span="24">
        <a-page-header title="Статистика" />
        <skin-prices-dynamics />
      </a-col>
      <a-col :span="24">
        <a-page-header title="Лоты" />
        <skin-sales-items-container />
      </a-col>
    </a-row>
  </div>
</template>

<script>
  import SkinInfo from '@/components/storeItem/SkinInfo'
  import SkinPricesDynamics from '@/components/storeItem/SkinPricesDynamics'
  import SkinSalesItemsContainer from '@/components/storeItem/SkinSalesItemsContainer'

  export default {
    name: 'StoreItem',

    components: {
      SkinInfo,
      SkinPricesDynamics,
      SkinSalesItemsContainer,
    },
  }
</script>

<style scoped>

</style>
