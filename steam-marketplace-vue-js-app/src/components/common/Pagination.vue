<template>
  <a-pagination
    v-model="page"
    v-bind="$props"
    :total="totalItems"
    :page-size="pageSize"
    :show-total="showTotal"
    :show-size-changer="true"
    :page-size-options="pageSizeOptions"
    :show-quick-jumper="showQuickJumper"
    @showSizeChange="onSizeChanged"
  />
</template>

<script>
  import { getTotalNumberFormat } from '@/services/utils/formatUtils'

  export default {
    name: 'Pagination',

    props: {
      value: {
        type: Number,
        required: true,
      },
      pageSize: {
        type: Number,
        required: true,
      },
      totalItems: {
        type: Number,
        required: true,
      },
      showTotal: {
        type: Function,
        default: getTotalNumberFormat,
        required: false,
      },
      showQuickJumper: {
        type: Boolean,
        default: true,
        required: false,
      },
    },

    data: () => ({
      pageSizeOptions: ['5', '10', '15', '25', '50', '75', '100']
    }),

    computed: {
      page: {
        get() {
          return this.value
        },
        set(value) {
          this.$emit('input', value)
        },
      },
    },

    methods: {
      onSizeChanged(current, size) {
        this.$emit('limit-changed', size)
      },
    },
  }
</script>
