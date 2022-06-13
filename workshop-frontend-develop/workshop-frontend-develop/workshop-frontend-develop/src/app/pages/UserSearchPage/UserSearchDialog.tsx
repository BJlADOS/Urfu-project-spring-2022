import React, { useState } from 'react'
import { AppDialog } from 'app/components/AppDialog'
import { DialogProps } from '@material-ui/core'
import { AppCard } from 'app/components/AppCard'
import { AppDropdown, DropdownOptionItem } from 'app/components/AppDropdown'
import { RoleModel } from 'app/models'
import { AppButton } from 'app/components/AppButton'

import styleDialog from './styleDialog.scss'

interface Props extends DialogProps {
  open: boolean
  createRequest: (str: string) => void
  onClose: any
  pressAdd: (value: boolean) => void
  rolesList: RoleModel[]
}

export const UserSearchDialog: React.FC<Props> = ({
  open,
  createRequest,
  onClose,
  pressAdd,
  rolesList,
  ...props
}) => {
  const [click, setClick] = useState(false)
  const [role, setRole] = useState<string>('')
  const [checked, setChecked] = useState(false)

  const dataConverter = <T extends { id: number, name: string }>(value: T): DropdownOptionItem<T> => ({
    key: value.id,
    label: value.name,
  })

  const checkBox = (
    <div className={styleDialog.additional}>
      <label>
        <input
          type='checkbox'
          checked={checked}
          onChange={() => {
            setChecked(!checked)
          }
          }
        />
        <span>Не назначать роль</span>
      </label>
    </div>
  )

  return (

    <AppDialog
      className={styleDialog.card}
      open={open}
      onClose={() => {
        setRole('')
        onClose()
      }}
      {...props}
    >
      <div className={styleDialog.dialog}>
        <AppCard
          header={'Роль пользователя'}
          className={styleDialog.dialogInnerCard}
        >

          <div className={styleDialog.mainContent}>
            <AppDropdown
              dataConverter={dataConverter}
              items={rolesList.filter(x => x.name !== 'Тимлид') || []}
              onChange={(value) => {
                setRole(value!.name)
              }}
              readOnly
              placeholder={'Выберите роль пользователю'}
            />
            {checkBox}
          </div>
          <div className={styleDialog.buttonAccept}>
            <AppButton
              disabled={(checked && !!role) || (!checked && !role) || (click)}
              onClick={() => {
                setClick(true)
                pressAdd(true)
                createRequest(role || 'Не назначено')
              }}
            >
              Пригласить пользователя
            </AppButton>
          </div>
        </AppCard>
      </div>
    </AppDialog>

  )
}
