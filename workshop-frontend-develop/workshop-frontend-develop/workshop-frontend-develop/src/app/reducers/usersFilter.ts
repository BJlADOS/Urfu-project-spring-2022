import { UsersFilterState } from 'app/reducers/state'
import { createImmerReducer } from 'app/reducers/create-reducer-utils'
import { UserFilterActions } from 'app/actions'

export const initialState: UsersFilterState = {
  allCompetencies: [],
  term: '',
  competenciesSearch: [],
}

interface PayloadUserFilters {
  payload:UsersFilterState,
  type:string
}
export default createImmerReducer(initialState, {
  [UserFilterActions.Type.SET_USER_FILTERS](draft:UsersFilterState, { payload }:PayloadUserFilters) {
    draft.allCompetencies = payload.allCompetencies
    draft.competenciesSearch = payload.competenciesSearch
    draft.term = payload.term
  },
  [UserFilterActions.Type.RESET_USER_FILTERS](draft:UsersFilterState) {
    draft.competenciesSearch = []
    draft.term = ''
  },
})
