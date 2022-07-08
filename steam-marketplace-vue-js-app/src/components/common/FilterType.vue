<template>
  <div class="filter">
    <template v-if="filter.type === 'range'">
      <filter-type-range :filter="filter" />
    </template>
  </div>
</template>

<script>
  import FilterTypeRange from '@/components/common/filters/FilterTypeRange'

  export default {
    name: 'FilterType',

    components: {
      FilterTypeRange,
    },

    props: {
      filter: {
        type: Object,
        required: true,
      },
    },
  }
</script>
