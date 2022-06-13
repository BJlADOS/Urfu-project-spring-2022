import React from 'react'
import { DebounceInput } from 'react-debounce-input'
import cls from 'classnames'

import style from './style.scss'

interface AppSearchProps {
  onChangeSearch: (value: string) => void
  value?: string | number
}

const DEBOUNCE_TIME_MS = 1000

interface Props extends AppSearchProps, Omit<React.DetailedHTMLProps<React.InputHTMLAttributes<HTMLInputElement>, HTMLInputElement>, 'value'> { }

export const AppSearch: React.FC<Props> = ({
  value,
  className,
  placeholder,
  onChangeSearch,
}) => {
  const searchClassName = cls([style.appInput, className])

  return (
    <DebounceInput
      type='text'
      className={searchClassName}
      debounceTimeout={DEBOUNCE_TIME_MS}
      placeholder={placeholder || 'Поиск'}
      value={value}
      onChange={(e) => onChangeSearch(e.target.value)}
    />
  )
}
