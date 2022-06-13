import React from 'react'
import { AppCard } from 'app/components/AppCard'
import { ExpertReviewModel } from 'app/models'

import style from './style.scss'

interface Props {
  data: ExpertReviewModel[]
}

export const FinalGrade: React.FC<Props> = ({ data }) => {
  const summaryExpertReview = data.reduce((acc, curr) => {
    const expertAverage = ((curr.goalsAndTasks ?? 0) + (curr.solution ?? 0) +
      (curr.presentation ?? 0) + (curr.technical ?? 0) +
      (curr.result ?? 0) + (curr.knowledge ?? 0)) / 6

    return acc + expertAverage
  }, 0)

  const grade = (summaryExpertReview / data.length).toFixed(2)

  return (
    <AppCard
      className={style.finalGradeCard}
      contentClassName={style.finalGradeContent}
    >
      <h2>Итоговая оценка</h2>
      <h3>{grade}</h3>
    </AppCard>
  )
}
