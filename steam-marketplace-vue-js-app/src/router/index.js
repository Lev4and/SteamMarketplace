import Vue from 'vue'
import VueRouter from 'vue-router'
import { some as _some } from 'lodash'
import store from '@/store'
import Home from '@/views/Home'
import Sales from '@/views/Sales'
import Store from '@/views/Store'
import Login from '@/views/Login'
import Logout from '@/views/Logout'
import Online from '@/views/Online'
import CSMoney from '@/views/CSMoney'
import Account from '@/views/Account'
import Purchases from '@/views/Purchases'
import StoreItem from '@/views/StoreItem'
import MyInventory from '@/views/MyInventory'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home,
    meta: { 
      title: 'Главная',
      authRequired: true, 
    },
  },
  {
    path: '/login',
    name: 'Login',
    component: Login,
    meta: {
      title: 'Авторизация',
    },
  },
  {
    path: '/logout',
    name: 'Logout',
    component: Logout,
    meta: {
      title: 'Выход',
      authRequired: true,
    },
  },
  {
    path: '/online',
    name: 'Online',
    component: Online,
    meta: {
      title: 'Онлайн',
      authRequired: true,
    },
  },
  {
    path: '/cSMoney',
    name: 'CSMoney',
    component: CSMoney,
    meta: {
      authRequired: true,
      title: 'Магазин CS.Money',
    },
  },
  {
    path: '/store',
    name: 'Store',
    component: Store,
    meta: {
      authRequired: true,
      title: 'Магазин',
    },
  },
  {
    path: '/store/:fullName',
    name: 'StoreItem',
    component: StoreItem,
    meta: {
      authRequired: true,
      title: 'Скин',
    },
  },
  {
    path: '/account',
    name: 'Account',
    component: Account,
    meta: {
      authRequired: true,
      title: 'Аккаунт',
    },
    children: [
      {
        path: 'myInventory/',
        name: 'MyInventory',
        component: MyInventory,
        meta: {
          authRequired: true,
          title: 'Мой инвентарь',
        },
      },
      {
        path: 'myPurchases/',
        name: 'MyPurchases',
        component: Purchases,
        meta: {
          authRequired: true,
          title: 'Мои покупки',
        },
      },
      {
        path: 'mySales/',
        name: 'MySales',
        component: Sales,
        meta: {
          authRequired: true,
          title: 'Мои продажи',
        },
      },
    ],
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

router.beforeEach((to, from, next) => {
  const authRequired = _some(to.matched, (route) => route.meta.authRequired)
  if (!authRequired) return next()
  if (store.getters['auth/isAuthorized']) return next()
  else next({ name: 'Login', query: { redirectFrom: to.fullPath } })
})

export default router
