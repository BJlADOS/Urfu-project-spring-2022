import { ErrorMessage, Field, FieldProps } from 'formik'
import cls from 'classnames'
import React from 'react'

import { AppDropdown, DropdownOptionItem } from '../AppDropdown'

import style from './style.scss'

interface Props<T, TOut> {
  name: string
  label?: string
  type?: string
  className?: string
  errorClassName?: string
  items: T[]
  dataConverter: (value: T) => DropdownOptionItem<TOut>
}

export const AppFormSelectField = <T extends Record<keyof T, any>, TOut extends unknown>({
  name,
  type,
  label,
  className,
  errorClassName,
  items,
  dataConverter,
}: Props<T, TOut>) => {
  const fieldStyles = cls([style.formField, className])
  const fieldErrorStyles = cls([style.error, errorClassName])

  const FormSelect = ({
    field: {
      name,
      value,
    },
    form: {
      setFieldValue,
      setFieldTouched,
    },
    ...props
  }: FieldProps) => (
    <AppDropdown
      {...props}
      value={value}
      items={items}
      dataConverter={dataConverter}
      onChange={(item) => setFieldValue(name, item)}
      onInputBlur={() => setFieldTouched(name)}
      readOnly
    />
  )

  return (
    <>
      <Field
        name={name}
        type={type}
        component={FormSelect}
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
