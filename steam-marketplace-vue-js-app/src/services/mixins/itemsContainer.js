import Vue from 'vue'
import { mapGetters } from 'vuex'
import { cloneDeep as _cloneDeep, set as _set } from 'lodash'

export default {
  data: () => ({
    result: {},
    loading: false,
  }),

  computed: {
    ...mapGetters({
      currentUser: 'auth/currentUser',
    }),
    query() {
      return _cloneDeep(this.$route.query)
    },
    page: {
      get() {
        return parseInt(this.query.p) || 1
      },
      set(value) {
        this.$router.push({ query: _set(this.query, 'p', value) })
      },
    },
    limit: {
      get() {
        return parseInt(this.query.limit) || 50
      },
      set(value) {
        this.$router.push({ query: _set(this.query, 'limit', value) })
      },
    },
    items() {
      return this.result?.items || []
    },
    pagination() {
      return this.result?.pageInfo
    },
    totalItems() {
      return this.pagination?.totalItems || 0
    },
  },

  watch: {
    page: {
      async handler() {
        await this.load(this.getItems)
      },
      immediate: true,
    },
    limit: {
      async handler() {
        await this.load(this.getItems)
      },
      immediate: true,
    },
  },

  methods: {
    onLimitChanged(limit) {
      this.limit = limit
    },
    async load(getItemsFunc) {
      try {
        this.loading = true
        const response = await getItemsFunc()
        if (response.status.isSuccessful) {
          this.result = response.result
        }
      } catch (exception) {
        Vue.error(exception.message, 'Ошибка при загрузке')
      } finally {
        this.loading = false
      }
    },
    async getItems() {

    },
  },
}
