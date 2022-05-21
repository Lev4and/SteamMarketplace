import store from '@/store'
import { resourceAPIClient } from '@/api/axios'
import { responsePost } from '@/services/utils/responseUtils'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export const importLatestExchangeRate = async (latestExchangeRate) => {
  const config = {
    headers: { 'Authorization': `Bearer ${await store.dispatch('auth/tryGetAccessToken')}` },
    validateStatus: (status) => { return status < 500 },
  }
  return new BaseResponseModel(await responsePost(resourceAPIClient, '/api/import/exchangeRate/import', latestExchangeRate, config))
}
