import axios from 'axios'
const apiUrl = process.env.APP_API

export const requestProposal = {
  createRequestProposal(userId:number, teamleadId:number, projectId:number, roleName:string): Promise<any> {
    return axios.post(`${apiUrl}/RequestProposal/CreateRequest`, { userId, teamleadId, projectId, roleName })
  },
  deleteRequestProposal: (proposalId:number):Promise<any> =>
    axios.delete(`${apiUrl}/RequestProposal/${proposalId}`),

  getUsersRequests: ():Promise<any> =>
    axios.get(`${apiUrl}/RequestProposal/GetUsersRequests`),

  CreateRequestInTeam: (teamId:number, projectId:number, roleName:string) =>
    axios.post(`${apiUrl}/RequestProposal/CreateRequestInTeam`, null, { params: { teamId, projectId, roleName } }),
}
