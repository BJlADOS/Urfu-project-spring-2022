import React, { useEffect, useState } from 'react'
import { connect } from 'react-redux'
import { useHistory } from 'react-router-dom'
import { RootState } from 'app/reducers'
import { AppCard } from 'app/components/AppCard'
import { AppButton } from 'app/components/AppButton'
import { EditRounded, SaveRounded } from '@material-ui/icons'
import { user } from 'app/provider'
import {
  CompetencyModel,
  UserCompetencyModel,
  UserCompetencyType,
  UserModel,
  UserProfileModel,
  UserTypes,
} from 'app/models'
import * as Yup from 'yup'
import { ProfileActions } from 'app/actions'
import { profileEdit } from 'app/nav'
import { userTypesMap } from 'app/pages/UsersListPage'
import { AppForm, AppFormTextarea } from 'app/components/AppForm'
import { FormField } from 'app/pages/ProfilePage/ProfileEdit'
import { validatePhone } from 'app/utils/validationPhone'

import { CompetenciesCard } from './CompetenciesCard'
import style from './style.scss'

const mapStateToProps = (state: RootState) => ({ profile: state.profile })

const mapDispatchToProps = { setUserProfile: (user: UserModel) => ProfileActions.setUserProfile(user) }

type StateProps = ReturnType<typeof mapStateToProps>
type DispatchProps = typeof mapDispatchToProps

interface Props extends StateProps, DispatchProps {
}

const ProfilePageComponent: React.FC<Props> = ({
  profile,
  setUserProfile,
}) => {
  const history = useHistory()

  const [competenciesList, setCurrentCompetenciesList] = useState<UserCompetencyModel[]>([])
  const userFullName = profile.profile?.lastName && profile.profile?.firstName
    ? `${profile.profile?.lastName} ${profile.profile?.firstName} ${profile.profile?.middleName || ''}`
    : `Пользователь №${profile?.profile?.id}`
  const userInfo = profile.profile?.academicGroup && profile.profile?.phoneNumber && profile.profile?.email
    ? `${profile.profile?.academicGroup} • ${profile.profile?.phoneNumber} • ${profile.profile?.email}`
    : 'Вы заполнили не все данные'

  const userType = userTypesMap.get(profile?.profile?.userType || UserTypes.Student)

  useEffect(() => {
    user.getCompetitions().then(res => {
      setCurrentCompetenciesList(res.data)
    })
  }, [])

  const addCurrentCompetency = (competency?: CompetencyModel) => {
    if (competency) {
      const competencyToAdd: UserCompetencyModel = {
        ...competency,
        userCompetencyType: UserCompetencyType.Current,
      }

      addCompetency(competencyToAdd)
    }
  }

  const addDesiredCompetency = (competency?: CompetencyModel) => {
    if (competency) {
      const competencyToAdd: UserCompetencyModel = {
        ...competency,
        userCompetencyType: UserCompetencyType.Desirable,
      }

      addCompetency(competencyToAdd)
    }
  }

  const addCompetency = (competency: UserCompetencyModel) => {
    if (profile.profile) {
      const newCompetencies = [...profile.profile.competencies || [], competency]
      const newProfile: UserProfileModel = {
        ...profile.profile,
        competencies: newCompetencies,
      }

      updateUserProfile(newProfile)
    }
  }
  const handleChangeUserType = (type: UserTypes) => {
    user.updateUserType(type)
      .then(user.getUserProfile)
      .then((res) => setUserProfile(res.data))
  }

  const handleCompetencyDelete = (id: number) => {
    if (profile.profile) {
      const editedCompetencies = profile.profile.competencies.filter(item => item.id !== id)
      const newProfile: UserProfileModel = {
        ...profile.profile,
        competencies: editedCompetencies,
      }

      updateUserProfile(newProfile)
    }
  }

  const updateUserProfile = (profile: UserProfileModel) => {
    user.editUserProfile(profile)
      .then(user.getUserProfile)
      .then(res => setUserProfile(res.data))
  }
  const initValues = {
    phoneNumber: profile.profile?.phoneNumber || '',
    about: profile.profile?.about || '',
  }

  const updateUserPhone = (value: typeof initValues) => {
    if (profile.profile) {
      const editedProfile = {
        ...profile.profile,
        ...value,
      }

      updateUserProfile(editedProfile)
    }
  }

  const filteredCompetencies = competenciesList.filter(el => profile.profile?.competencies.map(el => el.id).indexOf(el.id) === -1)

  const currentCompetencies = profile.profile?.competencies.filter(item => item.userCompetencyType === UserCompetencyType.Current)
  const desiredCompetencies = profile.profile?.competencies.filter(item => item.userCompetencyType === UserCompetencyType.Desirable)

  const competenciesHeader = (text: string, competencies?: UserCompetencyModel[]) =>
    competencies && competencies.length >= 3
      ? text
      : (
        <div className={style.competencyCardHeader}>
          <span>{text}</span>
          <span className={style.helper}>Выберите минимум 3 компетенции</span>
        </div>
      )

  const getSwitchStudentType = () => {
    switch (profile.profile?.userType) {
      case UserTypes.Student:
        return (<AppButton
          buttonType={'primary'}
          type={'button'}
          onClick={() => handleChangeUserType(UserTypes.Teamlead)}
        >
          Стать тимлидом
        </AppButton>)
      case UserTypes.Teamlead:
        return (
          <AppButton
            buttonType={'primary'}
            type={'button'}
            onClick={() => handleChangeUserType(UserTypes.Student)}
          >
            Стать студентом
          </AppButton>
        )
      default:
        return <></>
    }
  }

  const StudentTypeButton = profile.profile?.profileFilled && getSwitchStudentType()

  const editButton = ((process.env.LOGIN_URFU == 'true' && profile.profile?.userType === (UserTypes.Admin || UserTypes.Expert)) ||
    process.env.LOGIN_URFU == 'false') && (
    <div className={style.editButton}>
      <AppButton
        onClick={() => history.push(profileEdit())}
        buttonType='primary'
        icon={<EditRounded/>}
        type='button'
      >
        Редактировать
      </AppButton>
    </div>
  )

  const profileEditSchema = Yup.object().shape<Partial<typeof initValues>>({
    phoneNumber: Yup.string().trim()
      .required('Необходимо ввести данные для связи'),
    about: Yup.string().trim(),

  })
  const phoneCard = process.env.LOGIN_URFU == 'true' &&
    profile.profile?.userType === (UserTypes.Student || UserTypes.Teamlead) &&
    profile.profile.firstName && (
    <AppCard className={style.phoneCard}>
      <AppForm
        onSubmit={updateUserPhone}
        initValues={initValues}
        className={style.editForm}
        validationSchema={profileEditSchema}
      >
        <FormField
          name='phoneNumber'
          label='Введите контакт для связи с вами'
          className={style.cardArea}
          required
        />
        <label className={style.formField}>
          <span>О себе</span>
          <AppFormTextarea
            name='about'
            label='Расскажите о себе поподробнее !'
            className={style.aboutCard}
          />
        </label>

        <AppButton
          icon={<SaveRounded/>}
          type='submit'
        >
            Сохранить
        </AppButton>

      </AppForm>
    </AppCard>)

  return (
    <>
      <AppCard contentClassName={style.userInfoCard}>
        <div className={style.userInfo}>
          <h2>{userFullName}</h2>
          <span>{userInfo}</span>
        </div>
        {process.env.LOGIN_URFU == 'false' && editButton}
      </AppCard>
      {phoneCard}
      <div className={style.competenciesCards}>
        <CompetenciesCard
          header={competenciesHeader('Мои компетенции', currentCompetencies)}
          competenciesList={filteredCompetencies}
          selectedItems={currentCompetencies || []}
          onChange={addCurrentCompetency}
          onDelete={handleCompetencyDelete}
        />
        <CompetenciesCard
          header={competenciesHeader('Желаемые компетенции', desiredCompetencies)}
          competenciesList={filteredCompetencies}
          selectedItems={desiredCompetencies || []}
          onChange={addDesiredCompetency}
          onDelete={handleCompetencyDelete}
        />
      </div>
      <AppCard
        header={'Тип пользователя'}
        className={style.typeCard}
      >

        <span>{userType}</span>
        {!profile.profile?.team && StudentTypeButton}

      </AppCard>
    </>
  )
}

export const ProfilePage: React.FC = connect(
  mapStateToProps,
  mapDispatchToProps,
)(ProfilePageComponent)
