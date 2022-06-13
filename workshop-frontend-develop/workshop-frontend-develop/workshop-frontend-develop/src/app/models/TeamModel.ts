import { ShortProjectModel, ShortUserModel, TeamCompetencyReviewModel, TeamReviewModel, UserModel } from 'app/models'

import { TeamSlot } from './AuditoriumModel'

export interface TeamModel {
  id: number
  name: string
  teamStatus: TeamStatusses
  expert?: ExpertModel
  teamCompleteDate?: Date
  project: ShortProjectModel
  users: ShortUserModel[] | UserModel[]
  comment?: string
  mark?: number
  testLink?: string
  review?: TeamReviewModel
  teamSlot?: TeamSlot
  competencyReview?: TeamCompetencyReviewModel[]
  isEntried:boolean
}

export interface ShortTeamModel {
  id: number
  name: string
  users: ShortUserModel[],
  teamStatus: TeamStatusses
  isEntried:boolean
}

export interface TeamListItem {
  id: number
  name?: string
  usersCount: number
  teamStatus: TeamStatusses
  projectName: string
  projectDescription: string
  mark?: string
  teamCapacity: number
  teamCompleteDate?: Date
  teamSlot?: TeamSlot
  isEntried:boolean
}

export interface TeamWithSlotListItem {
  id: number
  name?: string
  usersCount: number
  projectName: string
  projectDescription: string
  teamCapacity: number
  mark?: string
  auditoriumId: number
  auditoriumName: string
  slotTime: string
}

export enum TeamStatusses {
  Incomplete = 'Incomplete',
  ReadyForEntry = 'ReadyForEntry',
  Completed = 'Complete',
  TestWork = 'TestWork',
  ExpertDiscussion = 'ExpertDiscussion',
  Finish = 'Finish'
}

export interface ExpertModel {
  id: number
  auditorium: string
}
