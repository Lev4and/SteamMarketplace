import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export class UserInventoriesClient extends ResourceAPIClient {
  constructor() {
    super('userInventories')
  }

  async getMyInventory(filters) {
    return new BaseResponseModel(await this.post('inventory', filters))
  }
}
