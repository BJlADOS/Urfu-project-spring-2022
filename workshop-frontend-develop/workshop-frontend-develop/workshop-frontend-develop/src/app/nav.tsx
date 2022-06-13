export function index(): string {
  return '/'
}

export function profile(): string {
  return '/profile'
}

export function profileEdit(): string {
  return profile() + '/edit'
}

export function login(): string {
  return '/login'
}

export function forgotPassword(): string {
  return '/forgot-password'
}

export function resetPassword(): string {
  return '/reset-password'
}

export function registration(): string {
  return '/registration'
}
export function registrationUrfu(): string {
  return '/registrationUrfu'
}
export function changePassword(): string {
  return '/change-pass'
}

export function monitoring(): string {
  return '/monitoring'
}

export function uploadProjects(): string {
  return '/upload-projects'
}

export function competencies(): string {
  return '/competencies'
}

export function teams(): string {
  return '/teams'
}

export function team(id: string | number): string {
  return teams() + '/' + id
}

export function projects(): string {
  return '/projects'
}

export function project(id: string | number): string {
  return projects() + '/' + id
}

export function events(): string {
  return '/events'
}

export function myTeam(): string {
  return '/my-team'
}

export function myProject(): string {
  return '/my-project'
}

export function usersManagement(): string {
  return '/users-management'
}

export function userInfo(id: string | number): string {
  return usersManagement() + '/' + id
}

export function auditoriums(): string {
  return '/auditoriums'
}

export function review(): string {
  return '/review'
}

export function develop(): string {
  return '/develop'
}

export function projectProposals(): string {
  return '/project-proposals'
}

export function projectProposal(id: string | number): string {
  return projectProposals() + '/' + id
}

export function createProjectProposal(): string {
  return projectProposals() + '/create'
}

export function addProject(): string {
  return '/addProject'
}

export function userSearch(): string {
  return '/userSearch'
}
export function requestProposals():string {
  return '/requests'
}
export function userProposalRequests():string {
  return '/teamRequests'
}
export function courses():string {
  return '/courses'
}

export function course(id: string | number): string {
  return courses() + '/' + id
}

export function courseTopic(topicId: string | number, courseId: string | number): string {
  return course(courseId) + '/' + topicId
}
