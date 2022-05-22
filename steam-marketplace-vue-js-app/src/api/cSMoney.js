import store from '@/store'
import { resourceAPIClient } from '@/api/axios'
import { responseGet } from '@/services/utils/responseUtils'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export const getInventory = async (limit, offset) => {
  const config = {
    params: {
      limit: limit,
      offset: offset,
    },
    headers: { 'Authorization': `Bearer ${await store.dispatch('auth/tryGetAccessToken')}` },
  }
  return new BaseResponseModel(await responseGet(resourceAPIClient, '/api/csMoney/store/inventory', config))
}