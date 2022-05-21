import { resourceAPIClient } from '@/api/axios'
import { responseGet } from '@/services/utils/responseUtils'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export const getLatestExchangeRates = async () => {
  return new BaseResponseModel(await responseGet(resourceAPIClient, '/api/cBR/exchangeRates/latest'))
}
