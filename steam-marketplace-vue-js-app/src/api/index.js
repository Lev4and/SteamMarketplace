import * as authorization from '@/api/authorization'
import * as cbr from '@/api/cbr'
import * as collections from '@/api/collections'
import * as cSMoney from '@/api/cSMoney'
import * as currencies from '@/api/currencies'
import * as exchangeRates from '@/api/exchangeRates'
import * as imports from '@/api/imports'
import * as items from '@/api/items'
import * as itemTypes from '@/api/itemTypes'
import * as online from '@/api/online'
import * as purchases from '@/api/purchases'
import * as qualities from '@/api/qualities'
import * as rarities from '@/api/rarities'
import * as sales from '@/api/sales'
import * as transactionTypes from '@/api/transactionTypes'
import * as userInventories from '@/api/userInventories'
import * as users from '@/api/users'

const API = {
  authorization: new authorization.AuthorizationClient(),
  cbr: new cbr.CbrClient(),
  collections: new collections.CollectionsClient(),
  cSMoney: new cSMoney.CSMoneyClient(),
  currencies: new currencies.CurrenciesClient(),
  exchangeRates: new exchangeRates.ExchangeRatesClient(),
  imports: new imports.ImportsClient(),
  importOnline: new imports.ImportHubClient(),
  items: new items.ItemsClient(),
  itemTypes: new itemTypes.ItemTypes(),
  online: new online.OnlineHubClient(),
  purchases: new purchases.PurchasesClient(),
  purchasesOnline: new purchases.PurchasesHubClient(),
  qualities: new qualities.QualitiesClient(),
  rarities: new rarities.RaritiesClient(),
  sales: new sales.SalesClient(),
  salesOnline: new sales.SalesHubClient(),
  transactionTypes: new transactionTypes.TransactionTypesClient(),
  userInventories: new userInventories.UserInventoriesClient(),
  users: new users.UsersClient(),
  usersOnline: new users.UsersHubClient(),
}

export default API
