import React, {useEffect, useState} from 'react'
import {admin} from 'app/provider'
import {AppCard} from 'app/components/AppCard'
import {ApiKeyModel, UserTypes} from 'app/models'
import {AppButton} from 'app/components/AppButton'
import {DeleteRounded} from '@material-ui/icons'
import {ApiKeyCreationCard} from './ApiKeyCreationCard'
import style from './style.scss'

export const DevelopersPage: React.FC = () => {
    const [keysList, setKeysList] = useState<ApiKeyModel[]>([])

    const fetchApiKeys = () => {
        admin.getApiKeys()
            .then((res) => setKeysList(res.data))
    }

    useEffect(() => {
        fetchApiKeys()
    }, [])

    const handleAddKey = (keyName: string, keyRole: UserTypes) => {
        admin.createApiKey(keyName, keyRole).then(fetchApiKeys)
    }

    const handleDeleteKey = (id: number) => {
        admin.deleteApiKey(id).then(fetchApiKeys)
    }

    const cards = keysList.map(item => (
        <AppCard
            key={item.id}
            className={style.card}
            contentClassName={style.cardContent}
        >
            <div className={style.keyInfo}>
                <span className={style.type}>{item.userType}</span>
                <span
                    className={style.name}
                    title={item.name}
                >
          {item.name}
        </span>
                <span className={style.key}>{item.keyString}</span>
            </div>
            <div className={style.controlButtons}>
                <AppButton
                    buttonType='transparent'
                    icon={<DeleteRounded/>}
                    onClick={() => handleDeleteKey(item.id)}
                />
            </div>
        </AppCard>
    ))

    return (
        <>
            <AppCard>
                <h2>Для разработчиков</h2>
            </AppCard>
            {cards}
            <ApiKeyCreationCard onSave={handleAddKey}/>
        </>
    )
}
