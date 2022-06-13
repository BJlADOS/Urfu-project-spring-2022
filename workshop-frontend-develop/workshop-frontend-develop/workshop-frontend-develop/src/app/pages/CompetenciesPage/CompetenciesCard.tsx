import React, {useState} from 'react'
import {AppCard} from 'app/components/AppCard'
import {CompetencyModel, CompetencyType} from 'app/models'
import {CompetenciesList} from 'app/components/CompetenciesList'
import {AppInput} from 'app/components/AppInput'
import {AppButton} from 'app/components/AppButton'

import style from './style.scss'

interface Props {
    selectedItems: CompetencyModel[]
    header: string
    onAdd: (competencyName?: string, competencyType?: CompetencyType) => void
    onDelete: (id: number) => void
}

export const CompetenciesCard: React.FC<Props> = ({
                                                      header,
                                                      selectedItems,
                                                      onAdd,
                                                      onDelete,
                                                  }) => {
    const [competencyName, setCompetencyName] = useState('')

    const handleAddCompetency = () => {
        onAdd(competencyName)
        setCompetencyName('')
    }

    return (
        <AppCard
            className={style.competenciesCard}
            header={header}
        >
            <>
                <div className={style.input}>
                    <AppInput
                        className={style.field}
                        value={competencyName}
                        onChange={(e) => setCompetencyName(e.target.value)}
                        placeholder='Введите название компетенции'
                    />
                    <AppButton
                        buttonType={competencyName === '' ? 'secondary' : 'primary'}
                        className={style.button}
                        disabled={competencyName === ''}
                        type={'button'}
                        onClick={handleAddCompetency}
                    >
                        Добавить
                    </AppButton>
                </div>
                <CompetenciesList
                    className={style.list}
                    list={selectedItems}
                    onDelete={onDelete}
                />
            </>
        </AppCard>
    )
}
