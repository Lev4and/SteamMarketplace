export const declinableWords = {
  'Элемент': ['элемент', 'элемента', 'элементов'],
  'Результат': ['результат', 'результата', 'результатов'],
}

export const declineByNumber = (number, words, showNumeral = false) => {
  if (words instanceof Array && words?.length === 3) {
    let result = showNumeral ? `${number} ` : ''
    switch (number % 100 > 19 ? number % 100 % 10 : number % 100) {
      case 1: 
        result += words[0] 
        break
      case 2:
      case 3: 
      case 4:
        result += words[1]
        break
      default:
        result += words[2]
        break
    }
    return result
  }
  return ''
}

export const getNumberFormat = (number, maximumFractionDigits, locales = 'us-US') => {
  return new Intl.NumberFormat(locales, { maximumFractionDigits: maximumFractionDigits }).format(number)
}

export const getPercentFormat = (number, maximumFractionDigits, locales = 'us-US') => {
  return new Intl.NumberFormat(locales, {
    style: 'percent',
    maximumFractionDigits: maximumFractionDigits,
    signDisplay: 'exceptZero'
  }).format(number)
}

export const getCurrencyFormat = (number, locales = 'us-US', currency = 'USD') => {
  return new Intl.NumberFormat(locales, { style: 'currency', currency: currency }).format(number)
}

export const getTotalNumberFormat = (total) => {
  return `Всего ${declineByNumber(total, declinableWords['Элемент'], true)}`
}
