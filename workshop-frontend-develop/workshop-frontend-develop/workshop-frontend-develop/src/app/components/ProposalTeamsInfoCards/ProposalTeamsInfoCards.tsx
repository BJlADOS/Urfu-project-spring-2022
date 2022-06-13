import React from 'react'
import { ProposalModel } from 'app/models/ProjectProposalModel'

import { AppCard } from '../AppCard'

import style from './style.scss'

interface Props {
  proposal: ProposalModel
}

export const ProposalTeamsInfoCard: React.FC<Props> = ({
  proposal,
}) => (
  <>
    <div className={style.card}>
      <AppCard>
        <h1>
          Количество команд:
          <span className={style.highlightText}> {proposal.maxTeamCount}</span>
        </h1>
      </AppCard>
    </div>
    <div className={style.card}>
      <AppCard>
        <h1>
          Количество участников в команде:
          <span className={style.highlightText}> {proposal.teamCapacity}</span>
        </h1>
      </AppCard>
    </div>
  </>
)
