import { createAction } from 'redux-actions'
import { SortingState } from 'app/reducers/state'

export namespace SortingAction {
  export enum Type {
    SET_SORTING = 'SET_SORTING',
  }

  export const setSorting = createAction<SortingState>(Type.SET_SORTING)
}

export type SortingAction = Omit<typeof SortingAction, 'Type'>
