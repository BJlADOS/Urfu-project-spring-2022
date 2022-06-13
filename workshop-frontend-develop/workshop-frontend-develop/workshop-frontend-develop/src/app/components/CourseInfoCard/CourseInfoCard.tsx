import React from 'react'
import { CourseModel } from 'app/models/CourseModel'

import { AppCard } from '../AppCard'
import { AppDivider } from '../AppDivider'

import style from './style.scss'

interface Props {
  course: CourseModel
}

export const CourseInfoCard: React.FC<Props> = ({
  course,
}) => {
  const projectInfoHeader = course?.image && (
    <div className={style.imgWrapper}>
      <img
        src={course?.image}
        alt='logo'
      />
    </div>
  )

  return (
    <AppCard
      header={projectInfoHeader}
      className={style.card}
    >
      <h4>Информация</h4>
      <div className={style.infoLine}>
        <span className={style.title}>Организатор</span>
        <span className={style.info}>{course.organizator}</span>
      </div>
      <AppDivider />
      <h4>Контакты</h4>
      <div className={style.infoLine}>
        <p>{course?.contacts}</p>
      </div>
      <AppDivider />
      <h4>Траектория</h4>
      <div className={style.infoLine}>
        <span className={style.title}>Роль</span>
        <span className={style.info}>{course.role.name}</span>
      </div>
    </AppCard>
  )
}
