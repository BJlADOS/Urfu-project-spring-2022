import React from 'react'
import cls from 'classnames'

import style from './style.scss'

type Props = React.DetailedHTMLProps<React.InputHTMLAttributes<HTMLInputElement>, HTMLInputElement>

export const AppInput = React.forwardRef((props: Props, ref: any) => {
  const className = cls([style.appInput, props.className])

  return (
    <input
      {...props}
      ref={ref}
      className={className}
    />
  )
})
