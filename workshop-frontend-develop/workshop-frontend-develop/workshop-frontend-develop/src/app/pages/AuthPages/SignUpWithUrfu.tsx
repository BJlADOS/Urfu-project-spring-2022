import React, {useEffect, useState} from 'react'
import {connect} from 'react-redux'
import {useHistory} from 'react-router-dom'
import * as Yup from 'yup'
import {ProfileActions} from 'app/actions'
import {login} from 'app/auth'
import {UserRegistrationModel, UserModel} from 'app/models'
import {user} from 'app/provider'
import {AppCard} from 'app/components/AppCard'
import {AppForm, AppFormTextField, AppFormSelectField} from 'app/components/AppForm'
import {AppButton} from 'app/components/AppButton'
import {EventModel} from 'app/models/EventModel'
import {login as loginRoute, registration} from 'app/nav'
import {AppDialog} from 'app/components/AppDialog'
import {validateEmail} from 'app/utils/validateEmail'
import {AppDivider} from 'app/components/AppDivider'

import style from './style.scss'

type RegistrationData = UserRegistrationModel

const mapDispatchToProps = {setUserProfile: (user: UserModel) => ProfileActions.setUserProfile(user)}

type DispatchProps = typeof mapDispatchToProps

type Props = DispatchProps

const SignUpUrfuComponent: React.FC<Props> = ({
                                                  setUserProfile,
                                              }) => {
    const history = useHistory()

    const [eventsList, setEventsList] = useState<EventModel[]>([])
    const [isDialogOpen, setIsDialogOpen] = useState(false)

    const [initValues, setInitValues] = useState<RegistrationData>({
        eventId: null as any,
        login: '',
        password: '',
    })

    useEffect(() => {
        user.getEvents()
            .then(res => setEventsList(res.data))
    }, [])

    const handleSignUp = (userData: RegistrationData) => {
        user.registrationWithUrfu(userData)
            .then(() => login(userData.login, userData.password, userData.eventId))
            .then(user.getUserProfile)
            .then(res => setUserProfile(res.data))
            .catch(() => {
                setIsDialogOpen(true)
                setInitValues(userData)
            })
    }

    const signUpSchema = Yup.object().shape<typeof initValues>({
        eventId: Yup.number()
            .typeError('Введите значение')
            .required(),
        login: Yup.string()
            .required('Введите почту от аккаунта УрФУ')
            .test('email-validation',
                'Вы ввели не валидную почту',
                test => validateEmail(test)),
        password: Yup.string()
            .required('Введите пароль'),
    })

    return (
        <>
            <div className={style.container}>
                <div className={style.logo}/>
                <AppCard
                    className={style.card}
                    contentClassName={style.cardContent}
                >
                    <h2>Регистрация через ЛК УрФУ</h2>
                    <AppForm
                        onSubmit={handleSignUp}
                        initValues={initValues}
                        className={style.form}
                        validationSchema={signUpSchema}
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
                            label='Почта от ЛК УрФУ'
                            className={style.input}
                        />
                        <AppFormTextField
                            name='password'
                            label='Пароль от ЛК УрФУ'
                            type='password'
                            className={style.input}
                        />
                        <div className={style.formButtons}>
                            <AppButton
                                type='submit'
                                className={style.authButton}
                            >
                                Зарегистрироваться
                            </AppButton>
                            <span>Уже зарегистрированы на сервисе?</span>
                            <a onClick={() => history.push(loginRoute())}>Войти</a>
                            <AppDivider/>
                            <span>Нет аккаунта УрФУ?</span>
                            <a onClick={() => history.push(registration())}>Используйте стандартную регистрацию</a>
                        </div>
                    </AppForm>
                </AppCard>
                <AppDialog
                    open={isDialogOpen}
                    onClose={() => setIsDialogOpen(false)}
                >
                    <AppCard className={style.dialogInnerCard}>
                        <h2>Ошибка!</h2>
                    </AppCard>
                    <AppCard className={style.dialogInnerCard}>
                        <p>
                            Произошла ошибка при регистрации нового пользователя!
                        </p>
                        <p>
                            Возможно, вы ввели неправильный логин или пароль от личного кабинета урфу
                        </p>
                    </AppCard>
                </AppDialog>
            </div>
        </>
    )
}

export const SignUpPageUrfu: React.FC = connect(
    undefined,
    mapDispatchToProps,
)(SignUpUrfuComponent)
