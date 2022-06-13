import React, { useState } from 'react'
import { ProfileState } from 'app/reducers'
import { AddBoxRounded } from '@material-ui/icons'
import { AppCard } from 'app/components/AppCard'
import { AppButton } from 'app/components/AppButton'
import { AppInput } from 'app/components/AppInput'
import { ProposalDescriptionEditCard } from 'app/components/ProposalDescriptionCard'
import { ProposalInfoEditCard } from 'app/components/ProposalInfoCard'
import { ProposalTeamsInfoEditCard } from 'app/components/ProposalTeamsInfoCards'
import { ProposalModel, ProposalStatus } from 'app/models/ProjectProposalModel'
import { user } from 'app/provider'

import style from './style.scss'

interface createProposal {
  profile: ProfileState,
  fetch: () => Promise<void>
}

type Props = createProposal

export const CreateProjectProposal: React.FC<Props> = ({
  profile,
  fetch,
}) => {
  const [proposal, setProposal] = useState<ProposalModel>({
    id: 5,
    name: '',
    description: '',
    purpose: '',
    result: '',
    contacts: '',
    curator: '',
    organization: '',
    teamCapacity: 1,
    maxTeamCount: 1,
    lifeScenarioName: '',
    keyTechnologyName: '',
    author: {
      id: profile.profile!.id,
      lastName: profile.profile!.lastName,
      firstName: profile.profile!.firstName,
      middleName: profile.profile!.middleName,
      about: profile.profile!.about,
      login: profile.profile!.login,
      email: profile.profile!.email,
      phoneNumber: profile.profile!.phoneNumber,
      academicGroup: profile.profile!.academicGroup,
      userType: profile.profile!.userType,
    },
    status: ProposalStatus.Pending,
    date: new Date().toString(),
  })
  const [add, setAdd] = useState(false)

  const actionButtons = (
    <div className={style.actionButtons}>
      <AppButton
        disabled={add || !proposal.name ||
        !proposal.purpose ||
        !proposal.result ||
        !proposal.curator ||
        !(proposal.teamCapacity < 6 && proposal.teamCapacity > 1) ||
        !proposal.description ||
        !proposal.keyTechnologyName ||
        !proposal.contacts}
        type='button'
        icon={<AddBoxRounded/>}
        onClick={() => {
          setAdd(true)
          user.createProjectProposal(proposal)
            .then(fetch)
            .then(() => setAdd(false))
        }
        }
      >
        Добавить заявку
      </AppButton>
    </div>
  )

  const headerCard = (
    <AppCard
      className={style.projectNameCard}
      contentClassName={style.headerCard}
    >
      <AppInput
        value={proposal.name}
        className={style.nameInput}
        onChange={(e) => setProposal({
          ...proposal,
          name: e.target.value,
        })}
        placeholder='Название проекта'
      />
      {actionButtons}
    </AppCard>
  )

  const pageContent = (
    <div className={style.pageContent}>
      <div className={style.largeColumn}>
        <div className={style.card}>
          <ProposalDescriptionEditCard
            proposal={proposal}
            onChange={(value) => setProposal({
              ...proposal,
              ...value,
            })}
          />
        </div>
        <div className={style.card}>
          <ProposalTeamsInfoEditCard
            proposal={proposal}
            onChange={(value) => setProposal({
              ...proposal,
              ...value,
              maxTeamCount: 1,
            })}
          />
        </div>
      </div>
      <div className={style.smallColumn}>
        <ProposalInfoEditCard
          proposal={proposal}
          isLifeScenarioNeed={false}
          onChange={(value) => setProposal({
            ...proposal,
            ...value,
            organization: 'ИРИТ-РТФ',
            curator: `${profile.profile?.lastName} ${profile.profile?.firstName} ${profile.profile?.middleName}`,
            lifeScenarioName: 'Предпринимательский',
          })}
        />
      </div>
    </div>
  )

  return (
    <>
      {headerCard}
      {pageContent}
    </>
  )
}
