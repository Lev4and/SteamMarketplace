import { ResourceAPIClient } from "@/api/axios"
import { BaseResponseModel } from "@/services/utils/modelsUtils"

export function CollectionsClient() {
  ResourceAPIClient.apply(this, [{ path: 'collections' }])
  this.getAllCollections = async () => {
    return new BaseResponseModel(await this.get('all'))
  }
}
