import React from 'react'
import { connect } from 'react-redux'
import { RootState } from 'app/reducers'
import { AppCard } from 'app/components/AppCard'
import { AppButton } from 'app/components/AppButton'
import { TeamMemberCard } from 'app/components/TeamMemberCard'
import { TeamStatusses, UserModel } from 'app/models'
import { projects, user } from 'app/provider'
import { ProfileActions } from 'app/actions'

import style from './style.scss'

const mapStateToProps = (state: RootState) => ({ profile: state.profile })

const mapDispatchToProps = { setUserProfile: (user: UserModel) => ProfileActions.setUserProfile(user) }

type StateProps = ReturnType<typeof mapStateToProps>
type DispatchProps = typeof mapDispatchToProps

const mapRegister = new Map()
  .set(true, 'Зарегистрирована')
  .set(false, 'Не зарегистрирована')
  .set(undefined, '123')

interface Props extends StateProps, DispatchProps {
}

const MyTeamPageComponent: React.FC<Props> = ({
  profile,
  setUserProfile,
}) => {
  const team = profile.profile?.team
  const teamName = team?.name || `Команда №${team?.id}`

  const fetchUser = () => user.getUserProfile()
    .then(res => setUserProfile(res.data))

  const teamMembers = (team?.users as UserModel[]).map(user => (
    <TeamMemberCard
      key={user.id}
      user={user}
    />
  ))

  const header = (
    <>
      <h2>{teamName}</h2>
      <span>{mapRegister.get(team?.isEntried)}</span>
    </>
  )

  const handleTeamLeave = () => {
    projects.leaveTeam()
      .then(fetchUser)
  }

  const leaveTeamButton = team?.teamStatus !== TeamStatusses.Completed && (
    <div className={style.buttonsSection}>
      <AppButton
        type='button'
        buttonType='danger'
        onClick={() => handleTeamLeave()}
      >
        Покинуть команду
      </AppButton>
    </div>
  )

  return (
    <>
      <AppCard contentClassName={style.teamNameCard}>
        {header}
      </AppCard>
      <div className={style.teamMembersContainer}>
        {teamMembers}
      </div>
      {leaveTeamButton}
    </>
  )
}

export const MyTeamPage: React.FC = connect(
  mapStateToProps,
  mapDispatchToProps,
)(MyTeamPageComponent)
