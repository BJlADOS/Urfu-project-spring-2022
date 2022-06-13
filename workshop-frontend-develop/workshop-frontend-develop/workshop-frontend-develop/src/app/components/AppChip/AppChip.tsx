import React from 'react'
import cls from 'classnames'
import { HighlightOffRounded } from '@material-ui/icons'

import style from './style.scss'

interface Props {
  className?: string
  onDelete?: () => void
}

export const AppChip: React.FC<Props> = ({
  children,
  className,
  onDelete,
}) => {
  const deleteButton = onDelete && (
    <HighlightOffRounded
      className={style.chipButton}
      onClick={() => onDelete()}
    />
  )

  const chipClassName = cls([className, style.appChip])

  return (
    <span className={chipClassName}>
      {children}
      {deleteButton}
    </span>
  )
}
