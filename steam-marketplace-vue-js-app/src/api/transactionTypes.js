import store from '@/store'
import { resourceAPIClient } from '@/api/axios'
import { responseGet } from '@/services/utils/responseUtils'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export const getAllTransactionTypes = async () => {
  const config = { headers: { 'Authorization': `Bearer ${await store.dispatch('auth/tryGetAccessToken')}` } }
  return new BaseResponseModel(await responseGet(resourceAPIClient, '/api/transactionTypes/all/', config))
}