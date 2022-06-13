import { RoleModel } from './RoleModel'

export enum LectureType {
  Lecture = 'Lecture',
  VideoLecture = 'VideoLecture',
  Test = 'Test'
}

export interface CourseModel {
  id: number,
  name: string,
  description: string,
  goal: string,
  organizator: string,
  direction: number,
  role: RoleModel,
  isAvailable: boolean,
  isPromoted: boolean,
  image: string,
  tasks: number,
  contacts: string,
}
//  TODO: expand LectureContent type to support tests
export interface TextLecture {
  id: number,
  title: string,
  text: string,
  picture?: string
}

export type LectureContent = string | TextLecture[]
export interface CourseSectionLectureModel {
  id: number,
  type: LectureType,
  title: string,
  content: LectureContent,
}

export interface CourseSectionModel {
  id: number,
  title: string,
  content: CourseSectionLectureModel[],
}

export interface CourseDataModel {
  id: number,
  content: CourseSectionModel[],
}

export interface CourseProgress {
  id: number,
  tasksCompleted: number,
}
