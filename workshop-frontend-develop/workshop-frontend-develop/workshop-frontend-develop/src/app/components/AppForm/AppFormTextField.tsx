import { ErrorMessage, Field } from 'formik'
import cls from 'classnames'
import React from 'react'

import { AppInput } from '../AppInput'

import style from './style.scss'

interface Props {
  name: string
  label?: string
  type?: string
  className?: string
  errorClassName?: string
}

export const AppFormTextField: React.FC<Props> = ({
  name,
  type,
  label,
  className,
  errorClassName,
}) => {
  const fieldStyles = cls([style.formField, className])
  const fieldErrorStyles = cls([style.error, errorClassName])

  return (
    <>
      <Field
        name={name}
        type={type}
        as={AppInput}
        className={fieldStyles}
        placeholder={label}
      />
      <ErrorMessage
        name={name}
        component='div'
        className={fieldErrorStyles}
      />
    </>
  )
}
