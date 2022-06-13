import React, {useEffect, useState} from 'react'
import {useHistory} from 'react-router-dom'
import * as Yup from 'yup'
import {UserRegistrationModel} from 'app/models'
import {user} from 'app/provider'
import {AppCard} from 'app/components/AppCard'
import {AppForm, AppFormTextField, AppFormSelectField} from 'app/components/AppForm'
import {AppButton} from 'app/components/AppButton'
import {EventModel} from 'app/models/EventModel'
import {login as loginRoute} from 'app/nav'
import {AppDialog} from 'app/components/AppDialog'
import {validateEmail} from 'app/utils/validateEmail'

import style from './style.scss'

type RegistrationData = UserRegistrationModel

export const UpdatePasswordUrfu: React.FC = () => {
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
        user.updatePassword(userData)
            .then(() => history.push(loginRoute()))
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
                    header={'Обновление пароля'}
                >
                    <h2>Введите логин и пароль от личного кабинета УрФУ</h2>
                    <AppForm
                        onSubmit={handleSignUp}
                        initValues={initValues}
                        className={style.form}
                        validationSchema={signUpSchema}
                    >
                        <AppFormSelectField
                            name='eventId'
                            items={eventsList}
                            label='Выберите событие, для которого нужно обновить пароль'
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
                                Обновить пароль
                            </AppButton>
                            <a onClick={() => history.push(loginRoute())}>Назад</a>
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
                            Произошла ошибка при востанновлении пароля!
                        </p>
                        <p>
                            Возможно, вы ввели неправильный логин или пароль от личного кабинета УрФУ
                        </p>
                    </AppCard>
                </AppDialog>
            </div>
        </>)
}
