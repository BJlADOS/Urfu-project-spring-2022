import React, { useEffect } from 'react'
import { connect } from 'react-redux'
import { ProfileActions } from 'app/actions'
import { UserModel, UserTypes } from 'app/models'
import { RootState } from 'app/reducers'
import { user } from 'app/provider'
import { loggedIn } from 'app/auth'
import { TeamleadWithTeam } from 'app/pages/TeamleadLayout/TeamleadLayout'
import { TeamleadWithoutTeam } from 'app/pages/TeamleadLayout/TeamleadWithoutTeam'
import { LayoutWithUrfu } from 'app/pages/LayoutWithUrfuAuth/LayoutWithUrfu'

import { AppLoadingSpinner } from './components/AppLoadingSpinner'
import { DefaultLayout } from './pages/DefaultLayout'
import { AdminLayout } from './pages/AdminLayout'
import { StudentWithoutTeam, StudentWithTeam } from './pages/StudentLayout'
import { ExpertLayout } from './pages/ExpertLayout'

import './style.scss'

const mapStateToProps = (state: RootState) => ({ profile: state.profile })

const mapDispatchToProps = { setUserProfile: (user: UserModel) => ProfileActions.setUserProfile(user) }

type StateProps = ReturnType<typeof mapStateToProps>
type DispatchProps = typeof mapDispatchToProps

interface Props extends StateProps, DispatchProps { }

const NewRoutingComponent: React.FC<Props> = ({ profile, setUserProfile }) => {
  useEffect(() => {
    if (loggedIn()) {
      user.getUserProfile().then(res => {
        setUserProfile(res.data)
      })
    }
  }, [])

  if (process.env.LOGIN_URFU == 'false' && !loggedIn()) {
    return <DefaultLayout />
  }
  if (process.env.LOGIN_URFU == 'true' && !loggedIn()) {
    return <LayoutWithUrfu />
  }

  if (profile.profile?.userType === UserTypes.Admin) {
    return <AdminLayout />
  }

  if (profile.profile?.userType === UserTypes.Expert) {
    return <ExpertLayout />
  }

  if (profile.profile?.userType === UserTypes.Student) {
    return profile.profile.team ? <StudentWithTeam /> : <StudentWithoutTeam />
  }

  if (profile.profile?.userType === UserTypes.Teamlead) {
    // debugger
    return profile.profile.team ? <TeamleadWithTeam /> : <TeamleadWithoutTeam />
  }

  return <AppLoadingSpinner fullHeight />
}

export const Routing: React.FC = connect(
  mapStateToProps,
  mapDispatchToProps,
)(NewRoutingComponent)
