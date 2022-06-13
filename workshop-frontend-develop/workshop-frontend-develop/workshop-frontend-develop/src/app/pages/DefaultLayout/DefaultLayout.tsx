import React from 'react'
import { Redirect, Route, Switch } from 'react-router-dom'
import { AppBaseLayout } from 'app/components/AppBaseLayout'
import { index, login, registration } from 'app/nav'

import { LoginPage, SignInPage } from '../AuthPages'
import { LandingPage } from '../LandingPage'

export const DefaultLayout: React.FC = () => (
  <AppBaseLayout>
    <Switch>
      <Route
        exact
        path={index()}
        component={LandingPage}
      />
      <Route
        exact
        path={login()}
        component={LoginPage}
      />
      <Route
        exact
        path={registration()}
        component={SignInPage}
      />
      <Redirect to={index()}/>
    </Switch>
  </AppBaseLayout>
)
