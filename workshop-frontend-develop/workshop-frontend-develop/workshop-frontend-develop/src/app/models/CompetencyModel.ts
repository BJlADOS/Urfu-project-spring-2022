export interface UserCompetencyModel {
  id: number
  name: string
  competencyType: CompetencyType
  userCompetencyType: UserCompetencyType
}

export interface CompetencyModel {
  id: number
  name: string
  competencyType: CompetencyType
}

export enum UserCompetencyType {
  Current = 'Current',
  Desirable = 'Desirable'
}

export enum CompetencyType {
  SoftSkill = 'SoftSkill',
  HardSkill = 'HardSkill'
}

export interface TeamCompetencyReviewModel {
  id: number
  mark: number
}
