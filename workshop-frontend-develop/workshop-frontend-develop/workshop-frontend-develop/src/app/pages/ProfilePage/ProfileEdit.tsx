import React from 'react'
import { connect } from 'react-redux'
import * as Yup from 'yup'
import { SaveRounded } from '@material-ui/icons'
import { RootState } from 'app/reducers'
import { AppCard } from 'app/components/AppCard'
import { user } from 'app/provider'
import { UserModel } from 'app/models'
import { ProfileActions } from 'app/actions'
import { AppForm, AppFormTextarea, AppFormTextField } from 'app/components/AppForm'
import { AppButton } from 'app/components/AppButton'
import { useHistory } from 'react-router-dom'
import { profile as profileRoute } from 'app/nav'
import cls from 'classnames'

import style from './style.scss'

const mapStateToProps = (state: RootState) => ({ profile: state.profile })

const mapDispatchToProps = { setUserProfile: (user: UserModel) => ProfileActions.setUserProfile(user) }

type StateProps = ReturnType<typeof mapStateToProps>
type DispatchProps = typeof mapDispatchToProps

interface Props extends StateProps, DispatchProps {
}

const ProfileEditComponent: React.FC<Props> = ({
  profile,
  setUserProfile,
}) => {
  const history = useHistory()

  const updateUserProfile = (value: typeof initValues) => {
    if (profile.profile) {
      const editedUser = {
        ...profile.profile,
        ...value,
      }

      user.editUserProfile(editedUser)
        .then(user.getUserProfile)
        .then(res => setUserProfile(res.data))
        .then(() => history.push(profileRoute()))
    }
  }
  const initValues = {
    lastName: profile.profile?.lastName || '',
    firstName: profile.profile?.firstName || '',
    middleName: profile.profile?.middleName || '',
    phoneNumber: profile.profile?.phoneNumber || '',
    about: profile.profile?.about || '',
    email: profile.profile?.email || '',
    academicGroup: profile.profile?.academicGroup || '',
  }

  const profileEditSchema = Yup.object().shape<Partial<typeof initValues>>({
    lastName: Yup.string().trim()
      .required('Введите значение'),
    firstName: Yup.string().trim()
      .required('Введите значение'),
    phoneNumber: Yup.string().trim()
      .required('Введите значение'),
    email: Yup.string().trim()
      .required('Введите значение'),
    academicGroup: Yup.string().trim()
      .required('Введите значение'),
  })

  const header = (
    <div className={style.editHeader}>
      <span>Редактирование</span>
      <AppButton
        type='button'
        buttonType='secondary'
        onClick={() => history.push(profileRoute())}
      >
        Назад
      </AppButton>
    </div>
  )

  return (
    <>
      <AppCard
        contentClassName={style.userInfoCard}
        header={header}
      >
        <AppForm
          onSubmit={updateUserProfile}
          initValues={initValues}
          className={style.editForm}
          validationSchema={profileEditSchema}
        >
          <div className={style.column}>
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
            <label className={style.formField}>
              <span>О себе</span>
              <AppFormTextarea
                name='about'
                label='О себе'
              />
            </label>
          </div>
          <div className={style.column}>
            <FormField
              name='phoneNumber'
              label='Контактный телефон'
              required
            />
            <FormField
              name='email'
              label='Электронная почта'
              required
            />
            <FormField
              name='academicGroup'
              label='Академическая группа'
              required
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
    </>
  )
}

export const ProfileEdit: React.FC = connect(
  mapStateToProps,
  mapDispatchToProps,
)(ProfileEditComponent)

interface FormFieldProps {
  name: string
  label: string
  required?: boolean
  className?: string
}

export const FormField: React.FC<FormFieldProps> = ({
  name,
  label,
  required,
  className,
}) => {
  const fieldStyles = cls([style.formField, className])

  return (
    <label className={fieldStyles}>
      <span className={required && style.required}>{label}</span>
      <AppFormTextField
        name={name}
        label={label}
      />
    </label>
  )
}
