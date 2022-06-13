import axios, { AxiosResponse } from 'axios'
import { AddProjectModel, CompetencyType, UserTypes } from 'app/models'
import { CreateProjectFromProposalModel, ProposalStatus } from 'app/models/ProjectProposalModel'

const apiUrl = process.env.APP_API

export const admin = {
  uploadProjects(data: FormData): Promise<any> {
    return axios.post(`${apiUrl}/admin/AdminProject/UploadFile`, data)
  },

  addProject(data: AddProjectModel): Promise<any> {
    return axios.post(`${apiUrl}/admin/AdminProject/CreateProject`, data)
  },

  addNewEvent(newEvent: string): Promise<any> {
    const data = {
      name: newEvent,
    }

    return axios.post(`${apiUrl}/admin/AddEvent`, data)
  },

  deleteProject: (projectId: number): Promise<any> =>
    axios.delete(`${apiUrl}/admin/AdminProject/${projectId}`),

  updateProject: (data: any): Promise<any> =>
    axios.put(`${apiUrl}/admin/AdminProject`, data),

  downloadTemplateTable: (): Promise<any> =>
    axios.get(`${apiUrl}/admin/AdminProject/GetProjectsTableTemplate`, { responseType: 'blob' }),

  downloadSchedule: (): Promise<any> =>
    axios.get(`${apiUrl}/admin/AdminProject/GetSchedule`, { responseType: 'blob' }),

  getCompetencies: (term?: string): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/Competency`, { params: { term } }),

  deleteCompetency: (id: number): Promise<any> =>
    axios.delete(`${apiUrl}/admin/AdminCompetencies/${id}`),

  createCompetency: (name: string, competencyType: CompetencyType): Promise<any> => {
    const data = {
      name,
      competencyType,
    }

    return axios.post(`${apiUrl}/admin/AdminCompetencies`, data)
  },

  updateCompetency: (id: number, name: string, competencyType: CompetencyType): Promise<any> => {
    const data = {
      id,
      name,
      competencyType,
    }

    return axios.put(`${apiUrl}/admin/AdminCompetencies`, data)
  },

  getEvents: (): Promise<any> => axios.get(`${apiUrl}/admin/Events`),

  updateEvent: (data: any): Promise<any> =>
    axios.put(`${apiUrl}/admin/UpdateEvent`, data),

  getFreeStudents: (term: string): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/admin/AdminTeam/GetFreeUsers`, { params: { term } }),

  getUsers: (term: string): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/admin/GetUsers`, { params: { term } }),

  getUser: (id: number): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/admin/GetUser/${id}`),

  changeUserType: (id: number, type: string): Promise<AxiosResponse<any>> =>
    axios.put(`${apiUrl}/admin/ChangeUserType`, {
      id,
      type,
    }),

  deleteTeam: (teamId: number): Promise<AxiosResponse<any>> =>
    axios.delete(`${apiUrl}/admin/AdminTeam/DeleteTeam`, { params: { teamId } }),

  addNewTeam: (projectId: number): Promise<AxiosResponse<any>> =>
    axios.post(`${apiUrl}/admin/AdminTeam/AddNewTeam`, null, { params: { projectId } }),

  addNewTeamSlot: (auditoriumId: number, time: Date): Promise<AxiosResponse<any>> =>
    axios.post(`${apiUrl}/admin/AdminTeamSlot`, {
      auditoriumId,
      time,
    }),

  getEventData: (): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/admin/AdminStatistic/GetEventData`, { responseType: 'blob' }),

  getStatistics: (): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/admin/AdminStatistic/GetStatistic`),

  getEventResult: (): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/admin/AdminProject/GetResults`, { responseType: 'blob' }),

  joinUserToProject: (userId: number, teamId: number, roleId: number): Promise<AxiosResponse<any>> =>
    axios.post(`${apiUrl}/admin/AdminTeam/AddUser`, {
      userId,
      teamId,
      roleId,
    }),

  deleteUserFromTeam: (userId: number, teamId: number): Promise<AxiosResponse<any>> =>
    axios.delete(`${apiUrl}/admin/AdminTeam/RemoveUser`, {
      params: {
        userId,
        teamId,
      },
    }),

  changeUserRole: (userId: number, roleId: number): Promise<AxiosResponse<any>> =>
    axios.put(`${apiUrl}/admin/AdminTeam/ChangeUserRole`, {
      userId,
      roleId,
    }),

  openAllProjects: (): Promise<any> => axios.post(`${apiUrl}/admin/AdminProject/OpenForEntry`),

  closeAllProjects: (): Promise<any> => axios.post(`${apiUrl}/admin/AdminProject/CloseForEntry`),

  deleteLifeScenario: (scenarioId: number): Promise<any> =>
    axios.delete(`${apiUrl}/admin/AdminProject/DeleteLifeScenario`, { params: { id: scenarioId } }),

  deleteKeyTechnology: (technologyId: number): Promise<any> =>
    axios.delete(`${apiUrl}/admin/AdminProject/DeleteKeyTechnology`, { params: { id: technologyId } }),

  addProjectRole: (roleName: string, projectId: number): Promise<any> =>
    axios.post(`${apiUrl}/admin/AdminProject/AddProjectRole`, {
      roleName,
      projectId,
    }),

  updateProjectRole: (id: number, name: string): Promise<AxiosResponse<any>> =>
    axios.put(`${apiUrl}/admin/AdminProject/UpdateProjectRole`, {
      id,
      name,
    }),

  deleteProjectRole: (roleId: number): Promise<any> =>
    axios.delete(`${apiUrl}/admin/AdminProject/DeleteProjectRole/${roleId}`),

  updateProjectProposalStatus: (proposalId: number, status: ProposalStatus): Promise<any> =>
    axios.put(`${apiUrl}/ProjectProposal`, {
      proposalId,
      status,
    }),

  getApiKeys: (): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/admin/ApiKey`),

  createApiKey: (name: string, userType: UserTypes): Promise<AxiosResponse<any>> =>
    axios.post(`${apiUrl}/admin/ApiKey`, {
      name,
      userType,
    }),

  deleteApiKey: (id: number): Promise<AxiosResponse<any>> =>
    axios.delete(`${apiUrl}/admin/ApiKey/${id}`),

  deleteTeamSlot: (id: number): Promise<AxiosResponse<any>> =>
    axios.delete(`${apiUrl}/admin/AdminTeamSlot/${id}`),

  getPendingProposals: (term: string): Promise<any> =>
    axios.get(`${apiUrl}/admin/AdminProjectProposal/GetPendingProposals`, { params: { term } }),

  getProposal: (proposalId: number): Promise<AxiosResponse<any>> =>
    axios.get(`${apiUrl}/ProjectProposal/${proposalId}`),

  createProjectFromProposal: (proposalId: number, createProject: CreateProjectFromProposalModel): Promise<any> =>
    axios.post(`${apiUrl}/admin/AdminProjectProposal`, createProject, { params: { id: proposalId } }),

  rejectProjectProposal: (proposalId: number): Promise<any> =>
    axios.put(`${apiUrl}/admin/AdminProjectProposal`, null, { params: { id: proposalId } }),

  updateTeamEntried: (teamId: number): Promise<any> =>
    axios.put(`${apiUrl}/admin/AdminTeam/UpdateTeamEntried`, null, { params: { id: teamId } }),

  openProjectForEntry: (): Promise<any> => axios.put(`${apiUrl}/admin/AdminProject/OpenForEntry`),

  closeProjectForEntry: (): Promise<any> => axios.put(`${apiUrl}/admin/AdminProject/CloseForEntry`),

  updateTeamProject: (projectId: number, teamId: number): Promise<any> =>
    axios.put(`${apiUrl}/admin/AdminTeam/UpdateTeamProject`, null, {
      params: {
        projectId,
        teamId,
      },
    }),
  getStudentsProjects: () :Promise<any> =>
    axios.get(`${apiUrl}/admin/AdminStatistic/GetStudentsStatistic`, { responseType: 'blob' }),

}
