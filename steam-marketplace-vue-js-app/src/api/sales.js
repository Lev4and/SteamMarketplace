import { ResourceAPISignalRClient } from './signalR'
import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

const path = 'store/sales'
const methods = ['ItemExposedOnSale', 'CertainItemExposedOnSale', 'SellerExposedOnSale', 'SaleClosed', 'CertainItemSaleClosed']

export class SalesClient extends ResourceAPIClient {
  constructor() {
    super('sales')
  }

  async getMySales(filters) {
    return new BaseResponseModel(await this.post('mySales', filters))
  }

  async getCountActiveSales() {
    return new BaseResponseModel(await this.get('mySales/active/count'))
  }
  
  async getSalesItem(filters) {
    return new BaseResponseModel(await this.post('item', filters))
  }

  async getPricesDynamicsItem(fullName) {
    const params = { name: fullName }
    return new BaseResponseModel(await this.get('pricesDynamics', params))
  }

  async getExposedSalesDynamics(fullName) {
    const params = { name: fullName }
    return new BaseResponseModel(await this.get('exposedSalesDynamics', params))
  }
}

export class SalesHubClient extends ResourceAPISignalRClient {
  constructor() {
    super(path, methods)
  }
}
