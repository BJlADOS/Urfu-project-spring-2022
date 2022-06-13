import React from 'react'
import { Redirect, Route, Switch } from 'react-router-dom'
import { AppBaseLayout } from 'app/components/AppBaseLayout'
import {
  AccountBalanceRounded,
  DashboardRounded,
  DeveloperBoardRounded,
  GroupRounded,
  PortraitRounded,
  SchoolRounded,
} from '@material-ui/icons'
import { NavigationItem } from 'app/components/AppNavigationBar'
import { auditoriums, courses, myProject, myTeam, profile, projects } from 'app/nav'

import { ProfileEdit, ProfilePage } from '../ProfilePage'
import { MyTeamPage } from '../MyTeamPage'
import { MyProjectPage } from '../MyProjectPage'
import { ProjectPage } from '../ProjectPage'
import { ProjectsPage } from '../ProjectsPage'
import { AuditoriumsStudentPage } from '../AuditoriumsPage'
import { CoursesPage } from '../CoursesPage'
import { CoursePage } from '../CoursePage'
import { TopicPage } from '../TopicPage'

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
    to: myTeam(),
    icon: <GroupRounded/>,
    text: 'Моя команда',
  },
  // TODO: Uncomment when ProjectProposals will be finished
  // {
  //   to: projectProposals(),
  //   icon: <LibraryBooksRounded />,
  //   text: 'Мои проектные заявки',
  //   exact: false,
  // },
  {
    to: auditoriums(),
    icon: <AccountBalanceRounded/>,
    text: 'Аудитории',
  },
  {
    to: courses(),
    icon: <SchoolRounded/>,
    text: 'Курсы',
    exact: false,
  },
  {
    to: profile(),
    icon: <PortraitRounded/>,
    text: 'Профиль',
    exact: false,
  },
  // TODO: Uncomment when review logic will be finished
  // {
  //   to: review(),
  //   icon: <AssessmentRounded />,
  //   text: 'Результаты защиты',
  // },
]

export const StudentWithTeam: React.FC = () => (
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
              component={ProjectPage}
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
        path={courses()}
        render={({ match: { url } }) => (
          <Switch>
            <Route
              exact
              path={`${url}/`}
              component={CoursesPage}
            />
            <Route
              exact
              path={`${url}/:courseId`}
              component={CoursePage}
            />
            <Route
              exact
              path={`${url}/:courseId/:topicId`}
              component={TopicPage}
            />
            <Redirect to={url}/>
          </Switch>
        )}
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
        component={MyTeamPage}
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
