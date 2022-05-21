import Vue from 'vue'
import API from '@/api'

const state = {
  isAuthorized: false,
  currentUser: null,
  accessToken: '',
}

const mutations = {
  setAccessToken(state, accessToken) {
    state.accessToken = accessToken
  },
  setIsAuthorized(state, value) {
    state.isAuthorized = value
  },
}

const getters = {
  isAuthorized(state) {
    return state.isAuthorized
  },
  currentUser(state) {
    return state.currentUser
  },
}

const actions = {
  async login({ commit }, login) {
    try {
      const response = await API.authorization.login(login)
      if (response.status.isSuccessful()) {
        commit('setAccessToken', response.result.accessToken)
        commit('setIsAuthorized', true)
        return true
      } else Vue.error(response.status.message, 'Ошибка авторизации')
    } catch (exception) {
      Vue.error(exception.message, 'Ошибка авторизации')
    }
    return false
  },
}

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions,
}
