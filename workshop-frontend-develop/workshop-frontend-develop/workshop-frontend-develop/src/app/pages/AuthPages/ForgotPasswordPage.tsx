import React, { useEffect, useState } from 'react'
import { Link, useHistory } from 'react-router-dom'
import * as Yup from 'yup'
import { UserPasswordResetModel } from 'app/models'
import { user } from 'app/provider'
import { AppCard } from 'app/components/AppCard'
import { AppForm, AppFormTextField, AppFormSelectField } from 'app/components/AppForm'
import { AppButton } from 'app/components/AppButton'
import { EventModel } from 'app/models/EventModel'
import { AppDialog } from 'app/components/AppDialog'
import { login } from 'app/nav'

import style from './style.scss'

export const ForgotPasswordPage: React.FC = () => {
  const history = useHistory()

  const [eventsList, setEventsList] = useState<EventModel[]>([])
  const [isDialogOpen, setIsDialogOpen] = useState(false)

  const [initValues] = useState<UserPasswordResetModel>({
    eventId: null as any,
    email: '',
  })

  useEffect(() => {
    user.getEvents().then(res => {
      setEventsList(res.data)
    })
  }, [])

  const handlePasswordReset = ({ eventId, email }: UserPasswordResetModel) => {
    console.log(eventId, email)
    // Old implementation was replaced with UrFU auth.
    // user.forgotPassword(email, eventId).then(
    //   () => setIsDialogOpen(true),
    // )
  }

  const loginSchema = Yup.object().shape<typeof initValues>({
    eventId: Yup.number()
      .typeError('Введите значение')
      .required(),
    email: Yup.string()
      .required('Введите значение'),
  })

  return (
    <>
      <div className={style.container}>
        <div className={style.logo} />
        <AppCard
          className={style.card}
          contentClassName={style.cardContent}
        >
          <h2>Восстановление пароля</h2>
          <AppForm
            onSubmit={handlePasswordReset}
            initValues={initValues}
            className={style.form}
            validationSchema={loginSchema}
          >
            <AppFormSelectField
              name='eventId'
              items={eventsList}
              label='Выберите событие'
              className={style.input}
              dataConverter={(event => ({
                key: event.id,
                label: event.name,
                value: event.id,
              }))}
            />
            <AppFormTextField
              name='email'
              label='Email или логин'
              className={style.input}
            />
            <div className={style.formButtons}>
              <AppButton
                type='submit'
                className={style.authButton}
              >
                Отправить письмо
              </AppButton>
              <Link to={login()}>Назад</Link>
            </div>
          </AppForm>
        </AppCard>
      </div>
      <AppDialog
        open={isDialogOpen}
        onClose={() => {
          setIsDialogOpen(false)
          history.push(login())
        }}
      >
        <AppCard className={style.dialogInnerCard}>
          <p>
            Мы отправили письмо с инструкцией по восстановлению пароля на указанный адрес электронной почты.
          </p>
        </AppCard>
      </AppDialog>
    </>
  )
}
