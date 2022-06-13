import React from 'react'
import { AppCard } from 'app/components/AppCard'
import { MailOutlineRounded, PhoneRounded } from '@material-ui/icons'
import { UserCompetencyType, UserModel } from 'app/models'

import { AppDivider } from '../AppDivider'
import { CompetenciesList } from '../CompetenciesList'

import style from './style.scss'

interface Props {
  user: UserModel
  hideContacts?: boolean
  hideDesiredCompetencies?: boolean
}

export const TeamMemberCard: React.FC<Props> = ({
  user,
  hideContacts,
  hideDesiredCompetencies,
}) => {
  const userFullName = `${user.firstName} ${user.lastName}`
  const cardHeader = (
    <div className={style.cardHeader}>
      <span>{userFullName}</span>
      <span className={style.role}>{user.role?.name}</span>
    </div>
  )

  const currentCompetenciesList = user.competencies.filter(item => item.userCompetencyType === UserCompetencyType.Current)
  const desirableCompetenciesList = user.competencies.filter(item => item.userCompetencyType === UserCompetencyType.Desirable)

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

  const phone = user.phoneNumber && (
    <div className={style.contact}>
      <PhoneRounded />
      <span>{user.phoneNumber}</span>
    </div>
  )

  const email = user.email && (
    <div className={style.contact}>
      <MailOutlineRounded />
      <span>{user.email}</span>
    </div>
  )

  const contacts = !hideContacts && (
    <>
      <h4>Контакты</h4>
      <div>
        {phone}
        {email}
      </div>
    </>
  )

  return (
    <>
      <AppCard
        header={cardHeader}
        className={style.card}
      >
        {contacts}
        {contacts && currentCompetencies && <AppDivider />}
        {currentCompetencies}
        {currentCompetencies && desirableCompetencies && <AppDivider />}
        {desirableCompetencies}
      </AppCard>
    </>
  )
}
