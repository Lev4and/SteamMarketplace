import { ResourceAPISignalRClient } from './signalR'
import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

const path = 'store/purchases'
const methods = ['ItemPurchased', 'CertainItemPurchased']

export function PurchasesClient() {
  ResourceAPIClient.apply(this, [{ path: 'purchases' }])
  this.getMyPurchases = async (filters) => {
    return new BaseResponseModel(await this.postAuth('myPurchases', filters))
  }
}

export function PurchasesHubClient() {
  ResourceAPISignalRClient.apply(this, [{ path: path, methods: methods }])
}
