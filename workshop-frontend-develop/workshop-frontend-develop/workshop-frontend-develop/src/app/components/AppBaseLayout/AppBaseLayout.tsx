import React from 'react'

import { AppHeader } from '../AppHeader'
import { AppNavigationBar, NavigationItem } from '../AppNavigationBar'
import scrollStyle from '../../pages/UserSearchPage/style.scss'

import style from './style.scss'

interface Props {
  navItems?: NavigationItem[]
}

export const AppBaseLayout: React.FC<Props> = ({ navItems, children }) => {
  const navigationBar = navItems && navItems.length > 0 && (
    <AppNavigationBar items={navItems} />
  )

  return (
    <section className={style.section}>
      <AppHeader />
      <div className={style.main}>
        {navigationBar}
        <div
          id ={scrollStyle.scrollContent}
          className={style.content}
        >
          {children}
        </div>
      </div>
    </section>
  )
}
