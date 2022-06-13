import React, { useState } from 'react'
import { AddCircleOutline, Done, Close } from '@material-ui/icons'
import { AppCard } from 'app/components/AppCard'
import { AppButton } from 'app/components/AppButton'
import { AppInput } from 'app/components/AppInput'

import style from './style.scss'

interface Props {
  onSave: (roleName: string) => void
}

export const AddProjectRoleCard: React.FC<Props> = ({
  onSave,
}) => {
  const [roleName, setRoleName] = useState('')
  const [editing, setEditing] = useState(false)

  const handleSave = () => {
    onSave(roleName)
    handleReset()
  }

  const handleReset = () => {
    setEditing(false)
    setRoleName('')
  }

  const defaultNewRole = (
    <AppButton
      type='button'
      buttonType='transparent'
      icon={<AddCircleOutline />}
      className={style.startEditingButton}
      onClick={() => setEditing(true)}
    >
      <h4>Добавить новую роль</h4>
    </AppButton>
  )

  const editNewRole = (
    <>
      <AppInput
        value={roleName}
        onChange={(e) => setRoleName(e.target.value)}
        placeholder='Введите название роли'
      />
      <div className={style.roleButtons}>
        <AppButton
          type='button'
          buttonType='transparent'
          disabled={roleName === ''}
          icon={<Done />}
          onClick={handleSave}
        />
        <AppButton
          type='button'
          buttonType='transparent'
          icon={<Close />}
          onClick={handleReset}
        />
      </div>
    </>
  )

  const cardContent = editing ? editNewRole : defaultNewRole

  return (
    <AppCard
      className={style.dialogInnerCard}
      contentClassName={style.roleCardContent}
    >
      {cardContent}
    </AppCard>
  )
}
