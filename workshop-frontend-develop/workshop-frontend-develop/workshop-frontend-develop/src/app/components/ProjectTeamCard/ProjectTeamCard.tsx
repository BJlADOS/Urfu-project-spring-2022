import React, { useState } from 'react'
import { HowToRegRounded } from '@material-ui/icons'
import { ProjectModel, ShortTeamModel, ShortUserModel, TeamStatusses } from 'app/models'
import { CheckFidelityDialog } from 'app/components/CheckFidelityDialog'

import { AppCard } from '../AppCard'
import { AppDivider } from '../AppDivider'
import { CompetenciesList } from '../CompetenciesList'
import { AppButton } from '../AppButton'
import { AppDialog } from '../AppDialog'

import style from './style.scss'

interface Props {
  team: ShortTeamModel
  project: ProjectModel
  onJoinTeam?: (value: string) => void
  hideJoinTeamButton?: boolean
}

export const ProjectTeamCard: React.FC<Props> = ({
  team,
  project,
  hideJoinTeamButton,
  onJoinTeam,
}) => {
  const [selectRoleDialog, setSelectRoleDialog] = useState(false)
  const [catchError, setCatchError] = useState(false)

  const joinTeamButton = team.teamStatus === TeamStatusses.Incomplete &&
  onJoinTeam && !hideJoinTeamButton ? (
      <AppButton
        type='button'
        buttonType='primary'
        icon={<HowToRegRounded/>}
        onClick={() => setSelectRoleDialog(true)}
      >
      Отправить запрос в команду
      </AppButton>
    ) : team.isEntried || team.teamStatus === TeamStatusses.Completed ? <span className={style.highlightText}>Зарегистрирована</span>
      : <span className={style.highlightText}>Не зарегистрирована</span>

  const teamName = team.name || `Команда №${team.id}`
  const header = (
    <div className={style.teamCardHeader}>
      <div className={style.teamName}>
        <div>
          {teamName}{' '}

          <span className={style.highlightText}>
          ({team.users.length}/{project.teamCapacity})
          </span>
        </div>
        {joinTeamButton}
      </div>

    </div>
  )

  const teamMember = (user: ShortUserModel) => {
    const userName = user.firstName && user.lastName &&
      `${user.lastName} ${user.firstName} ${user.middleName || ''}, ${user.academicGroup}`

    return (
      <>
        <div className={style.teamMemberHeader}>
          <span className={style.name}>{userName}</span>
          <span className={style.role}>{user.role?.name || 'Роль не выбрана'}</span>
        </div>
        <div className={style.competencies}>
          <h5>Компетенции</h5>
          <CompetenciesList list={user.competencies}/>
        </div>
      </>
    )
  }

  const teamMembersList = team.users.map((user, index) => (
    <div key={user.id}>
      {teamMember(user)}
      {index !== team.users.length - 1 && <AppDivider/>}
    </div>
  ))

  const noTeanMembers = (
    <span className={style.emptyTeam}>
      {project.isAvailable ? 'В команде ещё нет участников' : 'Запись закрыта'}
    </span>
  )

  const teamCardContent = team.users.length > 0
    ? teamMembersList : noTeanMembers

  const catchDialog = (
    <CheckFidelityDialog
      onClose={() => setCatchError(false)}
      open={catchError}
      onMakingAction={() => setCatchError(false)}
      header={'Вы уже имеете заявку в данную команду'}
      additionContent={'Проверьте вкладку "Приглашения в команду".'}
      buttonText={'Закрыть'}
      buttonType={'primary'}
    />
  )

  const rolesList = project.roles.length > 1 ? project.roles.filter(role => role.name !== 'Тимлид').map(role => (
    <AppButton
      key={role.id}
      className={style.roleButton}
      buttonType='secondary'
      type='button'
      onClick={() => {
        onJoinTeam &&
        onJoinTeam(role.name)
        setSelectRoleDialog(false)
      }
      }
    >
      {role.name}
    </AppButton>
  )) : (
    <AppButton
      className={style.roleButton}
      buttonType='secondary'
      type='button'
      onClick={() => {
        onJoinTeam &&
        onJoinTeam('Не указано')
        setSelectRoleDialog(false)
      }
      }
    >
      {'Не указывать роль'}
    </AppButton>
  )

  return (
    <>
      <AppCard header={header}>
        {teamCardContent}
      </AppCard>
      <AppDialog
        open={selectRoleDialog}
        onClose={() => setSelectRoleDialog(false)}
      >
        <AppCard className={style.dialogInnerCard}>
          <h3>Выберите роль в команде</h3>
        </AppCard>
        <AppCard className={style.dialogInnerCard}>
          {rolesList}
        </AppCard>
      </AppDialog>
      {catchDialog}
    </>
  )
}
