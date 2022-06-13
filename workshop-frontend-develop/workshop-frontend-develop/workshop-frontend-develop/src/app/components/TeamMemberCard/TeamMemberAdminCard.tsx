import React from 'react'
import { AppCard } from 'app/components/AppCard'
import { DeleteOutlineRounded, MailOutlineRounded, PhoneRounded } from '@material-ui/icons'
import { RoleModel, UserCompetencyType, UserModel } from 'app/models'

import { AppDivider } from '../AppDivider'
import { CompetenciesList } from '../CompetenciesList'
import { AppButton } from '../AppButton'
import { AppDropdown, DropdownOptionItem } from '../AppDropdown'

import style from './style.scss'

interface Props {
  user: UserModel
  roles: RoleModel[]
  onRoleChange: (userId: number, roleId: number) => void
  onDelete: (userId: number) => void
}

export const TeamMemberAdminCard: React.FC<Props> = ({
  user,
  roles,
  onRoleChange,
  onDelete,
}) => {
  const userFullName = `${user.firstName} ${user.lastName}`
  const cardHeader = (
    <div className={style.cardHeader}>
      <span>{userFullName}</span>
      <AppButton
        icon={<DeleteOutlineRounded />}
        onClick={() => onDelete(user.id)}
        type='button'
        buttonType='danger'
      />
    </div>
  )

  const currentCompetenciesList = user.competencies.filter(item => item.userCompetencyType === UserCompetencyType.Current)
  const desirableCompetenciesList = user.competencies.filter(item => item.userCompetencyType === UserCompetencyType.Desirable)

  const dataConverter = (value: RoleModel): DropdownOptionItem<RoleModel> => ({
    label: value.name,
    key: value.id,
  })

  const handleRoleChange = (value?: RoleModel) => {
    if (value) {
      onRoleChange(user.id, value.id)
    }
  }

  const currentCompetencies = currentCompetenciesList.length > 0 && (
    <>
      <AppDivider />
      <h4>Компетенции</h4>
      <CompetenciesList list={currentCompetenciesList}/>
    </>
  )

  const desirableCompetencies = desirableCompetenciesList.length > 0 && (
    <>
      <AppDivider />
      <h4>Желаемые компетенции</h4>
      <CompetenciesList list={desirableCompetenciesList}/>
    </>
  )

  const phone = user.phoneNumber && (
    <div className={style.contact}>
      <PhoneRounded />
      <span>{user.phoneNumber}</span>
    </div>
  )

  const email = user.email && (
    <div className={style.contact}>
      <MailOutlineRounded />
      <span>{user.email}</span>
    </div>
  )

  return (
    <>
      <AppCard
        header={cardHeader}
        className={style.card}
      >
        <h4>Роль</h4>
        <AppDropdown
          items={roles}
          dataConverter={dataConverter}
          value={user.role}
          onChange={handleRoleChange}
          placeholder='Роль'
          readOnly
        />
        <AppDivider />
        <h4>Контакты</h4>
        <div>
          {phone}
          {email}
        </div>
        {currentCompetencies}
        {desirableCompetencies}
      </AppCard>
    </>
  )
}
