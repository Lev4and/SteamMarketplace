<template>
  <section>
    <header v-if="title">
      {{ title }}
      <a-tooltip
        v-if="tooltip"
        slot="suffix"
      >
        <template slot="title">
          <span>{{ tooltip }}</span>
        </template>
        <a-icon
          type="info-circle"
          style="color: rgba(0,0,0,.45)"
        />
      </a-tooltip>
    </header>
    <main>
      <slot />
    </main>
  </section>
</template>

<script>
  export default {
    name: 'FilterBlock',
    
    props: {
      title: {
        default: '',
        type: String,
        required: false,
      },
      tooltip: {
        default: '',
        type: String,
        required: false,
      },
    },
  }
</script>

<style scoped>
  header {
    color: #001a34;
    cursor: pointer;
    font-size: 16px;
    font-weight: 700;
    line-height: 1.25;
    margin-bottom: 16px;
  }
</style>