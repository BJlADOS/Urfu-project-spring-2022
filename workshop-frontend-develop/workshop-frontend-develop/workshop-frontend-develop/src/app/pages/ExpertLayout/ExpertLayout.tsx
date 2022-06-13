import React from 'react'
import { Redirect, Route, Switch } from 'react-router-dom'
import { AppBaseLayout } from 'app/components/AppBaseLayout'
import { DashboardRounded, GroupRounded, PortraitRounded } from '@material-ui/icons'
import { NavigationItem } from 'app/components/AppNavigationBar'
import { profile, projects, teams } from 'app/nav'
import { ExpertProfilePage } from 'app/pages/ProfilePage/ExpertProfilePage'

import { ProjectsPage } from '../ProjectsPage'
import { ExpertTeamsPage } from '../TeamsPage'
import { TeamReviewPage } from '../TeamReviewPage'
import { ExpertProjectPage } from '../ProjectPage'

const items: NavigationItem[] = [
  {
    to: profile(),
    icon: <PortraitRounded/>,
    text: 'Профиль',
  },
  {
    to: projects(),
    icon: <DashboardRounded/>,
    text: 'Проекты',
    exact: false,
  },
  {
    to: teams(),
    icon: <GroupRounded/>,
    text: 'Команды',
    exact: false,
  },
]

export const ExpertLayout: React.FC = () => (
  <AppBaseLayout navItems={items}>
    <Switch>
      <Route
        exact
        path={profile()}
        component={ExpertProfilePage}
      />
      <Route
        path={projects()}
        render={({ match: { url } }) => (
          <Switch>
            <Route
              exact
              path={`${url}/`}
              component={ProjectsPage}
            />
            <Route
              exact
              path={`${url}/:projectId`}
              component={ExpertProjectPage}
            />
            <Redirect to={url}/>
          </Switch>
        )}
      />
      <Route
        path={teams()}
        render={({ match: { url } }) => (
          <Switch>
            <Route
              exact
              path={`${url}/`}
              component={ExpertTeamsPage}
            />
            <Route
              exact
              path={`${url}/:teamId`}
              component={TeamReviewPage}
            />
            <Redirect to={url}/>
          </Switch>
        )}
      />
      <Redirect to={teams()}/>
    </Switch>
  </AppBaseLayout>
)
