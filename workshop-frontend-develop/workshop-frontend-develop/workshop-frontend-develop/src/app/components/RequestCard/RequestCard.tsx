import React, { useState } from 'react'
import { AppCard } from 'app/components/AppCard'
import { ShortUserRequestModel, UserCompetencyType } from 'app/models'
import { AppButton } from 'app/components/AppButton'

import { AppDivider } from '../AppDivider'
import { CompetenciesList } from '../CompetenciesList'

import style from './style.scss'

interface Props {
  request: ShortUserRequestModel
  hideContacts?: boolean
  hideDesiredCompetencies?: boolean
  onDeleteRequest:()=>void
}

export const RequestCard: React.FC<Props> = ({
  request,
  onDeleteRequest,
  hideDesiredCompetencies,
}) => {
  const [clicked, setClicked] = useState(false)

  const userFullName = `${request.firstName} ${request.lastName}`
  const cardHeader = (
    <div className={style.cardHeader}>
      <span>{userFullName}</span>
      <span className={style.role}>{request.roleName}</span>
    </div>
  )

  const currentCompetenciesList = request.competencies.filter(item => item.userCompetencyType === UserCompetencyType.Current)
  const desirableCompetenciesList = request.competencies.filter(item => item.userCompetencyType === UserCompetencyType.Desirable)

  const currentCompetencies = currentCompetenciesList.length > 0 && (
    <>
      <h4>Компетенции</h4>
      <CompetenciesList list={currentCompetenciesList}/>
    </>
  )

  const desirableCompetencies = !hideDesiredCompetencies && desirableCompetenciesList.length > 0 && (
    <>
      <h4>Желаемые компетенции</h4>
      <CompetenciesList list={desirableCompetenciesList}/>
    </>
  )
  const buttonDelete = (
    <AppButton
      disabled={clicked}
      type={'button'}
      buttonType={'danger'}
      onClick={() => {
        setClicked(true)
        onDeleteRequest()
        setClicked(false)
      }}

    >Удалить заявку</AppButton>

  )

  return (
    <>
      <AppCard
        header={cardHeader}
        className={style.card}
      >
        <div className={style.mainContentCard}>
          <div>
            {currentCompetencies}
            {currentCompetencies && desirableCompetencies && <AppDivider />}
            {desirableCompetencies}
          </div >
          <div className={style.deleteButton}>
            {buttonDelete}
          </div>
        </div>
      </AppCard>
    </>
  )
}
