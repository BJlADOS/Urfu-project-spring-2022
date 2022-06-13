import axios, { AxiosResponse } from 'axios'

const apiUrl = process.env.APP_API

export const expert = {
  getTeams: (term: string): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/expert/ExpertTeam`, { params: { term } }),

  getTeamsForReview: (term: string): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/expert/ExpertTeam/GetReviewTeams`, { params: { term } }),

  getTeam: (teamId: number): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/expert/ExpertTeam/${teamId}`),

  callTeam: (teamId: number, auditoriumId: number): Promise<AxiosResponse<any>> =>
    axios.post(`${apiUrl}/expert/ExpertTeam/CallTeam`, { auditoriumId, teamId }),

  finishTeam: (teamId: number, comment: string, mark: number): Promise<AxiosResponse<any>> =>
    axios.post(`${apiUrl}/expert/ExpertTeam/FinishTeam`, { teamId, comment, mark }),

  updateExpertAuditorium: (data: any): Promise<AxiosResponse<any>> =>
    axios.post(`${apiUrl}/expert/ExpertUser/UpdateExpert`, data),

  removeExpertAuditorium: (data: any): Promise<AxiosResponse<any>> =>
    axios.post(`${apiUrl}/expert/ExpertAuditorium/RemoveExpert`, data),

  updateAuditorium: (auditorium: any): Promise<AxiosResponse<any>> =>
    axios.put(`${apiUrl}/expert/ExpertAuditorium`, auditorium),

  addAuditorium: (auditorium: any): Promise<AxiosResponse<any>> =>
    axios.post(`${apiUrl}/expert/ExpertAuditorium`, auditorium),

  deleteAuditorium: (id: number): Promise<AxiosResponse<any>> =>
    axios.delete(`${apiUrl}/expert/ExpertAuditorium/${id}`),

  getExperts: (): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/expert/ExpertUser/GetAvailableExperts`),

  getAuditoriums: (): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/Team/GetAuditoriums`),

  updateTeamReview: (teamId: number, review: any): Promise<AxiosResponse<any>> => {
    const dto = {
      teamId,
      marks: {
        ...review,
      },
    }

    return axios.put(`${apiUrl}/expert/ExpertTeam/UpdateTeamReview`, dto)
  },

  updateTeamCompetencyReview: (teamId: number, review: any): Promise<AxiosResponse<any>> => {
    const dto = {
      teamId,
      ...review,
    }

    return axios.put(`${apiUrl}/expert/ExpertTeam/UpdateTeamCompetencyReview`, dto)
  },
}
