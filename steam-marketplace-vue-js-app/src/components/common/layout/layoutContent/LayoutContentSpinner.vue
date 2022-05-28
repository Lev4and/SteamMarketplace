<template>
  <div class="layout-content-spinner">
    <spinner :loading="loading">
      <slot />
    </spinner>
  </div>
</template>

<script>
  import Spinner from '@/components/common/Spinner'

  export default {
    name: 'LayoutContentSpinner',

    components: {
      Spinner,
    },

    props: {
      loading: {
        type: Boolean,
        required: true,
      },
    },
  }
</script>

<style>
  .layout-content-spinner .spinner .ant-spin.ant-spin-spinning {
    top: 50% !important;
    z-index: 4 !important;
    width: auto !important;
    height: auto !important;
    position: fixed !important;
    margin-top: unset !important;
    margin-left: unset !important;
    left: calc(50% + 48px) !important;
  }
</style>
