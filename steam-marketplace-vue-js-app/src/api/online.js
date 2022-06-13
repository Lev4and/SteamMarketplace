import { ResourceAPISignalRClient } from './signalR'

const path = 'store/online'
const methods = ['OnlineChanged', 'OnlineUsers', 'UserConnected', 'UserDisconnected']

export function OnlineHubClient() {
  ResourceAPISignalRClient.apply(this, [{ path: path, methods: methods }])
}
