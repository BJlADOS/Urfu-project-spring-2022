import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { FlagRounded } from '@material-ui/icons'
import { AppCard } from 'app/components/AppCard'
import { AppLoadingSpinner } from 'app/components/AppLoadingSpinner'
import { courses } from 'app/provider'
import { UserModel } from 'app/models'
import { ProfileActions } from 'app/actions'
import { RootState } from 'app/reducers'
import { connect } from 'react-redux'
import { CourseDataModel, CourseModel, CourseSectionModel } from 'app/models/CourseModel'
import { CourseDescriptionCard } from 'app/components/CourseDescriptionCard'
import { CourseInfoCard } from 'app/components/CourseInfoCard'
import { CourseTopicCard } from 'app/components/CourseTopicCard'

import style from './style.scss'

const mapStateToProps = (state: RootState) => ({ profile: state.profile })

const mapDispatchToProps = { setUserProfile: (user: UserModel) => ProfileActions.setUserProfile(user) }

type StateProps = ReturnType<typeof mapStateToProps>
type DispatchProps = typeof mapDispatchToProps

interface Props extends StateProps, DispatchProps {
}

const CoursePageComponent: React.FC<Props> = ({
  profile, // currently unused
}) => {
  const { courseId } = useParams<{ courseId: string }>()
  const [course, setCourse] = useState<CourseModel>()
  const [courseTopics, setCourseTopics] = useState<CourseDataModel>()

  const fetchCourse = () => {
    courses.getCourse(Number(courseId))
      .then(res => setCourse(res.data))
    courses.getCourseTopics(Number(courseId))
      .then(res => setCourseTopics(res.data))
  }

  useEffect(() => {
    fetchCourse()
  }, [])

  if (!course || !courseTopics) {
    return <AppLoadingSpinner/>
  }

  const courseInfoCard = (
    <div className={style.card}>
      <CourseInfoCard course={course}/>
    </div>
  )

  const courseDescriptionCard = (
    <div className={style.card}>
      <CourseDescriptionCard course={course}/>
    </div>
  )

  const topicsCards = (courseTopics.content as CourseSectionModel[]).map(topic => (
    <div
      key={topic.id}
      className={style.card}
    >
      <CourseTopicCard
        topic={topic}
        course={course}
      />
    </div>
  ))

  return (
    <>
      <AppCard className={style.projectNameCard}>
        <h2>
          {course.name}
          {course.isPromoted && <FlagRounded/>}
        </h2>
      </AppCard>
      <div className={style.pageContent}>
        <div className={style.largeColumn}>
          {courseDescriptionCard}
          {topicsCards}
        </div>
        <div className={style.smallColumn}>
          {courseInfoCard}
        </div>
      </div>
    </>
  )
}

export const CoursePage: React.FC = connect(
  mapStateToProps,
  mapDispatchToProps,
)(CoursePageComponent)
