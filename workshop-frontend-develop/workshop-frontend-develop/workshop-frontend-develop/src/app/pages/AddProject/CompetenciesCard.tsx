import React, {ReactNode} from 'react'
import {AppCard} from 'app/components/AppCard'
import {CompetencyModel} from 'app/models'
import {AppDropdown} from 'app/components/AppDropdown'
import {CompetenciesList} from 'app/components/CompetenciesList'

import style from './style.scss'

interface Props {
    competenciesList: CompetencyModel[]
    selectedItems: CompetencyModel[]
    header: ReactNode
    onChange: (value?: CompetencyModel) => void
    onDelete: (id: number) => void
    className?: ReactNode
}

export const CompetenciesSkillsCard: React.FC<Props> = ({
                                                            header,
                                                            selectedItems,
                                                            competenciesList,
                                                            onChange,
                                                            onDelete,
                                                            className,
                                                        }) => {
    const competentiesDataConverter = (value: CompetencyModel) => ({
        key: value.id,
        label: value.name,
        value: value,
    })

    return (
        <AppCard
            className={className || style.myCompetencies}
            contentClassName={style.myCompetenciesContent}
            header={header}

        >
            <AppDropdown
                className={style.competencySearch}
                placeholder='Ввод компетенции'
                items={competenciesList}
                onChange={onChange}
                dataConverter={competentiesDataConverter}
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
