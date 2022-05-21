import { success, warning, error } from '@/services/utils/notification'

const AntNotify = {
  install: function (Vue) {
    Vue.success = function (message, title = '', duration = 5) {
      success(message, title || 'Успешно', duration)
    }
    Vue.warning = function (message, title = '', duration = 5) {
      warning(message, title || 'Предупреждение', duration)
    }
    Vue.error = function (message, title = '', duration = 5) {
      error(message, title || 'Ошибка', duration)
    }

    Vue.prototype.$success = function (message, title = '', duration = 5) {
      success(message, title || 'Успешно', duration)
    }
    Vue.prototype.$warning = function (message, title = '', duration = 5) {
      warning(message, title || 'Предупреждение', duration)
    }
    Vue.prototype.$error = function (message, title = '', duration = 5) {
      error(message, title || 'Ошибка', duration)
    }
  },
}

export default AntNotify