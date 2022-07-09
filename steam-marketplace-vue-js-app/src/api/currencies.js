import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export class CurrenciesClient extends ResourceAPIClient {
  constructor() {
    super('currencies')
  }
  
  async getAllCurrencies() {
    return new BaseResponseModel(await this.get('all'))
  }
}
