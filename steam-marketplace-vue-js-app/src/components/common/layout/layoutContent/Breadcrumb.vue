<template>
  <div id="breadcrumb">
    <a-breadcrumb style="margin: 16px 0;">
      <template v-for="(item, i) in items">
        <a-breadcrumb-item :key="i">
          <router-link :to="{ name: item.name }">
            {{ item.meta.title }}
          </router-link>
        </a-breadcrumb-item>
      </template>
    </a-breadcrumb>
  </div>
</template>

<script>
  export default {
    name: 'Breadcrumb',

    computed: {
      items() {
        return this.$route.matched || []
      },
    },
  }
</script>

<style scoped>

</style>
