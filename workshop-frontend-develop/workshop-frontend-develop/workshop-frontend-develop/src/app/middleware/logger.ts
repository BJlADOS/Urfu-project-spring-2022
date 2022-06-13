import { Middleware } from 'redux'

export const logger: Middleware = (store) => (next) => (action) => {
  if (process.env.NODE_ENV !== 'production') {
    console.log(`%c${action.type}:`, 'color: #fece00', {
      prevState: store.getState(),
      action: action.payload,
    })
  }
  return next(action)
}
