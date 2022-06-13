
export const requestProposal = {
  createRequestProposal: (userId:number, teamleadId:number, projectId:number, role:string):Promise<any> => {
    console.log('[POST] createRequestProposal', userId, teamleadId, projectId, role)
    return new Promise((resolve) => {
      setTimeout(() => resolve(), 1000)
    })
  },
  deleteRequestProposal: (proposalId:number):Promise<any> => {
    console.log('[DELETE] DeleteRequestProposal', proposalId)
    return new Promise((resolve) => {
      setTimeout(() => resolve(), 1000)
    })
  },
  getUsersRequests: ():Promise<any> => {
    console.log('[GET] GetUsersRequests')
    return new Promise((resolve) => {
      setTimeout(() => resolve(), 1000)
    })
  },
  CreateRequestInTeam: (teamId:number, projectId:number, roleName:string):Promise<any> => {
    console.log('[POST] POSTUsersRequests', teamId, projectId, roleName)
    return new Promise((resolve) => {
      setTimeout(() => resolve(), 1000)
    })
  },
}
