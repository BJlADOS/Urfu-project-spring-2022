import React, { useState } from 'react'
import style from 'app/pages/UsersListPage/style.scss'
import { CompetenciesList } from 'app/components/CompetenciesList'
import { RoleModel, ShortUserModel, UserCompetencyType } from 'app/models'
import { AppButton } from 'app/components/AppButton'
import { AppCard } from 'app/components/AppCard'
import { UserSearchDialog } from 'app/pages/UserSearchPage/UserSearchDialog'

interface Props {
  user: ShortUserModel,
  createRequestProposal: (str:string) => void,
  rolesList:RoleModel[]
}

export const UserSearchCard: React.FC<Props> = ({
  user,
  createRequestProposal,
  rolesList,
}) => {
  const userCardHeader = (user: ShortUserModel) => (
    <div className={style.userCardHeader}>
      <span>{user.lastName} {user.firstName} {user.middleName}</span>
      <span>Академическая группа {user.academicGroup === '-' ? 100000 : user.academicGroup}</span>
    </div>
  )
  const [buttonDisable, setButtonDisable] = useState(false)
  const [openDialog, setOpenDialog] = useState(false)
  const [addButtonPress, setAddButtonPress] = useState(false)
  const dialog = (
    <UserSearchDialog
      onClose={() => {
        setOpenDialog(false)
        addButtonPress ? setButtonDisable(true) : setButtonDisable(false)
      }}
      open = {openDialog}
      pressAdd = {(e) => setAddButtonPress(e)}
      createRequest = {(str) => {
        createRequestProposal(str)
      }

      }
      rolesList={rolesList}
    />
  )

  return (
    <>
      <AppCard
        key={user.id}
        className={style.card}
        header={userCardHeader(user)}
      >
        <CompetenciesList
          list={
            user.competencies.filter((c) => c.userCompetencyType === UserCompetencyType.Current)
          }
        />
        <br/>
        <AppButton
          buttonType={'primary'}
          className={style.addUserButton}
          disabled={buttonDisable}
          onClick={() => {
            setButtonDisable(true)
            setOpenDialog(true)
          }}
        >Пригласить пользователя</AppButton>
      </AppCard>
      {dialog}
    </>
  )
}
