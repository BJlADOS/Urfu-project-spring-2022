import React, {useEffect, useState} from 'react'
import cls from 'classnames'
import {
    Create,
    Done,
    Close,
    DeleteRounded,
    PersonAddDisabledRounded,
    DateRangeRounded,
    FindInPageRounded,
    RemoveCircleOutlineRounded
} from '@material-ui/icons'
import {AppCard} from 'app/components/AppCard'
import {AppButton} from 'app/components/AppButton'
import {AppInput} from 'app/components/AppInput'
import {AuditoriumModel, ExpertUserModel, TeamSlot} from 'app/models'
import {AppDropdown} from 'app/components/AppDropdown'
import {admin, expert, user} from 'app/provider'
import {team} from 'app/nav'
import {RootState} from 'app/reducers'
import {connect} from 'react-redux'
import {getSlotDateTime} from 'app/utils/getSlotDateTime'

import {AddTeamSlotDialog} from './AddTeamSlotDialog'
import style from './style.scss'

const mapStateToProps = (state: RootState) => ({profile: state.profile.profile})

type StateProps = ReturnType<typeof mapStateToProps>

interface CardProps {
    auditorium: AuditoriumModel
    experts?: ExpertUserModel[]
    isAdmin?: boolean
    onUpdate: () => void
    onAddTeamToSlot?: (slotId: number) => void
}

interface Props extends CardProps, StateProps {
}

const AuditoriumCardComponent: React.FC<Props> = ({
                                                      auditorium,
                                                      experts,
                                                      isAdmin,
                                                      onUpdate,
                                                      onAddTeamToSlot: onSlotAdd,
                                                      profile,
                                                  }) => {
    const [editing, setEditing] = useState(false)
    const [dialogOpen, setDialogOpen] = useState(false)
    const [auditoriumName, setAuditoriumName] = useState('')

    useEffect(() => {
        setAuditoriumName(auditorium.name)
    }, [auditorium])

    const handleChangeName = () => {
        expert.updateAuditorium({
            ...auditorium,
            name: auditoriumName,
        }).then(onUpdate)
        setEditing(false)
    }

    const handleAddSlot = (date: Date) => {
        admin.addNewTeamSlot(auditorium.id, date)
            .then(() => setDialogOpen(false))
            .then(onUpdate)
    }

    const handleEditReset = () => {
        setEditing(false)
        setAuditoriumName(auditorium.name)
    }

    const handleDelete = () => {
        expert.deleteAuditorium(auditorium.id).then(onUpdate)
    }

    const handleExpertAdd = (value?: ExpertUserModel) => {
        if (!value) {
            return
        }

        expert.updateExpertAuditorium({
            auditoriumId: auditorium.id,
            expertId: value.id,
        }).then(onUpdate)
    }

    const handleExpertDelete = (expertId: number) => {
        expert.removeExpertAuditorium({
            auditoriumId: auditorium.id,
            expertId,
        }).then(onUpdate)
    }

    const handleTeamEnroll = (slotId: number) => {
        if (profile?.team) {
            user.enrollToSlot(slotId, profile.team.id).then(onUpdate)
        }
    }

    const handleSlotDelete = (slotId: number) => {
        admin.deleteTeamSlot(slotId).then(onUpdate)
    }

    const teamSection = (slot: TeamSlot) => slot.team ? (
        <>
      <span className={cls([{
          [style.myTeam]: profile?.team?.id === slot.team?.id,
      }])}
      >
        {slot.team.name || `Команда №${slot.team.id}`}
      </span>
            {slot.team && isAdmin && (
                <AppButton
                    type='button'
                    buttonType='transparent'
                    className={style.teamLink}
                    to={team(slot.team.id)}
                    icon={<FindInPageRounded fontSize='inherit'/>}
                />
            )}
        </>
    ) : isAdmin && (
        <AppButton
            buttonType='secondary'
            onClick={() => onSlotAdd && onSlotAdd(slot.id)}
        >
            Добавить команду
        </AppButton>
    )

    const slotsSort = (a: TeamSlot, b: TeamSlot): number => Number(new Date(a.time)) - Number(new Date(b.time))

    const slots = auditorium.slots?.sort(slotsSort).map(item => (
        <div
            key={item.id}
            className={style.teamSlot}
        >
            {!isAdmin && <AppButton
                buttonType='secondary'
                disabled={Boolean(profile?.team?.teamSlot) || Boolean(item.team)}
                onClick={() => handleTeamEnroll(item.id)}
            >
                Записаться
            </AppButton>}
            {isAdmin && <AppButton
                buttonType='transparent'
                icon={<RemoveCircleOutlineRounded/>}
                className={style.slotDeleteBtn}
                onClick={() => handleSlotDelete(item.id)}
            />}
            <span className={style.date}>
        {getSlotDateTime(item.time)}
      </span>
            {teamSection(item)}
        </div>
    ))

    const cardHeader = (
        <div className={style.cardHeader}>
            <h3>
                {auditorium.name}
            </h3>
            {isAdmin && (
                <div className={style.controlButtons}>
                    <AppButton
                        buttonType='transparent'
                        icon={<DateRangeRounded/>}
                        onClick={() => setDialogOpen(true)}
                    />
                    <AppButton
                        buttonType='transparent'
                        icon={<Create/>}
                        onClick={() => setEditing(true)}
                    />
                    <AppButton
                        buttonType='transparent'
                        icon={<DeleteRounded/>}
                        onClick={handleDelete}
                    />
                </div>
            )}
        </div>
    )

    const editAuditroium = (
        <div className={style.cardHeader}>
            <AppInput
                value={auditoriumName}
                className={style.auditoriumNameInput}
                onChange={(e) => setAuditoriumName(e.target.value)}
                placeholder='Введите название события'
            />

            <div className={style.controlButtons}>
                <AppButton
                    buttonType='transparent'
                    disabled={auditoriumName === ''}
                    icon={<Done/>}
                    onClick={handleChangeName}
                />
                <AppButton
                    buttonType='transparent'
                    icon={<Close/>}
                    onClick={handleEditReset}
                />
            </div>
        </div>
    )

    const expertsItems = auditorium.experts?.length ? auditorium.experts?.map(item => (
        <div
            key={item.id}
            className={style.expertName}
        >
            {isAdmin &&
                <AppButton
                    icon={<PersonAddDisabledRounded/>}
                    buttonType='transparent'
                    onClick={() => handleExpertDelete(item.id)}
                />
            }
            <span>{item.lastName} {item.firstName} {item.middleName}</span>
        </div>
    )) : 'В аудитории нет экспертов'

    const dataConverter = (value: ExpertUserModel) => ({
        key: value.id,
        label: `${value.lastName} ${value.firstName} ${value.middleName}`,
        value: value,
    })

    return (
        <>
            <div className={style.row}>
                <div className={style.largeCol}>
                    <AppCard
                        header={editing ? editAuditroium : cardHeader}
                        className={cls(style.auditoriumCard)}
                    >
                        {slots?.length ? slots : 'В аудитории нет мест для записи'}
                    </AppCard>
                </div>
                <div className={style.smallCol}>
                    <AppCard className={style.expertsCard}>
                        <div className={style.sideCardContent}>
                            <h5>Эксперты</h5>
                            {expertsItems}
                            {isAdmin &&
                                <AppDropdown
                                    className={style.addExpert}
                                    placeholder='Добавить эксперта'
                                    items={(experts || []).filter(e => !auditorium.experts?.map(ex => ex.id).includes(e.id))}
                                    onChange={handleExpertAdd}
                                    dataConverter={dataConverter}
                                    hideSelected
                                />
                            }
                        </div>
                    </AppCard>
                </div>
            </div>
            {isAdmin && <AddTeamSlotDialog
                open={dialogOpen}
                onSave={handleAddSlot}
                onClose={() => setDialogOpen(false)}
            />}
        </>
    )
}

export const AuditoriumCard: React.FC<CardProps> = connect(
    mapStateToProps,
)(AuditoriumCardComponent)
