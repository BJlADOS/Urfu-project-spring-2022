import React from 'react'
import { ShortProjectModel } from 'app/models'
import { DeleteOutlineRounded } from '@material-ui/icons'

import { AppCard } from '../AppCard'
import { AppDivider } from '../AppDivider'
import { AppTextarea } from '../AppInput'
import { AppButton } from '../AppButton'

import style from './style.scss'

interface Props {
  project: ShortProjectModel
  onChange: (project: ShortProjectModel) => void
  onDelete?: (project: ShortProjectModel) => void
}

export const ProjectDescriptionEditCard: React.FC<Props> = ({
  project,
  onChange,
  onDelete,
}) => {
  const handleChange = (value: string, key: keyof ShortProjectModel) => {
    const editedData = {
      ...project,
      [key]: value,
    }

    onChange(editedData)
  }

  const header = !onDelete ? 'Описание проекта' : (
    <div className={style.header}>
      <h3>Описание проекта</h3>
      <AppButton
        buttonType='danger'
        icon={<DeleteOutlineRounded />}
        onClick={() => onDelete(project)}
      >
        Удалить проект
      </AppButton>
    </div>
  )

  return (
    <AppCard
      header={header}
      className={style.card}
    >
      <AppTextarea
        value={project.description}
        onChange={(e) => handleChange(e.target.value, 'description')}
        className={style.textEditor}
      />
      <AppDivider />
      <h4>Цель</h4>
      <AppTextarea
        value={project.purpose}
        onChange={(e) => handleChange(e.target.value, 'purpose')}
        className={style.textEditor}
      />
      <AppDivider />
      <h4>Итоговый продукт</h4>
      <AppTextarea
        value={project.result}
        onChange={(e) => handleChange(e.target.value, 'result')}
        className={style.textEditor}
      />
    </AppCard>
  )
}
