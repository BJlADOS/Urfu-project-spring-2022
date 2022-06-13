import React from 'react'
import { CompetencyModel, UserCompetencyModel } from 'app/models'
import { AppDropdown } from 'app/components/AppDropdown'
import { CompetenciesList } from 'app/components/CompetenciesList'

interface Props {
  competenciesList: CompetencyModel[]
  selectedItems: CompetencyModel[]
  onChange: (value?: UserCompetencyModel) => void
  onDelete: (id: number) => void
  className:any
}

export const CompetenciesSearchCard: React.FC<Props> = ({
  selectedItems,
  competenciesList,
  onChange,
  onDelete,
  className,
}) => {
  const competentiesDataConverter = (value: CompetencyModel) => ({
    key: value.id,
    label: value.name,
  })

  return (
    <>
      <AppDropdown
        className={className}
        placeholder='Поиск по компетенциям'
        items={competenciesList}
        onChange={onChange}
        dataConverter={competentiesDataConverter}
        hideSelected
      />
      <CompetenciesList
        list={selectedItems}
        onDelete={onDelete}
      />
    </>

  )
}
