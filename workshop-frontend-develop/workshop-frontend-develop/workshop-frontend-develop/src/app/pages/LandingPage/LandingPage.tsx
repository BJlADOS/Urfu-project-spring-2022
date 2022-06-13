import React, { useState } from 'react'

import { ContactsSection } from './ContactsSection'
import { CoursesSection } from './CoursesSection'
import { CustomersSection } from './CustomersSection'
import { IntroductionSection } from './IntroductionSection'
import { KeyInfoSection } from './KeyInfoSection'
import styles from './styles.scss'

const lastEventId = 1

export const LandingPage: React.FC = () => {
  const [selectedEventId, setSelectedEventId] = useState(lastEventId)

  return (
    <div className={styles.landingPage}>
      <IntroductionSection/>
      <KeyInfoSection
        selectedEventId={selectedEventId}
        onSelectedEventIdChange={setSelectedEventId}
      />
      <CoursesSection selctedEventId={selectedEventId}/>
      <CustomersSection selctedEventId={selectedEventId}/>
      <ContactsSection/>
    </div>
  )
}
