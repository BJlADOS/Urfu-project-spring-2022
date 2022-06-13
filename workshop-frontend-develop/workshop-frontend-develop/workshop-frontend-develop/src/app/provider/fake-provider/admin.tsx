import { AddProjectModel, CompetencyType, UserTypes } from 'app/models'
import { CreateProjectFromProposalModel, ProposalStatus } from 'app/models/ProjectProposalModel'
import { fakeFreeStudents } from 'app/provider/fake-data/fakeFreeStudents'
import { fakeMonitoring } from 'app/provider/fake-data/fakeMonitorind'
import { fakeProposals } from 'app/provider/fake-data/fakeProposals'

import { fakeAnyUsers, fakeDetailedUser } from '../fake-data/fakeAnyUsers'
import { fakeApiKeys } from '../fake-data/fakeApiKeys'
import { fakeEvents } from '../fake-data/fakeEvents'

export const admin = {
  uploadProjects(data: FormData): Promise<any> {
    console.log('[POST] uploadProjects', data)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200 }), 1000)
    })
  },
  addProject(data: AddProjectModel): Promise<any> {
    console.log('[POST] addProject', data)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },
  addNewEvent(data: string): Promise<any> {
    console.log('[POST] addNewEvent', data)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  deleteProject: (projectId: number): Promise<any> => {
    console.log('[DELETE] deleteProject', projectId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  updateProject: (data: any): Promise<any> => {
    console.log('[PUT] updateProject', data)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  downloadTemplateTable: (): Promise<any> => {
    console.log('[GET] downloadTemplateTable')
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200 }), 1000)
    })
  },

  downloadSchedule: (): Promise<any> => {
    console.log('[GET] downloadSchedule')
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200 }), 1000)
    })
  },

  deleteCompetency: (id: number): Promise<any> => {
    console.log('[DELETE] deleteCompetency', id)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  createCompetency: (name: string, competencyType: CompetencyType): Promise<any> => {
    console.log('[POST] createCompetency', name, competencyType)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  updateCompetency: (id: number, name: string, competencyType: CompetencyType): Promise<any> => {
    console.log('[PUT] updateCompetency', id, name, competencyType)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  updateEvent: (data: any): Promise<any> => {
    console.log('[PUT] updateEvent', data)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  getEvents(): Promise<any> {
    console.log('[GET] getAllEvents')
    return new Promise((resolve) => {
      resolve({
        status: 200,
        data: fakeEvents,
      })
    })
  },

  getFreeStudents: (term: string): Promise<any> => {
    console.log('[GET] getFreeStudents', term)
    return new Promise((resolve) => {
      setTimeout(() => resolve({
        status: 200,
        data: fakeFreeStudents,
      }), 1000)
    })
  },

  getUsers: (term: string): Promise<any> => {
    console.log('[GET] getUsers', term)
    return new Promise((resolve) => {
      setTimeout(() => resolve({
        status: 200,
        data: fakeAnyUsers,
      }), 1000)
    })
  },

  getUser: (id: number): Promise<any> => {
    console.log('[GET] getUser', id)
    return new Promise((resolve) => {
      setTimeout(() => resolve({
        status: 200,
        data: fakeDetailedUser,
      }), 1000)
    })
  },

  changeUserType: (id: number, type: string): Promise<any> => {
    console.log('[PUT] changeUserType', id, type)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  deleteTeam: (teamId: number): Promise<any> => {
    console.log('[DELETE] deleteTeam', teamId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  addNewTeam: (projectId: number): Promise<any> => {
    console.log('[POST] addNewTeam', projectId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  addNewTeamSlot: (auditoriumId: number, date: Date): Promise<any> => {
    console.log('[POST] addNewTeamSlot', auditoriumId, date)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  getEventData: (): Promise<any> => {
    console.log('[GET] getEventData')
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200 }), 1000)
    })
  },

  getStatistics: (): Promise<any> => {
    console.log('[GET] getStatistics')
    return new Promise((resolve) => {
      setTimeout(() => resolve({
        status: 200,
        data: fakeMonitoring,
      }), 1000)
    })
  },

  getEventResult: (): Promise<any> => {
    console.log('[GET] getEventResult')
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200 }), 1000)
    })
  },

  joinUserToProject: (userId: number, teamId: number, roleId: number): Promise<any> => {
    console.log('[POST] joinUserToProject', userId, teamId, roleId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  deleteUserFromTeam: (userId: number, teamId: number): Promise<any> => {
    console.log('[DELETE] deleteUserFromTeam', userId, teamId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  changeUserRole: (userId: number, roleId: number): Promise<any> => {
    console.log('[PUT] changeUserRole', userId, roleId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  openAllProjects: (): Promise<any> => {
    console.log('[POST] openAllProjects')
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  closeAllProjects: (): Promise<any> => {
    console.log('[POST] closeAllProjects')
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  deleteLifeScenario: (scenarioId: number): Promise<any> => {
    console.log('[DELETE] deleteLifeScenario', scenarioId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  deleteKeyTechnology: (technologyId: number): Promise<any> => {
    console.log('[DELETE] deleteKeyTechnology', technologyId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  updateProjectRole: (id: number, name: string): Promise<any> => {
    console.log('[PUT] updateProjectRole', id, name)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  addProjectRole: (roleName: string, projectId: number): Promise<any> => {
    console.log('[POST] addProjectRole', roleName, projectId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  deleteProjectRole: (roleId: number): Promise<any> => {
    console.log('[DELETE] deleteProjectRole', roleId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  updateProjectProposalStatus: (proposalId: number, status: ProposalStatus): Promise<any> => {
    console.log('[PUT] updateProjectProposalStatus', proposalId, status)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  getApiKeys: (): Promise<any> => {
    console.log('[GET] getApiKeys')
    return new Promise((resolve) => {
      setTimeout(() => resolve({
        status: 200,
        data: fakeApiKeys,
      }), 1000)
    })
  },

  createApiKey: (name: string, userType: UserTypes): Promise<any> => {
    console.log('[POST] getApiKeys', name, userType)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  deleteApiKey: (id: number): Promise<any> => {
    console.log('[DELETE] getApiKeys', id)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  deleteTeamSlot: (id: number): Promise<any> => {
    console.log('[DELETE] deleteTeamSlot', id)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  getPendingProposals: (term: string): Promise<any> => {
    console.log('[GET] getProposalsPending', term)
    return new Promise((resolve) => {
      setTimeout(() => resolve({
        status: 200,
        data: fakeProposals,
      }), 1000)
    })
  },

  getProposal: (id: number): Promise<any> => {
    console.log('[GET] getProposal', id)
    return new Promise((resolve) => {
      setTimeout(() => resolve({
        status: 200,
        data: fakeProposals,
      }), 1000)
    })
  },

  createProjectFromProposal: (proposalId: number, createProject: CreateProjectFromProposalModel): Promise<any> => {
    console.log('[POST createProjectFromProposal', proposalId, createProject)
    return new Promise((resolve) => {
      setTimeout(() => resolve({
        status: 200,
        data: fakeProposals,
      }), 1000)
    })
  },

  rejectProjectProposal: (proposalId: number): Promise<any> => {
    console.log('[PUT] Change Project Proposal', proposalId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({
        status: 200,
        data: fakeProposals,
      }), 1000)
    })
  },

  updateTeamEntried: (teamId: number): Promise<any> => {
    console.log('[PUT] Change Team Entried', teamId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({
        status: 200,
        data: fakeProposals,
      }), 1000)
    })
  },

  openProjectForEntry: (): Promise<any> => {
    console.log('[PUT] Open Entry Project')
    return new Promise((resolve) => {
      setTimeout(() => resolve({
        status: 200,
        data: fakeProposals,
      }), 1000)
    })
  },

  closeProjectForEntry: (): Promise<any> => {
    console.log('[PUT] Open Entry Project')
    return new Promise((resolve) => {
      setTimeout(() => resolve({
        status: 200,
        data: fakeProposals,
      }), 1000)
    })
  },
  updateTeamProject: (projectId: number, teamId: number): Promise<any> => {
    console.log(`[PUT Change Project For Team`, projectId, teamId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({
        status: 200,
        data: fakeProposals,
      }), 1000)
    })
  },
  getStudentsProjects: (): Promise<any> => {
    console.log(`[GET Get Students Projects]`)
    return new Promise((resolve) => {
      setTimeout(() => resolve({
        status: 200,
        data: fakeProposals,
      }), 1000)
    })
  },
}
