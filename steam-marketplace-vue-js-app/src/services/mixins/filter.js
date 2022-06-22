import { has as _has, set as _set, omit as _omit, isEqual as _isEqual, cloneDeep as _cloneDeep } from 'lodash'

export const filter = {
  props: {
    filter: {
      type: Object,
      required: true,
    },
  },

  computed: {
    query() {
      return _cloneDeep(this.$route.query)
    },
    filters() {
      return this.query.filters ? JSON.parse(this.query.filters) : {}
    },
    valueFromRoute() {
      return _has(this.filters, this.filter.key) ? this.filters[this.filter.key] : ''
    },
  },

  methods: {
    saveFilter(value) {
      if (!_isEqual(this.filters, _set(_cloneDeep(this.filters), this.filter.key, value))) {
        this.$router.push({ query: _set(this.query, 'filters', JSON.stringify(_set(this.filters, this.filter.key, value))) })
      }
    },
    clearFilter() {
      if (_has(this.filters, this.filter.key)) {
        this.$router.push({ query: _set(this.query, 'filters', JSON.stringify(_omit(this.filters, this.filter.key))) })
      }
    },
  },
}