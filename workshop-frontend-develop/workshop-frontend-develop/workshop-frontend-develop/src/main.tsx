import React from 'react'
import ReactDOM from 'react-dom'
import { BrowserRouter } from 'react-router-dom'
import { Provider } from 'react-redux'
import { configureStore } from 'app/store'
import { Routing } from 'app/routing'

const store = configureStore()

ReactDOM.render(
  <Provider store={store}>
    <BrowserRouter>
      <Routing />
    </BrowserRouter>
  </Provider>,
  document.getElementById('root'),
)
