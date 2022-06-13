import { combineReducers } from 'redux'

import { RootState, ProfileState } from './state'
import profileReducer from './profile'
import filtersReducer from './filters'
import sortingReducer from './sorting'
import userFiltersReducer from './usersFilter'
import courseFiltersReducer from './courseFilters'

export { RootState, ProfileState }

export const rootReducer = combineReducers<RootState>({
  profile: profileReducer as any,
  coursesFilters: courseFiltersReducer as any,
  filters: filtersReducer as any,
  sorting: sortingReducer as any,
  userFilters: userFiltersReducer as any,
})
