import * as signalR from '@microsoft/signalr'
import { forEach as _forEach } from 'lodash'
import { resourceAPIUrl } from './config'

export function SignalRClient(domain, path, methods) {
  this.connection = new signalR.HubConnectionBuilder()
    .withUrl(`${domain}/${path}`)
    .withAutomaticReconnect()
    .build()
  this.isConnected = () => { return this.connection.state === signalR.HubConnectionState.Connected }
  this.isDisconnected = () => { return this.connection.state === signalR.HubConnectionState.Disconnected }
  this.isDisconnecting = () => { return this.connection.state === signalR.HubConnectionState.Disconnecting }
  this.connect = async () => {
    if (this.isDisconnected()) {
      _forEach(methods, (method) => {
        this.connection.on(method, (data) => this.handlerMessage(method, data))
      })
      await this.connection.start()
    }
  }
  this.disconnect = async () => {
    if (!this.isDisconnected() && !this.isDisconnecting()) {
      _forEach(methods, (method) => {
        this.connection.off(method, (data) => this.handlerMessage(method, data))
      })
      await this.connection.stop()
    }
  }
  this.send = async (method, data) => {
    if (this.isConnected()) {
      if (data) await this.connection.invoke(method, data)
      else await this.connection.invoke(method)
    }
  }
  this.handlerMessage = (method, data) => {
    document.dispatchEvent(new CustomEvent(method.toLowerCase(), { 
      bubbles: true,
      detail: { data: () => data }
    }))
  }
}

export function DecorationSignalRClient(other) {
  this.other = other
  SignalRClient.apply(this, [other.domain, other.path, other.methods])
}

export function ResourceAPISignalRClient(other) {
  DecorationSignalRClient.apply(this, [{ domain: resourceAPIUrl, path: other.path, methods: other.methods}])
  this.matchUser = async (user) => await this.send('MatchUser', user)
  this.matchGroup = async (group) => await this.send('MatchGroup', group)
  this.unmatchGroup = async (group) => await this.send('UnmatchGroup', group)
  this.sendRequestOnGetOnlineUsers = async () => await this.send('GetUsers', null)
}
