import React, {useState} from 'react'
import {AppCard} from 'app/components/AppCard'
import {AppDialog} from 'app/components/AppDialog'
import {DialogProps, TextField} from '@material-ui/core'
import {AppButton} from 'app/components/AppButton'

import style from './style.scss'

interface AddTeamSlotDialogProps {
    onSave: (date: Date) => void
}

interface Props extends AddTeamSlotDialogProps, DialogProps {
}

export const AddTeamSlotDialog: React.FC<Props> = ({
                                                       open,
                                                       onSave,
                                                       ...props
                                                   }) => {
    const [date, setDate] = useState(new Date().toISOString().slice(0, 16))

    return (
        <AppDialog
            open={open}
            width='400px'
            {...props}
        >
            <div>
                <AppCard className={style.dialogCard}>
                    <h3>Добавить слот для записи</h3>
                </AppCard>
                <AppCard className={style.dialogCard}>
                    <TextField
                        label='Выберите дату и время'
                        type='datetime-local'
                        InputLabelProps={{
                            shrink: true,
                        }}
                        value={date}
                        onChange={(v) => setDate(v.target.value)}
                    />
                    <AppButton
                        onClick={() => onSave(new Date(date))}
                        className={style.addSlot}
                    >
                        Добавить
                    </AppButton>
                </AppCard>
            </div>
        </AppDialog>
    )
}
