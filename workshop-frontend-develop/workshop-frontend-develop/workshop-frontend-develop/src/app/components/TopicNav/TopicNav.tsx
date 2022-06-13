import React from 'react'
import { BookOutlined, QuestionAnswerOutlined, VideoLibraryOutlined } from '@material-ui/icons'
import { CourseSectionLectureModel, LectureType } from 'app/models/CourseModel'

import style from './style.scss'

interface Props {
  lecture: CourseSectionLectureModel
  navButton: () => void
  active: boolean
}

export const TopicNav: React.FC<Props> = ({
  lecture,
  navButton,
  active,
}) => {
  const icon = (lectureType: LectureType) => {
    switch (lectureType) {
      case LectureType.Lecture:
        return <BookOutlined />
      case LectureType.VideoLecture:
        return <VideoLibraryOutlined />
      case LectureType.Test:
        return <QuestionAnswerOutlined />
      default:
        return null
    }
  }

  return (
    <>
      <button
        key={lecture.id}
        className={style.topicButton + ' ' + (active ? style.active : null)}
        onClick={navButton}
      >{icon(lecture.type)}</button>
    </>
  )
}
