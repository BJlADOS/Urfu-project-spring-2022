import React from 'react'
import { ProposalModel } from 'app/models/ProjectProposalModel'

import { AppCard } from '../AppCard'
import { AppTextarea } from '../AppInput'

import style from './style.scss'

interface Props {
  proposal: ProposalModel
  onChange: (proposal: ProposalModel) => void
}

export const ProposalDescriptionEditCard: React.FC<Props> = ({
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

  const descriptionCard = (
    <AppCard
      header={<h3>Описание проекта</h3>}
      className={style.card}
    >
      <AppTextarea
        required
        value={proposal.description}
        onChange={(e) => handleChange(e.target.value, 'description')}
        className={style.textEditor}
        placeholder='Описание проекта'
      />
    </AppCard>
  )

  const purposeCard = (
    <AppCard
      header={<h3>Цель проекта</h3>}
      className={style.card}
    >
      <AppTextarea
        required
        value={proposal.purpose}
        onChange={(e) => handleChange(e.target.value, 'purpose')}
        className={style.textEditor}
        placeholder='Цель проекта'
      />
    </AppCard>
  )

  const resultCard = (
    <AppCard
      header={<h3>Итоговый результат</h3>}
      className={style.card}
    >
      <AppTextarea
        required
        value={proposal.result}
        onChange={(e) => handleChange(e.target.value, 'result')}
        className={style.textEditor}
        placeholder='Итоговый результат'
      />
    </AppCard>
  )

  return (
    <>
      {descriptionCard}
      {purposeCard}
      {resultCard}
    </>
  )
}
