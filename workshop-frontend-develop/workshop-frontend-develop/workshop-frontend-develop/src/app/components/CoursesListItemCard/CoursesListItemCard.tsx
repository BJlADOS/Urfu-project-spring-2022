import React from 'react'
import { FlagRounded } from '@material-ui/icons'
import { course as courseNav } from 'app/nav'
import { CourseModel, CourseProgress } from 'app/models/CourseModel'
import LinearProgress from '@material-ui/core/LinearProgress'

import { AppCard, AppLinkCard } from '../AppCard'

import style from './style.scss'

interface Props {
  course: CourseModel,
  profile: CourseProgress[] | undefined
}

export const CourseListItemCard: React.FC<Props> = ({
  course,
  profile,
}) => {
  const header = (
    <div className={style.header}>
      <h3>
        {course.name}
        {course.isPromoted && <FlagRounded />}
      </h3>
      {!course.isAvailable &&
        <span className={style.closed}>Запись закрыта</span>
      }
    </div>
  )

  const courseImage = course.image && (
    <div className={style.imgWrapper}>
      <img
        src={course?.image}
        alt='logo'
      />
    </div>
  )

  const tasksCompleted = profile?.find((item) => item.id === course.id)?.tasksCompleted ?? 0

  return (
    <div className={style.courseRow}>
      <div className={style.largeSection}>
        <AppLinkCard
          header={header}
          className={style.card}
          to={courseNav(course.id)}
        >
          <p>
            {course.description}
          </p>
          <div className={style.courseStats}>
            <div className={style.progressWrapper}>
              <span>Прогресс:</span>
              <LinearProgress
                variant='determinate'
                value={(tasksCompleted / course.tasks) * 100}
                color='primary'
              />
              <span>{Math.round((tasksCompleted / course.tasks) * 100)}%</span>
            </div>
            <div>
              Оставить отзыв
              <b></b>
            </div>
          </div>
        </AppLinkCard>
      </div>
      <div className={style.smallSection}>
        <AppCard className={style.card}>
          <div className={style.sideCardContent}>
            <div className={style.text}>
              <h5>Организация</h5>
              <span>{course.organizator}</span>
              <h5>Роль</h5>
              <span>{course.role.name}</span>
            </div>
            {courseImage}
          </div>
        </AppCard>
      </div>
    </div>
  )
}
