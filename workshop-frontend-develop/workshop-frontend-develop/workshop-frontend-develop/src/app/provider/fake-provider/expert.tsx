
import { fakeReviewTeamsList, fakeTeams, fakeTeamsList } from '../fake-data/fakeTeams'
import { fakeAuditoriums } from '../fake-data/fakeAuditoriums'
import { fakeExperts } from '../fake-data/fakeAnyUsers'

export const expert = {
  getTeams: (term: string): Promise<any> => {
    console.log('[GET] getTeams', term)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeTeamsList }), 1000)
    })
  },

  getTeamsForReview: (term: string): Promise<any> => {
    console.log('[GET] getTeamsForReview', term)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeReviewTeamsList }), 1000)
    })
  },

  getTeam: (teamId: number): Promise<any> => {
    console.log('[GET] getTeam', teamId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeTeams[0] }), 1000)
    })
  },

  callTeam: (teamId: number, auditoriumId: number): Promise<any> => {
    console.log('[POST] callTeam', teamId, auditoriumId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  finishTeam: (teamId: number, comment: string, mark: number): Promise<any> => {
    console.log('[POST] finishTeam', teamId, comment, mark)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  updateExpertAuditorium: (data: any): Promise<any> => {
    console.log('[POST] updateExpertAuditorium', data)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  removeExpertAuditorium: (data: any): Promise<any> => {
    console.log('[POST] removeExpertAuditorium', data)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  updateAuditorium: (auditorium: any): Promise<any> => {
    console.log('[POST] updateAuditorium', auditorium)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  getExperts: (): Promise<any> => {
    console.log('[GET] getExperts')
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeExperts }), 1000)
    })
  },

  getAuditoriums: (): Promise<any> => {
    console.log('[GET] getAuditoriums')
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeAuditoriums }), 1000)
    })
  },

  addAuditorium: (data: any): Promise<any> => {
    console.log('[POST] addAuditorium', data)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  deleteAuditorium: (id: number): Promise<any> => {
    console.log('[DELETE] deleteAuditorium', id)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  updateTeamReview: (teamId: number, review: any): Promise<any> => {
    console.log('[PUT] updateTeamReview', teamId, review)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  updateTeamCompetencyReview: (teamId: number, review: any): Promise<any> => {
    console.log('[PUT] updateTeamCompetencyReview', teamId, review)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },
}
