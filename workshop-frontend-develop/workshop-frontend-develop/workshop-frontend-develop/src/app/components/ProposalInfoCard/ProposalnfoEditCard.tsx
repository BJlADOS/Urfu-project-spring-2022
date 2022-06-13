import React, { useEffect, useState } from 'react'
import { ProposalModel } from 'app/models/ProjectProposalModel'
import { AppDropdown } from 'app/components/AppDropdown'
import { LifeScenarioModel } from 'app/models'
import { projects } from 'app/provider'

import { AppCard } from '../AppCard'
import { AppDivider } from '../AppDivider'
import { AppInput, AppTextarea } from '../AppInput'

import style from './style.scss'

interface Props {
  proposal: ProposalModel
  onChange: (proposal: ProposalModel) => void
  isLifeScenarioNeed:boolean
}

export const ProposalInfoEditCard: React.FC<Props> = ({
  proposal,
  onChange,
  isLifeScenarioNeed,
}) => {
  const handleChange = (value: unknown, key: keyof ProposalModel) => {
    const editedData = {
      ...proposal,
      [key]: value,
    }

    onChange(editedData)
  }

  const [scenariosList, setScenariosList] = useState<LifeScenarioModel[]>([])

  useEffect(() => {
    projects.getLifeScenario()
      .then(res => setScenariosList(res.data))
  }, [])

  const dataConverter = (value: string) => ({
    key: value,
    label: value,
  })

  const contactInfo = (
    <>
      <h4>Контакты представителя заказчика</h4>
      <div className={style.infoLine}>
        <AppTextarea
          required
          className={style.textEditor}
          value={proposal.contacts}
          onChange={(e) => handleChange(e.target.value, 'contacts')}
          placeholder='Контакты'
        />
      </div>
    </>
  )
  const lifeScenario = isLifeScenarioNeed && (
    <>
      <h4>Жизненный сценарий</h4>
      <AppDropdown
        items={scenariosList.map(x => x.name)}
        value={proposal.lifeScenarioName}
        dataConverter={dataConverter}
        onChange={(value) => handleChange(value, 'lifeScenarioName')}
        readOnly
      />
      <AppDivider/>
    </>
  )

  const trajectoryInfo = (
    <>
      <h4>Траектория</h4>
      <div className={style.infoLine}>
        <p className={style.title}>Ключевая технология</p>
        <AppInput
          required
          className={style.editField}
          value={proposal.keyTechnologyName}
          onChange={(e) => handleChange(e.target.value, 'keyTechnologyName')}
          placeholder='Ключевая технология'
        />
      </div>
    </>
  )

  return (
    <AppCard
      className={style.card}
    >
      {lifeScenario}

      {contactInfo}
      <AppDivider />
      {trajectoryInfo}
    </AppCard>
  )
}
