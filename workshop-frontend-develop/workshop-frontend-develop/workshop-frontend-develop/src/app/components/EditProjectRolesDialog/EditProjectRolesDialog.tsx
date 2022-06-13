import React from 'react'
import { AppCard } from 'app/components/AppCard'
import { RoleModel } from 'app/models'
import { AppDialog } from 'app/components/AppDialog'
import { DialogProps } from '@material-ui/core'
import { admin } from 'app/provider'

import { RoleEditCard } from './RoleEditCard'
import { AddProjectRoleCard } from './AddProjectRoleCard'
import style from './style.scss'

interface EditProjectRolesDialogProps {
  projectId: number
  roles: RoleModel[]
  onEditSideEffect: () => void
}

interface Props extends EditProjectRolesDialogProps, DialogProps { }

export const EditProjectRolesDialog: React.FC<Props> = ({
  roles,
  projectId,
  onEditSideEffect,
  open,
  ...props
}) => {
  const rolesList = roles.map(item => (
    <RoleEditCard
      key={item.id}
      role={item}
      onEditSideEffect={onEditSideEffect}
    />
  ))

  const handleRoleAdd = (roleName: string) => {
    admin.addProjectRole(roleName, projectId).then(onEditSideEffect)
  }

  return (
    <AppDialog
      open={open}
      {...props}
    >
      <div className={style.dialogContent}>
        <AppCard className={style.dialogInnerCard}>
          <h3>Проектные роли</h3>
        </AppCard>
        {rolesList}
        <AddProjectRoleCard onSave={handleRoleAdd} />
      </div>
    </AppDialog>
  )
}
