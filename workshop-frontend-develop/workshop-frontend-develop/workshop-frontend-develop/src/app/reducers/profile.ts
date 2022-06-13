import { ProfileActions } from 'app/actions/profile'
import { UserModel } from 'app/models'

import { createImmerReducer } from './create-reducer-utils'
import { ProfileState } from './state'

const initialState: ProfileState = {
  profile: null,
  loaded: false,
}

interface PayloadProfile {
  payload: UserModel,
  type: string
}

export default createImmerReducer(initialState, {
  [ProfileActions.Type.SET_USER_PROFILE](draft: any, { payload }: PayloadProfile) {
    draft.loaded = true
    draft.profile = payload
  },

  [ProfileActions.Type.REMOVE_USER_PROFILE](draft: any) {
    draft.loaded = false
    draft.profile = null
  },
})
