import { TeamModel, CompetencyModel, ShortTeamModel } from 'app/models'

import { RoleModel } from './RoleModel'

export interface ProjectModel {
  id: number
  name: string
  curator: string
  organization: string
  contacts: string
  purpose: string
  result: string
  teamCapacity: number
  lifeScenario: LifeScenarioModel
  keyTechnology: KeyTechnologyModel
  competencies: CompetencyModel[]
  teams: TeamModel[] | ShortTeamModel[]
  matchedCompetenciesCount: number
  fillTeamsCount: number
  participantsCount: number
  roles: RoleModel[]
  description: string
  maxTeamCount: number
  isAvailable: boolean
  isPromoted?: boolean
  image?: string
  isEntryOpen:boolean
}

export interface ShortProjectModel {
  id: number
  name: string
  curator: string
  organization: string
  contacts: string
  purpose: string
  result: string
  teamCapacity: number
  lifeScenario: LifeScenarioModel
  keyTechnology: KeyTechnologyModel
  competencies: CompetencyModel[]
  matchedCompetenciesCount: number
  fillTeamsCount: number
  participantsCount: number
  roles: RoleModel[]
  maxTeamCount: number
  description: string
  isAvailable: boolean
  isPromoted?: boolean
  image?: string
  isEntryOpen:boolean
}
export interface AddProjectModel{
  name: string,
  description: string,
  purpose: string,
  result: string,
  curator: string,
  organization: string,
  contacts: string,
  lifeScenarioName: string,
  keyTechnologyName: string,
  hardSkills: string[],
  softSkills: string[]
  roles: string[],
  teamsSize: number|null
  teamLimit: number|null
}

export interface TextDataProjectModel {
  id: number
  name: string
  curator: string
  organization: string
  contacts: string
  purpose: string
  result: string
  description: string
}

export interface LifeScenarioModel {
  id: number
  name: string
}

export interface KeyTechnologyModel {
  id: number
  name: string
}

export interface CuratorModel {
  id: number
  name: string
}

export interface CustomerModel {
  id: number
  name: string
}
