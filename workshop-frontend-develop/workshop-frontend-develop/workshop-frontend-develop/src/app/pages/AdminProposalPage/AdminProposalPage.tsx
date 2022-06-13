import React, { useEffect, useState } from 'react'
import { useHistory, useParams } from 'react-router-dom'
import { CreateProjectFromProposalModel, ProposalModel } from 'app/models/ProjectProposalModel'
import { admin, user } from 'app/provider'
import { projectProposals } from 'app/nav'
import { AppLoadingSpinner } from 'app/components/AppLoadingSpinner'
import style from 'app/pages/ProjectPage/style.scss'
import { AppButton } from 'app/components/AppButton'
import {
  AddCircleOutlineRounded,
  DeleteOutlineRounded,
  EditRounded,
  FlagRounded,
  HighlightOffRounded,
  SaveRounded,
} from '@material-ui/icons'
import { AppCard } from 'app/components/AppCard'
import { AppInput } from 'app/components/AppInput'
import { ProposalDescriptionCard, ProposalDescriptionEditCard } from 'app/components/ProposalDescriptionCard'
import { ProposalInfoCard, ProposalInfoEditCard } from 'app/components/ProposalInfoCard'
import { CheckFidelityDialog } from 'app/components/CheckFidelityDialog'
import { AppDivider } from 'app/components/AppDivider'
import { RoleCard } from 'app/pages/AddProject/RoleCard'

export const AdminProposalPage = () => {
  const { proposalId } = useParams<{ proposalId: string }>()
  const [proposal, setProposal] = useState<ProposalModel>()
  const [edit, setEdit] = useState(false)
  const [rejectProposalDialog, setRejectProposalDialog] = useState(false)
  const [acceptProposalDialog, setAcceptProposalDialog] = useState(false)
  const [rolesList, setRolesList] = useState<string[]>([])

  const history = useHistory()

  const fetchProposal = () => admin.getProposal(Number(proposalId))
    .then(res => setProposal(res.data))

  useEffect(() => {
    fetchProposal()
  }, [])

  if (!proposal) {
    return <AppLoadingSpinner/>
  }

  const handleCreateProjectFromProposal = async() => {
    const createProject: CreateProjectFromProposalModel = {
      name: proposal.name,
      description: proposal.description,
      purpose: proposal.purpose,
      result: proposal.result,
      contacts: proposal.contacts,
      curator: `${proposal.author.lastName} ${proposal.author.firstName} ${proposal.author.middleName}`,
      organization: proposal.organization,
      teamCapacity: proposal.teamCapacity,
      lifeScenarioName: proposal.lifeScenarioName,
      keyTechnologyName: proposal.keyTechnologyName,
      authorId: proposal.author.id,
      status: proposal.status,
      roleNames: rolesList,
    }

    await admin.createProjectFromProposal(Number(proposalId), createProject)
    history.push(projectProposals())
  }

  const handleRejectProjectProposal = () => {
    admin.rejectProjectProposal(Number(proposalId))
      .then(() => history.push(projectProposals()))
  }

  const handleProposalEditSave = (proposal: ProposalModel) => {
    user.updateProjectProposal(Number(proposalId), {
      ...proposal,
    })
      .then(() => setEdit(false))
      .then(fetchProposal)
  }

  const addRole = (role?: string) => {
    const tmp = rolesList.slice()

    if (role !== undefined) {
      tmp.push(role)
    }
    setRolesList(tmp)
  }
  const deleteLastRole = () => {
    const tmp = rolesList.slice()

    tmp.pop()
    setRolesList(tmp)
  }

  const editButtons = edit ? (
    <div className={style.editButtons}>
      <AppButton
        type='button'
        icon={<SaveRounded/>}
        onClick={() => handleProposalEditSave(proposal)}
      >
        Сохранить
      </AppButton>
      <AppButton
        type='button'
        buttonType='danger'
        icon={<HighlightOffRounded/>}
        onClick={() => fetchProposal().then(() => setEdit(false))}
      />
    </div>
  ) : (
    <AppButton
      type='button'
      icon={<EditRounded/>}
      onClick={() => setEdit(true)}
    >
      Редактировать
    </AppButton>
  )
  const headerCard = (
    <AppCard
      className={style.projectNameCard}
      contentClassName={style.adminFilledTeams}
    >
      {edit
        ? <AppInput
          value={proposal.name}
          className={style.nameInput}
          onChange={(e) => setProposal({
            ...proposal,
            name: e.target.value,
          })}
        />
        : <h2>
          {proposal.name}
          {proposal.status && <FlagRounded/>}
        </h2>}

      {editButtons}
    </AppCard>
  )
  const proposalDescriptionCard = (
    <div className={style.card}>
      {edit
        ? <ProposalDescriptionEditCard
          proposal={proposal}
          onChange={(value) => setProposal({
            ...proposal,
            ...value,
          })}
        />
        : <ProposalDescriptionCard proposal={proposal}/>
      }
    </div>
  )
  const proposalInfoCard = (
    <div className={style.card}>
      {edit
        ? <ProposalInfoEditCard
          isLifeScenarioNeed={true}
          proposal={proposal}
          onChange={(value) => setProposal({
            ...proposal,
            ...value,
          })}
        />
        : <ProposalInfoCard proposal={proposal}/>
      }
    </div>)

  const editCapacityButtons = (<div className={style.capacityButtons}>
    <AppButton
      type='button'
      icon={<DeleteOutlineRounded/>}
      onClick={() => {
        setProposal({
          ...proposal,
          teamCapacity: proposal.teamCapacity - 1,
        })
      }}
    >
      Убавить кол-во
    </AppButton>
    <AppButton
      type='button'
      icon={<AddCircleOutlineRounded/>}
      onClick={() => {
        setProposal({
          ...proposal,
          teamCapacity: proposal.teamCapacity + 1,
        })
      }}
    >
      Добавить кол-во
    </AppButton>
  </div>)

  const teamCapacityCard = (
    <div className={style.card}>
      <AppCard contentClassName={style.adminCapacityProposal}>
        <h3>Желаемое кол-во участников в команде:{' '}
          <span className={style.highlightText}>
            {proposal.teamCapacity}
          </span>
        </h3>
        {edit && editCapacityButtons}
      </AppCard>
    </div>
  )

  const editStatusButtons = (
    <div className={style.statusButtons}>
      <AppButton
        disabled={edit}
        type='button'
        buttonType='secondaryAccent'
        onClick={() => setAcceptProposalDialog(true)}
      >
        Подтвердить заявку
      </AppButton>
      <AppButton
        disabled={edit}
        type='button'
        buttonType='danger'
        onClick={() => setRejectProposalDialog(true)}
      >
        Отклонить заявку
      </AppButton>
    </div>
  )
  const additionAccept = (
    <>
      <AppDivider/>
      <h4 className={style.addItalic}>Данная заявка появиться в качестве проекта на сервисе</h4>
    </>
  )
  const rolesCard = (
    <RoleCard
      selectedItems={rolesList}
      header='Роли'
      onAdd={(e) => addRole(e)}
      onDelete={deleteLastRole}
    >
    </RoleCard>
  )

  return (
    <>
      {headerCard}
      <div className={style.pageContent}>
        <div className={style.largeColumn}>
          {proposalDescriptionCard}
          {teamCapacityCard}
          {rolesCard}

        </div>
        <div className={style.smallColumn}>
          {proposalInfoCard}
          {editStatusButtons}
        </div>
      </div>
      <CheckFidelityDialog
        header={'Вы уверены, что хотите подтвердить заявку ?'}
        additionContent={additionAccept}
        className={style.checkFidelityDialog}
        open={acceptProposalDialog}
        onClose={() => setAcceptProposalDialog(false)}
        onMakingAction={handleCreateProjectFromProposal}
        buttonType='primary'
        buttonText='Подтвердить заявку'
      />
      <CheckFidelityDialog
        header={'Вы уверены, что хотите отклонить заявку ?'}
        className={style.checkFidelityDialog}
        open={rejectProposalDialog}
        onClose={() => setRejectProposalDialog(false)}
        onMakingAction={handleRejectProjectProposal}
        buttonType='primaryAccent'
        buttonText='Отклонить заявку'
      />
    </>
  )
}
