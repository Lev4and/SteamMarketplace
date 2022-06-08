import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export function UsersClient() {
  ResourceAPIClient.apply(this, [{ path: 'users' }])
  this.getCurrentUser = async () => {
    return new BaseResponseModel(await this.getAuth('currentUser'))
  }
}
