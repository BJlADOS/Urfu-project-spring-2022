import React, { useEffect, useState } from 'react'
import { expert } from 'app/provider'
import { AppCard } from 'app/components/AppCard'
import { TeamWithSlotListItem } from 'app/models'
import { AppSearch } from 'app/components/AppInput'

import { TeamsExpertShortInfoCard } from './TeamsExpertShortInfoCard'
import style from './style.scss'

export const ExpertTeamsPage: React.FC = () => {
  const [teamsList, setTeamsList] = useState<TeamWithSlotListItem[]>([])

  useEffect(() => {
    fetchTeams()
  }, [])

  const fetchTeams = (term?: string) => {
    expert.getTeamsForReview(term || '')
      .then((res) => setTeamsList(res.data))
  }

  const handleSearch = (value: string) => {
    fetchTeams(value)
  }

  const sortTeams = (a: TeamWithSlotListItem, b: TeamWithSlotListItem): number => Number(new Date(a.slotTime)) - Number(new Date(b.slotTime))

  const teams = (array: TeamWithSlotListItem[]) => array.sort(sortTeams)
    .map(team => (
      <TeamsExpertShortInfoCard
        key={team.id}
        team={team}
      />
    ))

  const groupedTeamsList = teamsList.reduce((acc, curr) => {
    acc[curr.auditoriumId] = acc[curr.auditoriumId] || []
    acc[curr.auditoriumId].push(curr)
    return acc
  }, {} as Record<number, TeamWithSlotListItem[]>)

  const teamsSection = Object.keys(groupedTeamsList).map(key => {
    const array = groupedTeamsList[Number(key)]
    const auditoriumName = array[0]?.auditoriumName || `(id:${key})`

    return (
      <React.Fragment key={key}>
        <AppCard className={style.card}>
          <h3>Аудитория: {auditoriumName}</h3>
        </AppCard>
        {teams(array)}
      </React.Fragment>
    )
  })

  return (
    <>
      <AppCard className={style.card}>
        <h2>Команды для оценки</h2>
      </AppCard>
      <AppCard className={style.card}>
        <AppSearch
          onChangeSearch={handleSearch}
          placeholder='Поиск команды'
        />
      </AppCard>
      {teamsSection}
    </>
  )
}
