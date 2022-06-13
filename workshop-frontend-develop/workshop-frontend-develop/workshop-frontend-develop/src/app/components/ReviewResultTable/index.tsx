import React from 'react'
import { ExpertReviewModel } from 'app/models'
import { mean } from 'lodash'
import { AppTooltip } from 'app/components/AppTooltip'

import style from './style.scss'

interface Props {
  data: ExpertReviewModel[]
}

const Mark = ({ value }: {value: number | string | undefined}) => (
  <div className={style.markWrapper}>
    {value}
  </div>
)

const Row = ({ title, rowKey, data }: {title:string; rowKey:keyof ExpertReviewModel, data: ExpertReviewModel[] }) => {
  const marks = data.map(e => e[rowKey])

  return (
    <div className={style.tableRow}>
      <span className={style.tableRowTitle}>{title}</span>
      <div className={style.markContainer} >
        {marks.map((m, index) => <Mark
          key={index}
          value={m}
        />)}
        <Mark value={mean(marks)}/>
      </div>
    </div>
  )
}

export const ReviewResultTable: React.FC<Props> = ({ data }) => (
  <div className={style.tableBody}>
    <div className={style.nameHeader}>
      {data.map(e => (
        <div
          className={style.nameBox}
          key={e.lastName}
        >
          <AppTooltip content={<p className={style.tooltip}>{[e.lastName, e.firstName, e.middleName].join(' ')}</p>}>
            <span>
              {`${e.lastName[0]}.${e.firstName[0]}.${e.middleName[0] ?? ' '}`}
            </span>
          </AppTooltip>
        </div>))}
      <span
        className={style.nameBox}
      >Итого</span>
    </div>
    <Row
      title={'Формулировка цели и задач'}
      rowKey='goalsAndTasks'
      data={data}
    />
    <Row
      title={'Обоснование решения'}
      rowKey='solution'
      data={data}
    />
    <Row
      title={'Знание предметной области'}
      rowKey='knowledge'
      data={data}
    />
    <Row
      title={'Презентация проекта'}
      rowKey='presentation'
      data={data}
    />
    <Row
      title={'Техническая проработанность'}
      rowKey='technical'
      data={data}
    />
    <Row
      title={'Соответствие результата и цели'}
      rowKey='result'
      data={data}
    />
  </div>
)
