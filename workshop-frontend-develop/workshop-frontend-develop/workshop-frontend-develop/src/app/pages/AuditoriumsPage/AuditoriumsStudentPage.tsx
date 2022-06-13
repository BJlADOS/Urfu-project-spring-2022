import React, {useEffect, useState} from 'react'
import {connect} from 'react-redux'
import {expert} from 'app/provider'
import {AppCard} from 'app/components/AppCard'
import {AuditoriumModel, UserModel, UserProfileModel} from 'app/models'
import {ProfileActions} from 'app/actions'
import {RootState} from 'app/reducers'

import {AuditoriumCard} from './AuditoriumCard'

const mapStateToProps = (state: RootState) => ({profile: state.profile.profile})

const mapDispatchToProps = {setUserProfile: (user: UserProfileModel) => ProfileActions.setUserProfile(user as UserModel)}

type StateProps = ReturnType<typeof mapStateToProps>
type DispatchProps = typeof mapDispatchToProps

interface Props extends StateProps, DispatchProps {
}

const AuditoriumsStudentPageComponent: React.FC<Props> = ({
                                                              profile,
                                                              setUserProfile,
                                                          }) => {
    const [auditoriumsList, setAuditoriumsList] = useState<AuditoriumModel[]>([])

    const fetchAuditoriums = () => expert.getAuditoriums()
        .then((res) => setAuditoriumsList(res.data))

    const handleUpdate = () => {
        fetchAuditoriums()
            .then(() => setUserProfile({
                ...profile,
                // TODO: Remove this hack.
                team: {
                    ...profile?.team,
                    teamSlot: {
                        id: 0,
                        time: new Date().toISOString(),
                    },
                },
            } as any))
    }

    useEffect(() => {
        fetchAuditoriums()
    }, [])

    const auditoriumCards = auditoriumsList.map(item => (
        <AuditoriumCard
            key={item.id}
            auditorium={item}
            onUpdate={handleUpdate}
        />
    ))

    return (
        <>
            <AppCard>
                <h2>Аудитории</h2>
            </AppCard>
            {auditoriumCards}
        </>
    )
}

export const AuditoriumsStudentPage: React.FC = connect(
    mapStateToProps,
    mapDispatchToProps,
)(AuditoriumsStudentPageComponent)
