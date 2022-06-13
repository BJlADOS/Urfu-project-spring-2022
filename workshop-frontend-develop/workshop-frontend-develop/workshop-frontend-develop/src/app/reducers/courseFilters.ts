import { FiltersActions } from 'app/actions/filters'

import { createImmerReducer } from './create-reducer-utils'
import { CoursesFiltersState } from './state'

export const initialState: CoursesFiltersState = {
  term: '',
  roleList: [],
  role: '',
  hideClosed: false,
  hideCompleted: false,
  showMine: false,
}

interface PayloadFilters {
  payload: CoursesFiltersState,
  type: string
}

export default createImmerReducer(initialState, {
  [FiltersActions.Type.SET_FILTERS](draft: CoursesFiltersState, { payload }: PayloadFilters) {
    draft.term = payload.term
    console.log('123')
    draft.roleList = payload.roleList
    draft.role = payload.role
    draft.hideClosed = payload.hideClosed
    draft.hideCompleted = payload.hideCompleted
    draft.showMine = payload.showMine
  },

  [FiltersActions.Type.RESET_FILTERS](draft: CoursesFiltersState) {
    draft.roleList = []
    draft.term = ''
    draft.hideClosed = false
    draft.hideCompleted = false
    draft.showMine = false
  },
})
