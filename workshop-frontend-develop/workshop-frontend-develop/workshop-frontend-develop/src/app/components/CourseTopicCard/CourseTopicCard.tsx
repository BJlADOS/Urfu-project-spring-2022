import React, { useState } from 'react'
import { courseTopic as courseTopicNav } from 'app/nav'
import DoubleArrowIcon from '@material-ui/icons/DoubleArrow'
import cls from 'classnames'
import { HowToRegRounded } from '@material-ui/icons'
import { ProjectModel, ShortTeamModel, ShortUserModel, TeamStatusses } from 'app/models'
import { CheckFidelityDialog } from 'app/components/CheckFidelityDialog'
import { CourseModel, CourseSectionModel } from 'app/models/CourseModel'
import { Link } from 'react-router-dom'

import { AppCard, AppLinkCard } from '../AppCard'
import { AppDivider } from '../AppDivider'
import { CompetenciesList } from '../CompetenciesList'
import { AppButton } from '../AppButton'
import { AppDialog } from '../AppDialog'

import style from './style.scss'

interface Props {
  topic: CourseSectionModel
  course: CourseModel
}

export const CourseTopicCard: React.FC<Props> = ({
  topic,
  course,
}) => {
  const topicName = topic.title || `Раздел №${topic.id}`
  const header = (
    <div className={style.teamCardHeader}>
      <div className={style.teamName}>
        <div className={style.content}>
          {topicName}
          <DoubleArrowIcon />
        </div>
      </div>
    </div>
  )

  return (
    <>
      <Link
        className={style.appCard}
        to={courseTopicNav(topic.id, course.id)}
      >
        <div className={style.header}>
          {header}
        </div>
      </Link>
    </>
  )
}
