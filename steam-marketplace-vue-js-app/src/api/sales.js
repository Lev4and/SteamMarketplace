import { ResourceAPISignalRClient } from './signalR'
import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

const path = 'store/sales'
const methods = ['ItemExposedOnSale', 'CertainItemExposedOnSale', 'SellerExposedOnSale', 'SaleClosed', 'CertainItemSaleClosed']

export function SalesClient() {
  ResourceAPIClient.apply(this, [{ path: 'sales' }])
  this.getMySales = async (filters) => {
    return new BaseResponseModel(await this.postAuth('mySales', filters))
  }
  this.getCountActiveSales = async () => {
    return new BaseResponseModel(await this.getAuth('mySales/active/count'))
  }
  this.getSalesItem = async (filters) => {
    return new BaseResponseModel(await this.postAuth('item', filters))
  }
  this.getPricesDynamicsItem = async (fullName) => {
    const params = { name: fullName }
    return new BaseResponseModel(await this.getAuth('pricesDynamics', params))
  }
  this.getExposedSalesDynamics = async (fullName) => {
    const params = { name: fullName }
    return new BaseResponseModel(await this.getAuth('exposedSalesDynamics', params))
  }
}

export function SalesHubClient() {
  ResourceAPISignalRClient.apply(this, [{ path: path, methods: methods }])
}
