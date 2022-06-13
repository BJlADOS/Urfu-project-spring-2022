import React from 'react'
import cls from 'classnames'

import style from './style.scss'

type Props = React.DetailedHTMLProps<React.TextareaHTMLAttributes<HTMLTextAreaElement>, HTMLTextAreaElement>

export const AppTextarea: React.FC<Props> = (props) => {
  const className = cls([style.appInput, props.className])

  return (
    <textarea
      {...props}
      className={className}
    >
    </textarea>
  )
}
