import { TeamModel, TeamStatusses, CompetencyType, UserCompetencyType, UserTypes, TeamListItem, TeamWithSlotListItem } from 'app/models'

export const fakeTeams: TeamModel[] = [
  {
    id: 0,
    name: '',
    isEntried: true,
    teamStatus: TeamStatusses.TestWork,
    comment: 'Ребята огонь, написали самый прекрасный проект, самые лучшие. Ничего дорабатывать не нужно, нужно у этой команды поучиться другим. К такому уровню нужно стремиться, поднимать наш вуз и нашу страну.',
    mark: 5,
    teamCompleteDate: new Date(new Date().getTime() + 3600000),
    project: {
      id: 0,
      name: 'Разработка самого лучшего проекта',
      curator: 'Хлебников Н.А.',
      organization: 'УрФУ',
      contacts: 'Иванов Иван Иванович - 89990990909',
      purpose: 'Создание приложения для расчета разности массы в момент когда вы находитесь на планете земля и на других планетах нашей солнечной системы',
      result: 'Прототип приложения',
      teamCapacity: 2,
      isEntryOpen: false,
      participantsCount: 3,
      fillTeamsCount: 1,
      maxTeamCount: 2,
      isAvailable: true,
      lifeScenario: {
        id: 1,
        name: 'Веб разработка',
      },
      keyTechnology: {
        id: 1,
        name: 'Front-end разработка',
      },
      competencies: [
        {
          id: 0,
          name: 'Frontend',
          competencyType: CompetencyType.HardSkill,
        },
        {
          id: 1,
          name: 'UI/UX',
          competencyType: CompetencyType.SoftSkill,
        },
        {
          id: 2,
          name: 'Аналитика',
          competencyType: CompetencyType.HardSkill,
        },
        {
          id: 3,
          name: 'Менеджемент',
          competencyType: CompetencyType.SoftSkill,
        },
        {
          id: 4,
          name: 'Управление проектами',
          competencyType: CompetencyType.HardSkill,
        },
        {
          id: 5,
          name: 'Системный анализ',
          competencyType: CompetencyType.HardSkill,
        },
        {
          id: 6,
          name: 'Длинное название для компетенции, которое не влезает в строчку',
          competencyType: CompetencyType.SoftSkill,
        },
      ],
      matchedCompetenciesCount: 5,
      description: 'Большое описание большого проекта',
      roles: [
        {
          id: 1,
          name: 'UI/UX',
        },
        {
          id: 2,
          name: 'Backend',
        },
        {
          id: 3,
          name: 'Frontend',
        },
        {
          id: 4,
          name: 'Аналитик',
        },
        {
          id: 5,
          name: 'Менеджер',
        },
      ],
    },
    review: {
      goalsAndTasks: 4,
      presentation: 3,
      knowledge: 3,
      result: 4,
    },
    competencyReview: [
      {
        id: 5,
        mark: 1,
      },
      {
        id: 3,
        mark: 0,
      },
      {
        id: 1,
        mark: 1,
      },
    ],
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
          id: 3,
          name: 'Frontend',
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
            id: 2,
            name: 'Аналитика',
            competencyType: CompetencyType.HardSkill,
            userCompetencyType: UserCompetencyType.Desirable,
          },
          {
            id: 3,
            name: 'Менеджемент',
            competencyType: CompetencyType.HardSkill,
            userCompetencyType: UserCompetencyType.Desirable,
          },
          {
            id: 4,
            name: 'Управление проектами',
            competencyType: CompetencyType.HardSkill,
            userCompetencyType: UserCompetencyType.Desirable,
          },
          {
            id: 5,
            name: 'Системный анализ',
            competencyType: CompetencyType.HardSkill,
            userCompetencyType: UserCompetencyType.Desirable,
          },
        ],
      },
      {
        id: 1,
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
          name: 'Backend',
        },
        competencies: [
          {
            id: 0,
            name: 'Frontend',
            competencyType: CompetencyType.SoftSkill,
            userCompetencyType: UserCompetencyType.Current,
          },
          {
            id: 1,
            name: 'UI/UX',
            competencyType: CompetencyType.HardSkill,
            userCompetencyType: UserCompetencyType.Desirable,
          },
          {
            id: 2,
            name: 'Аналитика',
            competencyType: CompetencyType.SoftSkill,
            userCompetencyType: UserCompetencyType.Desirable,
          },
        ],
      },
      {
        id: 2,
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
          id: 1,
          name: 'UI/UX',
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
            competencyType: CompetencyType.HardSkill,
            userCompetencyType: UserCompetencyType.Current,
          },
          {
            id: 2,
            name: 'Аналитика',
            competencyType: CompetencyType.SoftSkill,
            userCompetencyType: UserCompetencyType.Current,
          },
        ],
      },
    ],
  },
  {
    id: 1,
    name: 'New Team',
    isEntried: true,
    teamStatus: TeamStatusses.TestWork,
    teamCompleteDate: new Date(new Date().getTime() + 3600000),
    project: {
      id: 0,
      roles: [],
      fillTeamsCount: 1,
      participantsCount: 1,
      name: 'Разработка самого лучшего проекта',
      curator: 'Хлебников Н.А.',
      organization: 'УрФУ',
      isAvailable: true,
      isEntryOpen: true,
      contacts: 'Иванов Иван Иванович - 89990990909',
      purpose: 'Создание приложения для расчета разности массы в момент когда вы находитесь на планете земля и на других планетах нашей солнечной системы',
      result: 'Прототип приложения',
      teamCapacity: 2,
      maxTeamCount: 2,
      description: 'Большое описание большого проекта',
      lifeScenario: {
        id: 1,
        name: 'Веб разработка',
      },
      keyTechnology: {
        id: 1,
        name: 'Front-end разработка',
      },
      competencies: [],
      matchedCompetenciesCount: 5,
    },
    expert: {
      id: 0,
      auditorium: 'И-232',
    },
    users: [
      {
        id: 0,
        lastName: 'Иванов',
        firstName: 'Илья',
        middleName: 'Сергеевич',
        academicGroup: 'ФО-320001',
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
            competencyType: CompetencyType.HardSkill,
            userCompetencyType: UserCompetencyType.Current,
          },
          {
            id: 2,
            name: 'Аналитика',
            competencyType: CompetencyType.HardSkill,
            userCompetencyType: UserCompetencyType.Current,
          },
        ],
      },
    ],
  },
]

export const fakeTeamsList: TeamListItem[] = [
  {
    id: 1,
    name: 'BIG SVM',
    teamCompleteDate: new Date(new Date().getTime() + 3600000),
    teamStatus: TeamStatusses.Completed,
    usersCount: 9,
    projectName: 'Модернизация сайта ИнФО',
    projectDescription: 'Усовершенствовать сайт ИнФО: путем проведения аналитики конкурентов и предпочтений целевой аудитории сформировать информативный контент; добавить новые функции; модернизовать дизайн.',
    teamCapacity: 10,
    isEntried: false,
  },
  {
    id: 2,
    teamStatus: TeamStatusses.Incomplete,
    teamCompleteDate: new Date(new Date().getTime() + 3600000),
    usersCount: 1,
    projectName: 'Geek Plants',
    projectDescription: 'Разработка кросс-платформенного мобильного приложения (или нативных версий для платформ - iOS/Android) по готовому дизайну в Figma. Задача следующей практики – разработка (или кастомизация готового решения) и обучение нейросети для определения растений по фото и внедрение в приложение – также может быть выполнена в рамках данного проекта.',
    teamCapacity: 3,
    isEntried: true,
  },
  {
    id: 3,
    teamStatus: TeamStatusses.Incomplete,
    usersCount: 0,
    projectName: 'Создание инструмента для идентификации физических упражнений и количества их повторений.',
    projectDescription: 'Создание инструмента для идентификации физических упражнений и количества их повторений.',
    teamCapacity: 5,
    isEntried: true,
  },
  {
    id: 4,
    name: 'Целый Байт',
    teamStatus: TeamStatusses.Completed,
    teamCompleteDate: new Date(new Date().getTime() + 3600000),
    usersCount: 3,
    projectName: 'Компьютерная игра аркада на стоматологическую тематику для школьников среднего звена',
    projectDescription: 'Формирование мотивации по уходу за полостью рта на основе интеллектуальной системы',
    teamCapacity: 3,
    isEntried: true,
  },
]

export const fakeReviewTeamsList: TeamWithSlotListItem[] = [
  {
    id: 1,
    name: 'BIG SVM',
    slotTime: new Date(new Date().getTime() + 3600000).toISOString(),
    usersCount: 9,
    projectName: 'Модернизация сайта ИнФО',
    projectDescription: 'Усовершенствовать сайт ИнФО: путем проведения аналитики конкурентов и предпочтений целевой аудитории сформировать информативный контент; добавить новые функции; модернизовать дизайн.',
    teamCapacity: 10,
    auditoriumId: 2,
    auditoriumName: 'Room 123',
  },
  {
    id: 2,
    slotTime: new Date(new Date().getTime() + 3600000).toISOString(),
    usersCount: 1,
    projectName: 'Geek Plants',
    projectDescription: 'Разработка кросс-платформенного мобильного приложения (или нативных версий для платформ - iOS/Android) по готовому дизайну в Figma. Задача следующей практики – разработка (или кастомизация готового решения) и обучение нейросети для определения растений по фото и внедрение в приложение – также может быть выполнена в рамках данного проекта.',
    teamCapacity: 3,
    auditoriumId: 1,
    auditoriumName: 'Tests',
  },
  {
    id: 4,
    slotTime: new Date(new Date().getTime() + 8600000).toISOString(),
    name: 'Целый Байт',
    usersCount: 3,
    projectName: 'Компьютерная игра аркада на стоматологическую тематику для школьников среднего звена',
    projectDescription: 'Формирование мотивации по уходу за полостью рта на основе интеллектуальной системы',
    teamCapacity: 3,
    auditoriumId: 1,
    auditoriumName: 'Tests',
  },
]
