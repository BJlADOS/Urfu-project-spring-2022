import axios, { AxiosResponse } from 'axios'

const apiUrl = process.env.APP_API

export const projects = {
  getLifeScenario: (): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/Project/GetLifeScenarios`),

  getKeyTechnology: (scenarioId?: number): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/Project/GetKeyTechnologies`, { params: { id: scenarioId } }),

  getCurators: (): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/Project/GetCurators`),

  getCustomers: (): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/Project/GetCustomers`),

  getProjects: (
    filters?: {
      term?: string,
      lifeScenarioId?: number,
      keyTechnologyId?: number,
      curator?: string,
      customer?: string
    }): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/Project`, { params: filters }),

  getPromotedProjects: (): Promise<any> =>
    axios.get(`${apiUrl}/Project/GetPromoted`),

  getProject: (projectId: number): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/Project/${projectId}`),

  joinTeam: (teamId: number, roleId: number): Promise<AxiosResponse<any>> =>
    axios.post(`${apiUrl}/Team/Join`, { teamId, roleId }),

  leaveTeam: (): Promise<AxiosResponse<any>> =>
    axios.delete(`${apiUrl}/Team/LeaveTeam`),

  renameTeam: (teamId: number, name: string): Promise<AxiosResponse<any>> =>
    axios.post(`${apiUrl}/Team/Rename`, { teamId, name }),

  endTest: (): Promise<any> =>
    axios.post(`${apiUrl}/Team/FinishTesting`),
}
