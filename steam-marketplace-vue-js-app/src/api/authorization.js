import { AuthorizationAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export function AuthorizationClient() {
  AuthorizationAPIClient.apply(this, [{ path: 'authorization' }])
  this.login = async (login) => {
    return new BaseResponseModel(await this.post('login', login))
  }
}
