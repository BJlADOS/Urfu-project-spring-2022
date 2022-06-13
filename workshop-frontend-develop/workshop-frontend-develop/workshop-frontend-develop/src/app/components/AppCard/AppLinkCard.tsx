import React from 'react'
import cls from 'classnames'
import { Link } from 'react-router-dom'

import style from './style.scss'

interface AppCardProps {
  header?: React.ReactNode
  contentClassName?: string
  to: string
}

interface Props extends AppCardProps, React.DetailedHTMLProps<React.HTMLAttributes<HTMLDivElement>, HTMLDivElement> { }

export const AppLinkCard: React.FC<Props> = ({
  header,
  children,
  className,
  contentClassName,
  to,
}) => {
  const appCardClassName = cls([className, style.appCard])
  const cardHeader = header && (
    <div className={style.header}>{header}</div>
  )

  const contentStyles = cls([contentClassName, style.content])

  return (
    <Link
      to={to}
      className={appCardClassName}
    >
      {cardHeader}
      <div className={contentStyles}>
        {children}
      </div>
    </Link>
  )
}
