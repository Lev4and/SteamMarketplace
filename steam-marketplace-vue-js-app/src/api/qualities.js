import { resourceAPIClient } from '@/api/axios'
import { responseGet } from '@/services/utils/responseUtils'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export const getAllQualities = async () => {
  return new BaseResponseModel(await responseGet(resourceAPIClient, '/api/qualities/all/'))
}
