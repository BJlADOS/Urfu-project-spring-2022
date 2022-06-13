import React, { useEffect, useState } from 'react'
import { useHistory, useParams } from 'react-router-dom'
import { AddCircleOutlineRounded, FindInPageRounded, LockOpenRounded } from '@material-ui/icons'
import { admin, expert } from 'app/provider'
import { AppCard } from 'app/components/AppCard'
import { AppButton } from 'app/components/AppButton'
import { TeamMemberAdminCard } from 'app/components/TeamMemberCard'
import { TeamModel, TeamStatusses, UserModel } from 'app/models'
import { AppLoadingSpinner } from 'app/components/AppLoadingSpinner'
import { AddFreeStudentDialog } from 'app/components/AddFreeStudentDialog'
import { project, teams } from 'app/nav'
import { AppDialog } from 'app/components/AppDialog'
import { AppInput } from 'app/components/AppInput'

import style from './style.scss'

export const TeamPage: React.FC = () => {
  const { teamId } = useParams<{ teamId: string }>()
  const [team, setTeam] = useState<TeamModel>()
  const [addUserDialog, setAddUserDialog] = useState(false)
  const [changeDialog, setChangeDialog] = useState(false)
  const [projectId, setProjectId] = useState('')

  const history = useHistory()

  const fetchTeam = () => {
    expert.getTeam(Number(teamId))
      .then(res => setTeam(res.data))
  }

  useEffect(() => {
    fetchTeam()
  }, [])

  if (!team) {
    return <AppLoadingSpinner/>
  }

  const handleUserDelete = (userId: number) => {
    admin.deleteUserFromTeam(userId, Number(teamId))
      .then(fetchTeam)
  }

  const handleUserRoleChange = (userId: number, roleId: number) => {
    admin.changeUserRole(userId, roleId).then(fetchTeam)
  }

  const updateTeamEntried = () => {
    admin.updateTeamEntried(Number(teamId))
      .then(fetchTeam)
  }

  const handleChangeProjectForTeam = () => {
    if (projectId !== '') {
      admin.updateTeamProject(Number(projectId), Number(teamId))
        .then(() => history.push(teams()))
    }
  }
  const changeProjectButton = (team.teamStatus === TeamStatusses.Completed || team.isEntried) && (
    <AppButton
      type='button'
      buttonType='secondary'
      icon={<LockOpenRounded/>}
      onClick={() => setChangeDialog(true)}
    >
            Сменить проект
    </AppButton>
  )

  const changeProjectDialog = (
    <>
      <AppDialog
        open={changeDialog}
        onClose={() => setChangeDialog(false)}
        className={style.dialogChangeProject}
      >
        <h3>Введите ID проекта для перезаписи команды</h3>
        <div className={style.teamChangeProject}>
          <AppInput
            className={style.input}
            value={projectId}
            type={'number'}
            onChange={(e) => !isNaN(+e.target.value) && setProjectId(e.target.value)}
            placeholder={'Введите id нового проекта'}
          />
          <AppButton
            type={'button'}
            buttonType={'secondary'}
            onClick={handleChangeProjectForTeam}
          >
                        Изменить проект команды
          </AppButton>
        </div>

      </AppDialog>
    </>
  )

  const teamMembers = (team.users as UserModel[]).map(user => (
    <TeamMemberAdminCard
      key={user.id}
      user={user}
      roles={team.project.roles}
      onRoleChange={handleUserRoleChange}
      onDelete={handleUserDelete}
    />
  ))

  const teamName = team.name || `Команда №${team.id}`
  const header = (
    <>
      <h2>{teamName} — <span className={style.highlight}>{team.project.name}</span></h2>
      <AppButton
        type='button'
        buttonType='transparent'
        to={project(team.project.id)}
        icon={<FindInPageRounded/>}
      />
    </>
  )

  const handleDialogOpen = () => {
    setAddUserDialog(true)
  }

  const handleDialogClose = () => {
    setAddUserDialog(false)
  }

  const updateEntriedButton = (!team.isEntried || team.teamStatus !== TeamStatusses.Completed) && (
    <AppButton
      type={'button'}
      buttonType={'secondary'}
      onClick={updateTeamEntried}
    >
            Зарегистрировать команду
    </AppButton>
  )

  return (
    <>
      <AppCard contentClassName={style.teamNameCard}>
        {header}
      </AppCard>
      <div className={style.teamMembersContainer}>
        {teamMembers}
      </div>
      <div className={style.buttonsSection}>
        <AppButton
          type='button'
          icon={<AddCircleOutlineRounded/>}
          onClick={handleDialogOpen}
        >
                    Добавить участника
        </AppButton>
        {changeProjectButton}
        {updateEntriedButton}
      </div>
      {changeProjectDialog}
      <AddFreeStudentDialog
        open={addUserDialog}
        onAddSideEffect={fetchTeam}
        onClose={handleDialogClose}
        teamId={team.id}
        roles={team.project.roles}
      />
    </>
  )
}
