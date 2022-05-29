import store from '@/store'
import { resourceAPIClient } from '@/api/axios'
import { responseGet, responsePost } from '@/services/utils/responseUtils'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export const getGroupedItems = async (filters) => {
  const config = {
    headers: { 'Authorization': `Bearer ${await store.dispatch('auth/tryGetAccessToken')}` },
  }
  return new BaseResponseModel(await responsePost(resourceAPIClient, '/api/items/groupedItems', filters, config))
}

export const getItemByFullName = async (fullName) => {
  const config = {
    headers: { 'Authorization': `Bearer ${await store.dispatch('auth/tryGetAccessToken')}` },
  }
  return new BaseResponseModel(await responseGet(resourceAPIClient, `/api/items/${fullName}`, config))
}