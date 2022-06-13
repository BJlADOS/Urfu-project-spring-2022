import React, { useEffect, useState } from 'react'
import cls from 'classnames'
import { Link, Route, Switch } from 'react-router-dom'

import style from './style.scss'

interface Props {
  tabs: TabInfo[]
  activeTab: string
  activeComponentIndex?: number
  className?: string
}

interface TabInfo {
  name: string,
  icon: any,
  component: any,
  url: string
}

export const CardTabs : React.FC<Props> = (props) => {
  const [activeTab, setActiveTab] = useState(props.activeComponentIndex || 0)

  useEffect(() => {
    props.tabs.forEach((tab, index) => {
      const { url } = tab

      if (url.endsWith(props.activeTab)) {
        setActiveTab(index)
      }
    })
  }, [])

  return (
    <div className={props.className}>
      <div className={style.cardTabs__tabs}>
        {props.tabs.map((tab, index) => (
          <Tab
            isActive={index === activeTab}
            key={index}
            index={index}
            onTab={(index: number) => setActiveTab(index)}
            icon={tab.icon}
            url={tab.url}
          >{tab.name}</Tab>
        ))}
      </div>
      <div className={style.cardTabs__content}>
        <Switch>
          {props.tabs.map((tab, index) => (
            <Route
              path={`${tab.url}`}
              key={index}
              exact
            >
              {tab.component}
            </Route>
          ))}
        </Switch>
      </div>
    </div>
  )
}

interface TabProps {
  isActive: boolean
  onTab: (index: number) => void
  index: number
  icon: any
  url: string
}

const Tab : React.FC<TabProps> = (props) => {
  const classNames = props.isActive ? cls([style.cardTabs__tab, style.cardTabs__tab_active]) : cls([style.cardTabs__tab])

  return (
    <Link
      to={props.url}
      className={style.cardTabs__link}
    >
      <div
        className={classNames}
        onClick={() => props.onTab(props.index)}
      >
        <h3><span className={style.cardTabs__icon}>{props.icon}</span> {props.children}</h3>
      </div>
    </Link>
  )
}
