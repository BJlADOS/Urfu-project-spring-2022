import React, { useState } from 'react'
import { AppCard } from 'app/components/AppCard'
import { RoleModel } from 'app/models'
import { Close, DeleteRounded, Done, EditRounded } from '@material-ui/icons'
import { admin } from 'app/provider'

import { AppButton } from '../AppButton'
import { AppInput } from '../AppInput'

import style from './style.scss'

interface Props {
  role: RoleModel
  onEditSideEffect: () => void
}

export const RoleEditCard: React.FC<Props> = ({
  role,
  onEditSideEffect,
}) => {
  const [editing, setEditing] = useState(false)
  const [roleName, setRoleName] = useState(role.name)

  const handleChangeName = () => {
    admin.updateProjectRole(role.id, roleName)
      .then(() => setEditing(false))
      .then(onEditSideEffect)
  }

  const handleResetEdit = () => {
    setEditing(false)
    setRoleName(role.name)
  }

  const handleRoleDelete = () => {
    admin.deleteProjectRole(role.id).then(onEditSideEffect)
  }

  const defaultRole = (
    <>
      <h4>{role.name}</h4>
      <div className={style.roleButtons}>
        <AppButton
          buttonType='transparent'
          icon={<EditRounded />}
          onClick={() => setEditing(true)}
        />
        <AppButton
          buttonType='transparent'
          icon={<DeleteRounded />}
          onClick={handleRoleDelete}
        />
      </div>
    </>
  )

  const editRole = (
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
          onClick={handleChangeName}
        />
        <AppButton
          type='button'
          buttonType='transparent'
          icon={<Close />}
          onClick={handleResetEdit}
        />
      </div>
    </>
  )

  const cardContent = editing ? editRole : defaultRole

  return (
    <AppCard
      className={style.dialogInnerCard}
      contentClassName={style.roleCardContent}
    >
      {cardContent}
    </AppCard>
  )
}
