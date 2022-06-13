import React, { useEffect, useState } from 'react'
import { DetailedUserModel, UserTypes } from 'app/models'
import { admin } from 'app/provider'
import { AppCard } from 'app/components/AppCard'
import { AppDropdown, DropdownOptionItem } from 'app/components/AppDropdown'
import { useParams } from 'react-router-dom'
import { AppLoadingSpinner } from 'app/components/AppLoadingSpinner'
import { AppButton } from 'app/components/AppButton'
import { FindInPageRounded } from '@material-ui/icons'
import { project, team } from 'app/nav'
import { CompetenciesList } from 'app/components/CompetenciesList'

import style from './style.scss'

const userTypesMap = new Map([
  [UserTypes.Admin, 'Администратор'],
  [UserTypes.Expert, 'Эксперт'],
  [UserTypes.Student, 'Студент'],
  [UserTypes.Teamlead, 'Тимлид'],
])

const userTypesList = [...userTypesMap.keys()]

export const UserInfoPage: React.FC = () => {
  const [user, setUser] = useState<DetailedUserModel>()

  const { userId } = useParams<{ userId: string }>()

  useEffect(() => {
    fetchUser()
  }, [])

  const fetchUser = () => {
    admin.getUser(Number(userId))
      .then((res) => setUser(res.data))
  }

  if (!user) {
    return <AppLoadingSpinner/>
  }

  const handleUserTypeChange = (id: number, type: UserTypes) => {
    admin.changeUserType(id, type).then(fetchUser)
  }

  const dataConverter = (value: UserTypes): DropdownOptionItem<UserTypes> => ({
    label: userTypesMap.get(value) || '',
    key: value,
  })

  const projectSection = user.project && (
    <AppCard
      className={style.card}
      contentClassName={style.cardInnerRow}
    >
      <h3>Проект: {user.project.name}</h3>
      <AppButton
        buttonType='transparent'
        to={project(user.project.id)}
        icon={<FindInPageRounded/>}
      />
    </AppCard>
  )

  const teamSection = user.team && (
    <AppCard
      className={style.card}
      contentClassName={style.cardInnerRow}
    >
      <h3>Команда: {user.team.name || `Команда №${user.team.id}`}</h3>
      <AppButton
        buttonType='transparent'
        to={team(user.team.id)}
        icon={<FindInPageRounded/>}
      />
    </AppCard>
  )

  const projectRoleSection = user.role && (
    <AppCard
      className={style.card}
      contentClassName={style.cardInnerRow}
    >
      <h3>Роль в проекте: {user.role.name}</h3>
    </AppCard>
  )

  const competenciesSection = user.competencies && user.competencies.length > 0 && (
    <AppCard
      className={style.card}
      contentClassName={style.cardInnerRow}
      header='Компетенции'
    >
      <CompetenciesList list={user.competencies}/>
    </AppCard>
  )

  const userProfileSection = (
    <AppCard
      header='Данные пользователя'
      className={style.card}
    >
      <span>Id: {user.id}</span>
      <span>Логин: {user.login}</span>
      <span>Контакт: {user.phoneNumber}</span>
      <span>Почта: {user.email}</span>
      <span>Дата регистрации: {new Date(user.registrationDate).toLocaleString()}</span>
      {user.academicGroup && <span>Академическая группа: {user.academicGroup}</span>}
      {user.about && <span>О пользователе: {user.about}</span>}
    </AppCard>
  )

  return (
    <>
      <AppCard className={style.card}>
        <h2>{user.lastName} {user.firstName} {user.middleName}</h2>
      </AppCard>
      <AppCard
        className={style.card}
        contentClassName={style.cardInnerRow}
      >
        <h3>Роль пользователя</h3>
        <AppDropdown
          items={userTypesList}
          dataConverter={dataConverter}
          value={user.userType}
          onChange={(value) => handleUserTypeChange(user.id, value || UserTypes.Student)}
          placeholder='Роль'
          readOnly
        />
      </AppCard>
      {projectSection}
      {teamSection}
      {projectRoleSection}
      {competenciesSection}
      {userProfileSection}
    </>
  )
}
