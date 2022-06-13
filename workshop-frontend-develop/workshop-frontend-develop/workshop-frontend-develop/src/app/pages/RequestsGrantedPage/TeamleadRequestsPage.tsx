import React, { useEffect, useState } from 'react'
import { AppCard } from 'app/components/AppCard'
import style from 'app/pages/UserSearchPage/style.scss'
import { LoadingStatus, ShortUserRequestModel, UserModel } from 'app/models'
import { AppLoadingSpinner } from 'app/components/AppLoadingSpinner'
import { UserRequestCard } from 'app/components/UserRequestCard'
import { requestProposal, teamlead, user } from 'app/provider'
import { RootState } from 'app/reducers'
import { connect } from 'react-redux'
import { CheckFidelityDialog } from 'app/components/CheckFidelityDialog'
import { ProfileActions } from 'app/actions'
import { RequestProposalStatus } from 'app/models/RequestProposalModel'

import teamleadStyle from './teamleadStyle.scss'

const mapStateToProps = (state: RootState) => ({ profile: state.profile })

const mapDispatchToProps = { setUserProfile: (user: UserModel) => ProfileActions.setUserProfile(user) }

type StateProps = ReturnType<typeof mapStateToProps>
type DispatchProps = typeof mapDispatchToProps

interface Props extends StateProps, DispatchProps {
}

const TeamleadRequestsComponent: React.FC<Props> = ({
  profile,
  setUserProfile,

}) => {
  const [loading, setLoading] = useState(LoadingStatus.Loading)
  const [usersList, setUsersList] = useState<ShortUserRequestModel[]>([])
  const [catchErrorDialog, setCatchErrorDialog] = useState(false)

  useEffect(() => {
    fetchUsersRequests()
  }, [])

  const fetchUsersRequests = () => {
    requestProposal.getUsersRequests()
      .then((res) => setUsersList(res.data))
      .then(() => setLoading(LoadingStatus.Finished))
  }
  const fetchUser = () => user.getUserProfile()
    .then(res => {
      console.log(res.data)
      setUserProfile(res.data)
    })

  const handleAddUserInTeam = (userId: number, roleName: string) => {
    if (profile.profile?.team) {
      teamlead.addUserInTeam(userId, profile.profile?.team?.id, roleName)
        .then(fetchUser)
        .then(fetchUsersRequests)
        .catch(() => setCatchErrorDialog(true))
    }
  }

  const handleDeleteRequest = (userId: number) => {
    requestProposal.deleteRequestProposal(userId)
      .then(fetchUsersRequests)
  }

  const catchError = (
    <CheckFidelityDialog
      onClose={() => setCatchErrorDialog(false)}
      open={catchErrorDialog}
      onMakingAction={() => setCatchErrorDialog(false)}
      header={'Вы не можете добавить пользователя в проект'}
      additionContent={'Похоже, вы превысили количество пользователей в своей команде'}
      buttonText={'Закрыть'}
      buttonType={'primary'}
    />
  )

  const header = (
    <AppCard
      className={teamleadStyle.headerCard}
    >
      <h2>Заявки в команду</h2>
    </AppCard>
  )

  const users = usersList.map(user =>
    (
      <UserRequestCard
        user={user}
        onAction={() => handleAddUserInTeam(user.id, user.roleName)}
        onDelete={() => handleDeleteRequest(user.id)}
        requestStatus={RequestProposalStatus[user.status]}
        key={user.id}
      />

    ))

  const usersContent = loading === LoadingStatus.Finished ? users : (
    <div className={style.loader}>
      {loading === LoadingStatus.Loading && <AppLoadingSpinner/>}
      {loading === LoadingStatus.Error && (
        <div>
          <h2>Произошла ошибка!</h2>
          <p>Не получилось загрузить заявки.</p>
          <p>Попробуйте перезагрузить страницу.</p>
        </div>
      )}
    </div>
  )

  return (
    <div className={teamleadStyle.mainContent}>
      {header}
      {usersContent}
      {catchError}
    </div>
  )
}

export const TeamleadRequestsPage: React.FC = connect(
  mapStateToProps,
  mapDispatchToProps,
)(TeamleadRequestsComponent)
