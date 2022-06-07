import moment from 'moment'
import { resourceAPIClient } from '@/api/axios'
import { responseGet } from '@/services/utils/responseUtils'
import { BaseResponseModel } from '@/services/utils/modelsUtils'

export const getLatestExchangeRates = async () => {
  const config = {
    params: {
      date: moment().subtract(1, 'days').format('YYYY-MM-DD')
    },
  }
  return new BaseResponseModel(await responseGet(resourceAPIClient, `/api/cBR/exchangeRates/daily`, config))
}
