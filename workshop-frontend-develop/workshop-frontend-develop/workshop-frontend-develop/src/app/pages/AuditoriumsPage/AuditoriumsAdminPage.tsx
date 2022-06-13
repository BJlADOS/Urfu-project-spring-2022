import React, { useEffect, useState } from 'react'
import { GetAppRounded } from '@material-ui/icons'
import { admin, expert } from 'app/provider'
import { AppCard } from 'app/components/AppCard'
import { AuditoriumModel, ExpertUserModel } from 'app/models'
import { AppButton } from 'app/components/AppButton'
import { handleDownloadResponse } from 'app/utils/handleDownloadResponse'

import { AuditoriumCard } from './AuditoriumCard'
import { AuditoriumCreationCard } from './AuditoriumCreationCard'
import { AddTeamToSlotDialog } from './AddTeamToSlotDialog'
import style from './style.scss'

export const AuditoriumsAdminPage: React.FC = () => {
  const [auditoriumsList, setAuditoriumsList] = useState<AuditoriumModel[]>([])
  const [experts, setExperts] = useState<ExpertUserModel[]>([])
  const [selectedSlotId, setSelectedSlotId] = useState<number | null>(null)

  const fetchAuditoriums = () => {
    expert.getAuditoriums()
      .then((res) => setAuditoriumsList(res.data))
  }

  const fetchExperts = () => {
    expert.getExperts().then(res => setExperts(res.data))
  }

  useEffect(() => {
    fetchAuditoriums()
    fetchExperts()
  }, [])

  const handleAuditoriumAdd = (name: string) => {
    expert.addAuditorium({
      name,
      isDefault: false,
      capacity: 100,
    }).then(fetchAuditoriums)
  }

  const handleAddTeamToSlot = () => {
    setSelectedSlotId(null)
    fetchAuditoriums()
  }

  const downloadSchedule = () => {
    admin.downloadSchedule()
      .then(handleDownloadResponse)
  }

  const auditoriumCards = auditoriumsList.map(item => (
    <AuditoriumCard
      key={item.id}
      auditorium={item}
      experts={experts}
      onUpdate={fetchAuditoriums}
      onAddTeamToSlot={(slotId) => setSelectedSlotId(slotId)}
      isAdmin
    />
  ))

  return (
    <>
      <AppCard>
        <h2>Аудитории</h2>
      </AppCard>
      <AuditoriumCreationCard onSave={handleAuditoriumAdd}/>
      {auditoriumCards}
      <AppButton
        className={style.downloadSchedule}
        onClick={() => downloadSchedule()}
        icon={<GetAppRounded/>}
      >
        Выгрузить расписание
      </AppButton>
      <AddTeamToSlotDialog
        open={selectedSlotId !== null}
        slotId={selectedSlotId}
        onSave={handleAddTeamToSlot}
        onClose={() => setSelectedSlotId(null)}
      />
    </>
  )
}
