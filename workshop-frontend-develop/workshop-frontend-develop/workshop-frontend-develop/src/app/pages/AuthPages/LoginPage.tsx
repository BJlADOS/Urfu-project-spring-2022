import React, { useEffect, useState } from 'react'
import { connect } from 'react-redux'
import { useHistory } from 'react-router-dom'
import * as Yup from 'yup'
import { ProfileActions } from 'app/actions'
import { login } from 'app/auth'
import { UserRegistrationModel, UserModel } from 'app/models'
import { user } from 'app/provider'
import { AppCard } from 'app/components/AppCard'
import { AppForm, AppFormTextField, AppFormSelectField } from 'app/components/AppForm'
import { AppButton } from 'app/components/AppButton'
import { EventModel } from 'app/models/EventModel'
import { changePassword, registration, registrationUrfu } from 'app/nav'
import { AppDialog } from 'app/components/AppDialog'

import style from './style.scss'

const mapDispatchToProps = { setUserProfile: (user: UserModel) => ProfileActions.setUserProfile(user) }

type DispatchProps = typeof mapDispatchToProps

type Props = DispatchProps

const LoginPageComponent: React.FC<Props> = ({
  setUserProfile,
}) => {
  const history = useHistory()

  const [eventsList, setEventsList] = useState<EventModel[]>([])
  const [isDialogOpen, setIsDialogOpen] = useState(false)

  const [initValues, setInitValues] = useState<UserRegistrationModel>({
    eventId: null as any,
    login: '',
    password: '',
  })

  useEffect(() => {
    user.getEvents().then(res => {
      setEventsList(res.data)
    })
  }, [])

  const handleLogin = ({ login: email, password, eventId }: UserRegistrationModel) => {
    login(email, password, eventId)
      .then(user.getUserProfile)
      .then((res) => setUserProfile(res.data))
      .catch(() => {
        setIsDialogOpen(true)
        setInitValues({ login: email, password, eventId })
      })
  }

  const loginSchema = Yup.object().shape<typeof initValues>({
    eventId: Yup.number()
      .typeError('Введите значение')
      .required(),
    login: Yup.string()
      .required('Введите значение'),
    password: Yup.string()
      .required('Введите значение'),
  })

  const changePassButton = process.env.LOGIN_URFU == 'true' && (<AppButton
    type='button'
    buttonType='secondaryAccent'
    className={style.updatePassButton}
    onClick={() => history.push(changePassword())}
  >
    Обновление пароля
  </AppButton>)

  return (
    <>
      <div className={style.container}>
        <div className={style.logo} />
        <AppCard
          className={style.card}
          contentClassName={style.cardContent}
        >
          <h2>Войдите в аккаунт</h2>
          <AppForm
            onSubmit={handleLogin}
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
              name='login'
              label='Логин'
              className={style.input}
            />
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
                Войти
              </AppButton>
              <AppButton
                type='button'
                buttonType='secondary'
                className={style.authButton}
                onClick={() => process.env.LOGIN_URFU == 'true' ? history.push(registrationUrfu()) : history.push(registration())}
              >
                Регистрация
              </AppButton>
              {changePassButton}
            </div>
          </AppForm>
        </AppCard>
      </div>
      <AppDialog
        open={isDialogOpen}
        onClose={() => setIsDialogOpen(false)}
      >
        <AppCard className={style.dialogInnerCard}>
          <h2>Ошибка!</h2>
        </AppCard>
        <AppCard className={style.dialogInnerCard}>
          <p>
            Вы ввели неверные данные от учетной записи.
          </p>
          <p>
            Пожалуйста, попробуйте ещё раз.
          </p>
        </AppCard>
      </AppDialog>
    </>
  )
}

export const LoginPage: React.FC = connect(
  undefined,
  mapDispatchToProps,
)(LoginPageComponent)
