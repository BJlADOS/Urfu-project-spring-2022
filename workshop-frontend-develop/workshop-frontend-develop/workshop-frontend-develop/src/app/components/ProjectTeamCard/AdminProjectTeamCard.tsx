import React from 'react'
import { DeleteOutlineRounded, FindInPageRounded, PersonAddRounded } from '@material-ui/icons'
import { ProjectModel, ShortTeamModel, ShortUserModel, TeamStatusses } from 'app/models'
import { team as teamRoute } from 'app/nav'

import { AppCard } from '../AppCard'
import { AppDivider } from '../AppDivider'
import { CompetenciesList } from '../CompetenciesList'
import { AppButton } from '../AppButton'

import style from './style.scss'

interface Props {
  team: ShortTeamModel
  project: ProjectModel
  onTeamDelete: (teamId: number) => void
  onTeamMemberAdd: (teamId: number) => void
  onTeamMemberDelete: (userId: number, teamId: number) => void
}

export const AdminProjectTeamCard: React.FC<Props> = ({
  team,
  project,
  onTeamDelete,
  onTeamMemberAdd,
  onTeamMemberDelete,
}) => {
  const teamName = team.name || `Команда №${team.id}`
  const header = (
    <div className={style.teamCardHeader}>
      <div className={style.teamName}>
        <div>
          {teamName}
          <span className={style.highlightText}>
          ({team.users.length}/{project.teamCapacity})
          </span>
          <AppButton
            type='button'
            buttonType='transparent'
            to={teamRoute(team.id)}
            icon={<FindInPageRounded/>}
          />
        </div>
        {team.isEntried || team.teamStatus === TeamStatusses.Completed ? <span className={style.highlightText}>Зарегистрирована</span>
          : <span className={style.highlightText}>Не зарегистрирована</span>}
        <AppButton
          type='button'
          buttonType='danger'
          icon={<DeleteOutlineRounded/>}
          onClick={() => onTeamDelete(team.id)}
        >
          Удалить
        </AppButton>
      </div>
      {/* <AppButton */}
      {/*    type={'button'} */}
      {/*    buttonType={'secondary'} */}
      {/*    onClick={() => onTeamRegister(team.id)} */}
      {/* ></AppButton> */}
    </div>
  )

  const teamMember = (user: ShortUserModel) => {
    const userName = user.firstName && user.lastName &&
      `${user.lastName} ${user.firstName} ${user.middleName || ''}, ${user.academicGroup}`

    return (
      <>
        <div className={style.teamMemberHeader}>
          <span className={style.name}>{userName}</span>
          <AppButton
            type='button'
            buttonType='danger'
            icon={<DeleteOutlineRounded/>}
            onClick={() => onTeamMemberDelete(user.id, team.id)}
          />
        </div>
        <span className={style.teamMemberRole}>{user.role?.name || 'Роль не выбрана'}</span>
        <div className={style.competencies}>
          <h5>Компетенции</h5>
          <CompetenciesList list={user.competencies}/>
        </div>
      </>
    )
  }

  const teamMembersList = team.users.map(user => (
    <div key={user.id}>
      {teamMember(user)}
      <AppDivider/>
    </div>
  ))

  return (
    <AppCard header={header}>
      {teamMembersList}
      <div className={style.addMember}>
        <AppButton
          type='button'
          icon={<PersonAddRounded/>}
          onClick={() => onTeamMemberAdd(team.id)}
        >
          Добавить участников
        </AppButton>
      </div>
    </AppCard>
  )
}
