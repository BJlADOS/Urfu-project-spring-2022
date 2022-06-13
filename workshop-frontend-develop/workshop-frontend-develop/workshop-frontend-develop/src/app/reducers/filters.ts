import { FiltersActions } from 'app/actions/filters'

import { createImmerReducer } from './create-reducer-utils'
import { FiltersState } from './state'

export const initialState: FiltersState = {
  scenarioList: [],
  keyTechnologyList: [],
  customersList: [],
  curatorsList: [],
  hideClosed: false,
  curator: '',
  customer: '',
  term: '',
  scenario: '',
  technology: '',
}

interface PayloadFilters {
  payload: FiltersState,
  type: string
}

export default createImmerReducer(initialState, {
  [FiltersActions.Type.SET_FILTERS](draft: FiltersState, { payload }: PayloadFilters) {
    draft.keyTechnologyList = payload.keyTechnologyList
    draft.scenario = payload.scenario
    draft.curator = payload.curator
    draft.customer = payload.customer
    draft.scenarioList = payload.scenarioList
    draft.technology = payload.technology
    draft.customersList = payload.customersList
    draft.curatorsList = payload.curatorsList
    draft.term = payload.term
    console.log('123')
    draft.hideClosed = payload.hideClosed
  },

  [FiltersActions.Type.RESET_FILTERS](draft: FiltersState) {
    draft.scenario = ''
    draft.curator = ''
    draft.customer = ''
    draft.technology = ''
    draft.term = ''
    draft.keyTechnologyList = []
  },
})
