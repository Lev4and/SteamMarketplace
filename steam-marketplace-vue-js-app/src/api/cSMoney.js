import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export function CSMoneyClient() {
  ResourceAPIClient.apply(this, [{ path: 'csMoney' }])
  this.getInventory = async (limit, offset) => {
    const params = { limit: limit, offset: offset }
    return new BaseResponseModel(await this.getAuth('store/inventory', params))
  }
}
