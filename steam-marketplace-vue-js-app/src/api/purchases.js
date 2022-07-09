import { ResourceAPISignalRClient } from './signalR'
import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

const path = 'store/purchases'
const methods = ['ItemPurchased', 'CertainItemPurchased']

export class PurchasesClient extends ResourceAPIClient {
  constructor() {
    super('purchases')
  }

  async getMyPurchases(filters) {
    return new BaseResponseModel(await this.post('myPurchases', filters))
  }
}

export class PurchasesHubClient extends ResourceAPISignalRClient {
  constructor() {
    super(path, methods)
  }
}
