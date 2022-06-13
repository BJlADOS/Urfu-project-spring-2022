import React, { useEffect, useState } from 'react'
import { useHistory, useParams } from 'react-router-dom'
import { DoubleArrow } from '@material-ui/icons'
import { AppLoadingSpinner } from 'app/components/AppLoadingSpinner'
import { courses } from 'app/provider'
import { UserModel } from 'app/models'
import { ProfileActions } from 'app/actions'
import { RootState } from 'app/reducers'
import { connect } from 'react-redux'
import { CourseDataModel, CourseModel, CourseSectionLectureModel, CourseSectionModel } from 'app/models/CourseModel'
import { TopicNav } from 'app/components/TopicNav'
import { Lecture } from 'app/components/Lecture'

import style from './style.scss'

const mapStateToProps = (state: RootState) => ({ profile: state.profile })

const mapDispatchToProps = { setUserProfile: (user: UserModel) => ProfileActions.setUserProfile(user) }

type StateProps = ReturnType<typeof mapStateToProps>
type DispatchProps = typeof mapDispatchToProps

interface Props extends StateProps, DispatchProps {
}

const TopicPageComponent: React.FC<Props> = () => {
  const { courseId, topicId } = useParams<{ courseId: string, topicId: string }>()
  const [course, setCourse] = useState<CourseModel>()
  const [courseTopics, setCourseTopics] = useState<CourseDataModel>()
  const [topic, setTopic] = useState<CourseSectionModel>()
  const [selectedLectureId, setSelectedLectureId] = useState<number>(0)

  const history = useHistory()
  const fetchCourse = () => {
    courses.getCourse(Number(courseId))
      .then(res => setCourse(res.data))
    courses.getCourseTopic(Number(topicId), Number(courseId))
      .then(res => setTopic(res.data))
    courses.getCourseTopics(Number(courseId))
      .then(res => setCourseTopics(res.data))
  }

  useEffect(() => {
    fetchCourse()
  }, [])

  if (!course || !topic) {
    return <AppLoadingSpinner/>
  }

  const navButton = (lectureId: number) => {
    setSelectedLectureId(lectureId)
  }

  const backToCourse = () => {
    history.push(`/courses/${courseId}`)
  }

  const nextLecture = () => {
    if (selectedLectureId < topic.content.length - 1) {
      setSelectedLectureId(selectedLectureId + 1)
    } else {
      history.push(`/courses/${courseId}/${Number(topicId) + 1}`)
      setSelectedLectureId(0)
      setTopic(undefined)
      courses.getCourseTopic(Number(topicId) + 1, Number(courseId))
        .then(res => setTopic(res.data))
    }
  }

  const previousLecture = () => {
    if (selectedLectureId > 0) {
      setSelectedLectureId(selectedLectureId - 1)
    } else {
      history.push(`/courses/${courseId}/${Number(topicId) - 1}`)
      setSelectedLectureId(0)
      setTopic(undefined)
      courses.getCourseTopic(Number(topicId) - 1, Number(courseId))
        .then(res => setTopic(res.data))
    }
  }

  const NavButtons = (topic.content as CourseSectionLectureModel[]).map(lecture => (
    <TopicNav
      key={lecture.id}
      lecture={lecture}
      navButton={() => navButton(lecture.id)}
      active={lecture.id === selectedLectureId}
    ></TopicNav>
  ))

  const topicNavButtons = (
    <>
      <button
        className={style.navButton}
        onClick={() => previousLecture()}
        disabled={courseTopics?.content[0].id === Number(topicId) && selectedLectureId === 0}
      >
        <DoubleArrow className={style.rotate}></DoubleArrow>
      </button>
      {NavButtons}
      <button
        className={style.navButton}
        onClick={() => nextLecture()}
        disabled={courseTopics?.content[courseTopics?.content.length - 1].id === Number(topicId) && selectedLectureId === topic.content.length - 1}
      >
        <DoubleArrow></DoubleArrow>
      </button>
    </>
  )

  const topicContent = (topic.content as CourseSectionLectureModel[]).map(lecture => {
    if (lecture.id === selectedLectureId) {
      return (
        <Lecture
          key={lecture.id}
          lecture={lecture}
        ></Lecture>
      )
    }
    return null
  })

  return (
    <>
      <div className={style.header}>
        <h2>
          {topic.title}
        </h2>
        <button onClick={() => backToCourse()}>Вернуться к курсу</button>
      </div>
      <div className={style.navButtons}>
        {topicNavButtons}
      </div>
      <div className={style.pageContent}>
        {topicContent}
      </div>
    </>
  )
}

export const TopicPage: React.FC = connect(
  mapStateToProps,
  mapDispatchToProps,
)(TopicPageComponent)
