const PATTERN = /^[^\s@]+@[^\s@]+.[^\s@]+$/

export function validateEmail(email:string | null | undefined) {
  if (!email) {
    return false
  }

  return PATTERN.test(email)
}
