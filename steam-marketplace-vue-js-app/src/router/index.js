import Vue from 'vue'
import VueRouter from 'vue-router'
import { some as _some } from 'lodash'
import store from '@/store'
import Home from '@/views/Home'
import Login from '@/views/Login'
import CSMoney from '@/views/CSMoney'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    meta: { 
      title: 'Главная',
      authRequired: true, 
    },
    component: Home,
  },
  {
    path: '/login',
    name: 'Login',
    meta: {
      title: 'Авторизация',
    },
    component: Login
  },
  {
    path: '/cSMoney',
    name: 'CSMoney',
    meta: {
      authRequired: true,
      title: 'Магазин CS.Money',
    },
    component: CSMoney
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
