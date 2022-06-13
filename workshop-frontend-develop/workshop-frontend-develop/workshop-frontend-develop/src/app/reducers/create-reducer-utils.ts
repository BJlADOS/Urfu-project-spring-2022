import produce from 'immer'

export function createImmerReducer(initialState: any, handlers: any) {
  return function reducer(state: any = initialState, action: any) {
    if (Object.prototype.hasOwnProperty.call(handlers, action.type)) {
      return produce(state, (draft: any) => {
        handlers[action.type](draft, action)
      })
    } else return state
  }
}
