import React, {useEffect, useState} from 'react'
import {useParams} from 'react-router-dom'
import cls from 'classnames'
import {FindInPageRounded} from '@material-ui/icons'
import {expert, projects} from 'app/provider'
import {AppCard} from 'app/components/AppCard'
import {AppButton} from 'app/components/AppButton'
import {CompetencyModel, CompetencyType, ProjectModel, TeamModel, TeamReviewModel, UserModel} from 'app/models'
import {AppLoadingSpinner} from 'app/components/AppLoadingSpinner'
import {project as projectNav} from 'app/nav'
import {TeamMemberCard} from 'app/components/TeamMemberCard'
import {ProjectDescriptionCard} from 'app/components/ProjectDescriptionCard'
import {AppDivider} from 'app/components/AppDivider'

import style from './style.scss'

const reviewMap: Array<{ key: keyof TeamReviewModel, title: string, description?: string }> = [
    {
        key: 'goalsAndTasks',
        title: 'Формулировка целей и задач',
    },
    {
        key: 'solution',
        title: 'Обоснование выбранного решения',
    },
    {
        key: 'presentation',
        title: 'Презентация проекта',
    },
    {
        key: 'technical',
        title: 'Техническая проработанность',
    },
    {
        key: 'result',
        title: 'Соответствие результата поставленной цели',
    },
    {
        key: 'knowledge',
        title: 'Знание предметной области',
    },
]

export const TeamReviewPage: React.FC = () => {
    const {teamId} = useParams<{ teamId: string }>()
    const [team, setTeam] = useState<TeamModel>()
    const [project, setProject] = useState<ProjectModel>()

    const fetchTeamAndProject = () => {
        fetchTeam()
            .then(team => projects.getProject(team.project.id))
            .then(res => setProject(res.data))
    }

    const fetchTeam = () => expert.getTeam(Number(teamId))
        .then(res => {
            setTeam(res.data)
            return res.data
        })

    useEffect(() => {
        fetchTeamAndProject()
    }, [])

    if (!team) {
        return <AppLoadingSpinner/>
    }

    const teamName = team.name || `Команда №${team.id}`
    const header = (
        <AppCard
            className={style.card}
            contentClassName={style.teamNameCard}
        >
            <h2>{teamName} — <span className={style.projectName}>{team.project.name}</span></h2>
            <AppButton
                type='button'
                buttonType='transparent'
                to={projectNav(team.project.id)}
                icon={<FindInPageRounded/>}
            />
        </AppCard>
    )

    const projectDescriptionCard = project && (
        <ProjectDescriptionCard
            project={project}
            className={style.card}
            hideProjectStatus
        />
    )

    const information = (
        <>
            <h4>Информация</h4>
            <div className={style.infoLine}>
                <span className={style.title}>Организация</span>
                <span className={style.info}>{project?.organization}</span>
            </div>
            <div className={style.infoLine}>
                <span className={style.title}>Куратор</span>
                <span className={style.info}>{project?.curator}</span>
            </div>
            <AppDivider/>
        </>
    )

    const trajectory = (
        <>
            <h4>Траектория</h4>
            <div className={style.infoLine}>
                <span className={style.title}>Жизненный сценарий</span>
                <span className={style.info}>{project?.lifeScenario?.name}</span>
            </div>
            <div className={style.infoLine}>
                <span className={style.title}>Ключевая технология</span>
                <span className={style.info}>{project?.keyTechnology?.name}</span>
            </div>
        </>
    )

    const projectInfoCard = (
        <AppCard
            className={style.infoCard}
            contentClassName={style.infoCardInner}
        >
            {information}
            {trajectory}
        </AppCard>
    )

    const projectDescription = project ? (
        <div className={style.projectDescription}>
            <div className={style.largeColumn}>
                {projectDescriptionCard}
            </div>
            <div className={style.smallColumn}>
                {projectInfoCard}
            </div>
        </div>
    ) : (
        <div className={style.projectDescription}>
            <AppLoadingSpinner/>
        </div>
    )

    const teamMembers = (team.users as UserModel[]).map(user => (
        <TeamMemberCard
            key={user.id}
            user={user}
            hideContacts
            hideDesiredCompetencies
        />
    ))

    const curatorReview = (team.mark || team.comment) && (
        <AppCard
            className={style.card}
            contentClassName={style.curatorReview}
            header='Отзыв куратора'
        >
            {team.mark && (
                <div className={style.markSection}>
          <span className={style.mark}>
            {team.mark}
          </span>
                    Оценка
                </div>
            )}
            {team.comment && <blockquote>{team.comment}</blockquote>}
        </AppCard>
    )

    const reviewButtons = (key?: keyof TeamReviewModel) => [0, 1, 2, 3, 4].map(index => (
        <AppButton
            key={index}
            type='button'
            buttonType={team.review && key && team.review[key] === index ? 'primary' : 'secondary'}
            className={style.markButton}
            onClick={() => {
                if (key) {
                    expert.updateTeamReview(team.id, {
                        ...team.review,
                        [key]: team.review && team.review[key] === index ? undefined : index,
                    }).then(fetchTeam)
                }
            }}
        >
            {index}
        </AppButton>
    ))

    const reviewBlock = reviewMap.map(({key, title, description}, index) => (
        <React.Fragment key={key}>
            <div className={style.reviewBlock}>
                <div className={style.reviewText}>
          <span className={style.header}>
            {title}
          </span>
                    {description &&
                        <span className={style.reviewDescription}>
              {description}
            </span>
                    }
                </div>
                <div className={style.reviewButtons}>
                    {reviewButtons(key)}
                </div>
            </div>
            {index !== reviewMap.length - 1 && <AppDivider/>}
        </React.Fragment>
    ))

    const reviewCard = (
        <AppCard
            className={style.card}
            header='Оценки для команды'
        >
            {reviewBlock}
        </AppCard>
    )

    const competencyReviewButtons = (item: CompetencyModel) => {
        const reviewItem = team.competencyReview?.find(c => c.id === item.id)
        const buttonTypeEnding = item.competencyType === CompetencyType.SoftSkill ? 'Accent' : ''
        const buttonType = (index: number) => reviewItem?.mark === index ? 'primary' : 'secondary'

        return [0, 1].map(index => (
            <AppButton
                key={index}
                buttonType={(buttonType(index) + buttonTypeEnding) as any}
                className={style.markButton}
                onClick={() => {
                    expert.updateTeamCompetencyReview(team.id, {
                        competencyId: item.id,
                        mark: index,
                    }).then(fetchTeam)
                }}
            >
                {index}
            </AppButton>
        ))
    }

    const competencyReviewCards = project && project.competencies.map((competency) => (
        <AppCard
            key={competency.id}
            className={style.competencyCard}
            contentClassName={style.competencyCardInner}
        >
            <h5
                className={style.name}
                title={competency.name}
            >
                {competency.name}
            </h5>
            <div className={cls(style.reviewButtons, {
                [style.hardSkill]: competency.competencyType === CompetencyType.HardSkill,
                [style.softSkill]: competency.competencyType === CompetencyType.SoftSkill,
            })}
            >
                {competencyReviewButtons(competency)}
            </div>
        </AppCard>
    ))

    const competenciesReviewSection = (
        <>
            <AppCard className={style.card}>
                <h3>Оценка компетенций</h3>
            </AppCard>
            <div className={style.competenciesSection}>
                {competencyReviewCards}
            </div>
        </>
    )

    return (
        <>
            {header}
            {projectDescription}
            <div className={style.teamMembersContainer}>
                {teamMembers}
            </div>
            {curatorReview}
            {reviewCard}
            {competenciesReviewSection}
        </>
    )
}
