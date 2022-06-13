import {
  UserProfileModel,
  UserRegistrationModel,
  CompetencyType,
  AuditoriumModel,
  AddAuditoriumModel,
  AddExpertAuditoriumModel,
  UserTypes,
  AddProjectModel,
} from 'app/models'
import { CreateProjectFromProposalModel, ProposalModel, ProposalStatus } from 'app/models/ProjectProposalModel'
import { RequestProposalStatus } from 'app/models/RequestProposalModel'

import * as fakeProvider from './fake-provider'
import * as apiProvider from './api-provider'

export interface Provider {
  teamlead:{
    addNewTeam(projectId:number):Promise<any>
    deleteTeam():Promise<any>
    getProposalByAuthor():Promise<any>
    getUsers(pageNumber:number, lastId?:number, filters?:{term?:string, competenciesIds:string}):Promise<any>
    registerTeam(teamId:number):Promise<any>
    addUserInTeam(userId:number, teamId:number, roleName:string):Promise<any>
  }
  user: {
    login(login: string, password: string, eventId: number): Promise<any>
    logout(): Promise<any>
    registrationWithUrfu(userData:UserRegistrationModel):Promise<any>
    getUserProfile(): Promise<any>
    editUserProfile(user: UserProfileModel): Promise<any>
    getCompetitions(): Promise<any>
    registration(userData: UserRegistrationModel): Promise<any>
    // resetPassword(newPassword: string, params: UserResetPasswordModel): Promise<any>
    getEvents(): Promise<any>
    completeTeam(teamId: number): Promise<any>
    openTeam(teamId: number): Promise<any>
    getProposals(authorID?: number, term?: string): Promise<any>
    // getProposal(proposalId: number): Promise<any>
    createProjectProposal(proposal: ProposalModel): Promise<any>
    deleteProjectProposal(proposalId: number): Promise<any>
    updateProjectProposal(proposalId: number, proposal: any): Promise<any>
    enrollToSlot(slotId: number, teamId: number): Promise<any>
    getRequests():Promise<any>
    getRequestsByAuthor():Promise<any>
    updateRequestInTeam(requestId:number, status:RequestProposalStatus):Promise<any>
    updateUserType(userType:UserTypes):Promise<any>
    updatePassword(userData:UserRegistrationModel):Promise<any>
    getUserReview(): Promise<any>
  },
  projects: {
    getLifeScenario(): Promise<any>
    getKeyTechnology(lifeScenario?: number): Promise<any>
    getProjects(filters?: { term?: string, lifeScenarioId?: number, keyTechnologyId?: number, curator?: string, customer?: string }): Promise<any>
    getCurators(): Promise<any>
    getCustomers(): Promise<any>
    getPromotedProjects(): Promise<any>
    getProject(projectId: number): Promise<any>
    joinTeam(teamId: number, roleId: number): Promise<any>
    leaveTeam(): Promise<any>
    renameTeam(teamId: number, name: string): Promise<any>
    endTest(): Promise<any>
  },
  courses: {
    getCourses(filters?: { term?: string, directionId?: number, roleId?: number, organizator?: string }): Promise<any>
    getCourse(courseId: number): Promise<any>
    getCourseTopics(courseId: number): Promise<any>
    getCourseTopic(topicId: number, courseId: number): Promise<any>
    getPromotedCourses(completedCoursesIds: number[]): Promise<any>
    getEnrolledCourses(enrolledCoursesIds: number[]): Promise<any>
    getCourseRoles(): Promise<any>
    joinCourse(courseId: number): Promise<any>
  },
  expert: {
    getTeams(term: string): Promise<any>
    getTeamsForReview(term: string): Promise<any>
    getTeam(teamId: number): Promise<any>
    callTeam(teamId: number, auditoriumId: number): Promise<any>
    finishTeam(teamId: number, comment: string, mark: number): Promise<any>
    updateExpertAuditorium(data: AddExpertAuditoriumModel): Promise<any>
    removeExpertAuditorium(data: AddExpertAuditoriumModel): Promise<any>
    getExperts(): Promise<any>
    getAuditoriums(): Promise<any>
    updateAuditorium(data: AuditoriumModel): Promise<any>
    addAuditorium(data: AddAuditoriumModel): Promise<any>
    deleteAuditorium(id: number): Promise<any>
    updateTeamReview(teamId: number, review: any): Promise<any>
    updateTeamCompetencyReview(teamId: number, review: any): Promise<any>
  }
  admin: {
    addProject(data: AddProjectModel):Promise<any>,
    uploadProjects(data: FormData): Promise<any>
    addNewEvent(newEvent: string): Promise<any>
    deleteProject(projectId: number): Promise<any>
    updateProject(project: any): Promise<any>
    downloadTemplateTable(): Promise<any>
    downloadSchedule(): Promise<any>
    deleteCompetency(id: any): Promise<any>
    createCompetency(name: string, competencyType: CompetencyType): Promise<any>
    updateCompetency(id: number, name: string, competencyType: CompetencyType): Promise<any>
    updateEvent(data: any): Promise<any>
    getEvents(): Promise<any>
    getFreeStudents(term: string): Promise<any>
    getUsers(term: string): Promise<any>
    getUser(id: number): Promise<any>
    changeUserType(id: number, type: string): Promise<any>
    deleteTeam(teamId: number): Promise<any>
    addNewTeam(projectId: any): Promise<any>
    addNewTeamSlot(auditoriumId: number, date: Date): Promise<any>
    getEventData(): Promise<any>
    getStatistics(): Promise<any>
    getEventResult(): Promise<any>
    openAllProjects(): Promise<any>
    closeAllProjects(): Promise<any>
    joinUserToProject(userId: number, teamId: number, roleId: number): Promise<any>
    deleteUserFromTeam(userId: number, teamId: number): Promise<any>
    changeUserRole(userId: number, roleId: number): Promise<any>
    deleteLifeScenario(scenarioId: number): Promise<any>
    deleteKeyTechnology(technologyId: number): Promise<any>
    addProjectRole(roleName: string, projectId: number): Promise<any>
    updateProjectRole(id: number, name: string): Promise<any>
    deleteProjectRole(roleId: number): Promise<any>
    updateProjectProposalStatus(proposalId: number, status: ProposalStatus): Promise<any>
    getApiKeys(): Promise<any>
    createApiKey(name: string, userType: UserTypes): Promise<any>
    deleteApiKey(id: number): Promise<any>
    deleteTeamSlot(id: number): Promise<any>
    getPendingProposals(term:string):Promise<any>
    getProposal(id:number):Promise<any>
    createProjectFromProposal(proposalId:number, createProject:CreateProjectFromProposalModel):Promise<any>
    rejectProjectProposal(proposalId:number):Promise<any>
    updateTeamEntried(teamId:number):Promise<any>
    openProjectForEntry():Promise<any>
    closeProjectForEntry():Promise<any>
    updateTeamProject(projectId:number, teamId:number):Promise<any>
    getStudentsProjects():Promise<any>
  },
  requestProposal:{
    deleteRequestProposal(proposalId:number):Promise<any>
    createRequestProposal(userId:number, teamleadId:number, projectId:number, roleName:string):Promise<any>
    getUsersRequests():Promise<any>
    CreateRequestInTeam(teamId:number, projectId:number, roleName:string):Promise<any>
  }
}

let provider: Provider

if (process.env.ENABLE_FAKES == 'true') { provider = fakeProvider } else { provider = apiProvider }

export const { user, projects, courses, expert, admin, teamlead, requestProposal } = provider
