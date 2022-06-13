import React from 'react'
import { ShortUserModel } from 'app/models'
import { AppCard } from 'app/components/AppCard'
import cls from 'classnames'

import style from './style.scss'

interface Props{
  className:string
  teamUser:ShortUserModel
  roleName:string
}

export const RequestUserTeamCard:React.FC<Props> = ({
  className,
  teamUser,
  roleName,
}) => {
  const cardClassName = cls(style.card, className)

  return (
    <AppCard
      className={cardClassName}
      header={`${teamUser.firstName} ${teamUser.lastName}`}

    >
      <span>Роль: <span className={style.highlighted}>{roleName}</span></span>
    </AppCard>
  )
}
