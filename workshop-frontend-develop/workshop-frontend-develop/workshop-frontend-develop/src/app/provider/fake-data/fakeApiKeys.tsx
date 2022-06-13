import { ApiKeyModel, UserTypes } from 'app/models'

export const fakeApiKeys: ApiKeyModel[] = [
  {
    id: 1,
    name: 'First key',
    eventId: 1,
    userType: UserTypes.Admin,
    keyString: 'test-key',
  },
  {
    id: 2,
    name: 'Second key',
    eventId: 1,
    userType: UserTypes.Student,
    keyString: 'test-key-student',
  },
  {
    id: 3,
    name: 'Third key with long nameeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee',
    eventId: 1,
    userType: UserTypes.Expert,
    keyString: 'test-key-student-test-key-student-test-key-student-test-key-studenttest-key-student',
  },
]
