import React from 'react'
import { Dialog, DialogProps, makeStyles } from '@material-ui/core'

interface AppDialogProps {
  allowOverflow?: boolean
  width?: string
}

interface Props extends AppDialogProps, DialogProps { }

export const AppDialog: React.FC<Props> = ({
  allowOverflow,
  children,
  width,
  ...props
}) => {
  const useStyles = makeStyles({
    paper: {
      padding: '1rem',
      color: 'var(--text-main)',
      backgroundColor: 'var(--bg-main)',
      borderRadius: 'var(--border-radius)',
      width: '100%',
      maxWidth: width || '600px',
      boxShadow: 'none',
      overflowY: allowOverflow ? 'unset' : 'auto',
    },
  })

  const classes = useStyles()

  return (
    <Dialog
      {...props}
      PaperProps={
        {
          className: classes.paper,
        }
      }
    >
      {children}
    </Dialog>
  )
}
