import { UserReviewModel } from 'app/models'

export const fakeUserReview: UserReviewModel = {
  expertReviews: [
    {
      lastName: 'Денисюк',
      firstName: 'Василина',
      middleName: 'Васильевна',
      goalsAndTasks: 3,
      solution: 2,
      presentation: 3,
      technical: 4,
      result: 2,
      knowledge: 4,
    },
    {
      lastName: 'Шадрин',
      firstName: 'Денис',
      middleName: 'Борисович',
      goalsAndTasks: 4,
      solution: 2,
      presentation: 1,
      technical: 4,
      result: 3,
      knowledge: 2,
    },
  ],
  averageReviews: [
    {
      criterion: 'Формулировка цели и задач',
      value: 3.5,
    },
    {
      criterion: 'Обоснование решения',
      value: 2,
    },
    {
      criterion: 'Презентация проекта',
      value: 2,
    },
    {
      criterion: 'Техническая проработанность',
      value: 4,
    },
    {
      criterion: 'Соответствие результата',
      value: 2.5,
    },
    {
      criterion: 'Знание предметной области',
      value: 3,
    },
  ],
  competencyReviews: [
    {
      competencyName: 'git',
      mark: 1,
    },
    {
      competencyName: 'c#',
      mark: 1,
    },
    {
      competencyName: 'figma',
      mark: 1,
    },
    {
      competencyName: 'javascript',
      mark: 1,
    },
    {
      competencyName: 'github',
      mark: 1,
    },
    {
      competencyName: 'react',
      mark: 1,
    },
  ],
}
