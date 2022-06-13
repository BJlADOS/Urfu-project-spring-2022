import React, {useEffect, useState} from 'react'
import {AppCard, AppLinkCard} from 'app/components/AppCard'
import {PendingProposalModel} from 'app/models/ProjectProposalModel'
import {AppSearch} from 'app/components/AppInput'
import {admin} from 'app/provider'
import {projectProposal, userInfo} from 'app/nav'
import {AppLoadingSpinner} from 'app/components/AppLoadingSpinner'

import style from './style.scss'

export const AllPendingProposals = () => {
    const [proposalsList, setProposalsList] = useState<PendingProposalModel[]>([])
    const [searchField, setSearchField] = useState('')

    const fetchProposals = (term?: string) => {
        admin.getPendingProposals(term || '')
            .then((res) => setProposalsList(res.data))
    }

    useEffect(() => {
        fetchProposals()
    }, [])

    if (!proposalsList) {
        return <AppLoadingSpinner/>
    }
    const handleSearch = (value: string) => {
        setSearchField(value)
        fetchProposals(value)
    }

    const proposals = proposalsList.map(proposal =>
        <div
            key={proposal.id}
            className={style.proposalContainer}
        >
            <div className={style.largeColumn}>
                <AppLinkCard
                    key={proposal.id}
                    className={style.card}
                    header={proposal.name}
                    to={projectProposal(proposal.id)}
                >
                    <p className={style.purpose}>{proposal.purpose}</p>
                    <div className={style.additional}>
                        <div>
                            Желаемое кол-во участников:
                            <b> {proposal.teamCapacity}</b>
                        </div>
                        <div>
                            Сценарий:
                            <span className={style.scenario}> {proposal.lifeScenarioName}</span>
                        </div>

                        <div>
                            Технология: <span className={style.keyTechnology}>{proposal.keyTechnologyName}</span>
                        </div>
                    </div>
                </AppLinkCard>
            </div>
            <div className={style.smallColumn}>
                <AppLinkCard
                    key={proposal.author.id}
                    className={style.authorCard}
                    to={userInfo(proposal.author.id)}
                    header={`${proposal.author.firstName} ${proposal.author.lastName} ${proposal.author.middleName}`}
                >

                    <span>Академическая группа: {proposal.author.academicGroup}</span>

                </AppLinkCard>
            </div>
        </div>,
    )

    return (
        <>
            <AppCard className={style.headerCard}>
                <h2>Проектные заявки <span className={style.proposalsCount}>
          {proposalsList.length > 0 && proposalsList.length}
        </span></h2>

            </AppCard>
            <AppCard
                className={style.searchSection}
            >
                <AppSearch
                    value={searchField}
                    onChange={(e) => setSearchField(e.target.value)}
                    onChangeSearch={handleSearch}
                    placeholder={'Поиск заявки'}
                />
            </AppCard>
            {proposals}
        </>
    )
}
