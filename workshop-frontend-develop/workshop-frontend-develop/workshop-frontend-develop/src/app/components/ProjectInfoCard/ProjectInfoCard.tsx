import React from 'react'
import { ShortProjectModel } from 'app/models'

import { AppCard } from '../AppCard'
import { AppDivider } from '../AppDivider'
import { CompetenciesList } from '../CompetenciesList'

import style from './style.scss'

interface Props {
  project: ShortProjectModel
}

export const ProjectInfoCard: React.FC<Props> = ({
  project,
}) => {
  const projectInfoHeader = project?.image && (
    <div className={style.imgWrapper}>
      <img
        src={project?.image}
        alt='logo'
      />
    </div>
  )

  const competencies = project.competencies.length > 0 && (
    <>
      <AppDivider />
      <h4>Компетенции</h4>
      <CompetenciesList list={project.competencies} />
    </>
  )

  return (
    <AppCard
      header={projectInfoHeader}
      className={style.card}
    >
      <h4>Информация</h4>
      <div className={style.infoLine}>
        <span className={style.title}>Организация</span>
        <span className={style.info}>{project.organization}</span>
      </div>
      <div className={style.infoLine}>
        <span className={style.title}>Куратор</span>
        <span className={style.info}>{project.curator}</span>
      </div>
      <AppDivider />
      <h4>Контакты</h4>
      <div className={style.infoLine}>
        <p>{project?.contacts}</p>
      </div>
      <AppDivider />
      <h4>Траектория</h4>
      <div className={style.infoLine}>
        <span className={style.title}>Жизненный сценарий</span>
        <span className={style.info}>{project.lifeScenario.name}</span>
      </div>
      <div className={style.infoLine}>
        <span className={style.title}>Ключевая технология</span>
        <span className={style.info}>{project.keyTechnology.name}</span>
      </div>
      {competencies}
    </AppCard>
  )
}
