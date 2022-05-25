import store from '@/store'
import { resourceAPIClient } from '@/api/axios'
import { responsePost } from '@/services/utils/responseUtils'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export const getMySales = async (filters) => {
  const config = {
    headers: { 'Authorization': `Bearer ${await store.dispatch('auth/tryGetAccessToken')}` },
  }
  return new BaseResponseModel(await responsePost(resourceAPIClient, '/api/sales/mySales', filters, config))
}