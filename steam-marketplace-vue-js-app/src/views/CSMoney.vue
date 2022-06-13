<template>
  <div id="cSMoney">
    <a-row :gutter="[0,32]">
      <a-col :span="24">
        <a-page-header title="Мониторинг">
          <a-spin slot="subTitle">
            <a-icon slot="indicator" type="loading" style="font-size: 24px" spin />
          </a-spin>
        </a-page-header>
        <cS-money-monitoring />
      </a-col>
      <a-col :span="24">
        <a-page-header title="Товары" />
        <cS-money-items-container />
      </a-col>
    </a-row>
  </div>
</template>

<script>
  import CSMoneyMonitoring from '@/components/cSMoney/CSMoneyMonitoring'
  import CSMoneyItemsContainer from '@/components/cSMoney/CSMoneyItemsContainer'

  export default {
    name: 'CSMoney',

    components: {
      CSMoneyMonitoring,
      CSMoneyItemsContainer,
    },
  }
</script>

<style scoped>

</style>
