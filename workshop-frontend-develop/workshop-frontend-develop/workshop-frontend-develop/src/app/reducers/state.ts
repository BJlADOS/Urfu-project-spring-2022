import {
  UserProfileModel,
  LifeScenarioModel,
  KeyTechnologyModel,
  CustomerModel,
  CuratorModel,
  CompetencyModel,
  RoleModel,
} from 'app/models'
import { SortType } from 'app/models/SortType'

export interface ProfileState {
  loaded: boolean
  profile: UserProfileModel | null
}

export interface FiltersState {
  term: string,
  scenarioList: LifeScenarioModel[]
  keyTechnologyList: KeyTechnologyModel[]
  customersList: CustomerModel[]
  curatorsList: CuratorModel[]
  scenario: number | string
  technology: number | string
  customer: number | string
  curator: number | string
  hideClosed: boolean
}

export interface CoursesFiltersState {
  term: string,
  roleList: RoleModel[],
  role: number | string
  hideClosed: boolean,
  hideCompleted: boolean,
  showMine: boolean,
}

export interface UsersFilterState {
  term:string,
  allCompetencies: CompetencyModel[]
  competenciesSearch:CompetencyModel[]
}

export interface SortingState {
  type: SortType
  isDesc: boolean
}

export interface RootState {
  profile: ProfileState
  userFilters:UsersFilterState
  filters: FiltersState
  coursesFilters: CoursesFiltersState
  sorting: SortingState
  router?: any
}
