<template>
  <div id="storeItem">
    <a-row :gutter="[0,64]">
      <a-col :span="24">
        <a-page-header title="Информация о скине" />
        <skin-info />
      </a-col>
      <a-col :span="24">
        <a-row :gutter="[0,16]">
          <a-col :span="24">
            <skin-prices-dynamics />
          </a-col>
          <a-col :span="24">
            <skin-exposed-sales-dynamics />
          </a-col>
          <a-col :span="24">
            <skin-added-dynamics />
          </a-col>
        </a-row>
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
  import SkinExposedSalesDynamics from '@/components/storeItem/SkinExposedSalesDynamics'
  import SkinAddedDynamics from '@/components/storeItem/SkinAddedDynamics'

  export default {
    name: 'StoreItem',

    components: {
      SkinInfo,
      SkinPricesDynamics,
      SkinSalesItemsContainer,
      SkinExposedSalesDynamics,
      SkinAddedDynamics,
    },
  }
</script>

<style scoped>

</style>
