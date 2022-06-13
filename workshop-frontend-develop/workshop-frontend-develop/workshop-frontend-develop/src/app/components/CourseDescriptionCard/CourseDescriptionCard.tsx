import React from 'react'
import cls from 'classnames'
import { CourseModel } from 'app/models/CourseModel'

import { AppCard } from '../AppCard'
import { AppDivider } from '../AppDivider'

import style from './style.scss'

interface Props {
  course: CourseModel
  hideCourseStatus?: boolean
  className?: string
}

export const CourseDescriptionCard: React.FC<Props> = ({
  course,
  hideCourseStatus,
  className,
}) => {
  const header = (
    <div className={style.header}>
      <h3>Описание курса</h3>
      {!hideCourseStatus && !course.isAvailable &&
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
      <p>{course.description}</p>
      <AppDivider />
      <h4>Цель</h4>
      <p>{course.goal}</p>
      <AppDivider />
      {/* <h4>Итоговый продукт</h4>
      <p>{course.result}</p> */}
    </AppCard>
  )
}
