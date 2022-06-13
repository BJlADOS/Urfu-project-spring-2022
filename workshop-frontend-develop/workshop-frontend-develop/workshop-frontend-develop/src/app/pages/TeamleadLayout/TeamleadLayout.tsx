import React from 'react'
import { Redirect, Route, Switch } from 'react-router-dom'
import { AppBaseLayout } from 'app/components/AppBaseLayout'
import {
  AccountBalanceRounded,
  DashboardRounded,
  DeveloperBoardRounded,
  GroupAddRounded,
  GroupRounded,
  PersonAddRounded,
  PortraitRounded,
} from '@material-ui/icons'
import { NavigationItem } from 'app/components/AppNavigationBar'
import { auditoriums, myProject, myTeam, profile, projects, requestProposals, userSearch } from 'app/nav'
import { UserSearchPage } from 'app/pages/UserSearchPage'
import { TeamleadRequestsPage } from 'app/pages/RequestsGrantedPage'

import { ProfileEdit, ProfilePage } from '../ProfilePage'
import { TeamleadTeamPage } from '../MyTeamPage'
import { MyProjectPage } from '../MyProjectPage'
import { TeamleadProjectPage } from '../ProjectPage'
import { ProjectsPage } from '../ProjectsPage'
import { AuditoriumsStudentPage } from '../AuditoriumsPage'

const items: NavigationItem[] = [
  {
    to: projects(),
    icon: <DashboardRounded/>,
    text: 'Проекты',
    exact: false,
  },
  {
    to: myProject(),
    icon: <DeveloperBoardRounded/>,
    text: 'Мой проект',
  },
  {
    to: profile(),
    icon: <PortraitRounded/>,
    text: 'Профиль',
    exact: false,
  },
  {
    to: myTeam(),
    icon: <GroupRounded/>,
    text: 'Моя команда',
  },
  {
    to: userSearch(),
    icon: <PersonAddRounded/>,
    text: 'Пригласить в команду',
  },

  {
    to: requestProposals(),
    icon: <GroupAddRounded/>,
    text: 'Заявки в команду',
  },
  {
    to: auditoriums(),
    icon: <AccountBalanceRounded/>,
    text: 'Аудитории',
  },
]

export const TeamleadWithTeam: React.FC = () => (
  <AppBaseLayout navItems={items}>
    <Switch>
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
              component={TeamleadProjectPage}
            />
            <Redirect to={url}/>
          </Switch>
        )}
      />
      <Route
        exact
        path={myProject()}
        component={MyProjectPage}
      />
      <Route
        path={profile()}
        render={({ match: { url } }) => (
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
        path={myTeam()}
        component={TeamleadTeamPage}
      />
      <Route
        exact
        path={userSearch()}
        component={UserSearchPage}
      />
      <Route
        path={requestProposals()}
        exact
        component={TeamleadRequestsPage}
      />
      <Route
        exact
        path={auditoriums()}
        component={AuditoriumsStudentPage}
      />
      <Redirect to={myProject()}/>
    </Switch>
  </AppBaseLayout>
)
