import React from 'react'
import cls from 'classnames'

import style from './style.scss'

type Props = React.DetailedHTMLProps<React.HTMLAttributes<HTMLHRElement>, HTMLHRElement>

export const AppDivider: React.FC<Props> = (props) => {
  const className = cls([style.divider, props.className])

  return (
    <hr
      {...props}
      className={className}
    />
  )
}
