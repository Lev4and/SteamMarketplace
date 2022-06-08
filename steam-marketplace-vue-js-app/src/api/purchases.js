import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export function PurchasesClient() {
  ResourceAPIClient.apply(this, [{ path: 'purchases' }])
  this.getMyPurchases = async (filters) => {
    return new BaseResponseModel(await this.postAuth('myPurchases', filters))
  }
}
