<template>
  <div id="spinner">
    <a-spin
        :spinning="loading"
      >
        <a-icon
          slot="indicator"
          type="loading"
          style="font-size: 96px"
          spin
        />
        <slot />
    </a-spin>
  </div>
</template>

<script>
  export default {
    name: 'Spinner',

    props: {
      loading: {
        type: Boolean,
        required: true,
      },
    },
  }
</script>

<style>
  #spinner .ant-spin.ant-spin-spinning {
    top: 50% !important;
    left: 50% !important;
    z-index: 4 !important;
    width: auto !important;
    height: auto !important;
    position: fixed !important;
    margin-top: -48px !important;
    margin-left: -48px !important;
  }
</style>