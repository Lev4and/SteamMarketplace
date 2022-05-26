export const getNumberFormat = (number, minimumFractionDigits, locales = 'us-US') => {
  return new Intl.NumberFormat(locales, { minimumFractionDigits: minimumFractionDigits }).format(number)
}

export const getCurrencyFormat = (number, locales = 'us-US', currency = 'USD') => {
  return new Intl.NumberFormat(locales, { style: 'currency', currency: currency }).format(number)
}
