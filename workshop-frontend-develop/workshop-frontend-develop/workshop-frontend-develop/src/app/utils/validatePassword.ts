const PATTERN = /^(?=\w*[A-Z])(?=\w*[a-z])(?=\w*[0-9])((?![^\w]).)*$/

export function validatePassword(password: string | null | undefined) {
  if (!password) {
    return false
  }

  return PATTERN.test(password)
}
