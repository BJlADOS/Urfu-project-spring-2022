import React, {ReactNode} from 'react'
import {AppCard} from 'app/components/AppCard'
import {UserCompetencyModel} from 'app/models'
import {AppDropdown} from 'app/components/AppDropdown'
import {CompetenciesList} from 'app/components/CompetenciesList'

import style from './style.scss'

interface Props {
    competenciesList: UserCompetencyModel[]
    selectedItems: UserCompetencyModel[]
    header: ReactNode
    onChange: (value?: UserCompetencyModel) => void
    onDelete: (id: number) => void
}

export const CompetenciesCard: React.FC<Props> = ({
                                                      header,
                                                      selectedItems,
                                                      competenciesList,
                                                      onChange,
                                                      onDelete,
                                                  }) => {
    const competenciesDataConverter = (value: UserCompetencyModel) => ({
        key: value.id,
        label: value.name,
        value: value,
    })

    return (
        <AppCard
            className={style.myCompetencies}
            contentClassName={style.myCompetenciesContent}
            header={header}
        >
            <AppDropdown
                className={style.competencySearch}
                placeholder='Поиск новой компетенции'
                items={competenciesList}
                onChange={onChange}
                dataConverter={competenciesDataConverter}
                hideSelected
            />
            <CompetenciesList
                list={selectedItems}
                className={style.competenciesList}
                onDelete={onDelete}
            />
        </AppCard>
    )
}
