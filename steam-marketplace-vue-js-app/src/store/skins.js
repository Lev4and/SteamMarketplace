import { Guid } from 'js-guid'
import { map as _map } from 'lodash'
import API from '@/api'

const state = {
  limit: 50,
  stepPrice: 1,
  foundItems: 0,
  importedItems: 0,
  importing: false,
}

const mutations = {

}

const getters = {
  limit(state) {
    return state.limit
  },
  stepPrice(state) {
    return state.stepPrice
  },
  importing(state) {
    return state.importing
  },
  foundItems(state) {
    return state.foundItems
  },
  importedItems(state) {
    return state.importedItems
  },
}

const actions = {
  async import({ state, dispatch }) {
    dispatch('resetData')
    state.importing = true
    for (let priceUsd = 0; priceUsd < 30000 && state.importing; priceUsd += state.stepPrice) {
      for (let offset = 0; offset < 5000 && state.importing; offset += state.limit) {
        const items = await dispatch('loadItems', { offset, priceUsd })
        if (items.length === 0) break
        await dispatch('saveItems', items)
      }
      dispatch('recalculateStepPrice', priceUsd)
    }
    state.importing = false
  },
  resetData({ state }) {
    state.stepPrice = 1
    state.foundItems = 0
    state.importedItems = 0
  },
  recalculateStepPrice({ state }, priceUsd) {
    if (priceUsd < 25) state.stepPrice = 1
    else if (priceUsd >= 25 && priceUsd <= 82) state.stepPrice = 3
    else if (priceUsd >= 85 && priceUsd <= 495) state.stepPrice = 10
    else if (priceUsd >= 505 && priceUsd <= 1905) state.stepPrice = 100
    else state.stepPrice = 1000
  },
  async loadItems({ state }, params) {
    const { offset, priceUsd } = params
    const response = await API.cSMoney.getInventory(state.limit, offset, priceUsd, priceUsd + state.stepPrice)
    return response?.result?.items || []
  },
  async saveItems({ dispatch }, items) {
    await Promise.all(_map(items, (item) => {
      dispatch('saveItem', item)
    }))
  },
  async saveItem({ state }, item) {
    state.foundItems += item.stackSize || 1
    const response = await API.imports.importItem(item)
    if (response.status.isSuccessful) {
      if (Guid.EMPTY !== response.result) {
        state.importedItems += item.stackSize || 1 
      }
    }
  },
  cancelImport({ state }) {
    state.importing = false
  },
}

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions,
}
