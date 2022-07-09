import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export class ItemTypes extends ResourceAPIClient {
  constructor() {
    super('itemTypes')
  }

  async getAllItemTypes() {
    return new BaseResponseModel(await this.get('all'))
  }
}
