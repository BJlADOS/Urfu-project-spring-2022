import React, { useEffect, useState } from 'react'
import { expert } from 'app/provider'
import { AppCard } from 'app/components/AppCard'
import { AppDialog } from 'app/components/AppDialog'
import { DialogProps } from '@material-ui/core'
import { AddRounded } from '@material-ui/icons'
import { ExpertUserModel } from 'app/models'

import { AppButton } from '../AppButton'

import style from './style.scss'

interface AddExpertDialogProps {
  auditoriumId: number
}

interface Props extends AddExpertDialogProps, DialogProps { }

export const AddExpertDialog: React.FC<Props> = ({
  open,
  auditoriumId,
  ...props
}) => {
  const [experts, setExperts] = useState<ExpertUserModel[]>([])

  useEffect(() => {
    if (open) {
      fetchExperts()
    }
  }, [open])

  const fetchExperts = () => {
    expert.getExperts().then(res => setExperts(res.data))
  }

  const handleExpertAdd = (expertId: number) => {
    expert.updateExpertAuditorium({
      auditoriumId,
      expertId,
    }).then(fetchExperts)
  }

  const expertsList = experts.map(user => (
    <AppCard
      key={user.id}
      className={style.dialogInnerCard}
    >
      <div className={style.expert}>
        <h4>{user.lastName} {user.firstName} {user.middleName}</h4>
        <AppButton
          buttonType={user.auditoriumId === auditoriumId ? 'primary' : 'secondary'}
          icon={<AddRounded />}
          onClick={() => handleExpertAdd(user.id)}
        />
      </div>
    </AppCard>
  ))

  return (
    <AppDialog
      open={open}
      {...props}
    >
      <div className={style.dialogContent}>
        <AppCard className={style.dialogInnerCard}>
          <h3>Список экспертов</h3>
        </AppCard>
        {expertsList}
      </div>
    </AppDialog>
  )
}
