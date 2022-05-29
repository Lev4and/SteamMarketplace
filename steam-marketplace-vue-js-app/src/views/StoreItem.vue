<template>
  <div id="storeItem">
    <a-row :gutter="[0,0]">
      <a-col :span="24">

      </a-col>
      <a-col :span="24">

      </a-col>
      <a-col :span="24">

      </a-col>
    </a-row>
  </div>
</template>

<script>
  export default {
    name: 'StoreItem',

    data: () => ({
      item: null,
    }),

    computed: {
      fullName() {
        return this.$route.params.fullName
      },
    },

    watch: {
      fillName: {
        handler() {
          
        },
        deep: true,
        immediate: true,
      },
    },

    methods: {

    },
  }
</script>

<style scoped>

</style>
