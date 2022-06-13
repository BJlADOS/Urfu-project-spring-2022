import React from 'react'
import classNames from 'classnames'
import keyInfoData from 'assets/data/key-info.json'

import styles from './styles.scss'

interface KeyInfo {
  period: string
  studentsCount: number
  projectsCount: number
  customersCount: number
}

interface SectionProps {
  selectedEventId: number,
  onSelectedEventIdChange: (id: number) => void
}

export const KeyInfoSection: React.FC<SectionProps> = ({
  selectedEventId,
  onSelectedEventIdChange,
}) => (
  <>
    <h2>Ключевые показатели</h2>
    <div className={styles.flexContainer}>
      {keyInfoData.map((item: KeyInfo, index: number) => {
        const classes = classNames({
          [styles.selected]: selectedEventId === index,
          [styles.selectableBlock]: true,
        })

        return (
          <div
            className={classes}
            key={item.period}
            onClick={() => onSelectedEventIdChange(index)}
          >
            <KeyInfoItem data={item}/>
          </div>
        )
      })}
    </div>
  </>
)

interface ItemProps {
  data: KeyInfo
}

const KeyInfoItem: React.FC<ItemProps> = ({
  data: {
    period,
    studentsCount,
    projectsCount,
    customersCount,
  },
}) => {
  const endingHelper = (count: number): string => {
    const lastDigit = count % 10

    return lastDigit >= 1 && lastDigit <= 4 ? 'а' : 'ов'
  }

  return (
    <div className={styles.keyInfoItem}>
      <h3>{period}</h3>
      <div className={styles.numbersBlock}>
        <span>{studentsCount}</span> Cтудентов
      </div>
      <div className={styles.numbersBlock}>
        <span>{projectsCount}</span> Проектов
      </div>
      <div className={styles.numbersBlock}>
        <span>{customersCount}</span> Заказчик{endingHelper(customersCount)}
      </div>
    </div>
  )
}
