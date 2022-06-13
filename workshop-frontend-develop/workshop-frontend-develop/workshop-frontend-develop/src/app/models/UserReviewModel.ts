export interface UserReviewModel {
  expertReviews: ExpertReviewModel[]
  averageReviews: AverageReviewModel[]
  competencyReviews: CompetencyReviewModel[]
}

export interface ExpertReviewModel {
  lastName: string
  firstName: string
  middleName: string
  goalsAndTasks?: number
  solution?: number
  presentation?: number
  technical?: number
  result?: number
  knowledge?: number
}

export interface AverageReviewModel {
  criterion: string
  value: number
}

export interface CompetencyReviewModel {
  competencyName: string
  mark: number
}
