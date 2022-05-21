import { cloneDeep as _cloneDeep } from 'lodash'

export function Status(status) {
  this.code = status.code
  this.name = status.name
  this.message = status.message
  this.isSuccessful = () => this.code === 200
}

export function Pagination(pageInfo) {
  this.page = pageInfo.page
  this.pages = pageInfo.items
  this.limit = pageInfo.limit
  this.pagesCount = pageInfo.pagesCount
  this.totalItems = pageInfo.totalItems
}

export function Paged(result) {
  this.items = result.items
  this.pageInfo = new Pagination(this.pageInfo)
}

export function BaseResponseModel(response) {
  this.result = response?.result
  this.status = new Status(response.status)
}

export function DecorationBaseResponseModel(primary) {
  this.primary = _cloneDeep(primary)
  BaseResponseModel.apply(this, [primary])
}

export function PagedResponseModel(response) {
  DecorationBaseResponseModel.apply(this, response)
  this.result = new Paged(response?.result)
}

export function Login(login, password) {
  this.password = password
  this.emailOrLogin = login
}