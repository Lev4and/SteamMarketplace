import { ResourceAPIClient } from '@/api/axios'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export function ItemsClient() {
  ResourceAPIClient.apply(this, [{ path: 'items' }])
  this.getGroupedItems = async (filters) => {
    return new BaseResponseModel(await this.postAuth('groupedItems', filters))
  }
  this.getItemByFullName = async (fullName) => {
    return new BaseResponseModel(await this.getAuth(`${fullName}`))
  }
  this.getExtendedInfo = async (fullName) => {
    return new BaseResponseModel(await this.getAuth(`${fullName}/extendedInfo`))
  }
  this.getAddedDynamics = async (fullName) => {
    return new BaseResponseModel(await this.getAuth(`${fullName}/addedItemsDynamics`))
  }
}
