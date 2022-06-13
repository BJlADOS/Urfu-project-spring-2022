import React, { useEffect, useState } from 'react'
import { AppCard } from 'app/components/AppCard'
import { RootState } from 'app/reducers'
import { connect } from 'react-redux'
import { user } from 'app/provider'
import { UserReviewModel, LoadingStatus } from 'app/models'
import { AppLoadingSpinner } from 'app/components/AppLoadingSpinner'
import { FinalGrade } from 'app/pages/ReviewResultPage/FinalGrade'
import { ReviewResultTable } from 'app/components/ReviewResultTable'
import cls from 'classnames'
import { CompetencyReviews } from 'app/pages/ReviewResultPage/CompetencyReviews'

import { SummaryRadarGraph } from './SummaryRadarGraph'
import style from './style.scss'

const mapStateToProps = (state: RootState) => ({ profile: state.profile })

type StateProps = ReturnType<typeof mapStateToProps>

type Props = StateProps

const header = (
  <AppCard className={style.headerCard}>
    <h2>
      Результаты защиты
    </h2>
  </AppCard>
)

const ReviewResultPageComponent: React.FC<Props> = () => {
  const [loading, setLoading] = useState(LoadingStatus.Loading)
  const [review, setReview] = useState<UserReviewModel>(
    {
      competencyReviews: [],
      averageReviews: [],
      expertReviews: [],
    })

  const fetchUserReview = () => {
    setLoading(LoadingStatus.Loading)
    user.getUserReview()
      .then(res => {
        setReview(res.data)
      })
      .then(() => setLoading(LoadingStatus.Finished))
      .catch(() => setLoading(LoadingStatus.Error))
  }

  useEffect(() => {
    fetchUserReview()
  }, [])

  const expertsReviews = (
    <AppCard
      header={'Оценки экспертов'}
      className={style.expertsReviewsCard}
    >
    </AppCard>
  )

  const reviewCards = expertsReviews && (
    <>
      <div className={style.row}>
        <SummaryRadarGraph data={review.averageReviews}/>
        <div className={cls(style.col, style.expertsReviewCol)}>
          <AppCard
            className={style.expertsReviewsCard}
            header={'Оценки команды на защите'}
          >
            <ReviewResultTable data={review.expertReviews}/>
          </AppCard>
          <FinalGrade data={review.expertReviews}/>
        </div>
      </div>
      <CompetencyReviews data={review.competencyReviews}/>
    </>
  )

  const content = loading === LoadingStatus.Finished ? reviewCards : (
    <div className={style.loader}>
      {loading === LoadingStatus.Loading && <AppLoadingSpinner />}
      {loading === LoadingStatus.Error && (
        <div>
          <h2>Произошла ошибка!</h2>
          <p>Не получилось загрузить результаты защиты.</p>
          <p>Попробуйте перезагрузить страницу.</p>
        </div>
      )}
    </div>
  )

  return (
    <>
      {header}
      {content}
    </>
  )
}

export const ReviewResultPage: React.FC = connect(
  mapStateToProps,
)(ReviewResultPageComponent)
