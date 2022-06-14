import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export function ItemNestedsClient() {
  ResourceAPIClient.apply(this, [{ path: 'itemNesteds' }])
  this.getItemNesteds = async (ids) => {
    return new BaseResponseModel(await this.postAuth('byItemIds', ids))
  }
}
