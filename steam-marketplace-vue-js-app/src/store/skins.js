import { Guid } from 'js-guid'
import { map as _map } from 'lodash'
import API from '@/api'

const state = {
  foundItems: 0,
  importedItems: 0,
  importing: false,
}

const mutations = {

}

const getters = {
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
    state.foundItems = 0
    state.importing = true
    state.importedItems = 0
    for (let i = 0; i < 30000; i += 1) {
      for (let j = 0; j < 5000; j += 50) {
        if (state.importing) {
          try {
            const response = await API.cSMoney.getInventory(50, j, i, i + 1)
            if (response?.status?.isSuccessful) {
              if (response.result.items.length === 0) break
              await Promise.all(_map(response.result.items, (item) => {
                dispatch('saveItem', item)
              }))
            } else break
          } catch (exception) {
            console.warn(exception.message)
          }
        }
      }
    }
    state.importing = false
  },
  cancelImport({ state }) {
    state.importing = false
  },
  async saveItem({ state }, item) {
    state.foundItems += 1
    const response = await API.imports.importItem(item)
    if (response.status.isSuccessful) {
      if (Guid.EMPTY !== response.result) {
        state.importedItems += item.stackSize || 1 
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
