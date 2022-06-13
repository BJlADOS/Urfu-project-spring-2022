import React, {useEffect, useState} from 'react'
import {expert} from 'app/provider'
import {AppCard} from 'app/components/AppCard'
import {TeamListItem, TeamStatusses} from 'app/models'
import {AppSearch} from 'app/components/AppInput'

import {TeamsShortInfoCard} from './TeamsShortInfoCard'
import style from './style.scss'

export const TeamsPage: React.FC = () => {
    const [teamsList, setTeamsList] = useState<TeamListItem[]>([])

    useEffect(() => {
        fetchTeams()
    }, [])

    const fetchTeams = (term?: string) => {
        expert.getTeams(term || '')
            .then((res) => setTeamsList(res.data))
    }

    const handleSearch = (value: string) => {
        fetchTeams(value)
    }

    const completedTeams = teamsList
        .filter(t => t.teamStatus === TeamStatusses.Completed || t.isEntried)
        .map(team => (
            <>
                <TeamsShortInfoCard
                    key={team.id}
                    team={team}
                />
            </>
        ))

    const incompleteTeams = teamsList
        .filter(t => t.teamStatus !== TeamStatusses.Completed)
        .map(team => (
            <TeamsShortInfoCard
                key={team.id}
                team={team}
            />
        ))

    return (
        <>
            <AppCard className={style.card}>
                <h2>Список команд</h2>
            </AppCard>
            <AppCard className={style.card}>
                <AppSearch
                    onChangeSearch={handleSearch}
                    placeholder='Поиск команды'
                />
            </AppCard>
            <AppCard className={style.card}>
                <h3>Зарегистрированные команды</h3>
            </AppCard>
            {completedTeams}
            <AppCard className={style.card}>
                <h3>Незарегистрированные команды</h3>
            </AppCard>
            {incompleteTeams}
        </>
    )
}
