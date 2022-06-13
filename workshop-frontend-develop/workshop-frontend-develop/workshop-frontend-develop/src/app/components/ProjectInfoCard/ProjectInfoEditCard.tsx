import React, { useEffect, useState } from 'react'
import { CompetencyModel, KeyTechnologyModel, LifeScenarioModel, ShortProjectModel } from 'app/models'
import { user, projects } from 'app/provider'

import { AppCard } from '../AppCard'
import { AppDivider } from '../AppDivider'
import { CompetenciesList } from '../CompetenciesList'
import { AppInput, AppTextarea } from '../AppInput'
import { AppDropdown } from '../AppDropdown'

import style from './style.scss'

interface Props {
  project: ShortProjectModel
  onChange: (project: ShortProjectModel) => void
}

export const ProjectInfoEditCard: React.FC<Props> = ({
  project,
  onChange,
}) => {
  const [сompetenciesList, setCompetenciesList] = useState<CompetencyModel[]>([])
  const [scenariosList, setScenariosListt] = useState<LifeScenarioModel[]>([])
  const [technologiesList, setTechnologiesList] = useState<KeyTechnologyModel[]>([])

  const handleChange = (value: unknown, key: keyof ShortProjectModel) => {
    const editedData = {
      ...project,
      [key]: value,
    }

    onChange(editedData)
  }

  useEffect(() => {
    projects.getLifeScenario()
      .then(res => setScenariosListt(res.data))
      .then(() => projects.getKeyTechnology())
      .then(res => setTechnologiesList(res.data))
      .then(user.getCompetitions)
      .then(res => setCompetenciesList(res.data))
  }, [])

  const competentiesDataConverter = (value: CompetencyModel) => ({
    key: value.id,
    label: value.name,
  })

  const projectInfoHeader = project?.image && (
    <div className={style.imgWrapper}>
      <img
        src={project?.image}
        alt='logo'
      />
    </div>
  )

  const dataConverter = (value: LifeScenarioModel) => ({
    key: value.id,
    label: value.name,
  })

  const filteredCompetencies = сompetenciesList.filter(el => project.competencies.map(el => el.id).indexOf(el.id) === -1)

  return (
    <AppCard
      header={projectInfoHeader}
      className={style.card}
    >
      <h4>Информация</h4>
      <div className={style.infoLine}>
        <span className={style.title}>Организация</span>
        <AppInput
          className={style.editField}
          value={project.organization}
          onChange={(e) => handleChange(e.target.value, 'organization')}
        />
      </div>
      <div className={style.infoLine}>
        <span className={style.title}>Куратор</span>
        <AppInput
          className={style.editField}
          value={project.curator}
          onChange={(e) => handleChange(e.target.value, 'curator')}
        />
      </div>
      <AppDivider />
      <h4>Контакты</h4>
      <div className={style.infoLine}>
        <AppTextarea
          value={project.contacts}
          onChange={(e) => handleChange(e.target.value, 'contacts')}
          className={style.textEditor}
        />
      </div>
      <AppDivider />
      <h4>Траектория</h4>
      <div className={style.infoLine}>
        <span className={style.title}>Жизненный сценарий</span>
        <AppDropdown
          items={scenariosList}
          value={project.lifeScenario}
          dataConverter={dataConverter}
          onChange={(value) => handleChange(value, 'lifeScenario')}
          readOnly
        />
      </div>
      <div className={style.infoLine}>
        <span className={style.title}>Ключевая технология</span>
        <AppDropdown
          items={technologiesList}
          value={project.keyTechnology}
          dataConverter={dataConverter}
          onChange={(value) => handleChange(value, 'keyTechnology')}
          readOnly
        />
      </div>
      <AppDivider />
      <h4>Компетенции</h4>
      <AppDropdown
        className={style.competencySearch}
        placeholder='Поиск компетенции'
        items={filteredCompetencies}
        onChange={(value) => handleChange([value, ...project.competencies], 'competencies')}
        dataConverter={competentiesDataConverter}
        hideSelected
      />
      <CompetenciesList
        list={project.competencies}
        onDelete={(id) => handleChange(project.competencies.filter(c => c.id !== id), 'competencies')}
      />
    </AppCard>
  )
}
