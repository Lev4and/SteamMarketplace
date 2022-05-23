import store from '@/store'
import { resourceAPIClient } from '@/api/axios'
import { responsePost } from '@/services/utils/responseUtils'

export const getMyInventory = async (filters) => {
  const config = {
    headers: { 'Authorization': `Bearer ${await store.dispatch('auth/tryGetAccessToken')}` },
  }
  return await responsePost(resourceAPIClient, '/api/userInventories/inventory', filters, config)
}
