import Vue from 'vue'
import App from './App'
import router from './router'
// import store from './store'


import Antd from 'ant-design-vue'
import 'ant-design-vue/dist/antd.css'

import VueApexCharts from 'vue-apexcharts'

Vue.config.productionTip = false
Vue.use(Antd)
Vue.use(VueApexCharts)

new Vue({
  router,
  // store,
  render: h => h(App)
}).$mount('#app')
