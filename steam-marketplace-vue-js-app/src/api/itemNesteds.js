import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export class ItemNestedsClient extends ResourceAPIClient {
  constructor() {
    super('itemNesteds')
  }

  async getItemNesteds(ids) {
    return new BaseResponseModel(await this.post('byItemIds', ids))
  }
}
