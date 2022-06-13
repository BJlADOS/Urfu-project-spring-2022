import React, {useState} from 'react'
import {AppCard} from 'app/components/AppCard'
import {CompetencyType} from 'app/models'
import {AppInput} from 'app/components/AppInput'
import {AppButton} from 'app/components/AppButton'

import {RoleList} from './RoleList'
import style from './style.scss'

interface Props {
    selectedItems: string[]
    header: string
    onAdd: (competencyName?: string, competencyType?: CompetencyType) => void
    onDelete: () => void
}

export const RoleCard: React.FC<Props> = ({
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
            className={style.roleCard}
            header={header}
        >
            <>
                <div className={style.input}>
                    <AppInput
                        className={style.field}
                        value={competencyName}
                        onChange={(e) => setCompetencyName(e.target.value)}
                        placeholder='Введите роль'
                    />
                    <AppButton
                        buttonType={competencyName === '' ? 'secondary' : 'primary'}
                        className={style.button}
                        disabled={competencyName === ''}
                        type='button'
                        onClick={handleAddCompetency}
                    >
                        Добавить
                    </AppButton>
                    <AppButton
                        buttonType={'secondary'}
                        className={style.button}
                        disabled={competencyName !== '' || selectedItems.length === 0}
                        type={'button'}
                        onClick={onDelete}
                    >
                        Удалить
                    </AppButton>
                </div>
                <RoleList
                    className={style.list}
                    list={selectedItems}
                />
            </>
        </AppCard>
    )
}
