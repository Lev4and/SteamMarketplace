import { ResourceAPISignalRClient } from './signalR'

const path = 'store/online'
const methods = ['OnlineChanged', 'OnlineUsers', 'UserConnected', 'UserDisconnected']

export class OnlineHubClient extends ResourceAPISignalRClient {
  constructor() {
    super(path, methods)
  }
}
