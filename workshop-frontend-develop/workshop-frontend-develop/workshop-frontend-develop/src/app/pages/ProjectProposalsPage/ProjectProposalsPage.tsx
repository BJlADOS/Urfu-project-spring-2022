import React, { useEffect, useState } from 'react'
import { connect } from 'react-redux'
import { RootState } from 'app/reducers'
import { ProfileActions } from 'app/actions'
import { AppCard } from 'app/components/AppCard'
import { AppButton } from 'app/components/AppButton'
import { teamlead, user } from 'app/provider'
import { ProposalModel, ProposalStatus } from 'app/models/ProjectProposalModel'
import { LoadingStatus, UserModel } from 'app/models'
import { CreateProjectProposal } from 'app/pages/CreateProjectProposalPage'
import { AppInput } from 'app/components/AppInput'
import { ProposalInfoCard, ProposalInfoEditCard } from 'app/components/ProposalInfoCard'
import { ProposalDescriptionCard, ProposalDescriptionEditCard } from 'app/components/ProposalDescriptionCard'
import { ProposalTeamsInfoCard, ProposalTeamsInfoEditCard } from 'app/components/ProposalTeamsInfoCards'
import { AppLoadingSpinner } from 'app/components/AppLoadingSpinner'

// import style from 'app/pages/ProjectPage/style.scss'
import style from './style.scss'

const statussesMap = new Map<string, string>()
  .set('Rejected', 'Отклонено')
  .set('Approved', 'Одобрено')
  .set('Pending', 'На рассмотрении')

const mapStateToProps = (state: RootState) => ({ profile: state.profile })

const mapDispatchToProps = { setUserProfile: (user: UserModel) => ProfileActions.setUserProfile(user) }

type StateProps = ReturnType<typeof mapStateToProps>
type DispatchProps = typeof mapDispatchToProps

interface Props extends StateProps, DispatchProps {
}

const ProjectProposals: React.FC<Props> = ({ profile }) => {
  const [proposal, setProposal] = useState<ProposalModel | undefined>(undefined)
  const [loading, setLoading] = useState(LoadingStatus.Loading)
  const [edit, setEdit] = useState(false)
  const [editDisabled, setEditDisabled] = useState(false)

  const fetchProposal = () =>
    teamlead.getProposalByAuthor()
      .then((res) => setProposal(res.data))

  useEffect(() => {
    fetchProposal()
      .then(() => setLoading(LoadingStatus.Finished))
  }, [loading])

  if (loading === LoadingStatus.Loading) {
    return <AppLoadingSpinner/>
  }

  const handleProposalEditSave = (proposal: ProposalModel) => {
    setEditDisabled(true)
    user.updateProjectProposal(
      proposal.id,
      {
        ...proposal,
        status: ProposalStatus.Pending,
      },
    )
      .then(() => setEdit(false))
      .then(fetchProposal)
      .then(() => setEditDisabled(false))
  }

  let projectProposal

  if (proposal) {
    const editButtons = edit ? (
      <div className={style.editButtons}>
        <AppButton
          type='button'
          buttonType='secondary'
          onClick={() => handleProposalEditSave(proposal)}
        >
          Сохранить изменения
        </AppButton>
        <AppButton
          type='button'
          buttonType='danger'
          onClick={() => setEdit(false)}
        >
          Отменить
        </AppButton>
      </div>
    ) : (
      <AppButton
        type='button'
        buttonType='secondary'
        disabled={editDisabled}
        onClick={() => setEdit(true)}
      >
        Редактировать
      </AppButton>
    )
    const headerNameCard = (
      <AppCard
        className={style.projectNameCard}
        contentClassName={style.headerCard}
      > {edit
          ? <AppInput
            required
            value={proposal.name}
            className={style.nameInput}
            onChange={(e) => setProposal({
              ...proposal,
              name: e.target.value,
            })}
          />
          : <h2>{proposal.name}</h2>}

        <span>{statussesMap.get(proposal.status.toString())}</span>
      </AppCard>
    )

    const proposalInfoCard = (
      <div className={style.card}>
        {edit
          ? <ProposalInfoEditCard
            isLifeScenarioNeed={false}
            proposal={proposal}
            onChange={(value) => setProposal({
              ...proposal,
              ...value,
            })}
          />
          : <ProposalInfoCard proposal={proposal}/>
        }
      </div>
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
          : <ProposalDescriptionCard
            proposal={proposal}
            // onDelete={handleProposalDelete}
          />
        }
      </div>
    )

    const teamsInfoCards = (
      <ProposalTeamsInfoCard proposal={proposal}/>
    )

    projectProposal = (

      <>
        <AppCard className={style.EditHeaderCard}>
          <div>
            <h2>Моя проектная заявка</h2>
          </div>
          <div>
            {ProposalStatus[ProposalStatus.Rejected] === proposal!.status.toString() && editButtons}
          </div>
        </AppCard>

        {headerNameCard}
        <div className={style.pageContent}>
          <div className={style.largeColumn}>
            {proposalDescriptionCard}
            {teamsInfoCards}
          </div>
          <div className={style.smallColumn}>
            {proposalInfoCard}
          </div>
        </div>
      </>
    )
  } else {
    projectProposal = (
      <>
        <AppCard
          header={'Создать проектную заявку'}
          className={style.CreateProposal}
        />
        <CreateProjectProposal
          fetch={fetchProposal}
          profile={profile}
        />
      </>)
  }

  return projectProposal

  // const content = proposal !== null ? proposal : <CreateProjectProposalPage/>
  // switch (proposal?.status) {
  //   case ProposalStatus.Pending:
  //     return (<>
  //       <AppCard>
  //         <div className={style.header}>
  //           <h2>Заявка на рассмотрении</h2>
  //         </div>
  //       </AppCard>
  //       {projectProposal}
  //     </>)
  //   case ProposalStatus.Rejected:
  //     return (<>
  //       <AppCard>
  //         <div className={style.proposalRejected}>
  //           <h2>Ваша заявка не принята</h2>
  //         </div>
  //       </AppCard>
  //       {projectProposal}
  //     </>)
  //   case ProposalStatus.Approved: return (<>
  //     <AppCard>
  //       <div className={style.proposalApproved}>
  //         <h2>Ваша заявка принята</h2>
  //       </div>
  //     </AppCard>
  //     {projectProposal}
  //   </>)
  //
  //   default:return projectProposal
}

export const ProjectProposalsPage: React.FC = connect(
  mapStateToProps,
  mapDispatchToProps,
)(ProjectProposals)
