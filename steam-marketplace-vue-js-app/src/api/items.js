import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export class ItemsClient extends ResourceAPIClient {
  constructor() {
    super('items')
  }

  async getGroupedItems(filters) {
    return new BaseResponseModel(await this.post('groupedItems', filters))
  }

  async getItemByFullName(fullName) {
    return new BaseResponseModel(await this.get(`${fullName}`))
  }

  async getExtendedInfo(fullName) {
    return new BaseResponseModel(await this.get(`${fullName}/extendedInfo`))
  }

  async getAddedDynamics(fullName) {
    return new BaseResponseModel(await this.get(`${fullName}/addedItemsDynamics`))
  }
}
