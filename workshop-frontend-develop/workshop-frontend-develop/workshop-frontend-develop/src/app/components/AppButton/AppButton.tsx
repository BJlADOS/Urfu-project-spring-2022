import React from 'react'
import cls from 'classnames'
import { Link } from 'react-router-dom'

import style from './style.scss'

interface AddProps {
  icon?: React.ReactNode
  buttonType?: AppButtonType
  /**
   * We can pass link in this propery to make link button.
   */
  to?: string
}

interface Props extends AddProps, React.DetailedHTMLProps<React.ButtonHTMLAttributes<HTMLButtonElement>, HTMLButtonElement> { }

const buttonTypesMap = {
  primary: style.primary,
  secondary: style.secondary,
  primaryAccent: style.primaryAccent,
  secondaryAccent: style.secondaryAccent,
  danger: style.danger,
  transparent: style.transparent,
}

export type AppButtonType = keyof typeof buttonTypesMap;

export const AppButton: React.FC<Props> = ({
  icon,
  type,
  buttonType,
  children,
  className,
  to,
  ...props
}) => {
  const buttonClassName = cls([buttonTypesMap[buttonType ?? 'primary'], style.appButton, className])

  const content = children && (
    <span className={style.content}>{children}</span>
  )

  const Wrapper: React.FC = ({ children }) => to ? (
    <Link
      className={buttonClassName}
      to={to}
    >
      {children}
    </Link>
  ) : (
    <button
      {...props}
      type={type || 'button'}
      className={buttonClassName}
    >
      {children}
    </button>
  )

  return (
    <Wrapper>
      {icon}
      {content}
    </Wrapper>
  )
}
