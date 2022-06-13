import { ShortTeamModel } from 'app/models/TeamModel'

export interface RequestProposalModel{
  id:number
  userId:number
  teamleadId:number
  projectId:number
  teamId:number
  status:RequestProposalStatus
}

export interface UserRequestProposalModel{
  id: number
  name: string
  contacts: string
  purpose: string
  result: string
  teamCapacity: number
  lifeScenarioName: string
  keyTechnologyName: string
  description: string
  roleName:string
  team: ShortTeamModel
  requestStatus:RequestProposalStatus
}

export enum RequestProposalStatus {
  'Accepted' = 2,
  'Rejected' = 1,
  'Expected' = 0
}
