import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { FlagRounded, HowToRegRounded } from '@material-ui/icons'
import { AppCard } from 'app/components/AppCard'
import { ProjectDescriptionCard } from 'app/components/ProjectDescriptionCard'
import { ProjectInfoCard } from 'app/components/ProjectInfoCard'
import { AppLoadingSpinner } from 'app/components/AppLoadingSpinner'
import { projects, teamlead, user } from 'app/provider'
import { ProjectModel, ShortTeamModel, UserModel } from 'app/models'
import { TeamleadProjectTeamCard } from 'app/components/ProjectTeamCard'
import { ProfileActions } from 'app/actions'
import { RootState } from 'app/reducers'
import { connect } from 'react-redux'
import { AppButton } from 'app/components/AppButton'

import style from './style.scss'

const mapStateToProps = (state: RootState) => ({ profile: state.profile })

const mapDispatchToProps = { setUserProfile: (user: UserModel) => ProfileActions.setUserProfile(user) }

type StateProps = ReturnType<typeof mapStateToProps>
type DispatchProps = typeof mapDispatchToProps

interface Props extends StateProps, DispatchProps {
}

export const TeamleadProjectPageComponent: React.FC<Props> = ({
  profile,
  setUserProfile,
}) => {
  const { projectId } = useParams<{ projectId: string }>()
  const [project, setProject] = useState<ProjectModel>()

  const fetchProject = () => {
    projects.getProject(Number(projectId))
      .then(res => setProject(res.data))
  }

  const fetchUser = () => user.getUserProfile()
    .then(res => setUserProfile(res.data))

  useEffect(() => {
    fetchProject()
  }, [])

  if (!project) {
    return <AppLoadingSpinner/>
  }

  const handleAddTeam = () => {
    teamlead.addNewTeam(Number(projectId))
      .then(fetchProject)
      .then(fetchUser)
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
      <TeamleadProjectTeamCard
        team={team}
        project={project}
      />
    </div>
  ))

  const addTeamTeamlead = (project.isAvailable && !profile.profile?.team)
    ? (
      <AppButton
        type='button'
        buttonType='primary'
        icon={<HowToRegRounded/>}
        onClick={() => handleAddTeam()}
      >
        Создать команду
      </AppButton>
    )
    : (
      <div>

      </div>
    )

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
          {addTeamTeamlead}
        </div>
        <div className={style.smallColumn}>
          {projectInfoCard}
        </div>
      </div>
    </>
  )
}

export const TeamleadProjectPage: React.FC = connect(
  mapStateToProps,
  mapDispatchToProps,
)(TeamleadProjectPageComponent)
