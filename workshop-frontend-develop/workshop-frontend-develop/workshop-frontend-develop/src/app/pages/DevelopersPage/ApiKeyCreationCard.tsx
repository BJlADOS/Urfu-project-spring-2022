import React, {useState} from 'react'
import {AddCircleOutline, Close, Done} from '@material-ui/icons'
import {AppCard} from 'app/components/AppCard'
import {AppButton} from 'app/components/AppButton'
import {AppInput} from 'app/components/AppInput'
import {UserTypes} from 'app/models'
import {AppDropdown, DropdownOptionItem} from 'app/components/AppDropdown'
import style from './style.scss'

const userTypesMap = new Map([
    [UserTypes.Admin, 'Администратор'],
    [UserTypes.Expert, 'Эксперт'],
    [UserTypes.Student, 'Студент'],
])

const userTypesList = [...userTypesMap.keys()]

interface Props {
    onSave: (name: string, role: UserTypes) => void
}

export const ApiKeyCreationCard: React.FC<Props> = ({
                                                        onSave,
                                                    }) => {
    const [keyName, setKeyName] = useState('')
    const [keyRole, setKeyRole] = useState<UserTypes>()
    const [editing, setEditing] = useState(false)

    const handleKeySave = () => {
        if (!keyRole) {
            return
        }
        onSave(keyName, keyRole)
        handleResetEdit()
    }

    const dataConverter = (value: UserTypes): DropdownOptionItem<UserTypes> => ({
        label: userTypesMap.get(value) || '',
        key: value,
    })

    const handleResetEdit = () => {
        setEditing(false)
        setKeyName('')
    }

    const defaultNewKey = (
        <AppButton
            type='button'
            buttonType='transparent'
            icon={<AddCircleOutline/>}
            className={style.startEditingButton}
            onClick={() => setEditing(true)}
        >
            <h3>Создать API ключ</h3>
        </AppButton>
    )

    const editNewKey = (
        <>
            <div className={style.keyInputs}>
                <AppInput
                    value={keyName}
                    onChange={(e) => setKeyName(e.target.value)}
                    placeholder='Введите название ключа'
                />
                <AppDropdown
                    items={userTypesList}
                    dataConverter={dataConverter}
                    onChange={(v) => setKeyRole(v)}
                    placeholder='Роль'
                    readOnly
                />
            </div>
            <div className={style.controlButtons}>
                <AppButton
                    buttonType='transparent'
                    disabled={!keyName || !keyRole}
                    icon={<Done/>}
                    onClick={handleKeySave}
                />
                <AppButton
                    buttonType='transparent'
                    icon={<Close/>}
                    onClick={handleResetEdit}
                />
            </div>
        </>
    )

    const cardContent = editing ? editNewKey : defaultNewKey

    return (
        <AppCard
            className={style.card}
            contentClassName={style.cardContent}
        >
            {cardContent}
        </AppCard>
    )
}
