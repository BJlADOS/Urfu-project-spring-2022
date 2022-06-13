import React, { useEffect, useState } from 'react'
import { connect } from 'react-redux'
import { RootState } from 'app/reducers'
import { AppCard } from 'app/components/AppCard'
import { CuratorModel, CustomerModel, LifeScenarioModel, LoadingStatus, ShortProjectModel } from 'app/models'
import { AppSearch } from 'app/components/AppInput'
import { projects } from 'app/provider'
import { AppDropdown, DropdownOptionItem } from 'app/components/AppDropdown'
import { FiltersActions } from 'app/actions/filters'
import { FiltersState } from 'app/reducers/state'
import { AppButton } from 'app/components/AppButton'
import { ProjectListItemCard } from 'app/components/ProjectListItemCard'
import { AppLoadingSpinner } from 'app/components/AppLoadingSpinner'
import { SortType } from 'app/models/SortType'

import { SortButtons } from './SortButtons'
import style from './style.scss'

type ProjectSortFunc = (a: ShortProjectModel, b: ShortProjectModel) => number

const mapStateToProps = (state: RootState) => ({
  filters: state.filters,
  sorting: state.sorting,
})

const mapDispatchToProps = {
  setFilters: (filters: FiltersState) => FiltersActions.setFilters(filters),
  resetFilters: FiltersActions.resetFilters,
}

type StateProps = ReturnType<typeof mapStateToProps>
type DispatchProps = typeof mapDispatchToProps

interface Props extends StateProps, DispatchProps {
}

const SORT_FUNCTIONS = new Map<SortType, ProjectSortFunc>([
  [SortType.ProjectName, (a, b) => a.name.localeCompare(b.name)],
  [SortType.ParticipantsCount, (a, b) => a.participantsCount - b.participantsCount],
])

const ProjectsPageComponent: React.FC<Props> = ({
  filters,
  sorting,
  setFilters,
  resetFilters,
}) => {
  const [projectsList, setProjectsList] = useState<ShortProjectModel[]>([])
  const [loading, setLoading] = useState(LoadingStatus.Loading)

  useEffect(() => {
    setProjectsList(sortProjects(projectsList))
  }, [sorting])

  const isFiltersSet = Boolean(
    filters.scenario ||
    filters.scenario === 0 ||
    filters.technology ||
    filters.technology === 0 ||
    filters.term ||
    filters.curator ||
    filters.customer,
  )

  useEffect(() => {
    if (
      filters.scenarioList.length === 0 ||
      filters.curatorsList.length === 0 ||
      filters.customersList.length === 0
    ) {
      fetchFilterOptions()
    }
  }, [])

  useEffect(() => {
    searchProjects(filters)
  }, [filters.term, filters.scenario, filters.technology, filters.customer, filters.curator])

  const searchProjects = (filters: Partial<FiltersState>) => {
    fetchProjects({
      term: filters.term,
      lifeScenarioId: Number(filters.scenario) || undefined,
      keyTechnologyId: Number(filters.technology) || undefined,
      curator: String(filters.curator) || undefined,
      customer: String(filters.customer) || undefined,
    })
  }

  const fetchProjects = (options?: {
    term?: string
    lifeScenarioId?: number
    keyTechnologyId?: number
    curator?: string
    customer?: string
  }) => {
    setLoading(LoadingStatus.Loading)
    projects.getProjects(options)
      .then(res => setProjectsList(sortProjects(res.data)))
      .then(() => setLoading(LoadingStatus.Finished))
      .catch(() => setLoading(LoadingStatus.Error))
  }

  const fetchFilterOptions = () => {
    let scenarioList: LifeScenarioModel[] = []
    let curatorsList: CustomerModel[] = []
    let customersList: CuratorModel[] = []

    projects.getLifeScenario()
      .then(res => {
        scenarioList = res.data
      })
      .then(projects.getCurators)
      .then(res => {
        curatorsList = res.data
      })
      .then(projects.getCustomers)
      .then(res => {
        customersList = res.data
      })
      .then(() => updateFilters({
        scenarioList,
        curatorsList,
        customersList,
      }))
  }

  const updateFilters = (options: Partial<FiltersState>) => {
    setFilters({
      ...filters,
      ...options,
    })
  }

  const handleFilterChange = (filterOption: keyof FiltersState, filterValue: string | number) => {
    updateFilters({
      [filterOption]: filterValue,
    })
  }

  const handleLifeScenarioChange = (value?: LifeScenarioModel) => {
    updateFilters({
      scenario: value?.id,
      technology: undefined,
      keyTechnologyList: [],
    })

    if (value) {
      projects.getKeyTechnology(value.id)
        .then(res => {
          updateFilters({
            keyTechnologyList: res.data,
            scenario: value.id,
            technology: undefined,
          })
        })
    }
  }

  const sortProjects = (projectsList: ShortProjectModel[]) => {
    const {
      isDesc,
      type,
    } = sorting

    const sortFunc = SORT_FUNCTIONS.get(type) || (() => 0)

    const sortedProjects = projectsList.sort((a, b) => isDesc ? -sortFunc(a, b) : sortFunc(a, b))

    return [...sortedProjects]
  }

  const dataConverter = <T extends { id: number, name: string }>(value: T): DropdownOptionItem<T> => ({
    key: value.id,
    label: value.name,
  })

  const header = (
    <AppCard className={style.headerCard}>
      <h2>
        Проекты
        <span className={style.projectsCount}>
          {projectsList.length > 0 && projectsList.length}
        </span>
      </h2>
    </AppCard>
  )

  const filtersSection = (
    <>
      <div className={style.searchSection}>
        <AppSearch
          placeholder='Поиск проекта'
          className={style.search}
          value={filters.term}
          onChangeSearch={(value) => handleFilterChange('term', value)}
        />
        <SortButtons/>
      </div>

      <div className={style.filtersSection}>
        <div className={style.options}>
          <AppDropdown
            className={style.filterOption}
            items={filters.scenarioList}
            value={filters.scenarioList.find(c => c.id === filters.scenario) || null}
            dataConverter={dataConverter}
            onChange={handleLifeScenarioChange}
            placeholder='Жизненный сценарий'
            readOnly
          />
          <AppDropdown
            className={style.filterOption}
            items={filters.keyTechnologyList}
            value={filters.keyTechnologyList.find(c => c.id === filters.technology) || null}
            dataConverter={dataConverter}
            onChange={(value) => handleFilterChange('technology', value?.id || 0)}
            placeholder='Ключевая технология'
            readOnly
          />
          <AppDropdown
            className={style.filterOption}
            items={filters.customersList}
            value={filters.customersList.find(c => c.name === filters.customer) || null}
            dataConverter={(value) => ({
              key: value.name,
              label: value.name,
            })}
            onChange={(value) => handleFilterChange('customer', value?.name || '')}
            placeholder='Заказчик'
          />
          <AppDropdown
            className={style.filterOption}
            items={filters.curatorsList}
            value={filters.curatorsList.find(c => c.name === filters.curator) || null}
            dataConverter={(value) => ({
              key: value.name,
              label: value.name,
            })}
            onChange={(value) => handleFilterChange('curator', value?.name || '')}
            placeholder='Куратор'
          />
          {isFiltersSet &&
          <AppButton
            type='button'
            buttonType='secondary'
            className={style.resetButton}
            onClick={resetFilters}
          >
            Сбросить
          </AppButton>
          }
        </div>
        <div className={style.additional}>
          <label>
            <input
              type='checkbox'
              checked={filters.hideClosed}
              onChange={(e) => updateFilters({ hideClosed: e.target.checked })}
            />
            <span>Скрыть закрытые</span>
          </label>
        </div>
      </div>
    </>
  )

  const filteredProjectsList = filters.hideClosed
    ? projectsList.filter(p => p.isAvailable)
    : projectsList

  const projectsListContent = filteredProjectsList.sort((a, b) => Number(b.isPromoted) - Number(a.isPromoted)).map(item => (
    <ProjectListItemCard
      key={item.id}
      project={item}
    />
  ))

  const content = loading === LoadingStatus.Finished ? projectsListContent : (
    <div className={style.loader}>
      {loading === LoadingStatus.Loading && <AppLoadingSpinner/>}
      {loading === LoadingStatus.Error && (
        <div>
          <h2>Произошла ошибка!</h2>
          <p>Не получилось загрузить проекты по вашему запросу.</p>
          <p>Попробуйте перезагрузить страницу.</p>
        </div>
      )}
    </div>
  )

  return (
    <>
      {header}
      {filtersSection}
      {content}
    </>
  )
}

export const ProjectsPage: React.FC = connect(
  mapStateToProps,
  mapDispatchToProps,
)(ProjectsPageComponent)
