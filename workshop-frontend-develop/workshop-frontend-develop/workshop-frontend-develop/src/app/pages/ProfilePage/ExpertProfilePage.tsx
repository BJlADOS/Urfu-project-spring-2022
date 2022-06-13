import React from 'react'
import {RootState} from 'app/reducers'
import {UserModel, UserTypes} from 'app/models'
import {ProfileActions} from 'app/actions'
import {connect} from 'react-redux'
import {userTypesMap} from 'app/pages/UsersListPage'
import {user} from 'app/provider'
import * as Yup from 'yup'
import {AppCard} from 'app/components/AppCard'
import style from 'app/pages/ProfilePage/style.scss'
import {AppForm} from 'app/components/AppForm'
import {FormField} from 'app/pages/ProfilePage/ProfileEdit'
import {AppButton} from 'app/components/AppButton'
import {SaveRounded} from '@material-ui/icons'
import {useHistory} from 'react-router-dom'
import {profile as profileRoute} from 'app/nav.tsx'

const mapStateToProps = (state: RootState) => ({
    profile: state.profile,
})

const mapDispatchToProps = {setUserProfile: (user: UserModel) => ProfileActions.setUserProfile(user)}

type StateProps = ReturnType<typeof mapStateToProps>

type DispatchProps = typeof mapDispatchToProps

interface Props extends StateProps, DispatchProps {
}

const ExpertProfileComponent: React.FC<Props> = ({
                                                     profile,
                                                     setUserProfile,
                                                 }) => {
    const history = useHistory()

    const userFullName = profile.profile?.lastName && profile.profile?.firstName
        ? `${profile.profile?.lastName} ${profile.profile?.firstName} ${profile.profile?.middleName || ''}`
        : `Пользователь №${profile?.profile?.id}`

    const userType = userTypesMap.get(profile?.profile?.userType || UserTypes.Student)

    const handleUpdateUser = (value: typeof initValues) => {
        console.log(321)
        if (profile.profile) {
            const editProfile = {
                ...profile.profile,
                ...value,
            }

            user.editUserProfile(editProfile)
                .then(user.getUserProfile)
                .then(res => setUserProfile(res.data))
                .then(() => history.push(profileRoute()))
        }
    }

    const initValues = {
        lastName: profile.profile?.lastName || '',
        firstName: profile.profile?.firstName || '',
        middleName: profile.profile?.middleName || '',
    }

    const profileEditSchema = Yup.object().shape<Partial<typeof initValues>>({
        lastName: Yup.string().trim()
            .required('Введите значение'),
        firstName: Yup.string().trim()
            .required('Введите значение'),
    })

    return (
        <>
            <AppCard>
                <div className={style.expertUserInfo}>
                    <h2>{userFullName}</h2>
                </div>
            </AppCard>
            <AppCard className={style.expertFormCard}>
                <AppForm
                    initValues={initValues}
                    validationSchema={profileEditSchema}
                    className={style.editFormExpert}
                    onSubmit={handleUpdateUser}
                >
                    <div className={style.row}>
                        <FormField
                            name='lastName'
                            label='Фамилия'
                            required
                        />
                        <FormField
                            name='firstName'
                            label='Имя'
                            required
                        />
                        <FormField
                            name='middleName'
                            label='Отчество'
                        />
                    </div>
                    <AppButton
                        icon={<SaveRounded/>}
                        type='submit'
                    >
                        Сохранить
                    </AppButton>
                </AppForm>
            </AppCard>

            <AppCard
                header={'Тип пользователя'}
                className={style.typeCard}
            >

                <span>{userType}</span>

            </AppCard>

        </>
    )
}

export const ExpertProfilePage = connect(
    mapStateToProps,
    mapDispatchToProps,
)(ExpertProfileComponent)
