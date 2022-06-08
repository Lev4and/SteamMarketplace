import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export function CurrenciesClient() {
  ResourceAPIClient.apply(this, [{ path: 'currencies' }])
  this.getAllCurrencies = async () => {
    return new BaseResponseModel(await this.get('all'))
  }
}
