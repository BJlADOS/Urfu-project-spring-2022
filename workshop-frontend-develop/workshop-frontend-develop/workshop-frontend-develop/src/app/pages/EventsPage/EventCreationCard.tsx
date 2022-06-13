import React, {useState} from 'react'
import {AddCircleOutline, Close, Done} from '@material-ui/icons'
import {AppCard} from 'app/components/AppCard'
import {AppButton} from 'app/components/AppButton'
import {AppInput} from 'app/components/AppInput'

import style from './style.scss'

interface Props {
    onSave: (eventName: string) => void
}

export const EventCreationCard: React.FC<Props> = ({
                                                       onSave,
                                                   }) => {
    const [eventName, setEventName] = useState('')
    const [editing, setEditing] = useState(false)

    const handleEventSave = () => {
        onSave(eventName)
        handleResetEdit()
    }

    const handleResetEdit = () => {
        setEditing(false)
        setEventName('')
    }

    const defaultNewEvent = (
        <AppButton
            type='button'
            buttonType='transparent'
            icon={<AddCircleOutline/>}
            className={style.startEditingButton}
            onClick={() => setEditing(true)}
        >
            <h3>Добавить новое событие</h3>
        </AppButton>
    )

    const editNewEvent = (
        <>
            <AppInput
                value={eventName}
                onChange={(e) => setEventName(e.target.value)}
                placeholder='Введите название события'
            />
            <div className={style.controlButtons}>
                <AppButton
                    type='button'
                    buttonType='transparent'
                    disabled={eventName === ''}
                    icon={<Done/>}
                    onClick={handleEventSave}
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

    const cardContent = editing ? editNewEvent : defaultNewEvent

    return (
        <AppCard
            className={style.eventCard}
            contentClassName={style.eventContent}
        >
            {cardContent}
        </AppCard>
    )
}
