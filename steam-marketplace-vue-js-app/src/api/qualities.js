import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export function QualitiesClient() {
  ResourceAPIClient.apply(this, [{ path: 'qualities' }])
  this.getAllQualities = async () => {
    return new BaseResponseModel(await this.get('all'))
  }
}
