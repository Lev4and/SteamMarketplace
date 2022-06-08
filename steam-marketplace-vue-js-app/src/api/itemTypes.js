import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export function ItemTypes() {
  ResourceAPIClient.apply(this, [{ path: 'itemTypes' }])
  this.getAllItemTypes = async () => {
    return new BaseResponseModel(await this.get('all'))
  }
}
