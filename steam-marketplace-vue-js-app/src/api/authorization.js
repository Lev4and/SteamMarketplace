import { AuthorizationAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export class AuthorizationClient extends AuthorizationAPIClient {
  constructor() {
    super('authorization')
  }

  async login(login) {
    return new BaseResponseModel(await this.post('login', login))
  }
}
