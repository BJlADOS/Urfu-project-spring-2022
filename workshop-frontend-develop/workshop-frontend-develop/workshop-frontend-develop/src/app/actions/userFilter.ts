import { createAction } from 'redux-actions'
import { UsersFilterState } from 'app/reducers/state'

export namespace UserFilterActions{
  export enum Type {
    SET_USER_FILTERS = 'SET_USER_FILTERS',
    RESET_USER_FILTERS = 'RESET_USER_FILTERS'
  }

  export const setUserFilters = createAction<UsersFilterState>(Type.SET_USER_FILTERS)
  export const resetUserFilters = createAction(Type.RESET_USER_FILTERS)
}

export type UserFilterActions = Omit<typeof UserFilterActions, 'Type'>
