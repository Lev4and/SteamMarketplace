<template>
  <div id="store">
    <a-row :gutter="[0,32]">
      <a-col :span="24">
        <a-page-header title="Мониторинг">
          <a-spin slot="subTitle">
            <a-icon slot="indicator" type="loading" style="font-size: 24px" spin />
          </a-spin>
        </a-page-header>
        <store-monitoring />
      </a-col>
      <a-col :span="24">
        <a-page-header title="Товары" />
        <grouped-items-container />
      </a-col>
    </a-row>
  </div>
</template>

<script>
  import StoreMonitoring from '@/components/dashboard/StoreMonitoring'
  import GroupedItemsContainer from '@/components/store/GroupedItemsContainer'

  export default {
    name: 'Store',

    components: {
      StoreMonitoring,
      GroupedItemsContainer,
    },
  }
</script>

<style scoped>

</style>
