import React, { useEffect, useState } from 'react'
import { admin } from 'app/provider'
import { AppCard } from 'app/components/AppCard'
import { RoleModel, UserModel, UserTypes } from 'app/models'
import { AppDialog } from 'app/components/AppDialog'
import { AppSearch } from 'app/components/AppInput'
import { CompetenciesList } from 'app/components/CompetenciesList'
import { DialogProps } from '@material-ui/core'

import { AppDropdown } from '../AppDropdown'

import style from './style.scss'

interface AddFreeStudentDialogProps {
  teamId: number
  roles: RoleModel[]
  onAddSideEffect: () => void
}

interface Props extends AddFreeStudentDialogProps, DialogProps {
}

export const AddFreeStudentDialog: React.FC<Props> = ({
  teamId,
  roles,
  open,
  onAddSideEffect,
  ...props
}) => {
  const [freeStudents, setFreeStudents] = useState<UserModel[]>([])
  const [search, setSearch] = useState('')

  useEffect(() => {
    if (open && freeStudents.length === 0) {
      fetchFreeStudent()
    }
  }, [open])

  const fetchFreeStudent = (search?: string) => {
    admin.getFreeStudents(search || '').then(res => setFreeStudents(res.data))
  }

  const handleFreeStudentAdd = (userId: number, roleId: number) => {
    admin.joinUserToProject(userId, teamId, roleId)
      .then(onAddSideEffect)
      .then(() => setFreeStudents(freeStudents.filter(user => user.id !== userId)))
  }

  const rolesDataConverter = (value: RoleModel) => ({
    key: value.id,
    label: value.name,
  })

  const filterWithTeamlead = roles.filter(x => x.name.startsWith('Тимлид'))
  const filterWithoutTeamlead = roles.filter(x => !x.name.startsWith('Тимлид'))

  const freeUserCardHeader = (user: UserModel) => (
    <div className={style.freeUserCardHeader}>
      <span>{user.firstName} {user.lastName}</span>
      <div>
        <AppDropdown
          items={user.userType === UserTypes.Teamlead ? filterWithTeamlead : filterWithoutTeamlead}
          dataConverter={rolesDataConverter}
          onChange={(role) => handleFreeStudentAdd(user.id, role?.id ?? 0)}
          placeholder='Роль'
          readOnly
        />
      </div>
    </div>
  )

  const freeStudentsList = freeStudents.map(user => (
    <AppCard
      key={user.id}
      className={style.dialogInnerCard}
      header={freeUserCardHeader(user)}
    >
      <CompetenciesList list={user.competencies}/>
    </AppCard>
  ))

  return (
    <AppDialog
      open={open}
      {...props}
    >
      <div className={style.dialogContent}>
        <AppCard className={style.dialogInnerCard}>
          <h3>Свободные участники</h3>
        </AppCard>
        <AppCard className={style.dialogInnerCard}>
          <AppSearch
            placeholder='Поиск участников'
            value={search}
            onChangeSearch={(value) => fetchFreeStudent(value)}
            onChange={(e) => setSearch(e.target.value)}
          />
        </AppCard>
        {freeStudentsList}
      </div>
    </AppDialog>
  )
}
