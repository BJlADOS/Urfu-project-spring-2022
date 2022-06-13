import React from 'react'
import { SortRounded } from '@material-ui/icons'
import { SortType } from 'app/models/SortType'
import { RootState } from 'app/reducers'
import { SortingAction } from 'app/actions'
import { SortingState } from 'app/reducers/state'
import { connect } from 'react-redux'

import style from './style.scss'

const sortOptionsMap = new Map(
  [
    [SortType.ProjectName, 'Названию'],
    [SortType.ParticipantsCount, 'Участникам'],
  ],
)

const mapStateToProps = (state: RootState) => ({ sorting: state.sorting })

const mapDispatchToProps = {
  setSorting: (sorting: SortingState) => SortingAction.setSorting(sorting),
}

type StateProps = ReturnType<typeof mapStateToProps>
type DispatchProps = typeof mapDispatchToProps

interface Props extends StateProps, DispatchProps {
}

const SortButtonsComponent: React.FC<Props> = ({
  sorting,
  setSorting,
}) => {
  const handleSortOrderChange = (type: SortType) => {
    let updatedIsDesc = sorting.isDesc

    if (type === sorting.type) {
      updatedIsDesc = !sorting.isDesc
    } else {
      updatedIsDesc = false
    }

    setSorting({
      type,
      isDesc: updatedIsDesc,
    })
  }

  const sortButton = [...sortOptionsMap.entries()].map(([type, label]) => (
    <div
      key={type}
      className={style.sortOption}
    >
      <button onClick={() => handleSortOrderChange(type)}>
        {label}
      </button>
      {sorting.type === type && <SortRounded className={!sorting.isDesc ? style.asc : ''}/>}
    </div>
  ))

  return (
    <div className={style.sorting}>
      <span className={style.sortBy}>
        Сортировать по:
      </span>
      {sortButton}
    </div>
  )
}

export const SortButtons: React.FC = connect(
  mapStateToProps,
  mapDispatchToProps,
)(SortButtonsComponent)
