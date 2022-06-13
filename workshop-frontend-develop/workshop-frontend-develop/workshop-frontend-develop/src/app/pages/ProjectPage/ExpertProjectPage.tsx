import React, {useEffect, useState} from 'react'
import {useParams} from 'react-router-dom'
import {FlagRounded} from '@material-ui/icons'
import {AppCard} from 'app/components/AppCard'
import {ProjectDescriptionCard} from 'app/components/ProjectDescriptionCard'
import {ProjectInfoCard} from 'app/components/ProjectInfoCard'
import {AppLoadingSpinner} from 'app/components/AppLoadingSpinner'
import {projects} from 'app/provider'
import {ProjectModel, ShortTeamModel} from 'app/models'
import {ProjectTeamCard} from 'app/components/ProjectTeamCard'

import style from './style.scss'

export const ExpertProjectPage: React.FC = () => {
    const {projectId} = useParams<{ projectId: string }>()
    const [project, setProject] = useState<ProjectModel>()

    const fetchProject = () => projects.getProject(Number(projectId))
        .then(res => setProject(res.data))

    useEffect(() => {
        fetchProject()
    }, [])

    if (!project) {
        return <AppLoadingSpinner/>
    }

    const headerCard = (
        <AppCard className={style.projectNameCard}>
            <h2>
                {project.name}
                {project.isPromoted && <FlagRounded/>}
            </h2>
        </AppCard>
    )

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
            <AppCard contentClassName={style.adminFilledTeams}>
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
            />
        </div>
    ))

    return (
        <>
            {headerCard}
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
        </>
    )
}
