import React, { useState } from 'react'
import { connect } from 'react-redux'
import { RootState } from 'app/reducers'
import { AppCard } from 'app/components/AppCard'
import { AppButton } from 'app/components/AppButton'
import { EditRounded, HighlightOffRounded, LockOpenRounded, SaveRounded } from '@material-ui/icons'
import { TeamMemberCard } from 'app/components/TeamMemberCard'
import { TeamStatusses, UserModel } from 'app/models'
import { AppInput } from 'app/components/AppInput'
import { projects, teamlead, user } from 'app/provider'
import { ProfileActions } from 'app/actions'
import { CheckFidelityDialog } from 'app/components/CheckFidelityDialog'
import { AppDivider } from 'app/components/AppDivider'

import style from './style.scss'

const mapStateToProps = (state: RootState) => ({ profile: state.profile })

const mapDispatchToProps = { setUserProfile: (user: UserModel) => ProfileActions.setUserProfile(user) }

type StateProps = ReturnType<typeof mapStateToProps>
type DispatchProps = typeof mapDispatchToProps

const mapRegister = new Map()
  .set(true, 'Зарегистрирована')
  .set(false, 'Не зарегистрирована')
  .set(undefined, '')

interface Props extends StateProps, DispatchProps {
}

const TeamleadTeamPageComponent: React.FC<Props> = ({
  profile,
  setUserProfile,
}) => {
  const team = profile.profile?.team
  const teamName = team?.name || `Команда №${team?.id}`

  const [edit, setEdit] = useState(false)
  const [editedTeamName, setEditedTeamName] = useState(teamName)
  const [DeleteTeamDialog, setDeleteTeamDialog] = useState(false)
  const [catchError, setCatchError] = useState(false)

  const fetchUser = () => user.getUserProfile()
    .then(res => setUserProfile(res.data))

  const handleTeamRename = () => {
    projects.renameTeam(team?.id ?? 0, editedTeamName)
      .then(fetchUser)
      .then(() => setEdit(false))
  }

  const teamMembers = (team?.users as UserModel[]).map(user => (
    <TeamMemberCard
      key={user.id}
      user={user}
    />
  ))

  const header = edit
    ? (
      <>
        <AppInput
          value={editedTeamName}
          onChange={(e) => setEditedTeamName(e.target.value)}
        />
        <div className={style.editButton}>
          <AppButton
            buttonType='primary'
            icon={<SaveRounded/>}
            type='button'
            onClick={handleTeamRename}
          >
            Сохранить
          </AppButton>
          <AppButton
            type='button'
            buttonType='danger'
            icon={<HighlightOffRounded/>}
            onClick={() => setEdit(false)}
          />
        </div>
      </>
    )
    : (
      <>
        <h2>{teamName}</h2>
        <span className={style.teamStatus}>{mapRegister.get(team?.isEntried)}</span>
        {team?.teamStatus !== TeamStatusses.Incomplete && team?.isEntried &&
        <div className={style.editButton}>
          <AppButton
            buttonType='primary'
            icon={<EditRounded/>}
            type='button'
            onClick={() => {
              setEdit(true)
              setEditedTeamName(teamName)
            }}
          >
            Редактировать
          </AppButton>
        </div>
        }
      </>
    )
  const additionError = (
    <>
      <AppDivider/>
      <h4 className={style.addItalic}>Попробуйте написать в группу проектного практикума.</h4>
    </>)
  const noRegisterDialog = catchError && (
    <CheckFidelityDialog
      onMakingAction={() => setCatchError(false)}
      header={'Кажется, вы не успели зарегистрировать команду'}
      additionContent={additionError}
      buttonType={'primary'}
      buttonText={'Закрыть'}
      open={catchError}
    />
  )

  const handleRegisterTeam = () => {
    teamlead.registerTeam(team?.id || 0)
      .then(fetchUser)
      .catch(() => setCatchError(true))
  }
  const handleDeleteTeam = () => {
    teamlead.deleteTeam()
      .then(fetchUser)
  }

  const leaveTeamButton = (
    <AppButton
      type='button'
      buttonType='danger'
      onClick={() => setDeleteTeamDialog(true)}
    >
      Удалить команду
    </AppButton>
  )

  const registerTeamButton = team?.teamStatus === TeamStatusses.ReadyForEntry && team?.project.isEntryOpen && (
    <AppButton
      type='button'
      buttonType='secondary'
      icon={<LockOpenRounded/>}
      onClick={handleRegisterTeam}
    >
      Зарегистрировать команду
    </AppButton>
  )

  const addition = (
    <>
      <AppDivider/>
      <h4 className={style.addItalic}>Учтите, что вы потеряете место в проекте.</h4>
    </>)

  return (
    <>
      <AppCard contentClassName={style.teamNameCard}>
        {header}
      </AppCard>
      <div className={style.teamMembersContainer}>
        {teamMembers}
      </div>
      <div className={style.buttonsSection}>
        {registerTeamButton}
        {leaveTeamButton}
      </div>
      <CheckFidelityDialog
        header={'Вы уверены, что хотите удалить команду ?'}
        additionContent={addition}
        className={style.checkFidelityDialog}
        open={DeleteTeamDialog}
        onClose={() => setDeleteTeamDialog(false)}
        onMakingAction={handleDeleteTeam}
        buttonType={'danger'}
        buttonText={'Удалить команду'}
      />
      {noRegisterDialog}
    </>
  )
}

export const TeamleadTeamPage: React.FC = connect(
  mapStateToProps,
  mapDispatchToProps,
)(TeamleadTeamPageComponent)
