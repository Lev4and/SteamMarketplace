import { CRUDResourceAPIClient } from "@/api/axios"
import { BaseResponseModel } from "@/services/utils/modelsUtils"

export function CollectionsClient() {
  CRUDResourceAPIClient.apply(this, [{ path: 'collections' }])
  this.getAllCollections = async () => {
    return new BaseResponseModel(await this.get('all'))
  }
}
