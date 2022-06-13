import React from 'react'

import { NavigationButton } from './NavigationButton'
import './style.scss'

interface Props {
  items: NavigationItem[]
}

export interface NavigationItem {
  to: string
  text: string
  icon?: React.ReactNode
  exact?: boolean
}

export const AppNavigationBar: React.FC<Props> = ({
  items,
  children,
}) => {
  const navigationButtons = items.map((item, index) => (
    <NavigationButton
      key={index}
      to={item.to}
      icon={item.icon}
      exact={item.exact}
    >
      {item.text}
    </NavigationButton>
  ))

  return (
    <aside>
      <nav>
        {navigationButtons}
        {children}
      </nav>
    </aside>
  )
}
