import { ResourceAPIClient } from "@/api/axios"
import { BaseResponseModel } from "@/services/utils/modelsUtils"

export class CollectionsClient extends ResourceAPIClient {
  constructor() {
    super('collections')
  }
  
  async getAllCollections() {
    return new BaseResponseModel(await this.get('all'))
  }
}
