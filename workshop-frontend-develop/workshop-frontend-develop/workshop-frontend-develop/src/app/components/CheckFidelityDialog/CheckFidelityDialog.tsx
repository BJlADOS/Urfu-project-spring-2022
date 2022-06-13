import React from 'react'
import { AppCard } from 'app/components/AppCard'
import { AppDialog } from 'app/components/AppDialog'
import { DialogProps } from '@material-ui/core'
import { AppButton, AppButtonType } from 'app/components/AppButton'

import style from './style.scss'

interface DeleteTeamDialog {
  onMakingAction: () => void
  header : any
  additionContent?:any
  buttonType:AppButtonType
  buttonText:string
}

interface Props extends DeleteTeamDialog, DialogProps {}

export const CheckFidelityDialog: React.FC<Props> = ({
  open,
  className,
  onMakingAction,
  header,
  additionContent,
  buttonText,
  buttonType,
  ...props
}) =>

  (
    <AppDialog
      open={open}
      {...props}
    >
      <div className={style.dialogContent}>
        <AppCard className={style.dialogInnerCard}>
          <h3>{header}</h3>
        </AppCard>
        {additionContent || <></>}

        <div className={style.checkButton}>
          <AppButton
            type='button'
            buttonType={buttonType}
            onClick={onMakingAction}
          >
            {buttonText}
          </AppButton>
        </div>
      </div>
    </AppDialog>
  )
