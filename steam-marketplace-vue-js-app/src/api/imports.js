import { ResourceAPISignalRClient } from './signalR'
import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

const path = 'store/items/import'
const methods = ['Imported', 'ItemImported']

export class ImportsClient extends ResourceAPIClient {
  constructor() {
    super('import')
  }

  async importLatestExchangeRate(latestExchangeRate) {
    return new BaseResponseModel(await this.post('exchangeRate/import', latestExchangeRate))
  }

  async importItem(item) {
    return new BaseResponseModel(await this.post('item/import', item))
  }
}

export class ImportHubClient extends ResourceAPISignalRClient {
  constructor() {
    super(path, methods)
  }
}
