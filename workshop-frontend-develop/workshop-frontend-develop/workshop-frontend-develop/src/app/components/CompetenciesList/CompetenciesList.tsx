import React from 'react'
import cls from 'classnames'
import { CompetencyModel, CompetencyType } from 'app/models'

import { AppChip } from '../AppChip'

import style from './style.scss'

interface Props {
  list: CompetencyModel[]
  className?: string
  onDelete?: (id: number) => void
}

export const CompetenciesList: React.FC<Props> = ({
  list,
  className,
  onDelete,
}) => {
  const items = list.map(item => {
    const handleDelete = onDelete && (() => onDelete(item.id))

    return (
      <AppChip
        key={item.id}
        onDelete={handleDelete}
        className={cls(style.competencyChip, {
          [style.hardSkill]: item.competencyType === CompetencyType.HardSkill,
          [style.softSkill]: item.competencyType === CompetencyType.SoftSkill,
        })}
      >
        <span
          className={style.name}
          title={item.name}
        >
          {item.name}
        </span>
      </AppChip>
    )
  })

  const competenciesListStyles = cls([className, style.competenciesList])

  return (
    <div className={competenciesListStyles}>
      {items}
    </div>
  )
}
