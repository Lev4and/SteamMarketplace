import Vue from 'vue'
import API from '@/api'
import moment from 'moment'
import jwt_decode from 'jwt-decode'

const state = {
  isAuthorized: false,
  currentUser: null,
  accessToken: '',
  accessTokenPayload: null,
  login: null,
}

const mutations = {
  setAccessToken(state, accessToken) {
    state.accessToken = accessToken
  },
  setAccessTokenPayload(state, payload) {
    state.accessTokenPayload = payload
  },
  setIsAuthorized(state, value) {
    state.isAuthorized = value
  },
  setLogin(state, login) {
    state.login = login
  },
}

const getters = {
  isAuthorized(state) {
    return state.isAuthorized
  },
  currentUser(state) {
    return state.currentUser
  },
  accessToken(state) {
    return state.accessToken
  },
  accessTokenPayload(state) {
    return state.accessTokenPayload
  },
}

const actions = {
  async login({ commit, dispatch }, login) {
    try {
      const response = await API.authorization.login(login)
      if (response.status.isSuccessful()) {
        commit('setAccessToken', response.result.accessToken)
        commit('setAccessTokenPayload', await dispatch('decodeAccessToken'))
        commit('setLogin', login)
        commit('setIsAuthorized', true)
        return true
      } else Vue.error(response.status.message, 'Ошибка авторизации')
    } catch (exception) {
      Vue.error(exception.message, 'Ошибка авторизации')
    }
    return false
  },
  logout({ commit }) {
    commit('setAccessToken', '')
    commit('setAccessTokenPayload', null)
    commit('setLogin', null)
    commit('setIsAuthorized', false)
  },
  decodeAccessToken({ state }) {
    return jwt_decode(state.accessToken)
  },
  isOverdue({ state }) {
    return !state.accessTokenPayload || moment.unix(state.accessTokenPayload.exp).diff(moment(), 'minutes') < 20
  },
  async tryGetAccessToken({ state, dispatch }) {
    if (state.login) {
      if (!(await dispatch('isOverdue'))) {
        return state.accessToken
      }
      await dispatch('login', state.login)
      return state.accessToken
    }
    return ''
  },
}

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions,
}
