import auth from './auth'
import common from './common'
import exchangeRates from './exchangeRates'

import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {

  },
  mutations: {

  },
  actions: {

  },
  modules: {
    auth,
    common,
    exchangeRates,
  },
})
