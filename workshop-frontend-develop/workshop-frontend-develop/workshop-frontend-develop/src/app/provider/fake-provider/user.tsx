import { UserProfileModel, UserRegistrationModel, UserTypes } from 'app/models'
import { RequestProposalStatus } from 'app/models/RequestProposalModel'
import { fakeUserReview } from 'app/provider/fake-data/fakeUserReview'

import { fakeStudentWithoutTeam, fakeStudentWithTeam, fakeExpert, fakeAdmin } from '../fake-data/fakeUsers'
import { fakeCompetitions } from '../fake-data/fakeCompetitions'
import { fakeEvents } from '../fake-data/fakeEvents'
import { fakeProposals } from '../fake-data/fakeProposals'

enum TestRoles {
  StudentWithTeam = 'StudentWithTeam',
  StudentWithoutTeam = 'StudentWithoutTeam',
  Expert = 'Expert',
  Admin = 'Admin'
}

// Роль авторизованного пользователя
const role: TestRoles = TestRoles.StudentWithTeam

export let fakeUser: UserProfileModel

switch (role as TestRoles) {
  case TestRoles.Admin:
    fakeUser = fakeAdmin
    break
  case TestRoles.Expert:
    fakeUser = fakeExpert
    break
  case TestRoles.StudentWithTeam:
    fakeUser = fakeStudentWithTeam
    break
  case TestRoles.StudentWithoutTeam:
    fakeUser = fakeStudentWithoutTeam
    break
  default:
    break
}

export const user = {
  login(login: string, password: string, eventId: number): Promise<any> {
    console.log('[POST] login', login, password, eventId)
    return new Promise((resolve) => {
      resolve({ status: 204 })
    })
  },

  registration(userData: UserRegistrationModel): Promise<any> {
    console.log('[POST] registration', userData)
    return new Promise((resolve) => {
      resolve({ status: 204 })
    })
  },

  registrationWithUrfu(userData: UserRegistrationModel): Promise<any> {
    console.log('[POST] registration With Urfu', userData)
    return new Promise((resolve) => {
      resolve({ status: 204 })
    })
  },
  updatePassword(userData:UserRegistrationModel):Promise<any> {
    console.log('[PUT] Update pass Urfu', userData)
    return new Promise((resolve) => {
      resolve({ status: 204 })
    })
  },

  logout(): Promise<any> {
    console.log('[POST] logout')
    return new Promise((resolve) => {
      resolve({ status: 204 })
    })
  },

  getUserProfile(): Promise<any> {
    console.log('[GET] getCurrentUserProfile')
    return new Promise((resolve) => {
      setTimeout(() => resolve({ data: fakeUser, status: 200 }), 1000)
    })
  },

  editUserProfile(user: UserProfileModel): Promise<any> {
    console.log('[POST] editUserProfile', user)
    return new Promise((resolve) => {
      resolve({ status: 204 })
    })
  },

  getCompetitions(): Promise<any> {
    console.log('[GET] getCompetitions')
    return new Promise((resolve) => {
      resolve({ status: 200, data: fakeCompetitions })
    })
  },

  getEvents(): Promise<any> {
    console.log('[GET] getEvents')
    return new Promise((resolve) => {
      resolve({
        status: 200,
        data: fakeEvents.filter(event => event.isActive),
      })
    })
  },

  completeTeam: (teamId: number): Promise<any> => {
    console.log('[POST] completeTeam', teamId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  openTeam: (teamId: number): Promise<any> => {
    console.log('[POST] openTeam', teamId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  getProposals: (authorID?: number, term?: string): Promise<any> => {
    console.log('[GET] getProposals', authorID, term)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeProposals }), 1000)
    })
  },

  // getProposal: (proposalId: number): Promise<any> => {
  //   console.log('[GET] getProject', proposalId)
  //   return new Promise((resolve) => {
  //     setTimeout(() => resolve({ status: 200, data: fakeProposals[proposalId] }), 1000)
  //   })
  // },

  createProjectProposal(data: any): Promise<any> {
    console.log('[POST] createProjectProposal', data)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200 }), 1000)
    })
  },

  deleteProjectProposal: (proposalId: number): Promise<any> => {
    console.log('[DELETE] deleteProjectPrroposal', proposalId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  updateProjectProposal: (proposalId: number, data: any): Promise<any> => {
    console.log('[PUT] updateProject', proposalId, data)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  enrollToSlot: (slotId: number, teamId: number): Promise<any> => {
    console.log('[POST] enrollToSlot', slotId, teamId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200 }), 1000)
    })
  },
  getRequests: ():Promise<any> => {
    console.log('[GET getRequests]')
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200 }), 1000)
    })
  },
  getRequestsByAuthor: ():Promise<any> => {
    console.log('[GET getRequestsByAuthor]')
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200 }), 1000)
    })
  },
  updateRequestInTeam: (requestId:number, status:RequestProposalStatus):Promise<any> => {
    console.log('[POST createRequestInTeam]', requestId, status)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200 }), 1000)
    })
  },
  updateUserType: (userType:UserTypes):Promise<any> => {
    console.log(`[PUT] update User Type`, userType)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200 }), 1000)
    })
  },

  getUserReview(): Promise<any> {
    console.log('[GET] getUserReview')
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeUserReview }), 1000)
    })
  },
}
