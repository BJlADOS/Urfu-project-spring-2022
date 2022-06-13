import React, {useState} from 'react'
import {AddCircleOutline, Done, Close} from '@material-ui/icons'
import {AppCard} from 'app/components/AppCard'
import {AppButton} from 'app/components/AppButton'
import {AppInput} from 'app/components/AppInput'

import style from './style.scss'

interface Props {
    onSave: (name: string) => void
}

export const AuditoriumCreationCard: React.FC<Props> = ({
                                                            onSave,
                                                        }) => {
    const [auditoriumName, setAuditoriumName] = useState('')
    const [editing, setEditing] = useState(false)

    const handleItemSave = () => {
        onSave(auditoriumName)
        handleResetEdit()
    }

    const handleResetEdit = () => {
        setEditing(false)
        setAuditoriumName('')
    }

    const defaultNewItem = (
        <AppButton
            type='button'
            buttonType='transparent'
            icon={<AddCircleOutline/>}
            className={style.startEditingButton}
            onClick={() => setEditing(true)}
        >
            <h3>Добавить новую аудиторию</h3>
        </AppButton>
    )

    const editNewItem = (
        <>
            <AppInput
                value={auditoriumName}
                className={style.auditoriumNameInput}
                onChange={(e) => setAuditoriumName(e.target.value)}
                placeholder='Введите название'
            />
            <div className={style.controlButtons}>
                <AppButton
                    type='button'
                    buttonType='transparent'
                    disabled={auditoriumName === ''}
                    icon={<Done/>}
                    onClick={handleItemSave}
                />
                <AppButton
                    type='button'
                    buttonType='transparent'
                    icon={<Close/>}
                    onClick={handleResetEdit}
                />
            </div>
        </>
    )

    const cardContent = editing ? editNewItem : defaultNewItem

    return (
        <AppCard
            className={style.row}
            contentClassName={style.content}
        >
            {cardContent}
        </AppCard>
    )
}
