import React from 'react'
import useDarkMode from 'use-dark-mode'
import { Brightness4Rounded, Brightness7Rounded } from '@material-ui/icons'
import style from 'app/style.scss'

import { AppButton } from '../AppButton'

interface Props {
  className?: string
}

export const AppThemeToggle: React.FC<Props> = ({
  className,
}) => {
  const darkMode = useDarkMode(false, {
    classNameDark: style.dark,
    element: document.documentElement,
  })

  const icon = darkMode.value ? <Brightness7Rounded /> : <Brightness4Rounded />

  return (
    <AppButton
      type='button'
      buttonType='transparent'
      icon={icon}
      className={className}
      onClick={() => darkMode.toggle()}
    />
  )
}
