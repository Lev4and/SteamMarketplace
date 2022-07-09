import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export class CSMoneyClient extends ResourceAPIClient {
  constructor() {
    super('csMoney')
  }

  async getInventory(limit, offset, minPrice = 0, maxPrice = 30000) {
    const params = { limit: limit, offset: offset, minPrice: minPrice, maxPrice: maxPrice }
    return new BaseResponseModel(await this.get('store/inventory', params))
  }
}
