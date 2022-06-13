import { AuditoriumModel } from 'app/models'

export const fakeAuditoriums: AuditoriumModel[] = [
  {
    id: 0,
    name: 'И-306',
    experts: [
      {
        id: 1,
        lastName: 'Иванов',
        firstName: 'Иван',
        middleName: 'Иванович',
      },
      {
        id: 2,
        lastName: 'Expert',
        firstName: 'Number',
        middleName: '2',
      },
      {
        id: 3,
        lastName: 'Expert',
        firstName: 'Test',
        middleName: '2',
      },
    ],
    slots: [
      {
        id: 1,
        team: {
          id: 1,
          name: 'Test team',
        },
        time: new Date().toString(),
      },
      {
        id: 2,
        time: new Date().toString(),
      },
      {
        id: 3,
        time: new Date().toString(),
      },
      {
        id: 4,
        team: {
          id: 12,
          name: 'New team',
        },
        time: new Date().toString(),
      },
    ],
  },
  {
    id: 1,
    name: 'И-322',
    experts: [],
    slots: [],
  },
  {
    id: 2,
    name: 'И-535',
    experts: [
      {
        id: 1,
        lastName: 'Иванов',
        firstName: 'Иван',
        middleName: 'Иванович',
      },
      {
        id: 3,
        lastName: 'Андреев',
        firstName: 'Николай',
        middleName: 'Иванович',
      },
    ],
    slots: [
      {
        id: 6,
        team: {
          id: 11,
        },
        time: new Date().toString(),
      },
      {
        id: 7,
        time: new Date().toString(),
      },
    ],
  },
  {
    id: 3,
    name: 'ГУК Паркет',
    experts: [
      {
        id: 1,
        lastName: 'Иванов',
        firstName: 'Иван',
        middleName: 'Иванович',
      },
      {
        id: 2,
        lastName: 'Иванов',
        firstName: 'Иван',
        middleName: 'Иванович',
      },
      {
        id: 3,
        lastName: 'Иванов',
        firstName: 'Иван',
        middleName: 'Иванович',
      },
      {
        id: 4,
        lastName: 'Иванов',
        firstName: 'Иван',
        middleName: 'Иванович',
      },
    ],
    slots: [
      {
        id: 8,
        time: new Date(new Date().getTime() + 99000000).toString(),
      },
      {
        id: 9,
        time: new Date(new Date().getTime() - 3600000).toString(),
      },
      {
        id: 10,
        time: new Date(new Date().getTime() + 1800000).toString(),
      },
    ],
  },
  {
    id: 4,
    name: 'Т-901',
    experts: [],
    slots: [],
  },
]
