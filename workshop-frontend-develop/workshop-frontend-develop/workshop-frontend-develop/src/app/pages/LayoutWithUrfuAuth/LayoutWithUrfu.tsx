import React from 'react'
import {Redirect, Route, Switch} from 'react-router-dom'
import {AppBaseLayout} from 'app/components/AppBaseLayout'
import {changePassword, index, login, registration, registrationUrfu} from 'app/nav'
import {LandingPage} from 'app/pages/LandingPage'
import {LoginPage, SignInPage} from 'app/pages/AuthPages'
import {SignUpPageUrfu} from 'app/pages/AuthPages/SignUpWithUrfu'
import {UpdatePasswordUrfu} from 'app/pages/AuthPages/UpdatePassword'

export const LayoutWithUrfu: React.FC = () => (
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
                path={registrationUrfu()}
                component={SignUpPageUrfu}
            />
            <Route
                exact
                path={changePassword()}
                component={UpdatePasswordUrfu}
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
