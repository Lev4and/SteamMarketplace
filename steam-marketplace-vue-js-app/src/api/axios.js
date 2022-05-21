import axios from 'axios'
import { resourceAPIUrl, authorizationAPIUrl } from './config'

export const resourceAPIClient = axios.create({
  baseURL: resourceAPIUrl,
  withCredentials: false,
  headers: {
    'Content-Type': 'application/json',
  },
})

export const authorizationAPIClient = axios.create({
  baseURL: authorizationAPIUrl,
  withCredentials: false,
  headers: {
    'Content-Type': 'application/json',
  },
})
