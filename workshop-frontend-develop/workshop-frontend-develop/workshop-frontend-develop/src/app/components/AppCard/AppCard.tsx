import React from 'react'
import cls from 'classnames'

import style from './style.scss'

interface AppCardProps {
  header?: React.ReactNode
  contentClassName?: string
}

interface Props extends AppCardProps, React.DetailedHTMLProps<React.HTMLAttributes<HTMLDivElement>, HTMLDivElement> { }

export const AppCard: React.FC<Props> = ({
  header,
  children,
  className,
  contentClassName,
  ...props
}) => {
  const appCardClassName = cls([className, style.appCard])
  const cardHeader = header && (
    <div className={style.header}>{header}</div>
  )

  const contentStyles = cls([contentClassName, style.content])

  return (
    <div
      {...props}
      className={appCardClassName}
    >
      {cardHeader}
      <div className={contentStyles}>
        {children}
      </div>
    </div>
  )
}
