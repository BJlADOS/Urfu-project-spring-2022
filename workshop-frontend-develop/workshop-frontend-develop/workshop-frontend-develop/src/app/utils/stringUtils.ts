export function truncate(string: string, count: number) {
  return (string.length > count) ? string.substr(0, count - 1) + '\u2026' : string
}

export function getWordForm(number: number, oneForm: string, twoForm: string, fiveForm: string) {
  const lastTwoDigit = Math.abs(number) % 100
  const lastDigit = lastTwoDigit % 10

  if (lastTwoDigit > 10 && lastTwoDigit < 20) {
    return fiveForm
  }

  if (lastDigit > 1 && lastDigit < 5) {
    return twoForm
  }

  if (lastDigit == 1) {
    return oneForm
  }

  return fiveForm
}
