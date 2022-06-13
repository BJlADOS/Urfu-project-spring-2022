import React from 'react'
import { NavLink } from 'react-router-dom'

import style from './style.scss'

interface Props {
  to: string
  icon?: React.ReactNode
  exact?: boolean
}

export const NavigationButton: React.FC<Props> = ({
  to,
  children,
  icon,
  exact,
}) => (
  <NavLink
    to={to}
    exact={exact}
    className={style.navlink}
    activeClassName={style.active}
  >
    <span className={style.icon}>
      {icon}
    </span>
    <span className={style.buttonText}>
      {children}
    </span>
  </NavLink>
)

NavigationButton.defaultProps = {
  exact: true,
}
