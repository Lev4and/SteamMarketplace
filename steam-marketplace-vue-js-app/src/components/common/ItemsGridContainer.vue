<template>
  <a-row :gutter="[0,16]" class="items-grid-container">
    <a-col :span="24" class="items-container">
      <a-row :gutter="[8,8]">
        <template v-for="(item, index) in items">
          <a-col :key="index" :sm="12" :md="8" :lg="6" :xl="4" :xxl="4">
            <slot name="item" v-bind:item="item" />
          </a-col>
        </template>
      </a-row>
    </a-col>
    <a-col :span="24">
      <slot name="pagination" />
    </a-col>
  </a-row>
</template>

<script>
  export default {
    name: 'ItemsGridContainer',

    props: {
      items: {
        type: Array,
        required: true,
      },
    },
  }
</script>

<style>
  .items-grid-container ul.pagination {
    display: flex;
    flex-direction: row;
    justify-content: center;
  }
</style>

<style scoped>
  .items-container {
    padding-right: 10px;
  }
</style>
