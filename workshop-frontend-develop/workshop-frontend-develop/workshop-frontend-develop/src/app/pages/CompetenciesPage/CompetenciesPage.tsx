import React, {useEffect, useState} from 'react'
import {CompetencyModel, CompetencyType} from 'app/models'
import {admin, user} from 'app/provider'
import {AppCard} from 'app/components/AppCard'

import {CompetenciesCard} from './CompetenciesCard'

export const CompetenciesPage: React.FC = () => {
    const [competenciesList, setCurrentCompetenciesList] = useState<CompetencyModel[]>([])

    const setCompetencies = () => {
        user.getCompetitions()
            .then(res => setCurrentCompetenciesList(res.data))
    }

    useEffect(() => {
        setCompetencies()
    }, [])

    const hardCompetencies = competenciesList.filter(el => el.competencyType == CompetencyType.HardSkill)
    const softCompetencies = competenciesList.filter(el => el.competencyType == CompetencyType.SoftSkill)

    const handleCompetencyDelete = (id: number) => {
        admin.deleteCompetency(id)
            .then(() => setCompetencies())
    }

    const createCompetency = (competencyName?: string, competencyType?: CompetencyType) => {
        if (competencyName && competencyType) {
            admin.createCompetency(competencyName, competencyType)
                .then(() => setCompetencies())
        }
    }

    return (
        <>
            <AppCard>
                <h2>Добавить новые компетенции</h2>
            </AppCard>

            <CompetenciesCard
                header='Hard Skills'
                selectedItems={hardCompetencies || []}
                onAdd={(competencyName?: string) => createCompetency(competencyName, CompetencyType.HardSkill)}
                onDelete={handleCompetencyDelete}
            />

            <CompetenciesCard
                header='Soft Skills'
                selectedItems={softCompetencies || []}
                onAdd={(competencyName?: string) => createCompetency(competencyName, CompetencyType.SoftSkill)}
                onDelete={handleCompetencyDelete}
            />
        </>
    )
}
