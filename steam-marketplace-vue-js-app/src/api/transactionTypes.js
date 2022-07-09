import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export class TransactionTypesClient extends ResourceAPIClient {
  constructor() {
    super('transactionTypes')
  }

  async getAllTransactionTypes() {
    return new BaseResponseModel(await this.get('all'))
  }
}
