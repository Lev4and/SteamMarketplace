import Vue from 'vue'
import API from '@/api'

const state = {
  latestExchangeRate: null,
  exchangeRates: [],
}

const mutations = {
  setLatestExchangeRate(state, latestExchangeRate) {
    state.latestExchangeRate = latestExchangeRate
  },
  setExchangeRates(state, exchangeRates) {
    state.exchangeRates = exchangeRates
  },
}

const getters = {
  latestExchangeRate(state) {
    return state.latestExchangeRate
  },
  exchangeRates(state) {
    return state.exchangeRates
  },
}

const actions = {
  async init({ state, commit, dispatch }) {
    if (!state.latestExchangeRate) {
      try {
        const response = await API.cbr.getLatestExchangeRates()
        if (response.status.isSuccessful()) {
          commit('setLatestExchangeRate', response.result)
          await dispatch('import')
        } else Vue.error(response.status.message, 'Ошибка')
      } catch (exception) {
        Vue.error('Возникла ошибка при получении обменных курсов', 'Ошибка')
      }
    }
  },
  async loadExchangeRates({ commit }, currencyId) {
    try {
      const response = await API.exchangeRates.getExchangeRates(currencyId)
      if (response.status.isSuccessful()) commit('setLatestExchangeRate', response.result) 
      else Vue.error(response.status.message, 'Ошибка')
    } catch (exception) {
      Vue.error('Возникла ошибка при получении обменных курсов', 'Ошибка')
    }
  },
  async import() {
    if (state.latestExchangeRate) {
      try {
        await API.imports.importLatestExchangeRate(state.latestExchangeRate)
      } catch (exception) {
        Vue.error('Возникла ошибка при импорте обменных курсов', 'Ошибка')
      }
    }
  },
}

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions,
}
