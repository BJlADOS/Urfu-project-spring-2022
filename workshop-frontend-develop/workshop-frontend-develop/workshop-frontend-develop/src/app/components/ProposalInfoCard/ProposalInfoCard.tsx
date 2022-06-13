import React from 'react'
import { ProposalModel } from 'app/models/ProjectProposalModel'

import { AppCard } from '../AppCard'
import { AppDivider } from '../AppDivider'

import style from './style.scss'

interface Props {
  proposal: ProposalModel
}

export const ProposalInfoCard: React.FC<Props> = ({
  proposal,
}) =>

  (
    <AppCard
      header={'Информация'}
      className={style.card}
    >
      <div className={style.infoLine}>
        <p className={style.title}>Организация заказчика</p>
        <p className={style.info}>{proposal.organization}</p>
      </div>
      <div className={style.infoLine}>
        <p className={style.title}>Куратор проекта</p>
        <p className={style.info}>{proposal.curator}</p>
      </div>
      <AppDivider />
      <h4>Контакты</h4>
      <div className={style.infoLine}>
        <p className={style.info}>{proposal?.contacts}</p>
      </div>
      <AppDivider />
      <h4>Траектория</h4>
      <div className={style.infoLine}>
        <p className={style.title}>Ключевая технология</p>
        <p className={style.info}>{proposal.keyTechnologyName}</p>
      </div>
      <div className={style.infoLine}>
        <p className={style.title}>Жизненный сценарий</p>
        <p className={style.info}>{proposal.lifeScenarioName}</p>
      </div>
    </AppCard>
  )
