const PATTERN = /^(\+7|7|8)?[\s\-]?\(?[489][0-9]{2}\)?[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2}$/

export function validatePhone(phone:string | null | undefined) {
  if (!phone) {
    return false
  }
  return PATTERN.test(phone)
}
