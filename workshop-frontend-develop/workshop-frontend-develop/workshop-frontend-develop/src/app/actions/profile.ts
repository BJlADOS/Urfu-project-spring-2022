import { createAction } from 'redux-actions'
import { UserModel } from 'app/models'

export namespace ProfileActions {
  export enum Type {
    SET_USER_PROFILE = 'SET_USER_PROFILE',
    REMOVE_USER_PROFILE = 'REMOVE_USER_PROFILE',
  }

  export const setUserProfile = createAction<UserModel>(Type.SET_USER_PROFILE)
  export const removeUserProfile = createAction(Type.REMOVE_USER_PROFILE)
}

export type ProfileActions = Omit<typeof ProfileActions, 'Type'>
