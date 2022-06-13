import axios, { AxiosResponse } from 'axios'
import { UserProfileModel, UserRegistrationModel, UserTypes } from 'app/models'
import { RequestProposalStatus } from 'app/models/RequestProposalModel'

export const apiUrl = process.env.APP_API

export const user = {
  login: (login: string, password: string, eventId: number): Promise<AxiosResponse<any>> => axios.post(`${apiUrl}/Security/Login`, { login, password, eventId }),

  registration(userData: UserRegistrationModel): Promise<any> {
    return axios.post(`${apiUrl}/Security/Registration`, userData)
  },

  registrationWithUrfu(userData:UserRegistrationModel): Promise<any> {
    return axios.post(`${apiUrl}/Security/RegistrationWithUrfu`, userData)
  },
  updatePassword(userData: UserRegistrationModel): Promise<any> {
    return axios.put(`${apiUrl}/Security/RefreshPassword`, userData)
  },

  logout: (): Promise<AxiosResponse<any>> => axios.post(`${apiUrl}/Security/Logout`),

  getUserProfile: (): Promise<AxiosResponse<any>> => axios.get(`${apiUrl}/User`),

  editUserProfile(user: UserProfileModel): Promise<AxiosResponse<any>> {
    return axios.post(`${apiUrl}/User`, user)
  },

  getCompetitions(): Promise<AxiosResponse<any>> {
    return axios.get(`${apiUrl}/Competency`)
  },

  getEvents(): Promise<any> {
    return axios.get(`${apiUrl}/Event`)
  },

  completeTeam: (teamId: number): Promise<AxiosResponse<any>> =>
    axios.post(`${apiUrl}/Team/StartTesting`, { teamId }),

  openTeam: (teamId: number): Promise<AxiosResponse<any>> =>
    axios.post(`${apiUrl}/Team/OpenTeam`, { teamId }),

  enrollToSlot: (slotId: number, teamId: number): Promise<AxiosResponse<any>> =>
    axios.post(`${apiUrl}/TeamSlot`, { slotId, teamId }),

  getProposals: (authorID?: number, term?: string): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/ProjectProposal`, { params: { authorID, term } }),

  createProjectProposal(data: any): Promise<any> {
    return axios.post(`${apiUrl}/ProjectProposal`, data)
  },

  deleteProjectProposal: (proposalId: number): Promise<any> =>
    axios.delete(`${apiUrl}/ProjectProposal/${proposalId}`),

  updateProjectProposal: (proposalId: number, data: any): Promise<any> =>
    axios.put(`${apiUrl}/ProjectProposal/${proposalId}`, data),

  getRequests: ():Promise<any> =>
    axios.get(`${apiUrl}/RequestProposal/GetRequestsByUser`),

  getRequestsByAuthor: ():Promise<any> =>
    axios.get(`${apiUrl}/RequestProposal/GetUsersByAuthor`),

  updateRequestInTeam: (requestId:number, status:RequestProposalStatus):Promise<any> =>
    axios.put(`${apiUrl}/RequestProposal/${requestId}`, null, { params: { id: requestId, status: status } }),

  updateUserType: (userType: UserTypes) :Promise<any> =>
    axios.put(`${apiUrl}/User/UpdateUserType`, null, { params: { userType: userType } }),
  getUserReview(): Promise<AxiosResponse<any>> {
    return axios.get(`${apiUrl}/User/GetUserReview`)
  },
}
