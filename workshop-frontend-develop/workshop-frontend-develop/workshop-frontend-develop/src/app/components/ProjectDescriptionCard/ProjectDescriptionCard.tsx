import React from 'react'
import cls from 'classnames'
import { ShortProjectModel } from 'app/models'

import { AppCard } from '../AppCard'
import { AppDivider } from '../AppDivider'

import style from './style.scss'

interface Props {
  project: ShortProjectModel
  hideProjectStatus?: boolean
  className?: string
}

export const ProjectDescriptionCard: React.FC<Props> = ({
  project,
  hideProjectStatus,
  className,
}) => {
  const header = (
    <div className={style.header}>
      <h3>Описание проекта</h3>
      {!hideProjectStatus && !project.isAvailable &&
        <span className={style.closed}>Запись закрыта</span>
      }
    </div>
  )

  const cardClassName = cls(style.card, className)

  return (
    <AppCard
      header={header}
      className={cardClassName}
    >
      <p>{project.description}</p>
      <AppDivider />
      <h4>Цель</h4>
      <p>{project.purpose}</p>
      <AppDivider />
      <h4>Итоговый продукт</h4>
      <p>{project.result}</p>
    </AppCard>
  )
}
