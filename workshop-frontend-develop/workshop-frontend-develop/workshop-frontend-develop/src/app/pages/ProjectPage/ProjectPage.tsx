import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { FlagRounded } from '@material-ui/icons'
import { AppCard } from 'app/components/AppCard'
import { ProjectDescriptionCard } from 'app/components/ProjectDescriptionCard'
import { ProjectInfoCard } from 'app/components/ProjectInfoCard'
import { AppLoadingSpinner } from 'app/components/AppLoadingSpinner'
import { projects, requestProposal } from 'app/provider'
import { ProjectModel, ShortTeamModel, UserModel } from 'app/models'
import { ProjectTeamCard } from 'app/components/ProjectTeamCard'
import { ProfileActions } from 'app/actions'
import { RootState } from 'app/reducers'
import { connect } from 'react-redux'
import { CheckFidelityDialog } from 'app/components/CheckFidelityDialog'

import style from './style.scss'

const mapStateToProps = (state: RootState) => ({ profile: state.profile })

const mapDispatchToProps = { setUserProfile: (user: UserModel) => ProfileActions.setUserProfile(user) }

type StateProps = ReturnType<typeof mapStateToProps>
type DispatchProps = typeof mapDispatchToProps

interface Props extends StateProps, DispatchProps {
}

const ProjectPageComponent: React.FC<Props> = ({
  profile,
}) => {
  const { projectId } = useParams<{ projectId: string }>()
  const [project, setProject] = useState<ProjectModel>()

  const [catchError, setCatchError] = useState(false)

  const fetchProject = () => {
    projects.getProject(Number(projectId))
      .then(res => setProject(res.data))
  }

  useEffect(() => {
    fetchProject()
  }, [])

  if (!project) {
    return <AppLoadingSpinner/>
  }

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

  const handleCreateRequestInTeam = (teamId: number, projectId: number, role: string) => {
    requestProposal.CreateRequestInTeam(teamId, projectId, role)
      .catch(() => setCatchError(true))
  }

  const projectInfoCard = (
    <div className={style.card}>
      <ProjectInfoCard project={project}/>
    </div>
  )

  const projectDescriptionCard = (
    <div className={style.card}>
      <ProjectDescriptionCard project={project}/>
    </div>
  )

  const teamsCountCard = (
    <div className={style.card}>
      <AppCard>
        <h3>Сформировано команд{' '}
          <span className={style.highlightText}>
            {project.fillTeamsCount}/{project.maxTeamCount}
          </span>
        </h3>
      </AppCard>
    </div>
  )

  const teamsCards = (project.teams as ShortTeamModel[]).map(team => (
    <div
      key={team.id}
      className={style.card}
    >
      <ProjectTeamCard
        team={team}
        project={project}
        onJoinTeam={(value) => handleCreateRequestInTeam(team.id, project?.id, value)}
        hideJoinTeamButton={!!profile.profile?.team}
      />
    </div>
  ))

  return (
    <>
      <AppCard className={style.projectNameCard}>
        <h2>
          {project.name}
          {project.isPromoted && <FlagRounded/>}
        </h2>
      </AppCard>
      <div className={style.pageContent}>
        <div className={style.largeColumn}>
          {projectDescriptionCard}
          {teamsCountCard}
          {teamsCards}
        </div>
        <div className={style.smallColumn}>
          {projectInfoCard}
        </div>
      </div>
      {catchDialog}
    </>
  )
}

export const ProjectPage: React.FC = connect(
  mapStateToProps,
  mapDispatchToProps,
)(ProjectPageComponent)
