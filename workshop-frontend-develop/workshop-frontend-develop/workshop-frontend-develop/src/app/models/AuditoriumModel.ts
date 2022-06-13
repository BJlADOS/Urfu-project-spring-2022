import { ExpertUserModel } from './UserModel'

export interface AuditoriumModel {
  id: number
  name: string
  experts?: ExpertUserModel[]
  slots?: TeamSlot[]
}

export interface TeamSlot {
  id: number
  team?: {
    id: number
    name?: string
  }
  time: string
}

export interface ShortAuditoriumModel {
  id: number
  name: string
}

export interface ExpertAuditoriumModel {
  id: number
  auditorium: string
}

export interface AddAuditoriumModel {
  name: string
  capacity: number
  isDefault: boolean
}

export interface AddExpertAuditoriumModel {
  expertId: number
  auditoriumId: number
}
