import React, {useEffect, useState} from 'react'
import {admin} from 'app/provider'
import {AppCard} from 'app/components/AppCard'
import {EventModel} from 'app/models/EventModel'
import {RootState} from 'app/reducers'
import {connect} from 'react-redux'

import {EventCard} from './EventCard'
import {EventCreationCard} from './EventCreationCard'

const mapStateToProps = (state: RootState) => ({profile: state.profile})

type StateProps = ReturnType<typeof mapStateToProps>

type Props = StateProps

const EventsPageComponent: React.FC<Props> = ({
                                                  profile,
                                              }) => {
    const [eventsList, setEventsList] = useState<EventModel[]>([])

    const fetchEvents = () => {
        admin.getEvents()
            .then((res) => setEventsList(res.data))
    }

    useEffect(() => {
        fetchEvents()
    }, [])

    const handleAddEvent = (eventName: string) => {
        admin.addNewEvent(eventName)
            .then(fetchEvents)
    }

    const handleEventDataChange = (event: EventModel) => {
        admin.updateEvent(event)
            .then(fetchEvents)
    }

    const eventCards = eventsList.map((event: EventModel) => (
        <EventCard
            key={event.id}
            event={event}
            isCurrent={profile.profile?.eventId === event.id}
            onSave={handleEventDataChange}
        />
    ))

    return (
        <>
            <AppCard>
                <h2>События</h2>
            </AppCard>
            {eventCards}
            <EventCreationCard onSave={handleAddEvent}/>
        </>
    )
}

export const EventsPage: React.FC = connect(
    mapStateToProps,
)(EventsPageComponent)
