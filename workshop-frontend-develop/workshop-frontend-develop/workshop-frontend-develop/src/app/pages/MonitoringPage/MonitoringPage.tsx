import React, { useEffect, useState } from 'react'
import cls from 'classnames'
import { admin } from 'app/provider'
import { AppCard } from 'app/components/AppCard'
import { MonitoringModel } from 'app/models/MonitoringModel'
import { AppButton } from 'app/components/AppButton'
import { handleDownloadResponse } from 'app/utils/handleDownloadResponse'

import style from './style.scss'

export const MonitoringPage: React.FC = () => {
  const [monitoring, updateMonitoring] = useState<MonitoringModel[]>([])

  const getEventData = () => {
    admin.getEventData()
      .then((response) => handleDownloadResponse(response))
  }

  const getEventResult = () => {
    admin.getEventResult()
      .then((response) => handleDownloadResponse(response))
  }

  const makeSet = (pairs: { id: number, name: string }[]) => {
    const ids = pairs.map(el => el.id)

    return pairs.filter((uniq, index) => index === ids.indexOf(uniq.id))
  }

  useEffect(() => {
    fetchData()
  }, [])

  const fetchData = () => {
    admin.getStatistics().then(res => {
      updateMonitoring(res.data)
    })
  }

  const createTextElement = (text: string, index: number, contentClassName?: string) => (
    <div
      key={index}
      className={cls([contentClassName, style.gridElement])}
    >
      <p>{text}</p>
    </div>
  )

  const createStatisticsElement =
    (statisticsNode: { parameter: string, value: number }[], index: number, contentClassName?: string) => (
      <div
        key={index}
        className={cls([contentClassName, style.gridElement, style.statisticsGridElement])}
      >
        {statisticsNode.map(line =>
          <div key={line.parameter}>
            <span className={style.parameter}>{line.parameter + ': '}</span> {line.value}
          </div>,
        )}
      </div>
    )

  const lifeScenarios = makeSet(monitoring.map(e => e.lifeScenario))
  const keyTechnologies = makeSet(monitoring.map(e => e.keyTechnology))
  const totalSum = {
    teamsCount: 0,
    studentsCount: 0,
  }

  const getElements = () => (
    keyTechnologies.map((technology) => (
      lifeScenarios.map((scenario, columnIndex) => {
        const monitor = monitoring
          .filter(el => el.lifeScenario.id == scenario.id)
          .filter(el => el.keyTechnology.id == technology.id)

        if (monitor.length > 0) {
          totalSum.studentsCount += monitor[0].studentsCount
          totalSum.teamsCount += monitor[0].teamsCount
          return createStatisticsElement([
            {
              parameter: '??????????????????',
              value: monitor[0].studentsCount,
            },
            {
              parameter: '????????????',
              value: monitor[0].teamsCount,
            },
          ], columnIndex)
        }

        return createTextElement('', columnIndex)
      })
    ))
  )

  const getSumElements = (criterionSet: { id: number, name: string }[], isVertical: boolean, contentClassName?: string) => (
    criterionSet.map((criterion, index) => {
      let peopleSum = 0
      let commandSum = 0

      // TODO: Needs refactoring.
      monitoring.filter(el =>
        (isVertical && el.keyTechnology.id === criterion.id) ||
        (!isVertical && el.lifeScenario.id === criterion.id))
        .forEach(el => {
          peopleSum += el.studentsCount
          commandSum += el.teamsCount
        })

      return createStatisticsElement([
        {
          parameter: '??????????????????',
          value: peopleSum,
        },
        {
          parameter: '????????????',
          value: commandSum,
        },
      ], index, contentClassName)
    })
  )

  return (
    <>
      <AppCard header={'????????????????????'}>
        <div className={style.monitoringGridContainer}>
          <div className={style.gridElement}/>
          {/* ???????????????? ?????????????????? */}
          <div className={style.horizontalGridContainer}>
            {lifeScenarios.map(e => createTextElement(e.name, e.id))}
          </div>

          {createTextElement('??????????', 2, style.backgroundColorAccent)}

          {/* ???????????????? ???????????????????? */}
          <div className={style.verticalGridContainer}>
            {keyTechnologies.map(e => createTextElement(e.name, e.id))}
          </div>
          {/* ?????????????????? ???????????????? (??????????????????) */}
          <div
            className={style.elementsGridContainer}
            style={{
              gridTemplateColumns: `repeat(${lifeScenarios.length}, minmax(0, 1fr))`,
              gridTemplateRows: `repeat(${keyTechnologies.length}, minmax(0, 1fr))`,
            }}
          >
            {getElements()}
          </div>

          {/* ?????????? ???????????????????? */}
          <div className={style.verticalGridContainer}>
            {getSumElements(keyTechnologies, true, style.backgroundColorAccent)}
          </div>

          {createTextElement('??????????', 6, style.backgroundColorAccent)}

          {/* ?????????? ?????????????????? */}
          <div className={style.horizontalGridContainer}>
            {getSumElements(lifeScenarios, false, style.backgroundColorAccent)}
          </div>

          {/* ?????????? ???? ???????? ?????????????????? ?? ?????????????????????? */}
          {createStatisticsElement(
            [
              {
                parameter: '??????????????????',
                value: totalSum.studentsCount,
              },
              {
                parameter: '????????????',
                value: totalSum.teamsCount,
              },
            ],
            8, style.backgroundColorDanger)}
        </div>
      </AppCard>

      <div className={style.buttons}>
        <AppButton
          className={style.button}
          onClick={getEventData}
        >
          ?????????????????? ????????????
        </AppButton>
        <AppButton
          className={style.button}
          onClick={getEventResult}
        >
          ?????????????????? ????????????????????
        </AppButton>
      </div>
    </>
  )
}
