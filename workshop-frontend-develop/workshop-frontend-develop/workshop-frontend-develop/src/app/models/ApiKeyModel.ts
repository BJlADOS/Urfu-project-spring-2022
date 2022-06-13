import { UserTypes } from './UserModel'

export interface ApiKeyModel {
  id: number
  name: string
  keyString: string
  eventId: number
  userType: UserTypes
}
