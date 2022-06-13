import React, { useEffect, useState } from 'react'
import { UsersListItemModel, UserTypes } from 'app/models'
import { admin } from 'app/provider'
import { AppCard, AppLinkCard } from 'app/components/AppCard'
import { AppSearch } from 'app/components/AppInput'
import { userInfo } from 'app/nav'

import style from './style.scss'

export const userTypesMap = new Map([
  [UserTypes.Admin, 'Администратор'],
  [UserTypes.Expert, 'Эксперт'],
  [UserTypes.Student, 'Студент'],
  [UserTypes.Teamlead, 'Тимлид'],
])

export const UsersListPage: React.FC = () => {
  const [usersList, setUsersList] = useState<UsersListItemModel[]>([])
  const [searchField, setSearchField] = useState('')

  useEffect(() => {
    fetchUsers()
  }, [])

  const fetchUsers = (term?: string) => {
    admin.getUsers(term || '')
      .then((res) => setUsersList(res.data))
  }

  const handleSearch = (value: string) => {
    setSearchField(value)
    fetchUsers(value)
  }

  const userCardHeader = (user: UsersListItemModel) => (
    <div className={style.userCardHeader}>
      <span>{user.lastName} {user.firstName} {user.middleName}</span>
      <span className={style.userRole}>{userTypesMap.get(user.userType)}</span>
    </div>
  )

  const users = usersList.map(user => (
    <AppLinkCard
      key={user.id}
      className={style.card}
      header={userCardHeader(user)}
      to={userInfo(user.id)}
    >
      <span>Id: {user.id}</span>
      <span>Логин: {user.login}</span>
      <span>Контакт: {user.phoneNumber}</span>
      <span>Почта: {user.email}</span>
      {user.academicGroup && <span>Академическая группа: {user.academicGroup}</span>}
    </AppLinkCard>
  ))

  return (
    <>
      <AppCard className={style.card}>
        <h2>Управление пользователями</h2>
      </AppCard>
      <AppCard className={style.card}>
        <AppSearch
          value={searchField}
          onChange={(e) => setSearchField(e.target.value)}
          onChangeSearch={handleSearch}
          placeholder='Поиск пользователя'
        />
      </AppCard>
      {users}
    </>
  )
}
