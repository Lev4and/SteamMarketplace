import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export function TransactionTypesClient() {
  ResourceAPIClient.apply(this, [{ path: 'transactionTypes' }])
  this.getAllTransactionTypes = async () => {
    return new BaseResponseModel(await this.get('all'))
  }
}
