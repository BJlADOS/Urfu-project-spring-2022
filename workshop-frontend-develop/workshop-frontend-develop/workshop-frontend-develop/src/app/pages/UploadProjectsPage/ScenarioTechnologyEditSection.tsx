import React, {useEffect, useState} from 'react'
import {AppCard} from 'app/components/AppCard'
import {KeyTechnologyModel, LifeScenarioModel} from 'app/models'
import {admin, projects} from 'app/provider'
import {AppButton} from 'app/components/AppButton'
import {DeleteRounded} from '@material-ui/icons'

import style from './style.scss'

export const ScenarioTechnologyEditSection: React.FC = () => {
    const [scenariosList, setScenariosList] = useState<LifeScenarioModel[]>([])
    const [technologiesList, setTechnologiesList] = useState<KeyTechnologyModel[]>([])

    useEffect(() => {
        fetchScenarios()
        fetchTechnologies()
    }, [])

    const fetchScenarios = () => {
        projects.getLifeScenario()
            .then(res => setScenariosList(res.data))
    }

    const fetchTechnologies = () => {
        projects.getKeyTechnology()
            .then(res => setTechnologiesList(res.data))
    }

    const handleScenarioDelete = (id: number) => {
        admin.deleteLifeScenario(id).then(fetchScenarios)
    }

    const handleTechnologyDelete = (id: number) => {
        admin.deleteKeyTechnology(id).then(fetchTechnologies)
    }

    const lifeScenariosContent = scenariosList.map(item => (
        <Item
            key={item.id}
            data={item}
            onDelete={handleScenarioDelete}
        />
    ))

    const keyTechnologiesContent = technologiesList.map(item => (
        <Item
            key={item.id}
            data={item}
            onDelete={handleTechnologyDelete}
        />
    ))

    return (
        <div className={style.scenarioTechnologySection}>
            <AppCard
                header='Жизненные сценарии'
                className={style.card}
            >
                {lifeScenariosContent}
                <p className={style.footnote}>
                    <i>Примечание: </i>
                    Для удаления элемента у него не должно быть прикрепленных проектов.
                    После удаления требуется перезагрузка страницы.
                </p>
            </AppCard>
            <AppCard
                header='Ключевые технологии'
                className={style.card}
            >
                {keyTechnologiesContent}
                <p className={style.footnote}>
                    <i>Примечание: </i>
                    Для удаления элемента у него не должно быть прикрепленных проектов.
                    После удаления требуется перезагрузка страницы.
                </p>
            </AppCard>
        </div>
    )
}

interface ItemProps {
    data: LifeScenarioModel | KeyTechnologyModel
    onDelete: (id: number) => void
}

const Item: React.FC<ItemProps> = ({
                                       data,
                                       onDelete,
                                   }) => (
    <div className={style.item}>
        <span className={style.name}>{data.name}</span>
        <AppButton
            buttonType='transparent'
            onClick={() => onDelete(data.id)}
            icon={<DeleteRounded/>}
        />
    </div>
)
