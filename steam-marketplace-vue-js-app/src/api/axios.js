import axios from 'axios'
import store from '@/store'
import { set as _set } from 'lodash'
import { resourceAPIUrl, authorizationAPIUrl } from './config'

export function AxiosClient(domain, path) {
  this.client = axios.create({
    baseURL: `${domain}/${path}/`,
    withCredentials: false,
    headers: {
      'Content-Type': 'application/json',
    },
  })
  this.get = async (url, params = {}, headers = {}) => {
    const config = {
      params: params,
      headers: headers,
    }
    return (await this.client.get(url, config).catch((error) => { return error.response.data })).data
  }
  this.post = async (url, body, headers = {}) => {
    const config = {
      headers: headers,
    }
    return (await this.client.post(url, body, config).catch((error) => { return error.response.data })).data
  }
}

export function DecorationAxiosClient(other) {
  this.other = other
  AxiosClient.apply(this, [other.url, other.path])
}

export function AuthorizationClient(url, path) {
  DecorationAxiosClient.apply(this, [{ url: url, path: path }])
  this.authorize = async (headers = {}) => {
    _set(headers, 'Authorization', `Bearer ${await store.dispatch('auth/tryGetAccessToken')}`)
  }
  this.getAuth = async (url, params = {}, headers = {}) => {
    await this.authorize(headers)
    return await this.get(url, params, headers)
  }
  this.postAuth = async (url, body, headers = {}) => {
    await this.authorize(headers)
    return await this.post(url, body, headers)
  }
}

export function DecorationAuthorizationClient(other) {
  this.other = other
  AuthorizationClient.apply(this, [other.url, other.path])
}

export function AuthorizationAPIClient(other) {
  DecorationAxiosClient.apply(this, [{ url: authorizationAPIUrl, path: `/api/${other.path}` }])
}

export function ResourceAPIClient(other) {
  DecorationAuthorizationClient.apply(this, [{ url: resourceAPIUrl, path: `/api/${other.path}` }])
}
