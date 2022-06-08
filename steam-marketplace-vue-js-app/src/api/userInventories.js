import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export function UserInventoriesClient() {
  ResourceAPIClient.apply(this, [{ path: 'userInventories' }])
  this.getMyInventory = async (filters) => {
    return new BaseResponseModel(await this.postAuth('inventory', filters))
  }
}
