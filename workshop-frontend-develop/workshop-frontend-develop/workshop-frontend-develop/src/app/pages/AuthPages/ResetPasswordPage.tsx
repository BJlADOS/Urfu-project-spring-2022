import React, { useState } from 'react'
import { useHistory, useLocation } from 'react-router-dom'
import * as Yup from 'yup'
import { AppCard } from 'app/components/AppCard'
import { AppForm, AppFormTextField } from 'app/components/AppForm'
import { AppButton } from 'app/components/AppButton'
import { AppDialog } from 'app/components/AppDialog'
import { login } from 'app/nav'
import { validatePassword } from 'app/utils/validatePassword'

import style from './style.scss'

export const ResetPasswordPage: React.FC = () => {
  const history = useHistory()

  const location = useLocation()
  const params = new URLSearchParams(location.search)

  const [isDialogOpen, setIsDialogOpen] = useState(false)

  const [initValues] = useState({
    password: '',
  })

  const handlePasswordReset = ({ password }: typeof initValues) => {
    const eventId = params.get('eventId')
    const userId = params.get('userId')
    const token = params.get('token')

    if (eventId && userId && token) {
      console.log(password)
      // Old Implementation was replaced with UrFU auth.
      // user.resetPassword(password, {
      //   eventId,
      //   userId,
      //   token,
      // }).then(() => setIsDialogOpen(true))
    }
  }

  const loginSchema = Yup.object().shape<typeof initValues>({
    password: Yup.string().required('Введите значение')
      .min(6, 'Пароль должен быть не менее 6 символов')
      .test(
        'password-validation',
        'Обязательно наличие цифр, строчных и заглавных символов',
        (value) => validatePassword(value),
      ),
  })

  return (
    <>
      <div className={style.container}>
        <div className={style.logo} />
        <AppCard
          className={style.card}
          contentClassName={style.cardContent}
        >
          <h2>Установить новый пароль</h2>
          <AppForm
            onSubmit={handlePasswordReset}
            initValues={initValues}
            className={style.form}
            validationSchema={loginSchema}
          >
            <AppFormTextField
              name='password'
              label='Пароль'
              type='password'
              className={style.input}
            />
            <div className={style.formButtons}>
              <AppButton
                type='submit'
                className={style.authButton}
              >
                Сохранить
              </AppButton>
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
            Новый пароль сохранён
          </p>
        </AppCard>
      </AppDialog>
    </>
  )
}
