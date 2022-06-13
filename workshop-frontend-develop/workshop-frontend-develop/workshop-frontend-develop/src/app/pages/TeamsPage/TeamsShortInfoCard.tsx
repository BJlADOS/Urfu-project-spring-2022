import React from 'react'
import {AppLinkCard} from 'app/components/AppCard'
import {TeamListItem} from 'app/models'
import {team as teamNav} from 'app/nav'

import style from './style.scss'

interface Props {
    team: TeamListItem
}

export const TeamsShortInfoCard: React.FC<Props> = ({
                                                        team,
                                                    }) => {
    const header = (
        <h3>
            {team.name || `Команда №${team.id}`}
            <span className={style.highlight}>
        ({team.usersCount}/{team.teamCapacity})
      </span>
        </h3>
    )

    return (
        <AppLinkCard
            header={header}
            className={style.card}
            to={teamNav(team.id)}
        >
            <h4>{team.projectName}</h4>
            <p>{team.projectDescription}</p>
        </AppLinkCard>
    )
}
