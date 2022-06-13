import React, { useRef } from 'react'
import Select from 'react-select'

import { AppInput } from '../AppInput'

import style from './style.scss'

interface Props<T, TOut> {
  onChange?: (value?: TOut) => void
  value?: T | null
  items: T[]
  placeholder?: string
  readOnly?: boolean
  className?: string
  hideSelected?: boolean
  dataConverter: (value: T) => DropdownOptionItem<TOut>
  onInputBlur?: (event: React.FocusEvent<HTMLInputElement>) => void
}

export const AppDropdown = <T extends unknown, TOut = T>({
  value,
  items,
  onChange,
  onInputBlur,
  dataConverter,
  className,
  placeholder,
  hideSelected,
  readOnly,
}: Props<T, TOut>) => {
  const ref = useRef<any>(null)

  const Control = (props: any) => {
    const onBlur = (event: React.FocusEvent<HTMLInputElement>) => {
      props.selectProps.onMenuClose()
      onInputBlur && onInputBlur(event)
    }

    return (
      <div className={readOnly && style.controlWrapper}>
        <AppInput
          value={props.menuIsOpen ? props.selectProps.inputValue : props.selectProps.value?.label || ''}
          placeholder={props.selectProps.placeholder}
          onClick={props.selectProps.onMenuOpen}
          onBlur={onBlur}
          ref={ref}
          onChange={(ev) => {
            props.selectProps.onMenuOpen()
            props.selectProps.onInputChange(ev.target.value)
          }}
          readOnly={readOnly}
          className={readOnly && style.readonlyDropdown}
        />
      </div>
    )
  }

  const components = {
    Control,
  }

  const onChangeWrapper = (item?: DropdownOptionItem<TOut>) => {
    if (!item) {
      return
    }
    ref.current && ref.current.blur()
    onChange && onChange(item.value)
  }

  const optionsList: DropdownOptionItem<T | TOut>[] = items.map(item => {
    const convertedItem = dataConverter(item)

    return {
      key: convertedItem.key,
      label: convertedItem.label,
      value: convertedItem.value || item,
    }
  })

  const selectedValue = value && dataConverter(value)

  const getSelectedValue = (): any => {
    /*
      Some tricky logic here.
      We should pass 'null' as value if we want to reset dropdown value.
    */
    if (hideSelected || value === null) {
      return null
    }
    return optionsList.find(option => {
      if (selectedValue?.key) {
        return option.key === selectedValue?.key
      }
      return option.value === value
    })
  }

  return (
    <Select
      options={optionsList}
      components={components}
      value={getSelectedValue()}
      onChange={(item) => onChangeWrapper(item as any)}
      className={className}
      noOptionsMessage={() => 'Нет доступных вариантов'}
      placeholder={placeholder || 'Выберите значение...'}
      openMenuOnFocus={true}
      styles={{
        option: (provided, state) => ({
          ...provided,
          color: state.isSelected ? 'var(--primary-light)' : 'var(--text-main)',
          backgroundColor: state.isFocused ? 'var(--bg-primary)' : 'var(--bg-secondary)',
          cursor: 'pointer',
          padding: '0.5rem',
          ':hover': {
            backgroundColor: state.isFocused ? 'var(--bg-primary)' : 'var(--bg-secondary)',
          },
          borderBottom: '1px solid var(--bg-main)',
          ':last-of-type': {
            borderBottom: 'none',
          },
          overflowX: 'hidden',
        }),
        menu: (provided) => ({
          ...provided,
          backgroundColor: 'var(--bg-secondary)',
          marginTop: 2,
          padding: 0,
          overflow: 'hidden',
          borderRadius: 'var(--border-radius)',
          border: 'var(--input-border)',
          fontFamily: 'var(--content-font-family)',
          fontSize: '1rem',
        }),
        menuList: (provided) => ({
          ...provided,
          padding: 0,
        }),
      }}
    />
  )
}

export interface DropdownOptionItem<T> {
  label: string
  key: number | string
  value?: T
}
