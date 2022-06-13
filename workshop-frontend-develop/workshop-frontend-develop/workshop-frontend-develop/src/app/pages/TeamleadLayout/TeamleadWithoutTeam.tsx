import React from 'react'
import {connect} from 'react-redux'
import {Redirect, Route, Switch} from 'react-router-dom'
import {AppBaseLayout} from 'app/components/AppBaseLayout'
import {DashboardRounded, LibraryBooksRounded, PortraitRounded} from '@material-ui/icons'
import {NavigationItem} from 'app/components/AppNavigationBar'
import {profile, projectProposals, projects} from 'app/nav'
import {RootState} from 'app/reducers'
import {ProjectProposalsPage} from 'app/pages/ProjectProposalsPage'
import {TeamleadProjectPage} from 'app/pages/ProjectPage/TeamleadProjectPageComponent'
import {ProfileEdit, ProfilePage} from 'app/pages/ProfilePage'
import {ProjectsPage} from 'app/pages/ProjectsPage'

const items: NavigationItem[] = [
    {
        to: projects(),
        icon: <DashboardRounded/>,
        text: 'Проекты',
        exact: false,
    },
    {
        to: projectProposals(),
        icon: <LibraryBooksRounded/>,
        text: 'Проектная заявка',
        exact: false,
    },
    {
        to: profile(),
        icon: <PortraitRounded/>,
        text: 'Профиль',
        exact: false,
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

const mapStateToProps = (state: RootState) => ({
    user: state.profile,
})

type StateProps = ReturnType<typeof mapStateToProps>

type Props = StateProps

const TeamleadWithoutTeamComponent: React.FC<Props> = ({
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
                                component={TeamleadProjectPage}
                            />
                            <Redirect to={url}/>
                        </Switch>
                    )}
                />
                <Route
                    path={projectProposals()}
                    render={({match: {url}}) => (
                        <Route
                            exact
                            path={`${url}/`}
                            component={ProjectProposalsPage}
                        />
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
                <Redirect to={projects()}/>
            </Switch>
        </AppBaseLayout>
    )
}

export const TeamleadWithoutTeam: React.FC = connect(
    mapStateToProps,
)(TeamleadWithoutTeamComponent)
