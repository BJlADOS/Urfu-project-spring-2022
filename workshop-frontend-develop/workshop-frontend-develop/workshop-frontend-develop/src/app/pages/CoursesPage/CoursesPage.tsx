import React, { useEffect, useState } from 'react'
import { connect } from 'react-redux'
import { RootState } from 'app/reducers'
import { AppCard } from 'app/components/AppCard'
import { LoadingStatus, RoleModel } from 'app/models'
import { AppSearch } from 'app/components/AppInput'
import { courses } from 'app/provider'
import { AppDropdown, DropdownOptionItem } from 'app/components/AppDropdown'
import { FiltersActions } from 'app/actions/filters'
import { CoursesFiltersState } from 'app/reducers/state'
import { AppButton } from 'app/components/AppButton'
import { AppLoadingSpinner } from 'app/components/AppLoadingSpinner'
import { SortType } from 'app/models/SortType'
import { CourseModel } from 'app/models/CourseModel'
import { CourseListItemCard } from 'app/components/CoursesListItemCard'

import { SortButtons } from './SortButtons'
import style from './style.scss'

type CoursesSortFunc = (a: CourseModel, b: CourseModel) => number

const mapStateToProps = (state: RootState) => ({
  coursesFilters: state.coursesFilters,
  sorting: state.sorting,
  profile: state.profile,
})

const mapDispatchToProps = {
  setFilters: (coursesFilters: CoursesFiltersState) => FiltersActions.setFilters(coursesFilters),
  resetFilters: FiltersActions.resetFilters,
}

type StateProps = ReturnType<typeof mapStateToProps>
type DispatchProps = typeof mapDispatchToProps

interface Props extends StateProps, DispatchProps {
}

const SORT_FUNCTIONS = new Map<SortType, CoursesSortFunc>([
  [SortType.ProjectName, (a, b) => a.name.localeCompare(b.name)],
])

const CoursesPageComponent: React.FC<Props> = ({
  coursesFilters,
  sorting,
  profile,
  setFilters,
  resetFilters,
}) => {
  const [coursesList, setCoursesList] = useState<CourseModel[]>([])
  const [loading, setLoading] = useState(LoadingStatus.Loading)

  useEffect(() => {
    setCoursesList(sortCourses(coursesList))
  }, [sorting])

  const isFiltersSet = Boolean(
    coursesFilters.role ||
    coursesFilters.role === 0 ||
    coursesFilters.term,
  )

  useEffect(() => {
    if (
      coursesFilters.roleList.length === 0
    ) {
      fetchFilterOptions()
    }
  }, [])

  useEffect(() => {
    searchCourses(coursesFilters)
  }, [coursesFilters.term, coursesFilters.role])

  const searchCourses = (filters: Partial<CoursesFiltersState>) => {
    fetchCourses({
      term: filters.term,
      roleId: Number(filters.role) || undefined,
      hideClosed: filters.hideClosed,
      hideCompleted: filters.hideCompleted,
      showMine: filters.showMine,
    })
  }

  const fetchCourses = (options?: {
    term?: string
    roleId?: number
    hideClosed?: boolean
    hideCompleted?: boolean,
    showMine?: boolean,
  }) => {
    setLoading(LoadingStatus.Loading)
    courses.getCourses(options)
      .then(res => setCoursesList(sortCourses(res.data)))
      .then(() => setLoading(LoadingStatus.Finished))
      .catch(() => setLoading(LoadingStatus.Error))
  }

  const fetchFilterOptions = () => {
    let roleList: RoleModel[] = []

    courses.getCourseRoles()
      .then(res => {
        roleList = res.data
      }).then(() => updateFilters({
        roleList,
      }))
  }

  const updateFilters = (options: Partial<CoursesFiltersState>) => {
    setFilters({
      ...coursesFilters,
      ...options,
    })
  }

  const handleFilterChange = (filterOption: keyof CoursesFiltersState, filterValue: string | number) => {
    updateFilters({
      [filterOption]: filterValue,
    })
  }

  const handleRoleChange = (value?: RoleModel) => {
    updateFilters({
      role: value?.id,
    })
  }

  const sortCourses = (coursesList: CourseModel[]) => {
    const {
      isDesc,
      type,
    } = sorting

    const sortFunc = SORT_FUNCTIONS.get(type) || (() => 0)

    const sortedCourses = coursesList.sort((a, b) => isDesc ? -sortFunc(a, b) : sortFunc(a, b))

    return [...sortedCourses]
  }

  const dataConverter = <T extends { id: number, name: string }>(value: T): DropdownOptionItem<T> => ({
    key: value.id,
    label: value.name,
  })

  const header = (
    <AppCard className={style.headerCard}>
      <h2>
        Курсы
        <span className={style.coursesCount}>
          {coursesList.length > 0 && coursesList.length}
        </span>
      </h2>
    </AppCard>
  )

  const filtersSection = (
    <>
      <div className={style.searchSection}>
        <AppSearch
          placeholder='Поиск курса'
          className={style.search}
          value={coursesFilters.term}
          onChangeSearch={(value) => handleFilterChange('term', value)}
        />
        <SortButtons/>
      </div>

      <div className={style.filtersSection}>
        <div className={style.options}>
          <AppDropdown
            className={style.filterOption}
            items={coursesFilters.roleList}
            value={coursesFilters.roleList.find(c => c.id === coursesFilters.role) || null}
            dataConverter={dataConverter}
            onChange={handleRoleChange}
            placeholder='Роль'
            readOnly
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
              checked={coursesFilters.hideClosed}
              onChange={(e) => updateFilters({ hideClosed: e.target.checked })}
            />
            <span>Скрыть закрытые</span>
          </label>
          <label>
            <input
              type='checkbox'
              checked={coursesFilters.hideCompleted}
              onChange={(e) => updateFilters({ hideCompleted: e.target.checked })}
            />
            <span>Скрыть завершённые</span>
          </label>
          <label>
            <input
              type='checkbox'
              checked={coursesFilters.showMine}
              onChange={(e) => updateFilters({ showMine: e.target.checked })}
            />
            <span>Только мои курсы</span>
          </label>
        </div>
      </div>
    </>
  )

  const filteredcoursesList = coursesFilters.hideClosed
    ? coursesList.filter(p => p.isAvailable)
    : coursesList

  const coursesListContent = filteredcoursesList.sort((a, b) => Number(b.isPromoted) - Number(a.isPromoted)).map(item => (
    <CourseListItemCard
      key={item.id}
      course={item}
      profile = {profile.profile?.coursesProgress}
    />
  ))

  const content = loading === LoadingStatus.Finished ? coursesListContent : (
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

export const CoursesPage: React.FC = connect(
  mapStateToProps,
  mapDispatchToProps,
)(CoursesPageComponent)
