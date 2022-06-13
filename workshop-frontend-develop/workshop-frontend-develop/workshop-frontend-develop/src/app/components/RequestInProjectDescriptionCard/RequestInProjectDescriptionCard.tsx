import React from 'react'
import { UserRequestProposalModel } from 'app/models/RequestProposalModel'
import cls from 'classnames'
import { AppCard } from 'app/components/AppCard'
import { AppDivider } from 'app/components/AppDivider'

import style from './style.scss'
interface Props{
  request:UserRequestProposalModel
  className?:string
}

const RequestInProjectDescriptionCard:React.FC<Props> = ({
  request,
  className,
}) => {
  const header = (
    <div className={style.header}>
      <h3>Описание проекта</h3>
    </div>
  )
  const cardClassName = cls(style.card, className)

  return (<>
    <AppCard
      header={header}
      className={cardClassName}
    >
      <p>{request.description}</p>
      <AppDivider/>
      <h4>Цель</h4>
      <p>{request.purpose}</p>
      <AppDivider/>
      <h4>Итоговый продукт</h4>
      <p>{request.result}</p>
    </AppCard>
  </>

  )
}

export default RequestInProjectDescriptionCard
