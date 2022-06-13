import { fakeCourseRoles, fakeCourses, fakeCoursesData } from '../fake-data/fakeCourses'

export const courses = {
  getCourses: (filters?: { term?: string, directionId?: number, roleId?: number, organizator?: string }): Promise<any> => {
    console.log('[GET] getCources', filters)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeCourses }), 1000)
    })
  },
  getCourse: (courseId: number): Promise<any> => {
    console.log('[GET] getCourse', courseId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeCourses.find(course => course.id === courseId) }), 1000)
    })
  },
  getCourseTopics: (courseId: number): Promise<any> => {
    console.log('[GET] getCourseTopics', courseId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeCoursesData.find(course => course.id === courseId) }), 1000)
    })
  },
  getCourseTopic: (topicId: number, courseId: number): Promise<any> => {
    console.log('[GET] getCourseTopic', topicId, courseId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeCoursesData[courseId].content[topicId] }), 1000)
    })
  },
  getPromotedCourses: (completedCoursesIds: number[]): Promise<any> => {
    console.log('[GET] getPromotedCourses', completedCoursesIds)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeCourses.filter(course => completedCoursesIds.includes(course.id)) }), 1000)
    })
  },
  getEnrolledCourses: (enrolledCoursesIds: number[]): Promise<any> => {
    console.log('[GET] getPromotedCourses', enrolledCoursesIds)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeCourses.filter(course => enrolledCoursesIds.includes(course.id)) }), 1000)
    })
  },
  getCourseRoles: (): Promise<any> => {
    console.log('[GET] getCourseRoles')
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 200, data: fakeCourseRoles }), 1000)
    })
  },
  joinCourse: (courseId: number): Promise<any> => {
    console.log('[POST] joinCourse', courseId)
    return new Promise((resolve) => {
      setTimeout(() => resolve({ status: 204 }), 1000)
    })
  },
}
