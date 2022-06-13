import React from 'react'
import { CircularProgress } from '@material-ui/core'
import cls from 'classnames'

import style from './style.scss'

interface Props {
  fullHeight?: boolean
}

export const AppLoadingSpinner: React.FC<Props> = ({
  fullHeight,
}) => (
  <div className={cls(style.loading, { [style.fullHeight]: fullHeight })}>
    <CircularProgress
      color='primary'
      style={{ color: 'var(--primary)' }}
      size={50}
    />
  </div>
)
