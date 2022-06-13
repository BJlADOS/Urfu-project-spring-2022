import React from 'react'
import { AppCard } from 'app/components/AppCard'
import { CompetencyReviewModel } from 'app/models'

import style from './style.scss'

interface Props {
  data: CompetencyReviewModel[]
}

export const CompetencyReviews: React.FC<Props> = ({ data }) => {
  const competencyReviewCards = data && data.map((competency) => (
    <AppCard
      key={competency.competencyName}
      className={style.competencyCard}
      contentClassName={style.competencyCardInner}
    >
      <h5
        className={style.name}
        title={competency.competencyName}
      >
        {competency.competencyName}
      </h5>
      <div className={style.mark}>
        {competency.mark}
      </div>
    </AppCard>
  ))

  return (

    <>
      <AppCard className={style.competencyReviewsCard}>
        <h3>Оценка компетенций</h3>
      </AppCard>
      <div className={style.competenciesSection}>
        {competencyReviewCards}
      </div>
    </>
  )
}
