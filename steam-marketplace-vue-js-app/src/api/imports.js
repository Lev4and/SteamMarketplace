import { ResourceAPISignalRClient } from './signalR'
import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

const path = 'store/items/import'
const methods = ['Imported', 'ItemImported']

export function ImportsClient() {
  ResourceAPIClient.apply(this, [{ path: 'import' }])
  this.importLatestExchangeRate = async (latestExchangeRate) => {
    return new BaseResponseModel(await this.post('exchangeRate/import', latestExchangeRate))
  }
  this.importItem = async (item) => {
    return new BaseResponseModel(await this.postAuth('item/import', item))
  }
}

export function ImportHubClient() {
  ResourceAPISignalRClient.apply(this, [{ path: path, methods: methods }])
}
