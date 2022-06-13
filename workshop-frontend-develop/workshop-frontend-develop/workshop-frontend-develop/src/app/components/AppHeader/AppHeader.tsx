import React from 'react'
import { connect } from 'react-redux'
import { NavLink, useHistory } from 'react-router-dom'
import { ExitToAppRounded } from '@material-ui/icons'
import { index, login } from 'app/nav'
import { RootState } from 'app/reducers'
import { ProfileActions } from 'app/actions'
import { logout } from 'app/auth'
import { FiltersActions } from 'app/actions/filters'
import { FiltersState } from 'app/reducers/state'
import { initialState } from 'app/reducers/filters'

import { AppButton } from '../AppButton'
import { AppThemeToggle } from '../AppThemeToggle'
import pack from '../../../../package.json'

import style from './style.scss'

const mapStateToProps = (state: RootState) => ({ profile: state.profile })

const mapDispatchToProps = {
  logoutUser: () => ProfileActions.removeUserProfile(),
  setFilters: (state: FiltersState) => FiltersActions.setFilters(state),
}

type StateProps = ReturnType<typeof mapStateToProps>
type DispatchProps = typeof mapDispatchToProps

interface Props extends StateProps, DispatchProps { }

const AppHeaderComponent: React.FC<Props> = ({
  profile,
  logoutUser,
  setFilters,
}) => {
  /**
   * Current user name.
   */
  const userName = profile.loaded && profile.profile?.firstName && profile.profile?.lastName &&
    `${profile.profile?.lastName} ${profile.profile?.firstName}`

  const handleLogout = () => {
    logout()
    logoutUser()
    setFilters(initialState)
  }

  const history = useHistory()

  const loggedUserButtons = (
    <>
      <span className={style.userName}>{userName}</span>
      <AppButton
        buttonType='secondary'
        onClick={handleLogout}
        className={style.logout}
        type='button'
      >
        Выход
      </AppButton>
    </>
  )

  const defaultButtons = (
    <AppButton
      buttonType='secondary'
      icon={<ExitToAppRounded />}
      className={style.logout}
      onClick={() => history.push(login())}
      type='button'
    >
      Вход
    </AppButton>
  )

  const headerButtons = profile.loaded ? loggedUserButtons : defaultButtons

  return (
    <header>
      <Logo />
      <AppThemeToggle className={style.themeToggle} />
      <div className={style.spacer} />
      {headerButtons}
    </header>
  )
}

export const AppHeader: React.FC = connect(
  mapStateToProps,
  mapDispatchToProps,
)(AppHeaderComponent)

const Logo: React.FC = () => (
  <div
    className={style.logo}
    title={`Версия системы ${pack.version}`}
  >
    <NavLink to={index()} />
  </div>
)
