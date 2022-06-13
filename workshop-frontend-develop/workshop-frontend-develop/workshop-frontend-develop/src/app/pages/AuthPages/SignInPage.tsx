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
import {login as loginRoute} from 'app/nav'
import {validatePassword} from 'app/utils/validatePassword'
import {validateLogin} from 'app/utils/validateLogin'
import {AppDialog} from 'app/components/AppDialog'

import style from './style.scss'

type RegistrationData = UserRegistrationModel & { confirmPassword: string }

const mapDispatchToProps = {setUserProfile: (user: UserModel) => ProfileActions.setUserProfile(user)}

type DispatchProps = typeof mapDispatchToProps

type Props = DispatchProps

const SignInPageComponent: React.FC<Props> = ({
                                                  setUserProfile,
                                              }) => {
    const history = useHistory()

    const [eventsList, setEventsList] = useState<EventModel[]>([])
    const [isDialogOpen, setIsDialogOpen] = useState(false)

    const [initValues, setInitValues] = useState<RegistrationData>({
        eventId: null as any,
        login: '',
        password: '',
        confirmPassword: '',
    })

    useEffect(() => {
        user.getEvents().then(res => {
            setEventsList(res.data)
        })
    }, [])

    const handleSignIn = (userData: RegistrationData) => {
        user.registration(userData)
            .then(() => login(userData.login, userData.password, userData.eventId))
            .then(user.getUserProfile)
            .then((res) => setUserProfile(res.data))
            .catch(() => {
                setIsDialogOpen(true)
                setInitValues(userData)
            })
    }

    const signupSchema = Yup.object().shape<typeof initValues>({
        eventId: Yup.number()
            .typeError('Введите значение')
            .required(),
        login: Yup.string()
            .required('Введите значение')
            .min(4, 'Логин не может быть короче 4 символов')
            .test(
                'login-validation',
                'Можно использовать только числа и буквы латинского алфавита',
                value => validateLogin(value),
            ),
        password: Yup.string()
            .required('Введите значение')
            .min(6, 'Пароль должен быть не менее 6 символов')
            .test(
                'password-validation',
                'Обязательно наличие цифр, строчных и заглавных символов',
                (value) => validatePassword(value),
            ),
        confirmPassword: Yup.string()
            .oneOf([Yup.ref('password'), ''], 'Пароли не совпадают')
            .required('Введите значение'),
    })

    return (
        <>
            <div className={style.container}>
                <div className={style.logo}/>
                <AppCard
                    className={style.card}
                    contentClassName={style.cardContent}
                >
                    <h2>Регистрация</h2>
                    <AppForm
                        onSubmit={handleSignIn}
                        initValues={initValues}
                        className={style.form}
                        validationSchema={signupSchema}
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
                        <AppFormTextField
                            name='confirmPassword'
                            label='Подтвердите пароль'
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
                            <span>Уже зарегистрированы?</span>
                            <a onClick={() => history.push(loginRoute())}>Войти</a>
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
                        Произошла ошибка при регистрации нового пользователя!
                    </p>
                    <p>
                        Возможно, такой логин уже занят, попробуйте создать аккаунт с другим логином.
                    </p>
                </AppCard>
            </AppDialog>
        </>
    )
}

export const SignInPage: React.FC = connect(
    undefined,
    mapDispatchToProps,
)(SignInPageComponent)
