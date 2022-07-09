import moment from 'moment'
import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export class CbrClient extends ResourceAPIClient {
  constructor() {
    super('cBR')
  }

  async getLatestExchangeRates() {
    const params = { date: moment().subtract(1, 'days').format('YYYY-MM-DD') }
    return new BaseResponseModel(await this.get('exchangeRates/daily', params))
  }
}
