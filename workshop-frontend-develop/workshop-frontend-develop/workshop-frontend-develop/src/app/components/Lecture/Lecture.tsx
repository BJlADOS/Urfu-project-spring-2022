import React from 'react'
import { QuestionAnswerOutlined } from '@material-ui/icons'
import { CourseSectionLectureModel, LectureType, TextLecture } from 'app/models/CourseModel'

import style from './style.scss'

interface Props {
  lecture: CourseSectionLectureModel
}

export const Lecture: React.FC<Props> = ({
  lecture,
}) => {
  const content = (lecture: CourseSectionLectureModel) => {
    switch (lecture.type) {
      case LectureType.Lecture:
        return (
          <div className={style.wrapper}>
            <h1>{lecture.title}</h1>
            {(lecture.content as TextLecture[]).map(content =>
              <div
                key={content.id}
                className={style.content}
              >
                <h3>{content.title}</h3>
                {content.picture ? (<img src={content.picture}></img>) : null}
                <p>{content.text}</p>
              </div>,
            )}
          </div>
        )
      case LectureType.VideoLecture:
        return (<div className={style.wrapper}>
          <h1>{lecture.title}</h1>
          <img src='/assets/img/no-video.jpg'></img>
        </div>)
      case LectureType.Test:
        return <p>Здесь должен быть тест</p>
      default:
        return null
    }
  }

  return (
    <>
      {content(lecture)}
    </>
  )
}
