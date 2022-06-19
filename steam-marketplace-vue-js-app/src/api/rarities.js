import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export function RaritiesClient() {
  ResourceAPIClient.apply(this, [{ path: 'rarities' }])
  this.getAllRarities = async () => {
    return new BaseResponseModel(await this.get('all'))
  }
}
