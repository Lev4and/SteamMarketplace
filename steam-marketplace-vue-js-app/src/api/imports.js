import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export function ImportsClient() {
  ResourceAPIClient.apply(this, [{ path: 'import' }])
  this.importLatestExchangeRate = async (latestExchangeRate) => {
    return new BaseResponseModel(await this.post('exchangeRate/import', latestExchangeRate))
  }
}
