export const teamlead = {
  addNewTeam: (projectId: number): Promise<any> => {
    console.log('[POST] add new team', projectId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },
  deleteTeam: (): Promise<any> => {
    console.log(`[DELETE] delete team`)
    return new Promise(resolve => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },
  getProposalByAuthor: (): Promise<any> => {
    console.log(`[GET] get proposal`)
    return new Promise(resolve => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },
  getUsers: (pageNumber:number, lastId?:number, filters?:{term:string, competenciesIds:string}): Promise<any> => {
    console.log(`[GET] get users`, filters, pageNumber, lastId)
    return new Promise(resolve => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },
  registerTeam: (teamId:number):Promise<any> => {
    console.log(`[PUT] UPDATE Entry for Project`, teamId)
    return new Promise(resolve => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },
  addUserInTeam: (userId:number, teamId:number, roleName:string):Promise<any> => {
    console.log(`[POST] ADD user in team`, userId, teamId, roleName)
    return new Promise(resolve => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },
}
