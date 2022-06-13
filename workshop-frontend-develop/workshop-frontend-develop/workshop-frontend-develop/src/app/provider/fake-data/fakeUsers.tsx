import {
  UserProfileModel,
  UserTypes,
  TeamStatusses,
  CompetencyType,
  UserCompetencyType,
} from 'app/models'

import { fakeProjects } from './fakeProjects'

// Аккаунт Администратора
export const fakeAdmin: UserProfileModel =
{
  id: 1,
  eventId: 1,
  lastName: 'Admin',
  firstName: 'Test',
  middleName: '',
  login: '',
  phoneNumber: '',
  about: '',
  email: '',
  academicGroup: '',
  userType: UserTypes.Admin,
  competencies: [],
}

// Аккаунт Cтудента с командой
export const fakeStudentWithTeam: UserProfileModel =
{
  id: 1,
  eventId: 1,
  lastName: 'Иванов',
  firstName: 'Иван',
  middleName: 'Иванович',
  login: '14urfu@gmail.com',
  phoneNumber: '+79090207012',
  about: '',
  email: 'test@gmail.com',
  academicGroup: 'РИ-100020',
  userType: UserTypes.Student,
  profileFilled: true,
  coursesProgress: [
    {
      id: 0,
      tasksCompleted: 56,
    },
  ],
  competencies: [
    {
      id: 0,
      name: 'Web-разработка',
      competencyType: CompetencyType.HardSkill,
      userCompetencyType: UserCompetencyType.Current,
    },
    {
      id: 1,
      name: 'TypeScript',
      competencyType: CompetencyType.HardSkill,
      userCompetencyType: UserCompetencyType.Current,
    },
    {
      id: 2,
      name: 'NodeJS',
      competencyType: CompetencyType.HardSkill,
      userCompetencyType: UserCompetencyType.Current,
    },
    {
      id: 3,
      name: 'Коммуникабельность',
      competencyType: CompetencyType.SoftSkill,
      userCompetencyType: UserCompetencyType.Current,
    },
    {
      id: 4,
      name: 'UI/UX',
      competencyType: CompetencyType.HardSkill,
      userCompetencyType: UserCompetencyType.Desirable,
    },
    {
      id: 5,
      name: 'Умение работать в команде',
      competencyType: CompetencyType.SoftSkill,
      userCompetencyType: UserCompetencyType.Desirable,
    },
  ],
  team: {
    id: 1,
    name: 'Test Team',
    isEntried: true,
    teamStatus: TeamStatusses.ExpertDiscussion,
    expert: {
      id: 1,
      auditorium: 'Т-506',
    },
    teamSlot: {
      id: 1,
      time: new Date().toString(),
    },
    teamCompleteDate: new Date(new Date().getTime() + 3600000),
    project: fakeProjects[0],
    users: [
      {
        id: 0,
        lastName: 'Иванов',
        firstName: 'Илья',
        middleName: 'Сергеевич',
        academicGroup: 'ФО-320001',
        login: '',
        about: '',
        userType: UserTypes.Student,
        phoneNumber: '+79090207012',
        email: '14neruin@gmail.com',
        role: {
          id: 2,
          name: 'Дизайнер',
        },
        competencies: [
          {
            id: 0,
            name: 'Frontend',
            competencyType: CompetencyType.HardSkill,
            userCompetencyType: UserCompetencyType.Current,
          },
          {
            id: 1,
            name: 'UI/UX',
            competencyType: CompetencyType.SoftSkill,
            userCompetencyType: UserCompetencyType.Current,
          },
          {
            id: 4,
            name: 'Figma',
            competencyType: CompetencyType.HardSkill,
            userCompetencyType: UserCompetencyType.Current,
          },
          {
            id: 2,
            name: 'Аналитика',
            competencyType: CompetencyType.HardSkill,
            userCompetencyType: UserCompetencyType.Desirable,
          },
        ],
      },
      {
        id: 1,
        lastName: 'Александров',
        firstName: 'Александр',
        middleName: 'Александрович',
        academicGroup: 'ФО-320001',
        login: '',
        about: '',
        userType: UserTypes.Student,
        phoneNumber: '+79042421',
        email: '115neruin@gmail.com',
        role: {
          id: 1,
          name: 'Аналитик',
        },
        competencies: [
          {
            id: 0,
            name: 'Frontend',
            competencyType: CompetencyType.HardSkill,
            userCompetencyType: UserCompetencyType.Current,
          },
          {
            id: 1,
            name: 'UI/UX',
            competencyType: CompetencyType.SoftSkill,
            userCompetencyType: UserCompetencyType.Current,
          },
        ],
      },
      {
        id: 2,
        lastName: 'Андреев',
        firstName: 'Андрей',
        middleName: 'Андреевич',
        academicGroup: 'ФО-320001',
        login: '',
        about: '',
        userType: UserTypes.Student,
        phoneNumber: '+7239123023',
        email: 'test134@gmail.com',
        competencies: [],
      },
    ],
  },
}

// Аккаунт Cтудента без команды
export const fakeStudentWithoutTeam: UserProfileModel =
{
  id: 1,
  eventId: 1,
  lastName: 'Иванов',
  firstName: 'Иван',
  middleName: 'Иванович',
  login: '14urfu@gmail.com',
  phoneNumber: '+79090207012',
  about: '',
  email: 'test@gmail.com',
  academicGroup: 'РИ-200020',
  userType: UserTypes.Student,
  profileFilled: true,
  coursesProgress: [
    {
      id: 0,
      tasksCompleted: 56,
    },
  ],
  competencies: [
    {
      id: 0,
      name: 'Web-разработка',
      competencyType: CompetencyType.HardSkill,
      userCompetencyType: UserCompetencyType.Current,
    },
    {
      id: 1,
      name: 'TypeScript',
      competencyType: CompetencyType.HardSkill,
      userCompetencyType: UserCompetencyType.Current,
    },
    {
      id: 2,
      name: 'NodeJS',
      competencyType: CompetencyType.HardSkill,
      userCompetencyType: UserCompetencyType.Current,
    },
    {
      id: 3,
      name: 'Коммуникабельность',
      competencyType: CompetencyType.SoftSkill,
      userCompetencyType: UserCompetencyType.Current,
    },
    {
      id: 4,
      name: 'UI/UX',
      competencyType: CompetencyType.HardSkill,
      userCompetencyType: UserCompetencyType.Desirable,
    },
    {
      id: 5,
      name: 'Умение работать в команде',
      competencyType: CompetencyType.SoftSkill,
      userCompetencyType: UserCompetencyType.Desirable,
    },
  ],
}

// Аккаунт Эксперта
export const fakeExpert: UserProfileModel =
{
  id: 1,
  eventId: 1,
  lastName: 'Иванов',
  firstName: 'Иван',
  middleName: 'Иванович',
  login: '14urfu@gmail.com',
  phoneNumber: '+79090207012',
  about: '',
  email: '',
  academicGroup: '',
  userType: UserTypes.Expert,
  auditorium: 'И-322',
  competencies: [],
}
