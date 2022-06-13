import { createAction } from 'redux-actions'
import { CoursesFiltersState, FiltersState } from 'app/reducers/state'

export namespace FiltersActions {
  export enum Type {
    SET_FILTERS = 'SET_FILTERS',
    RESET_FILTERS = 'RESET_FILTERS'
  }

  export const setFilters = createAction<FiltersState | CoursesFiltersState>(Type.SET_FILTERS)
  export const resetFilters = createAction(Type.RESET_FILTERS)
}

export type FiltersActions = Omit<typeof FiltersActions, 'Type'>
