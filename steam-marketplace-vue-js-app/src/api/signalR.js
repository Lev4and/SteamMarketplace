import * as signalR from '@microsoft/signalr'
import { forEach as _forEach } from 'lodash'
import { resourceAPIUrl } from './config'

export class SignalRClient {
  #path
  #domain
  #methods
  #connection
  constructor(domain, path, methods) {
    this.#path = path
    this.#domain = domain
    this.#methods = methods
    this.#connection = new signalR.HubConnectionBuilder()
      .withUrl(`${this.#domain}/${this.#path}`)
      .withAutomaticReconnect()
      .build()
  }

  get isConnected() {
    return this.#connection.state === signalR.HubConnectionState.Connected
  }

  get isDisconnected() {
    return this.#connection.state === signalR.HubConnectionState.Disconnected
  }

  get isDisconnecting() {
    return this.#connection.state === signalR.HubConnectionState.Disconnecting
  }

  async connect() {
    if (this.isDisconnected) {
      _forEach(this.#methods, (method) => {
        this.#connection.on(method, (data) => this.#handlerMessage(method, data))
      })
      await this.#connection.start()
    }
  }

  #handlerMessage(method, data) {
    document.dispatchEvent(new CustomEvent(method.toLowerCase(), { 
      bubbles: true,
      detail: { data: () => data }
    }))
  }

  async disconnect() {
    if (!this.isDisconnected && !this.isDisconnecting) {
      _forEach(this.#methods, (method) => {
        this.#connection.off(method, (data) => this.#handlerMessage(method, data))
      })
      await this.#connection.stop()
    }
  }

  async send(method, data) {
    document.dispatchEvent(new CustomEvent(method.toLowerCase(), { 
      bubbles: true,
      detail: { data: () => data }
    }))
  }
}

export class ResourceAPISignalRClient extends SignalRClient {
  constructor(path, methods) {
    super(resourceAPIUrl, path, methods)
  }

  async matchUser(user) {
    await this.send('MatchUser', user)
  }

  async matchGroup(group) {
    await this.send('MatchGroup', group)
  }

  async unmatchGroup(group) {
    await this.send('UnmatchGroup', group)
  }

  async sendRequestOnGetOnlineUsers() {
    await this.send('GetUsers', null)
  }
}
