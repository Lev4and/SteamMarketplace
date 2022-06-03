import { resourceAPIClient } from '@/api/axios'
import { responseGet } from '@/services/utils/responseUtils'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export const getExchangeRates = async (currencyId) => {
  return new BaseResponseModel(await responseGet(resourceAPIClient, `/api/exchangeRates/${currencyId}/`))
}
