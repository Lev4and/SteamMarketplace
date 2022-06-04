import { ResourceAPISignalRClient } from './signalR'

const path = 'store/online'
const methods = ['OnlineChanged', 'OnlineUsers', 'UserConnected', 'UserDisconnected']

export function OnlineHubClient() {
  ResourceAPISignalRClient.apply(this, [{ path: path, methods: methods }])
  this.matchUser = async (user) => await this.send('MatchUser', user)
  this.sendRequestOnGetOnlineUsers = async () => await this.send('GetUsers', null)
}
