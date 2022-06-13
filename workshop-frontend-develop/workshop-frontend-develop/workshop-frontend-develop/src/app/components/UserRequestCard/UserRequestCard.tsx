import React, { useState } from 'react'
import { CompetenciesList } from 'app/components/CompetenciesList'
import { ShortUserRequestModel, UserCompetencyType } from 'app/models'
import { AppButton } from 'app/components/AppButton'
import { AppCard } from 'app/components/AppCard'
import { RequestProposalStatus } from 'app/models/RequestProposalModel'
import { CheckFidelityDialog } from 'app/components/CheckFidelityDialog'
import { user as userInfo } from 'app/provider'

import style from './style.scss'

interface Props {
  user: ShortUserRequestModel,
  onAction: () => void,
  requestStatus: any,
  onDelete: () => void
}

const status = new Map()
  .set(RequestProposalStatus.Accepted, 'Подтверждено')
  .set(RequestProposalStatus.Rejected, 'Отклонено')
  .set(RequestProposalStatus.Expected, 'На рассмотрении')

export const UserRequestCard: React.FC<Props> = ({
  user,
  onAction,
  requestStatus,
  onDelete,

}) => {
  const [disable, setDisable] = useState(false)
  const [dialogOpen, setDialogOpen] = useState(false)

  const userCardHeader = (user: ShortUserRequestModel) => (
    <div className={style.userCardHeader}>
      <span>{user.lastName} {user.firstName} {user.middleName}</span>
      <span>{user.academicGroup === '-' ? 'Группа: Не указано' : user.academicGroup}</span>
      <span>{status.get(requestStatus)}</span>
    </div>
  )

  const addButton = requestStatus === RequestProposalStatus.Accepted && (
    <AppButton
      type={'button'}
      buttonType={'primary'}
      disabled={disable}
      onClick={() => {
        setDisable(true)
        onAction()
        setDisable(false)
      }}
    >
      Подтвердить
    </AppButton>
  )

  return (
    <div className={style.mainContentRequestCard}>
      <AppCard
        key={user.id}
        className={style.card}
        header={userCardHeader(user)}
      >
        <CompetenciesList
          list={
            user.competencies.filter((c) => c.userCompetencyType === UserCompetencyType.Current)
          }
        />
        <AppButton
          className={style.aboutButton}
          type={'button'}
          buttonType={'secondary'}
          onClick={() => setDialogOpen(true)}
        >

          О студенте
        </AppButton>
      </AppCard>
      <AppCard className={style.buttonsCard}>
        {addButton}
        <AppButton
          type={'button'}
          buttonType={'danger'}
          disabled={disable}
          onClick={() => {
            setDisable(true)
            onDelete()
            setDisable(false)
          }}
        >
          Удалить
        </AppButton>

      </AppCard>
      <CheckFidelityDialog
        open={dialogOpen}
        header={'Доп. Информация о студенте'}
        onMakingAction={() => setDialogOpen(false)}
        additionContent={user.about || ''}
        onClose={() => setDialogOpen(false)}
        buttonType={'danger'}
        buttonText={'Закрыть'}
      />
    </div>
  )
}
