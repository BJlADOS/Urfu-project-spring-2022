import { UserTypes } from './UserModel'

export interface ProposalModel {
  id: number
  name: string
  description: string
  purpose: string
  result: string
  contacts: string
  curator: string
  organization: string
  teamCapacity: number
  maxTeamCount: number
  lifeScenarioName: string
  keyTechnologyName: string
  author: {
    id: number
    lastName: string
    firstName: string
    middleName: string
    about: string
    login: string
    email: string
    phoneNumber: string
    academicGroup: string
    userType: UserTypes
  }
  status: ProposalStatus
  date: string
}

export interface ProjectProposalModel{
  id: number | undefined
  name: string
  description: string
  purpose: string
  result: string
  contacts: string
  curator: string
  organization: string
  teamCapacity: number
  maxTeamCount: number
  lifeScenarioName: string
  keyTechnologyName: string
  author: {
    id: number
    lastName: string
    firstName: string
    middleName: string
    about: string
    login: string
    email: string
    phoneNumber: string
    academicGroup: string
    userType: UserTypes
  }
  status: ProposalStatus
  date: string
}
export interface PendingProposalModel {
  id: number
  name: string
  description: string
  purpose: string
  result: string
  contacts: string
  organization: string
  teamCapacity: number
  lifeScenarioName: string
  keyTechnologyName: string
  author:{
    id: number
    lastName: string
    firstName: string
    middleName: string
    academicGroup: string
  }
}

export interface ShortProposalModel {
  id: number
  name: string
  purpose: string
  maxTeamCount: number
  teamCapacity: number
  author: {
    id: number
    lastName: string
    firstName: string
    middleName: string
    about: string
    login: string
    email: string
    phoneNumber: string
    academicGroup: string
    userType: UserTypes
  }
  status: ProposalStatus
  date: string
}

export interface CreateProjectFromProposalModel{
  name: string
  description: string
  purpose: string
  result: string
  contacts: string
  organization: string
  teamCapacity: number
  lifeScenarioName: string
  keyTechnologyName: string
  curator:string,
  authorId:number;
  status: ProposalStatus
  roleNames:string[]
}

export enum ProposalStatus {
  Pending = 0,
  Rejected = 1,
  Approved =2,
}
