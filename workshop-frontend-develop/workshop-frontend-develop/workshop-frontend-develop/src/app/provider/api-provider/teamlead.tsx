import axios, { AxiosResponse } from 'axios'

const apiUrl = process.env.APP_API

export const teamlead = {
  addNewTeam: (projectId: number): Promise<AxiosResponse<any>> =>
    axios.post(`${apiUrl}/TeamLead/AddNewTeam/`, null, { params: { projectId } }),
  deleteTeam: (): Promise<AxiosResponse<any>> =>
    axios.delete(`${apiUrl}/TeamLead/DeleteTeam/`),
  createProjectProposal(data: any): Promise<any> {
    return axios.post(`${apiUrl}/ProjectProposal`, data)
  },
  getProposalByAuthor(): Promise<any> {
    return axios.get(`${apiUrl}/ProjectProposal/GetByUser`)
  },
  getUsers(pageNumber:number, lastId?:number,
    filters?:{
      term?:string,
      competenciesIds:string
    }): Promise<AxiosResponse<any>> {
    console.log(filters?.competenciesIds)
    return axios.get(`${apiUrl}/TeamLead/GetStudentsFiltered`, { params: { term: filters?.term, competenciesIds: filters?.competenciesIds, pageNumber: pageNumber, lastItemId: lastId } })
  },
  deleteProjectProposal: (proposalId: number): Promise<any> =>
    axios.delete(`${apiUrl}/ProjectProposal/${proposalId}`),

  updateProjectProposal: (proposalId: number, data: any): Promise<any> =>
    axios.put(`${apiUrl}/ProjectProposal/${proposalId}`, data),

  registerTeam: (teamId:number) : Promise<any> =>
    axios.put(`${apiUrl}/Teamlead/UpdateProjectsForEntry`, null, { params: { id: teamId } }),
  addUserInTeam: (userId:number, teamId:number, roleName:string):Promise<any> =>
    axios.post(`${apiUrl}/Teamlead/AddUser`, { userId, teamId, roleName }),
}
