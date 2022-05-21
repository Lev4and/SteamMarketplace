import Vue from 'vue'
import App from './App'
import router from './router'
import store from './store'


import Antd from 'ant-design-vue'
import 'ant-design-vue/dist/antd.css'
import VueApexCharts from 'vue-apexcharts'
import AntNotify from '@/services/plugins/notify'

Vue.config.productionTip = false
Vue.use(Antd)
Vue.use(VueApexCharts)
Vue.use(AntNotify)

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
