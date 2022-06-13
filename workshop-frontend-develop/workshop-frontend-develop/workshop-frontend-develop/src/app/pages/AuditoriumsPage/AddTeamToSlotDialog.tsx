import React, { useEffect, useState } from 'react'
import cls from 'classnames'
import { AppCard } from 'app/components/AppCard'
import { AppDialog } from 'app/components/AppDialog'
import { DialogProps } from '@material-ui/core'
import { TeamListItem, TeamStatusses } from 'app/models'
import { expert, user } from 'app/provider'
import { AppSearch } from 'app/components/AppInput'

import style from './style.scss'

interface AddTeamToSlotDialogProp {
  slotId: number | null
  onSave: () => void
}

interface Props extends AddTeamToSlotDialogProp, DialogProps {
}

export const AddTeamToSlotDialog: React.FC<Props> = ({
  open,
  slotId,
  onSave,
  ...props
}) => {
  const [teams, setTeams] = useState<TeamListItem[]>([])

  useEffect(() => {
    if (open) {
      fetchTeams()
    }
  }, [open])

  const fetchTeams = (search?: string) => {
    expert.getTeams(search || '').then(res => setTeams(res.data))
  }

  const handleSearch = (value: string) => {
    fetchTeams(value)
  }

  const handleTeamAddToSlot = (teamId: number) => {
    if (slotId !== null) {
      user.enrollToSlot(slotId, teamId)
        .then(onSave)
    }
  }

  const header = (team: TeamListItem) => (
    <h3>
      {team.name || `Команда №${team.id}`}
      <span className={style.highlight}>
        ({team.usersCount}/{team.teamCapacity})
      </span>
    </h3>
  )

  const teamsCards = teams
    .filter(t => t.teamStatus !== TeamStatusses.Incomplete && !t.teamSlot)
    .map(team => (
      <AppCard
        key={team.id}
        header={header(team)}
        onClick={() => handleTeamAddToSlot(team.id)}
        className={cls(style.dialogCard, style.clickable)}
      >
        <h4>{team.projectName}</h4>
        <p>{team.projectDescription}</p>
      </AppCard>
    ))

  return (
    <AppDialog
      open={open}
      {...props}
    >
      <div>
        <AppCard className={style.dialogCard}>
          <h2>Список команд</h2>
        </AppCard>
        <AppCard className={style.dialogCard}>
          <AppSearch
            onChangeSearch={handleSearch}
            placeholder='Поиск команды'
          />
        </AppCard>
        {teamsCards}
      </div>
    </AppDialog>
  )
}
