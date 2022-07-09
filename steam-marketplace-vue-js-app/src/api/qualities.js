import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export class QualitiesClient extends ResourceAPIClient {
  constructor() {
    super('qualities')
  }
  
  async getAllQualities() {
    return new BaseResponseModel(await this.get('all'))
  }
}
