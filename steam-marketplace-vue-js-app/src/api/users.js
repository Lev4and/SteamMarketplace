import { ResourceAPISignalRClient } from './signalR'
import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

const path = 'store/users'
const methods = ['UserRegistered']

export function UsersClient() {
  ResourceAPIClient.apply(this, [{ path: 'users' }])
  this.getCurrentUser = async () => {
    return new BaseResponseModel(await this.getAuth('currentUser'))
  }
}

export function UsersHubClient() {
  ResourceAPISignalRClient.apply(this, [{ path: path, methods: methods }])
}
