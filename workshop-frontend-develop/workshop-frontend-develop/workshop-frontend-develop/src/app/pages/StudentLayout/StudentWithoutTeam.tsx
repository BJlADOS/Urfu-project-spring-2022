import React from 'react'
import {connect} from 'react-redux'
import {Redirect, Route, Switch} from 'react-router-dom'
import {AppBaseLayout} from 'app/components/AppBaseLayout'
import {DashboardRounded, GroupAddRounded, PortraitRounded} from '@material-ui/icons'
import {NavigationItem} from 'app/components/AppNavigationBar'
import {profile, projects, userProposalRequests} from 'app/nav'
import {RootState} from 'app/reducers'
import {UserProposalsPage} from 'app/pages/RequestsGrantedPage/UserRequestsPage'

import {ProfileEdit, ProfilePage} from '../ProfilePage'
import {ProjectPage} from '../ProjectPage'
import {ProjectsPage} from '../ProjectsPage'

const items: NavigationItem[] = [
    {
        to: projects(),
        icon: <DashboardRounded/>,
        text: 'Проекты',
        exact: false,
    },
    {
        to: profile(),
        icon: <PortraitRounded/>,
        text: 'Профиль',
        exact: false,
    },
    {
        to: userProposalRequests(),
        icon: <GroupAddRounded/>,
        text: 'Приглашения в команду',
    },
]

const nonFilledProfileItems: NavigationItem[] = [
    {
        to: profile(),
        icon: <PortraitRounded/>,
        text: 'Профиль',
        exact: false,
    },
]

const mapStateToProps = (state: RootState) => ({user: state.profile})

type StateProps = ReturnType<typeof mapStateToProps>

type Props = StateProps

const StudentWithoutTeamComponent: React.FC<Props> = ({
                                                          user,
                                                      }) => {
    const isProfileFilled = user.profile?.profileFilled

    if (!isProfileFilled) {
        return (
            <AppBaseLayout navItems={nonFilledProfileItems}>
                <Switch>
                    <Route
                        path={profile()}
                        render={({match: {url}}) => (
                            <Switch>
                                <Route
                                    exact
                                    path={`${url}/`}
                                    component={ProfilePage}
                                />
                                <Route
                                    exact
                                    path={`${url}/edit`}
                                    component={ProfileEdit}
                                />
                                <Redirect to={url}/>
                            </Switch>
                        )}
                    />
                    <Redirect to={profile()}/>
                </Switch>
            </AppBaseLayout>
        )
    }

    return (
        <AppBaseLayout navItems={items}>
            <Switch>
                <Route
                    path={projects()}
                    render={({match: {url}}) => (
                        <Switch>
                            <Route
                                exact
                                path={`${url}/`}
                                component={ProjectsPage}
                            />
                            <Route
                                exact
                                path={`${url}/:projectId`}
                                component={ProjectPage}
                            />
                            <Redirect to={url}/>
                        </Switch>
                    )}
                />
                <Route
                    path={profile()}
                    render={({match: {url}}) => (
                        <Switch>
                            <Route
                                exact
                                path={`${url}/`}
                                component={ProfilePage}
                            />
                            <Route
                                exact
                                path={`${url}/edit`}
                                component={ProfileEdit}
                            />
                            <Redirect to={url}/>
                        </Switch>
                    )}
                />
                <Route
                    exact
                    path={userProposalRequests()}
                    component={UserProposalsPage}
                />
                <Redirect to={projects()}/>
            </Switch>
        </AppBaseLayout>
    )
}

export const StudentWithoutTeam: React.FC = connect(
    mapStateToProps,
)(StudentWithoutTeamComponent)
