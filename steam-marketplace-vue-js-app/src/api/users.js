import { ResourceAPISignalRClient } from './signalR'
import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

const path = 'store/users'
const methods = ['UserRegistered']

export class UsersClient extends ResourceAPIClient {
  constructor() {
    super('users')
  }

  async getCurrentUser() {
    return new BaseResponseModel(await this.get('currentUser'))
  }
}

export class UsersHubClient extends ResourceAPISignalRClient {
  constructor() {
    super(path, methods)
  }
}
