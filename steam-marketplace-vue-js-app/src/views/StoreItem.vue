<template>
  <div id="storeItem">
    <a-row :gutter="[0,32]">
      <a-col :span="24">
        <skin-info />
      </a-col>
      <a-col :span="24">
        <skin-prices-dynamics />
      </a-col>
      <a-col :span="24">

      </a-col>
    </a-row>
  </div>
</template>

<script>
  import SkinInfo from '@/components/storeItem/SkinInfo'
  import SkinPricesDynamics from '@/components/storeItem/SkinPricesDynamics'

  export default {
    name: 'StoreItem',

    components: {
      SkinInfo,
      SkinPricesDynamics,
    },
  }
</script>

<style scoped>

</style>
