import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export class RaritiesClient extends ResourceAPIClient {
  constructor() {
    super('rarities')
  }

  async getAllRarities() {
    return new BaseResponseModel(await this.get('all'))
  }
}
