import React, { useState } from 'react'
import { ShortUserModel } from 'app/models'
import { RequestProposalStatus, UserRequestProposalModel } from 'app/models/RequestProposalModel'
import { AppCard } from 'app/components/AppCard'
import { AppButton } from 'app/components/AppButton'
import { AppDialog } from 'app/components/AppDialog'
import RequestInProjectDescriptionCard
  from 'app/components/RequestInProjectDescriptionCard/RequestInProjectDescriptionCard'
import { RequestUserTeamCard } from 'app/components/RequestUserTeamCard'
import { AppDivider } from 'app/components/AppDivider'

import style from './style.scss'

interface Props {
  request: UserRequestProposalModel
  onUpdateRequest:(status:RequestProposalStatus)=>void
  requestStatus:any
}

export const RequestUserCard:React.FC<Props> = ({
  request,
  onUpdateRequest,
  requestStatus,
}) => {
  const [open, setOpen] = useState(false)

  // useEffect(()=>{
  //   projects.getProject(request.projectId)
  //
  // })
  //   const fetchInfo
  //
  //   const projectDescriptionCard =
  const teamUsers = request.team.users.map((user:ShortUserModel) =>
    <RequestUserTeamCard
      teamUser={user}
      roleName={user.role?.name ||
      'Не указано'}
      className={style.teamUserCard}
      key={user.id}
    />,
  )
  const acceptButton = requestStatus !== RequestProposalStatus.Accepted && (
    <AppButton
      type={'button'}
      onClick={() => onUpdateRequest(RequestProposalStatus.Accepted) }
      buttonType={'primary'}
    >
            Подтвердить заявку
    </AppButton>)
  const deleteButton = requestStatus !== RequestProposalStatus.Rejected && (<AppButton
    type = {'button'}
    buttonType={'danger'}
    onClick={() => onUpdateRequest(RequestProposalStatus.Rejected)}
  >
    Отклонить заявку
  </AppButton>)

  const actionButtons = (
    <AppCard className = {style.actionButtons}>
      {acceptButton}
      {deleteButton}
    </AppCard>
  )

  const contacts = (
    <AppCard
      className={style.contactCard}
      header={'Контакты'}
    >
      <p>{request.contacts}</p>
    </AppCard>
  )

  const dialogRequest = (
    <div className={style.dialogContent}>
      <AppDialog
        open={open}
        width={'1200px'}
        onClose={() => setOpen(!open)}
      >
        <div className={style.dialogMainContent}>
          <AppCard
            className = {style.projectNameCard}
            header={request.name}
          >

          </AppCard>
          <div className={style.dialogContent}>
            <div className={style.largeColumn}>
              <RequestInProjectDescriptionCard
                request={request}
                className={'requestProjectDescription'}
              />
            </div>
            <div className={style.additionalContent}>
              {contacts}
              <div className={style.headerTeamCard}>
                <h3>Команда {request.team.name ? request.team.name : `№${request.team.id}`}</h3>
              </div>

              <div className={style.teamContent}>
                {teamUsers}
              </div>
            </div>
          </div>
        </div>
      </AppDialog>
    </div>
  )

  return (

    <div className={style.mainContent}>
      <AppCard className={style.mainContentCard}>
        <AppButton
          buttonType={'transparent'}
          onClick={() => {
            setOpen(!open)
          }}
        >
          <div
            className={style.headerRequestCard}
          >
            <h2>{request.name}</h2>
            <AppDivider/>
            <div className={style.desiredRole}>
              <span>Желаемая роль:</span><span className={style.additional}>{request.roleName}</span>
            </div>
          </div>
        </AppButton>
      </AppCard>
      {actionButtons}

      {dialogRequest}
    </div>
  )
}
