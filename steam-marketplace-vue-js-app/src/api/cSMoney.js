import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export function CSMoneyClient() {
  ResourceAPIClient.apply(this, [{ path: 'csMoney' }])
  this.getInventory = async (limit, offset, minPrice = 0, maxPrice = 30000) => {
    const params = { limit: limit, offset: offset, minPrice: minPrice, maxPrice: maxPrice }
    return new BaseResponseModel(await this.getAuth('store/inventory', params))
  }
}
