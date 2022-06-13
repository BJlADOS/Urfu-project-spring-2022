import React, { useEffect, useState } from 'react'
import { expert } from 'app/provider'
import { AppCard } from 'app/components/AppCard'
import { AuditoriumModel } from 'app/models'
import { AppDialog } from 'app/components/AppDialog'
import { DialogProps } from '@material-ui/core'
import { CheckRounded } from '@material-ui/icons'

import { AppButton } from '../AppButton'

import style from './style.scss'

interface ChangeTeamAuditoriumDialogProps {
  selectedId?: number
  onItemSelect: (id: number) => void
}

interface Props extends ChangeTeamAuditoriumDialogProps, DialogProps { }

export const ChangeTeamAuditoriumDialog: React.FC<Props> = ({
  open,
  selectedId,
  onItemSelect,
  ...props
}) => {
  const [auditoriums, setAuditoriums] = useState<AuditoriumModel[]>([])

  useEffect(() => {
    if (open && auditoriums.length === 0) {
      fetchAuditoriums()
    }
  }, [open])

  const fetchAuditoriums = () => {
    expert.getAuditoriums().then(res => setAuditoriums(res.data))
  }

  const expertsList = auditoriums.map(item => (
    <AppCard
      key={item.id}
      className={style.dialogInnerCard}
    >
      <div className={style.item}>
        <h4>{item.name}</h4>
        <AppButton
          buttonType={selectedId === item.id ? 'primary' : 'secondary'}
          icon={<CheckRounded />}
          onClick={() => onItemSelect(item.id)}
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
          <h3>Список аудиторий</h3>
        </AppCard>
        {expertsList}
      </div>
    </AppDialog>
  )
}
