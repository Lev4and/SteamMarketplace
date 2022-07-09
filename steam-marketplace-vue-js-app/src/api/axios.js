import axios from 'axios'
import store from '@/store'
import { set as _set } from 'lodash'
import { resourceAPIUrl, authorizationAPIUrl } from './config'

export class AxiosClient {
  #path
  #domain
  #client
  constructor(domain, path) {
    this.#path = path
    this.#domain = domain
    this.#client = axios.create({
      baseURL: `${this.#domain}/${this.#path}/`,
      withCredentials: false,
      headers: {
        'Content-Type': 'application/json',
      },
    })
  }

  async get(url, params = {}, headers = {}) {
    const config = {
      params: params,
      headers: headers,
    }
    return await this.#client.get(url, config).then((response) => { return response.data })
      .catch((error) => { return error.response.data })
  }

  async post(url, body, headers = {}) {
    const config = {
      headers: headers,
    }
    return await this.#client.post(url, body, config).then((response) => { return response.data })
      .catch((error) => { return error.response.data })
  }
}

export class AuthorizationClient extends AxiosClient {
  constructor(domain, path) {
    super(domain, path)
  }

  async #authorize(headers = {}) {
    _set(headers, 'Authorization', `Bearer ${await store.dispatch('auth/tryGetAccessToken')}`)
  }

  async get(url, params = {}, headers = {}) {
    await this.#authorize(headers)
    return await super.get(url, params, headers)
  }

  async post(url, body, headers = {}) {
    await this.#authorize(headers)
    return await super.post(url, body, headers)
  }
}

export class AuthorizationAPIClient extends AxiosClient {
  constructor(path) {
    super(authorizationAPIUrl, `api/${path}`)
  }
}

export class ResourceAPIClient extends AuthorizationClient {
  constructor(path) {
    super(resourceAPIUrl, `api/${path}`)
  }
}
