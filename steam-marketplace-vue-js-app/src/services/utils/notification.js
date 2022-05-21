import { notification } from 'ant-design-vue'

const notify = (type, message, title, duration) => {
  if (notification[type]) {
    notification[type]({
      message: title,
      description: message,
      duration: duration,
    })
  }
}
export const success = (message, title, duration) => {
  notify('success', message, title, duration)
}
export const warning = (message, title, duration) => {
  notify('warning', message, title, duration)
}
export const error = (message, title, duration) => {
  notify('error', message, title, duration)
}

export default {
  success,
  warning,
  error,
}
