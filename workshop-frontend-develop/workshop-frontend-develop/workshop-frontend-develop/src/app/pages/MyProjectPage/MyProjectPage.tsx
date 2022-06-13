import React from 'react'
import { connect } from 'react-redux'
import { RootState } from 'app/reducers'
import { AppCard } from 'app/components/AppCard'
import { ProjectDescriptionCard } from 'app/components/ProjectDescriptionCard'
import { ProjectInfoCard } from 'app/components/ProjectInfoCard'

import style from './style.scss'

const mapStateToProps = (state: RootState) => ({ profile: state.profile })

type StateProps = ReturnType<typeof mapStateToProps>

type Props = StateProps

const MyProjectPageComponent: React.FC<Props> = ({
  profile,
}) => {
  const project = profile.profile?.team?.project

  const projectInfoCard = project &&
    <div className={style.card}>
      <ProjectInfoCard project={project}/>
    </div>

  const projectDescriptionCard = project &&
    <div className={style.card}>
      <ProjectDescriptionCard project={project}/>
    </div>

  const futureStepsCard = (
    <div className={style.card}>
      <AppCard header='Дальнейшие шаги для тимлида'>
        <div className={style.rectangle}>
          <ol>
            <li><span>Дождитесь открытия регистрации команд на проекты заказчиков</span></li>
            <li><span>Примите участие в проекте на странице вашей команды</span></li>
            <li><span>Свяжитесь со своими сокомандниками.</span></li>
            <li><span>Свяжитесь с куратором проекта.</span></li>
            <li><span>Начните работу над проектом.</span></li>
          </ol>
        </div>
      </AppCard>
    </div>
  )

  const link = `https://прокомпетенции.рф/projects/${project?.id}`

  return (
    <>
      <AppCard className={style.projectNameCard}>
        <h2>{project?.name}</h2>
      </AppCard>
      <div className={style.pageContent}>
        <div className={style.largeColumn}>
          {projectDescriptionCard}
          {futureStepsCard}
        </div>
        <div className={style.smallColumn}>
          {projectInfoCard}
          <div className={style.projectLink}>
            <h4>
              Ссылка на проект
            </h4>
            <a
              href={link}
              target='_blank'
              rel='noopener noreferrer'
            >
              {link}
            </a>
          </div>
        </div>
      </div>
    </>
  )
}

export const MyProjectPage: React.FC = connect(
  mapStateToProps,
)(MyProjectPageComponent)
