const PATTERN = /^((?![^\w-]).)*$/

export function validateLogin(login: string | null | undefined) {
  if (!login) {
    return false
  }

  return PATTERN.test(login)
}
