<template>
  <filter-block :title="filter.name">
    <a-slider
      v-model="sliderRange"
      range
      :min="range.min"
      :max="range.max"
      :step="0.1"
      @change="onChangeRangeSlider"
    />
    <a-row>
      <a-col :span="8">
        <a-input-number
          v-model="range.from"
          :min="range.min"
          :max="range.max"
          :step="0.1"
          class="w-full"
          @change="onChangeRangeFrom"
        />
      </a-col>
      <a-col :span="8" />
      <a-col :span="8">
        <a-input-number
          v-model="range.to"
          :min="range.min"
          :max="range.max"
          :step="0.1"
          class="w-full"
          @change="onChangeRangeTo"
        />
      </a-col>
    </a-row>
  </filter-block>
</template>

<script>
  import { map as _map, has as _has } from 'lodash'
  import { filter } from '@/services/mixins/filter'
  import FilterBlock from '@/components/common/FilterBlock'

  export default {
    name: 'FilterTypeRange',

    components: {
      FilterBlock,
    },

    mixins: [filter],

    data: () => ({
      range: null,
      sliderRange: [],
    }),

    watch: {
      range: {
        handler() {
          const range = this.filter.range
          if (this.range) {
            if (this.range?.from && this.range?.to) {
              if (!(this.range.from === range?.min && this.range.to === range?.max)) {
                this.saveFilter(`${this.range.from.toFixed(3)};${this.range.to.toFixed(3)}`)
              } else this.clearFilter()
            } else this.clearFilter()
          }
        },
        deep: true,
        immediate: true,
      },
      filters: {
        handler(newValue, oldValue) {
          const newRange = _has(newValue, this.filter.key) ? newValue[this.filter.key] : ''
          const oldRange = _has(oldValue, this.filter.key) ? oldValue[this.filter.key] : ''
          if ((newRange !== oldRange) || (!newRange && !oldRange)) {
            const range = this.filter.range
            const rangeFromRoute = this.valueFromRoute ? _map(this.valueFromRoute.split(';'), (value) => parseFloat(value)) : null
            if (rangeFromRoute) {
              range.from = rangeFromRoute[0]
              range.to = rangeFromRoute[1]
            }
            this.range = range
            this.sliderRange = [range.from, range.to]
          }
        },
        deep: true,
        immediate: true,
      },
    },

    methods: {
      onChangeRangeSlider(value) {
        this.range.from = value[0]
        this.range.to = value[1]
      },
      onChangeRangeFrom(value) {
        this.sliderRange = [value, this.range.to]
      },
      onChangeRangeTo(value) {
        this.sliderRange = [this.range.from, value]
      },
    },
  }
</script>
