import { forEach as _forEach, find as _find } from 'lodash'
import API from '@/api'

const state = {
  items: [],
  importedItems: [],
}

const mutations = {
  addItem(state, item) {
    if(!_find(state.items, (savedItem) => savedItem.id === item.id)) {
      state.items.push(item)
    }
  },
  addImportedItem(state, item) {
    if(!_find(state.importedItems, (importedItem) => importedItem.id === item.id)) {
      state.importedItems.push(item)
    }
  },
}

const getters = {
  items(state) {
    return state.items
  },
  importedItems(state) {
    return state.importedItems
  },
}

const actions = {
  async import({ dispatch }) {
    for(let i = 0; i < 150000; i += 50) {
      const response = await API.cSMoney.getInventory(50, i)
      if (response.status.isSuccessful()) {
        _forEach(response.result.items, (item) => {
          dispatch('saveItem', item)
        })
      }
    }
  },
  saveItem({ commit, dispatch }, item) {
    commit('addItem', item)
    commit('addImportedItem', item)
    _forEach(item.stackItems, (stackItem) => {
      dispatch('saveItem', stackItem)
    })
  },
}

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions,
}
