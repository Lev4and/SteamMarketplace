import { authorizationAPIClient } from '@/api/axios'
import { responsePost } from '@/services/utils/responseUtils'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export const login = async (login) => {
  return new BaseResponseModel(await responsePost(authorizationAPIClient, '/api/authorization/login/', login))
}
