import { fakeLifeScenario } from '../fake-data/fakeLifeScenario'
import { fakeKeyTechnology } from '../fake-data/fakeKeyTechnology'
import { fakeProjects } from '../fake-data/fakeProjects'
import { fakeCurators } from '../fake-data/fakeCurators'
import { fakeCustomers } from '../fake-data/fakeCustomers'

export const projects = {
  getLifeScenario: (): Promise<any> => {
    console.log('[GET] getLifeScenario')
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeLifeScenario }), 1000)
    })
  },

  getKeyTechnology: (lifeScenario?: number): Promise<any> => {
    console.log('[GET] getKeyTechnology', lifeScenario)
    const data = lifeScenario ? fakeKeyTechnology[lifeScenario] : fakeKeyTechnology.reduce((a, v) => [...a, ...v], [])

    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data }), 1000)
    })
  },

  getCurators: (): Promise<any> => {
    console.log('[GET] getCurators')
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeCurators }), 1000)
    })
  },

  getCustomers: (): Promise<any> => {
    console.log('[GET] getCustomers')
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeCustomers }), 1000)
    })
  },

  getProjects: (filters?: { term?: string, lifeScenarioId?: number, keyTechnologyId?: number, curator?: string, customer?: string }): Promise<any> => {
    console.log('[GET] getProjects', filters)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeProjects }), 1000)
    })
  },

  getProject: (projectId: number): Promise<any> => {
    console.log('[GET] getProject', projectId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeProjects[projectId] }), 1000)
    })
  },

  getPromotedProjects: (): Promise<any> => {
    console.log('[GET] getPromotedProjects')
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeProjects.filter(p => p.isPromoted) }), 1000)
    })
  },

  joinTeam: (teamId: number, roleId: number): Promise<any> => {
    console.log('[POST] joinTeam', teamId, roleId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  leaveTeam: (): Promise<any> => {
    console.log('[DELETE] leaveTeam')
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  renameTeam: (teamId: number, name: string): Promise<any> => {
    console.log('[POST] renameTeam', teamId, name)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },

  endTest: (): Promise<any> => {
    console.log('[POST] endTest')
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },
}
