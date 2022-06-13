import React from 'react'
import { Redirect, Route, Switch } from 'react-router-dom'
import {
  AccountBalanceRounded,
  AddBoxRounded,
  DashboardRounded,
  EqualizerRounded,
  EventNoteRounded,
  GroupRounded,
  LibraryBooksRounded,
  PostAddRounded,
  SettingsApplicationsRounded,
  SupervisorAccountRounded,
  WidgetsRounded,
} from '@material-ui/icons'
import { AppBaseLayout } from 'app/components/AppBaseLayout'
import { NavigationItem } from 'app/components/AppNavigationBar'
import {
  addProject,
  auditoriums,
  competencies,
  develop,
  events,
  monitoring,
  projectProposals,
  projects,
  teams,
  uploadProjects,
  usersManagement,
} from 'app/nav'
import { CompetenciesPage } from 'app/pages/CompetenciesPage'
import { TeamsPage } from 'app/pages/TeamsPage'
import AddProjectLayout from 'app/pages/AddProject/AddProjectLayout'
import { AllPendingProposals } from 'app/pages/AdminProposalsPage'
import { AdminProposalPage } from 'app/pages/AdminProposalPage'

import { TeamPage } from '../TeamPage'
import { MonitoringPage } from '../MonitoringPage'
import { EventsPage } from '../EventsPage'
import { AdminProjectPage } from '../ProjectPage'
import { ProjectsPage } from '../ProjectsPage'
import { UploadProjectsPage } from '../UploadProjectsPage'
import { UsersListPage } from '../UsersListPage'
import { AuditoriumsAdminPage } from '../AuditoriumsPage'
import { UserInfoPage } from '../UserInfoPage'
import { DevelopersPage } from '../DevelopersPage'

const items: NavigationItem[] = [
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
  {
    to: monitoring(),
    icon: <EqualizerRounded/>,
    text: 'Статистика',
  },
  {
    to: competencies(),
    icon: <WidgetsRounded/>,
    text: 'Компетенции',
  },
  {
    to: events(),
    icon: <EventNoteRounded/>,
    text: 'События',
  },
  {
    to: uploadProjects(),
    icon: <PostAddRounded/>,
    text: 'Загрузить проекты',
  },
  // TODO доделать страницу добавления команды не через excel таблицу.
  // {
  //     to: addProject(),
  //     icon: <AddBoxRounded/>,
  //     text: 'Добавить проект',
  // },
  {
    to: projectProposals(),
    icon: <LibraryBooksRounded/>,
    text: 'Проектные заявки',
    exact: false,
  },
  {
    to: usersManagement(),
    icon: <SupervisorAccountRounded/>,
    text: 'Пользователи',
    exact: false,
  },
  {
    to: auditoriums(),
    icon: <AccountBalanceRounded/>,
    text: 'Аудитории',
  },
  {
    to: develop(),
    icon: <SettingsApplicationsRounded/>,
    text: 'Разработчикам',
  },
]

export const AdminLayout: React.FC = () => (
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
              component={AdminProjectPage}
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
              component={TeamsPage}
            />
            <Route
              exact
              path={`${url}/:teamId`}
              component={TeamPage}
            />
            <Redirect to={url}/>
          </Switch>
        )}
      />
      <Route
        exact
        path={monitoring()}
        component={MonitoringPage}
      />
      <Route
        exact
        path={competencies()}
        component={CompetenciesPage}
      />
      <Route
        exact
        path={events()}
        component={EventsPage}
      />
      <Route
        exact
        path={uploadProjects()}
        component={UploadProjectsPage}
      />
      <Route
        exact
        path={addProject()}
        component={AddProjectLayout}
      />
      <Route
        path={usersManagement()}
        render={({ match: { url } }) => (
          <Switch>
            <Route
              exact
              path={`${url}/`}
              component={UsersListPage}
            />
            <Route
              exact
              path={`${url}/:userId`}
              component={UserInfoPage}
            />
            <Redirect to={url}/>
          </Switch>
        )}
      />
      <Route
        exact
        path={auditoriums()}
        component={AuditoriumsAdminPage}
      />
      {<Route
        path={projectProposals()}
        render={({ match: { url } }) => (
          <Switch>
            <Route
              exact
              path={`${url}/`}
              component={AllPendingProposals}
            />
            <Route
              exact
              path={`${url}/:proposalId`}
              component={AdminProposalPage}
            />

            <Redirect to={url}/>
          </Switch>
        )}
      />}
      <Route
        exact
        path={develop()}
        component={DevelopersPage}
      />
      <Redirect to={projects()}/>
    </Switch>
  </AppBaseLayout>
)
