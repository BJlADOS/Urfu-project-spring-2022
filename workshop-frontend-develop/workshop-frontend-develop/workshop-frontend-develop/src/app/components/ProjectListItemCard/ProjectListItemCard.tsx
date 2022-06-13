import React from 'react'
import { FlagRounded } from '@material-ui/icons'
import { ShortProjectModel } from 'app/models'
import { project as projectNav } from 'app/nav'

import { AppCard, AppLinkCard } from '../AppCard'

import style from './style.scss'

interface Props {
  project: ShortProjectModel
}

export const ProjectListItemCard: React.FC<Props> = ({
  project,
}) => {
  const header = (
    <div className={style.header}>
      <h3>
        {project.name}
        {project.isPromoted && <FlagRounded />}
      </h3>
      {!project.isAvailable &&
        <span className={style.closed}>Запись закрыта</span>
      }
    </div>
  )

  const projectImage = project.image && (
    <div className={style.imgWrapper}>
      <img
        src={project?.image}
        alt='logo'
      />
    </div>
  )

  return (
    <div className={style.projectRow}>
      <div className={style.largeSection}>
        <AppLinkCard
          header={header}
          className={style.card}
          to={projectNav(project.id)}
        >
          <p>
            {project.purpose}
          </p>
          <div className={style.projectStats}>
            <span>
              Количество участников:
              <b> {project.participantsCount}</b>
            </span>
            <span>
              Сформированные команды:
              <b> {project.fillTeamsCount} / {project.maxTeamCount}</b>
            </span>
          </div>
        </AppLinkCard>
      </div>
      <div className={style.smallSection}>
        <AppCard className={style.card}>
          <div className={style.sideCardContent}>
            <div className={style.text}>
              <h5>Организация</h5>
              <span>{project.organization}</span>
              <h5>Куратор</h5>
              <span>{project.curator}</span>
            </div>
            {projectImage}
          </div>
        </AppCard>
      </div>
    </div>
  )
}
