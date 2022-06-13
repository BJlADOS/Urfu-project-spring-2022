import React, { useState } from 'react'
import cls from 'classnames'

import style from './style.scss'

interface Props {
  tabs: TabInfo[]
  activeComponentIndex?: number
}

interface TabInfo {
  name: string,
  component: any
}

export const LineTabs : React.FC<Props> = (props) => {
  const [activeTab, setActiveTab] = useState(props.activeComponentIndex || 0)

  return (
    <>
      <div className={style.lineTabs__tabs}>
        {props.tabs.map((tab, index) => (
          <Tab
            isActive={index === activeTab}
            key={index}
            index={index}
            onTab={(index: number) => setActiveTab(index)}
          >{tab.name}</Tab>),
        )}
      </div>
      <div className={style.lineTabs__content}>
        {props.tabs[activeTab].component}
      </div>
    </>
  )
}

interface TabProps {
  isActive: boolean
  onTab: (index: number) => void
  index: number
}

const Tab : React.FC<TabProps> = (props) => {
  const classNames = props.isActive ? cls([style.lineTabs__tab, style.lineTabs__tab_active]) : cls([style.lineTabs__tab])

  return (
    <div
      className={classNames}
      onClick={() => props.onTab(props.index)}
    >
      {props.children}
    </div>
  )
}
