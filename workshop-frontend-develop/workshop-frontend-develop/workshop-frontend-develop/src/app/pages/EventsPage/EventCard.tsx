import React, {useEffect, useState} from 'react'
import cls from 'classnames'
import {AccountCircleRounded, Close, Create, Done, Visibility, VisibilityOff} from '@material-ui/icons'
import {AppCard} from 'app/components/AppCard'
import {AppButton} from 'app/components/AppButton'
import {EventModel} from 'app/models/EventModel'
import {AppInput} from 'app/components/AppInput'

import style from './style.scss'

interface Props {
    event: EventModel
    isCurrent: boolean
    onSave: (event: EventModel) => void
}

export const EventCard: React.FC<Props> = ({
                                               event,
                                               onSave,
                                               isCurrent,
                                           }) => {
    const [editing, setEditing] = useState(false)
    const [eventName, setEventName] = useState('')

    useEffect(() => {
        setEventName(event.name)
    }, [event])

    const handleChangeVisibility = () => {
        onSave({...event, isActive: !event.isActive})
    }

    const handleChangeName = () => {
        onSave({...event, name: eventName})
        setEditing(false)
    }

    const handleResetEdit = () => {
        setEditing(false)
        setEventName(event.name)
    }

    const defaultEvent = (
        <>
            <h3 className={cls({
                [style.disabledEvent]: !event.isActive,
            })}
            >
                {event.name}
                {isCurrent && <AccountCircleRounded className={style.icon}/>}
            </h3>
            <div className={style.controlButtons}>
                <AppButton
                    type='button'
                    buttonType='transparent'
                    icon={<Create/>}
                    onClick={() => setEditing(true)}
                />
                <AppButton
                    type='button'
                    buttonType='transparent'
                    icon={event.isActive ? <Visibility/> : <VisibilityOff/>}
                    onClick={handleChangeVisibility}
                />
            </div>
        </>
    )

    const editEvent = (
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
                    onClick={handleChangeName}
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

    const cardContent = editing ? editEvent : defaultEvent

    return (
        <AppCard
            className={style.eventCard}
            contentClassName={style.eventContent}
        >
            {cardContent}
        </AppCard>
    )
}
