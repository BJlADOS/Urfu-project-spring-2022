import { SortingAction } from 'app/actions'
import { SortType } from 'app/models/SortType'

import { createImmerReducer } from './create-reducer-utils'
import { SortingState } from './state'

const initialState: SortingState = {
  isDesc: false,
  type: SortType.ProjectName,
}

interface PayloadSorting {
  payload: SortingState,
  type: string
}

export default createImmerReducer(initialState, {
  [SortingAction.Type.SET_SORTING](draft: any, { payload }: PayloadSorting) {
    draft.isDesc = payload.isDesc
    draft.type = payload.type
  },
})
