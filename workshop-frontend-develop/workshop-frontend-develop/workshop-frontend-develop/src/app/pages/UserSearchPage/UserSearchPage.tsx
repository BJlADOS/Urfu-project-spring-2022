import React, { useEffect, useState } from 'react'
import { AppCard } from 'app/components/AppCard'
import { AppSearch } from 'app/components/AppInput'
import { CompetencyModel, LoadingStatus, ShortProjectModel, ShortUserModel } from 'app/models'
import { projects, requestProposal, teamlead, user } from 'app/provider'
import { RootState } from 'app/reducers'
import { connect } from 'react-redux'
import { UsersFilterState } from 'app/reducers/state'
import { UserFilterActions } from 'app/actions'
import { UserSearchCard } from 'app/components/UserSearchCard'
import { CompetenciesSearchCard } from 'app/components/CompetenciesSearchCard'
import { AppLoadingSpinner } from 'app/components/AppLoadingSpinner'
import { AppButton } from 'app/components/AppButton'

import style from './style.scss'

const mapStateToProps = (state: RootState) => ({
  profile: state.profile,
  userFilters: state.userFilters,
})

const mapDispatchToProps = {
  setUserFilters: (filters: UsersFilterState) => UserFilterActions.setUserFilters(filters),
  resetUserFilters: UserFilterActions.resetUserFilters,
}

type StateProps = ReturnType<typeof mapStateToProps>
type DispatchProps = typeof mapDispatchToProps

interface Props extends StateProps, DispatchProps {
}

export const UserSearchPageComponent: React.FC<Props> = ({
  profile,
  userFilters,
  setUserFilters,
  resetUserFilters,

}) => {
  const [usersList, setUsersList] = useState<ShortUserModel[]>([])
  const [selectedCompetencies, setSelectedCompetencies] = useState<CompetencyModel[]>([])
  const [project, setProject] = useState<ShortProjectModel | undefined>(undefined)
  const [currentPage, setCurrentPage] = useState(1)
  const [loading, setLoading] = useState(LoadingStatus.Loading)

  useEffect(() => {
    let competenciesList: CompetencyModel[] = []

    projects.getProject(profile!.profile!.team!.project.id)
      .then((res) => setProject(res.data))
      .then(user.getCompetitions)
      .then(result => {
        competenciesList = result.data
      })
      .then(() => updateFilters({ allCompetencies: competenciesList }))
  }, [])

  useEffect(() => {
    const div = document.getElementById(style.scrollContent)

    // TODO Ref in div-scroll
    div?.removeEventListener('scroll', handleScroll)
    div?.addEventListener('scroll', handleScroll)
    return () => {
      div?.removeEventListener('scroll', handleScroll)
    }
  }, [])

  useEffect(() => {
    if (loading === LoadingStatus.Paginating) {
      searchUsers(userFilters)
    }
  }, [loading])

  useEffect(() => {
    if (loading === LoadingStatus.Loading) {
      searchUsers(userFilters)
    }
  }, [loading, userFilters.term, userFilters.competenciesSearch])

  const isFilterSet = Boolean(
    userFilters.term ||
    userFilters.competenciesSearch.length !== 0,
  )
  const handleScroll = (e: any) => {
    if (usersList.length % 50 === 0 && (e.target.scrollHeight - (e.target.scrollTop + window.innerHeight) < 300)) {
      setLoading(LoadingStatus.Paginating)
    }
  }

  const handleResetFilters = () => {
    resetUserFilters()
    setUsersList([])
    setCurrentPage(1)
    setLoading(LoadingStatus.Loading)
  }
  const searchUsers = (userFilters: Partial<UsersFilterState>) => {
    if (loading === LoadingStatus.Paginating) {
      fetchUsersPagination(usersList.length, {
        term: userFilters.term,
        competenciesIds: userFilters.competenciesSearch?.map((x) => x.id).toString() || '',
      })
    } else {
      fetchUsers({
        term: userFilters.term,
        competenciesIds: userFilters.competenciesSearch?.map((x) => x.id).toString() || '',
      })
    }
  }

  const fetchUsersPagination = (count: number, options?: {
    term?: string,
    competenciesIds: string
  }) => {
    teamlead.getUsers(currentPage, (usersList.length >= 1 && usersList[usersList.length - 1].id) || undefined, options)
      .then((res) => {
        setUsersList([...usersList, ...res.data])
      })
      .then(() => setLoading(LoadingStatus.Finished))
      .catch(() => setLoading(LoadingStatus.Error))
      .then(() => setCurrentPage(prevState => prevState + 1))
  }

  const fetchUsers = (options?: {
    term?: string,
    competenciesIds: string
  }) => {
    teamlead.getUsers(1, undefined, options)
      .then((res) => {
        setUsersList(res.data)
      })
      .then(() => setLoading(LoadingStatus.Finished))
      .catch(() => setLoading(LoadingStatus.Error))
  }

  const updateFilters = (options: Partial<UsersFilterState>) => {
    setUserFilters({
      ...userFilters,
      ...options,
    })
    setLoading(LoadingStatus.Loading)
  }
  // (value) => handleFilterChange('competency', value?.id || 0)}
  const addSearchCompetency = (competency?: CompetencyModel) => {
    if (competency) {
      const newSelectedCompetencies = selectedCompetencies.some(c => c.id === competency.id)
        ? [...selectedCompetencies]
        : selectedCompetencies.concat(competency)

      setSelectedCompetencies(newSelectedCompetencies)
      updateFilters({
        competenciesSearch: newSelectedCompetencies,
      })
    }
  }
  const handleFilterChange = (filterOption: keyof UsersFilterState, filterValue: string | number) => {
    updateFilters({
      [filterOption]: filterValue,
    })
  }

  const createRequestProposal = (userId: number, teamleadId: number, projectId: number, role: string) => {
    requestProposal.createRequestProposal(userId, teamleadId, projectId, role)
      .then(fetchUsers)
      .then(() => resetUserFilters())
  }
  const handleCompetencyDelete = (id: number) => {
    const editedCompetencies = selectedCompetencies.filter(item => item.id !== id)

    setSelectedCompetencies(editedCompetencies)
    updateFilters({
      competenciesSearch: [...editedCompetencies],
    })
  }

  const usersContent = (
    <div className={style.loader}>
      {loading === (LoadingStatus.Loading || LoadingStatus.Paginating) && <AppLoadingSpinner/>}
      {loading === LoadingStatus.Error && (
        <div>
          <h2>Произошла ошибка!</h2>
          <p>Не получилось загрузить пользователей по вашему запросу.</p>
          <p>Попробуйте перезагрузить страницу.</p>
        </div>
      )}
    </div>
  )

  return (
    <>
      <AppCard className={style.searchContent}>
        <h2>Поиск пользователей</h2>
      </AppCard>
      <br/>
      <AppCard>
        <AppSearch
          className={style.search}
          value={userFilters.term}
          onChangeSearch={(value) => handleFilterChange('term', value)}
          placeholder='Поиск пользователя'
        />
        <CompetenciesSearchCard
          className={style.SearchCompetencies}
          competenciesList={userFilters.allCompetencies.filter(c => selectedCompetencies.every(s => s.id !== c.id))}
          selectedItems={userFilters.competenciesSearch}
          onDelete={handleCompetencyDelete}
          onChange={addSearchCompetency}
        />
        {isFilterSet && <AppButton
          type='button'
          buttonType='secondary'
          className={style.resetButton}
          onClick={handleResetFilters}
        >Сбросить</AppButton>}
      </AppCard>
      <br/>
      {usersList.map(user =>
        (
          <UserSearchCard
            user={user}
            createRequestProposal={(role) => {
              if (profile.profile?.team) {
                createRequestProposal(user.id, profile.profile.id, profile.profile.team.project.id, role)
              }
            }}
            key={user.id}
            rolesList={project?.roles || []}
          />
        ),
      )
      }
      {/* {Нужный тернарник} */}
      {loading === LoadingStatus.Finished ? <></> : usersContent}
    </>
  )
}
export const UserSearchPage: React.FC = connect(
  mapStateToProps,
  mapDispatchToProps,
)(UserSearchPageComponent)
