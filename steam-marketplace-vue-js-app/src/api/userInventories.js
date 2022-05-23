import store from '@/store'
import { resourceAPIClient } from '@/api/axios'
import { responsePost } from '@/services/utils/responseUtils'
/* import { PagedResponseModel } from '@/services/utils/modelsUtils' */

export const getMyInventory = async (filters) => {
  const config = {
    headers: { 'Authorization': `Bearer ${await store.dispatch('auth/tryGetAccessToken')}` },
  }
  return await responsePost(resourceAPIClient, '/api/userInventories/inventory', filters, config)
}
