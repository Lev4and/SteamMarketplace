import { resourceAPIClient } from '@/api/axios'
import { responseGet } from '@/services/utils/responseUtils'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export const getAllTransactionTypes = async () => {
  return new BaseResponseModel(await responseGet(resourceAPIClient, '/api/transactionTypes/all/'))
}
