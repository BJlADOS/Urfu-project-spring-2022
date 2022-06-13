import Cookie from 'js-cookie'
import { user } from 'app/provider'

const SID_KEY = 'SID'

export function loggedIn(): boolean {
  return Boolean(Cookie.get(SID_KEY))
}

export function getToken(): string {
  return Cookie.get(SID_KEY) || ''
}

export function login(email: string, password: string, eventId: number): Promise<void> {
  return user.login(email, password, eventId).then(() => {
    if (process.env.ENABLE_FAKES == 'true') { Cookie.set(SID_KEY, String(Math.random())) }
  })
}

export function logout(): void {
  Cookie.remove(SID_KEY)
}

export function clear(): void {
  Cookie.remove(SID_KEY)
}
