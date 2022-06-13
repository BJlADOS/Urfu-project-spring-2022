import React, { useEffect, useState } from 'react'
import { AppCard } from 'app/components/AppCard'
import style from 'app/pages/UserSearchPage/style.scss'
import { LoadingStatus } from 'app/models'
import { AppLoadingSpinner } from 'app/components/AppLoadingSpinner'
import { user } from 'app/provider'
import { RootState } from 'app/reducers'
import { connect } from 'react-redux'
import { RequestProposalStatus, UserRequestProposalModel } from 'app/models/RequestProposalModel'
import { RequestUserCard } from 'app/components/RequestUserItem/RequestUserCard'

import teamleadStyle from './teamleadStyle.scss'

const mapStateToProps = (state: RootState) => ({ profile: state.profile })

type StateProps = ReturnType<typeof mapStateToProps>
type Props = StateProps

const TeamleadRequestsComponent: React.FC<Props> = () => {
  const [loading, setLoading] = useState(LoadingStatus.Loading)
  const [reqeustList, setRequestList] = useState<UserRequestProposalModel[]>([])

  useEffect(() => {
    fetchRequests()
  }, [])

  const fetchRequests = () => {
    user.getRequests()
      .then((res) => setRequestList(res.data))
      .then(() => setLoading(LoadingStatus.Finished))
  }
  const handleUpdateStatus = (requestId: number, status: RequestProposalStatus) => {
    user.updateRequestInTeam(requestId, status)
      .then(fetchRequests)
  }

  const requests = reqeustList && reqeustList.map(request => (
    <RequestUserCard
      key={request.id}
      request={request}
      onUpdateRequest={(status: RequestProposalStatus) => handleUpdateStatus(request.id, status)}
      requestStatus={RequestProposalStatus[request.requestStatus]}
    />

  ))

  const header = (
    <AppCard
      className={teamleadStyle.headerCard}
    >

      <h2>Приглашения в команду</h2>
    </AppCard>
  )

  const usersContent = loading === LoadingStatus.Finished ? requests : (
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
    </div>
  )
}

export const UserProposalsPage: React.FC = connect(
  mapStateToProps,
)(TeamleadRequestsComponent)
