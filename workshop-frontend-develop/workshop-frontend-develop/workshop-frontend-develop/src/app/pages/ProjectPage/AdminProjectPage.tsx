import React, { useEffect, useState } from 'react'
import { useHistory, useParams } from 'react-router-dom'
import {
  AddCircleOutlineRounded,
  EditRounded,
  FlagRounded,
  HighlightOffRounded,
  RemoveCircleOutlineRounded,
  SaveRounded,
} from '@material-ui/icons'
import { AppCard } from 'app/components/AppCard'
import { ProjectDescriptionCard, ProjectDescriptionEditCard } from 'app/components/ProjectDescriptionCard'
import { ProjectInfoCard, ProjectInfoEditCard } from 'app/components/ProjectInfoCard'
import { AppLoadingSpinner } from 'app/components/AppLoadingSpinner'
import { admin, projects } from 'app/provider'
import { projects as projectsPath } from 'app/nav'
import { ProjectModel, ShortProjectModel, ShortTeamModel } from 'app/models'
import { AdminProjectTeamCard } from 'app/components/ProjectTeamCard'
import { AppButton } from 'app/components/AppButton'
import { AddFreeStudentDialog } from 'app/components/AddFreeStudentDialog'
import { AppInput } from 'app/components/AppInput'
import { EditProjectRolesDialog } from 'app/components/EditProjectRolesDialog'

import style from './style.scss'

export const AdminProjectPage: React.FC = () => {
  const { projectId } = useParams<{ projectId: string }>()
  const [project, setProject] = useState<ProjectModel>()
  const [addUserDialog, setAddUserDialog] = useState(false)
  const [editRolesDialog, setEditRolesDialog] = useState(false)
  const [selectedTeamId, setSelectedTeamId] = useState(0)
  const [edit, setEdit] = useState(false)

  const history = useHistory()

  const fetchProject = () => projects.getProject(Number(projectId))
    .then(res => setProject(res.data))

  useEffect(() => {
    fetchProject()
  }, [])

  if (!project) {
    return <AppLoadingSpinner/>
  }

  const handleAddTeam = () => {
    admin.addNewTeam(project.id)
      .then(fetchProject)
  }

  const handleDeleteTeam = (teamId: number) => {
    admin.deleteTeam(teamId)
      .then(fetchProject)
  }

  const handleTeamMemberDelete = (userId: number, teamId: number) => {
    admin.deleteUserFromTeam(userId, teamId)
      .then(fetchProject)
  }

  const handleTeamMemberAdd = (teamId: number) => {
    setSelectedTeamId(teamId)
    setAddUserDialog(true)
  }

  const handleDialogClose = () => {
    setAddUserDialog(false)
    setSelectedTeamId(0)
  }

  const handleProjectEditSave = (project: ProjectModel, limit?: number) => {
    project.maxTeamCount = limit !== undefined ? project.maxTeamCount + limit : project.maxTeamCount
    admin.updateProject({
      ...project,
      competenciesIds: project.competencies.map(c => c.id),
      lifeScenarioId: project.lifeScenario.id,
      keyTechnologyId: project.keyTechnology.id,
    })
      .then(() => setEdit(false))
      .then(fetchProject)
  }

  const handleProjectDelete = (project: ShortProjectModel) => {
    admin.deleteProject(project.id)
      .then(() => history.push(projectsPath()))
  }

  const editButtons = edit ? (
    <div className={style.editButtons}>
      <AppButton
        type='button'
        icon={<SaveRounded/>}
        onClick={() => handleProjectEditSave(project)}
      >
        Сохранить
      </AppButton>
      <AppButton
        type='button'
        buttonType='danger'
        icon={<HighlightOffRounded/>}
        onClick={() => fetchProject().then(() => setEdit(false))}
      />
    </div>
  ) : (
    <AppButton
      type='button'
      icon={<EditRounded/>}
      onClick={() => setEdit(true)}
    >
      Редактировать
    </AppButton>
  )

  const headerCard = (

    <AppCard
      className={style.projectNameCard}
      contentClassName={style.adminFilledTeams}
    >
      <span className={style.spanProjectNameCard}>Id проекта: {projectId}</span> <br/>
      {edit
        ? <AppInput
          value={project.name}
          className={style.nameInput}
          onChange={(e) => setProject({
            ...project,
            name: e.target.value,
          })}
        />
        : <h2>
          {project.name}
          {project.isPromoted && <FlagRounded/>}
        </h2>}

      {editButtons}
    </AppCard>
  )

  const projectInfoCard = (
    <div className={style.card}>
      {edit
        ? <ProjectInfoEditCard
          project={project}
          onChange={(value) => setProject({
            ...project,
            ...value,
          })}
        />
        : <ProjectInfoCard project={project}/>
      }
    </div>
  )

  const projectDescriptionCard = (
    <div className={style.card}>
      {edit
        ? <ProjectDescriptionEditCard
          project={project}
          onChange={(value) => setProject({
            ...project,
            ...value,
          })}
          onDelete={handleProjectDelete}
        />
        : <ProjectDescriptionCard project={project}/>
      }
    </div>
  )

  const teamsCountCard = (
    <div className={style.card}>
      <AppCard contentClassName={style.adminFilledTeams}>
        <h3>Сформировано команд{' '}
          <span className={style.highlightText}>
            {project.fillTeamsCount}/{project.maxTeamCount}
          </span>
        </h3>
        <AppButton
          type='button'
          icon={<AddCircleOutlineRounded/>}
          onClick={handleAddTeam}
        >
          Добавить команду
        </AppButton>
      </AppCard>
    </div>
  )

  const teamsCards = (project.teams as ShortTeamModel[]).map(team => (
    <div
      key={team.id}
      className={style.card}
    >
      <AdminProjectTeamCard
        team={team}
        project={project}
        onTeamMemberAdd={handleTeamMemberAdd}
        onTeamDelete={handleDeleteTeam}
        onTeamMemberDelete={handleTeamMemberDelete}
      />
    </div>
  ))

  const pinProjectButton = project.isPromoted ? (
    <AppButton
      type='button'
      buttonType='secondary'
      onClick={() => {
        setProject({
          ...project,
          isPromoted: false,
        })
        handleProjectEditSave({
          ...project,
          isPromoted: false,
        })
      }}
    >
      Открепить
    </AppButton>
  ) : (
    <AppButton
      type='button'
      buttonType='secondary'
      onClick={() => handleProjectEditSave({
        ...project,
        isPromoted: true,
      })}
    >
      Закрепить
    </AppButton>
  )

  const activateProjectButton = project.isAvailable ? (
    <AppButton
      type='button'
      buttonType='secondary'
      onClick={() => handleProjectEditSave({
        ...project,
        isAvailable: false,
      })}
    >
      Закрыть
    </AppButton>
  ) : (
    <AppButton
      type='button'
      buttonType='secondary'
      onClick={() => handleProjectEditSave({
        ...project,
        isAvailable: true,
      })}
    >
      Открыть
    </AppButton>
  )

  const adminButtons = (
    <div className={style.adminButtons}>
      {pinProjectButton}
      {activateProjectButton}
    </div>
  )
  const limitButtons = (
    <AppCard
      className={style.adminChangeLimitProject}
      header={'Изменение лимита команд'}
    >

      <AppButton
        type='button'
        icon={<AddCircleOutlineRounded/>}
        onClick={() => handleProjectEditSave({ ...project }, 1)}
      >
        Добавить лимит
      </AppButton>
      <AppButton
        type='button'
        disabled={project.maxTeamCount === 0}
        icon={<RemoveCircleOutlineRounded/>}
        onClick={() => handleProjectEditSave({ ...project }, -1)}
      >
        Убавить лимит
      </AppButton>

    </AppCard>
  )

  return (
    <>
      {headerCard}
      <div className={style.pageContent}>
        <div className={style.largeColumn}>
          {projectDescriptionCard}
          {limitButtons}
          {teamsCountCard}
          {teamsCards}
        </div>
        <div className={style.smallColumn}>
          {projectInfoCard}
          {adminButtons}
          <AppButton
            type='button'
            buttonType='secondary'
            className={style.editRolesButton}
            onClick={() => setEditRolesDialog(true)}
          >
            Редактировать роли
          </AppButton>
        </div>
      </div>
      <AddFreeStudentDialog
        open={addUserDialog}
        onAddSideEffect={fetchProject}
        onClose={handleDialogClose}
        teamId={selectedTeamId}
        roles={project.roles}
      />
      <EditProjectRolesDialog
        open={editRolesDialog}
        projectId={project.id}
        onEditSideEffect={fetchProject}
        onClose={() => setEditRolesDialog(false)}
        roles={project.roles}
      />
    </>
  )
}
