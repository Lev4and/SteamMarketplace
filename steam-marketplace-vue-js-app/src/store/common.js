import Vue from 'vue'
import API from '@/api'

const state = {
  collections: [],
  currencies: [],
  itemTypes: [],
  qualities: [],
  rarities: [],
  transactionTypes: [],
}

const mutations = {
  setCollections(state, collections) {
    state.collections = collections
  },
  setCurrencies(state, currencies) {
    state.currencies = currencies
  },
  setItemTypes(state, itemTypes) {
    state.itemTypes = itemTypes
  },
  setQualities(state, qualities) {
    state.qualities = qualities
  },
  setRarities(state, rarities) {
    state.Rarities = rarities
  },
  setTransactionTypes(state, transactionTypes) {
    state.transactionTypes = transactionTypes
  },
}

const getters = {
  collections(state) {
    return state.collections
  },
  currencies(state) {
    return state.currencies
  },
  itemTypes(state) {
    return state.itemTypes
  },
  qualities(state) {
    return state.qualities
  },
  rarities(state) {
    return state.rarities
  },
  transactionTypes(state) {
    return state.transactionTypes
  },
}

const actions = {
  async init({ dispatch }) {
    await dispatch('loadCollections')
    await dispatch('loadCurrencies')
    await dispatch('loadItemTypes')
    await dispatch('loadQualities')
    await dispatch('loadRarities')
    await dispatch('loadTransactionTypes')
  },
  async loadCollections({ state, commit }) {
    try {
      if (state.collections && state.collections.length === 0) {
        const response = await API.collections.getAllCollections()
        if (response.status.isSuccessful) {
          commit('setCollections', response.result)
        } else Vue.error(response.status.message, 'Ошибка')
      }
    } catch (exception) {
      Vue.error('Ошибка при получении коллекций', 'Ошибка')
    }
  },
  async loadCurrencies({ state, commit }) {
    try {
      if (state.currencies && state.currencies.length === 0) {
        const response = await API.currencies.getAllCurrencies()
        if (response.status.isSuccessful) {
          commit('setCurrencies', response.result)
        } else Vue.error(response.status.message, 'Ошибка')
      }
    } catch (exception) {
      Vue.error('Ошибка при получении валют', 'Ошибка')
    }
  },
  async loadItemTypes({ state, commit }) {
    try {
      if (state.itemTypes && state.itemTypes.length === 0) {
        const response = await API.itemTypes.getAllItemTypes()
        if (response.status.isSuccessful) {
          commit('setItemTypes', response.result)
        } else Vue.error(response.status.message, 'Ошибка')
      }
    } catch (exception) {
      Vue.error('Ошибка при получении типов скинов', 'Ошибка')
    }
  },
  async loadQualities({ state, commit }) {
    try {
      if (state.qualities && state.qualities.length === 0) {
        const response = await API.qualities.getAllQualities()
        if (response.status.isSuccessful) {
          commit('setQualities', response.result)
        } else Vue.error(response.status.message, 'Ошибка')
      }
    } catch (exception) {
      Vue.error('Ошибка при получении качеств скинов', 'Ошибка')
    }
  },
  async loadRarities({ state, commit }) {
    try {
      if (state.rarities && state.rarities.length === 0) {
        const response = await API.rarities.getAllRarities()
        if (response.status.isSuccessful) {
          commit('setRarities', response.result)
        } else Vue.error(response.status.message, 'Ошибка')
      }
    } catch (exception) {
      Vue.error('Ошибка при получении редкостей скинов', 'Ошибка')
    }
  },
  async loadTransactionTypes({ state, commit }) {
    try {
      if (state.transactionTypes && state.transactionTypes.length === 0) {
        const response = await API.transactionTypes.getAllTransactionTypes()
        if (response.status.isSuccessful) {
          commit('setTransactionTypes', response.result)
        } else Vue.error(response.status.message, 'Ошибка')
      }
    } catch (exception) {
      Vue.error('Ошибка при получении типов транзакций', 'Ошибка')
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
