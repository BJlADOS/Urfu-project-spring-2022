import React from 'react'
import { ProposalModel } from 'app/models/ProjectProposalModel'

import { AppCard } from '../AppCard'
import { AppInput } from '../AppInput'

import style from './style.scss'

interface Props {
  proposal: ProposalModel
  onChange: (proposal: ProposalModel) => void
}

export const ProposalTeamsInfoEditCard: React.FC<Props> = ({
  proposal,
  onChange,
}) => {
  const handleChange = (value: string, key: keyof ProposalModel) => {
    const editedData = {
      ...proposal,
      [key]: value,
    }

    onChange(editedData)
  }

  const teamCapacityCard = (
    <AppCard
      header={<h3>Количество участников в команде</h3>}
      className={style.card}
    >

      <AppInput
        required
        inputMode={'numeric'}
        className={style.editField}
        value={proposal.teamCapacity}
        onChange={(e) => handleChange(e.target.value, 'teamCapacity')}
        placeholder='1'
        max={6}
        min={1}
      />
    </AppCard>
  )

  return (
    <>
      {/* {maxTeamCountCard} */}
      {teamCapacityCard}
    </>
  )
}
