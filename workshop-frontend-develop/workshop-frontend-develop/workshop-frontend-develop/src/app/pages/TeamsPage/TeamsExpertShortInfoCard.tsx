import React from 'react'
import {AppLinkCard} from 'app/components/AppCard'
import {TeamWithSlotListItem} from 'app/models'
import {team as teamNav} from 'app/nav'
import {getSlotDateTime} from 'app/utils/getSlotDateTime'

import style from './style.scss'

interface Props {
    team: TeamWithSlotListItem
}

export const TeamsExpertShortInfoCard: React.FC<Props> = ({
                                                              team,
                                                          }) => {
    const header = (
        <div className={style.expertCardHeader}>
            <h3>
                {team.name || `Команда №${team.id}`}
                <span className={style.highlight}>
        ({team.usersCount}/{team.teamCapacity})
        </span>
            </h3>
            <span>{getSlotDateTime(team.slotTime)}</span>
        </div>
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
