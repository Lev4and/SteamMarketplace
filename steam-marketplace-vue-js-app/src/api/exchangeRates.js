import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export function ExchangeRatesClient() {
  ResourceAPIClient.apply(this, [{ path: 'exchangeRates' }])
  this.getExchangeRates = async (currencyId) => {
    return new BaseResponseModel(await this.get(`${currencyId}`))
  }
}
