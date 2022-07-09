import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export class ExchangeRatesClient extends ResourceAPIClient {
  constructor() {
    super('exchangeRates')
  }

  async getExchangeRates(currencyId) {
    return new BaseResponseModel(await this.get(`${currencyId}`))
  }
}
