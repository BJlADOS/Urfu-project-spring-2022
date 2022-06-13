import React from 'react'
import { ProposalModel } from 'app/models/ProjectProposalModel'
import { DeleteOutlineRounded } from '@material-ui/icons'

import { AppCard } from '../AppCard'
import { AppDivider } from '../AppDivider'
import { AppButton } from '../AppButton'

import style from './style.scss'

interface Props {
  proposal: ProposalModel
  onDelete?: (proposal: ProposalModel) => void
}

export const ProposalDescriptionCard: React.FC<Props> = ({
  proposal,
  onDelete,
}) => {
  const header = !onDelete ? 'Описание проекта' : (
    <div className={style.header}>
      <h3>Описание проекта</h3>
      <AppButton
        buttonType='danger'
        icon={<DeleteOutlineRounded />}
        onClick={() => onDelete(proposal)}
      >
        Удалить
      </AppButton>
    </div>
  )

  return (
    <AppCard
      header={header}
      className={style.card}
    >
      <p>{proposal.description}</p>
      <AppDivider />
      <h4>Цель проекта</h4>
      <p>{proposal.purpose}</p>
      <AppDivider />
      <h4>Итоговый результат</h4>
      <p>{proposal.result}</p>
    </AppCard>
  )
}
