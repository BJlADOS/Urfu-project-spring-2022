import React from 'react'
import { projectProposal as proposalNav } from 'app/nav'
import { ProposalStatus, ShortProposalModel } from 'app/models/ProjectProposalModel'
import cls from 'classnames'

import { AppCard, AppLinkCard } from '../AppCard'

import style from './style.scss'

const statussesMap = new Map([
  [ProposalStatus.Approved, 'Одобрено'],
  [ProposalStatus.Rejected, 'Отклонено'],
  [ProposalStatus.Pending, 'На рассмотрении'],
])

interface Props {
  proposal: ShortProposalModel
}

export const ProposalListItemCard: React.FC<Props> = ({
  proposal,
}) => {
  const header = (
    <div className={style.header}>
      <h3>
        {proposal.name}
      </h3>
    </div>
  )

  const proposalStats = (
    <div className={style.proposalStats}>
      <span>
        Количество команд:
        <b> {proposal.maxTeamCount}</b>
      </span>
      <span>|</span>
      <span>
        Количество участников в команде:
        <b> {proposal.teamCapacity}</b>
      </span>
    </div>
  )

  const status = (
    <div className={cls(style.competencyChip, {
      [style.approved]: proposal.status === ProposalStatus.Approved,
      [style.rejected]: proposal.status === ProposalStatus.Rejected,
      [style.pending]: proposal.status === ProposalStatus.Pending,
    })}
    >
      <span>{statussesMap.get(proposal.status)}</span>
    </div>
  )

  const sideCard = (
    <div className={style.sideCardContent}>
      <div className={style.text}>
        <h5>Автор</h5>
        <span>{proposal.author.lastName} {proposal.author.firstName} {proposal.author.middleName}</span>
        <h5>Статус заявки</h5>
        {status}
        <h5>Дата создания</h5>
        <span>{new Date(proposal.date).toLocaleString()}</span>
      </div>
    </div>
  )

  return (
    <div className={style.proposalRow}>
      <div className={style.largeSection}>
        <AppLinkCard
          header={header}
          className={style.card}
          to={proposalNav(proposal.id)}
        >
          <p>
            {proposal.purpose}
          </p>
          {proposalStats}
        </AppLinkCard>
      </div>
      <div className={style.smallSection}>
        <AppCard className={style.card}>
          {sideCard}
        </AppCard>
      </div>
    </div>
  )
}
