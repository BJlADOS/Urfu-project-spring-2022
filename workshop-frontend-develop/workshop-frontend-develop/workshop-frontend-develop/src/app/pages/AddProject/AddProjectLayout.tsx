import React, { useEffect, useState } from 'react'
import { RootState } from 'app/reducers'
import { admin } from 'app/provider/api-provider'
import { AppForm, AppFormTextarea, AppFormTextField } from 'app/components/AppForm'
import { AddProjectModel, CompetencyModel, CompetencyType } from 'app/models'
import { AppCard } from 'app/components/AppCard'
import { AppButton } from 'app/components/AppButton'

import { RoleCard } from './RoleCard'
import style from './style.scss'
import { CompetenciesSkillsCard } from './CompetenciesCard'

const mapStateToProps = (state: RootState) => ({ profile: state.profile })

type StateProps = ReturnType<typeof mapStateToProps>
type Props = StateProps

const AddProjectLayout: React.FC<Props> = () => {
  const [hardSkillsList, setHardSkillsList] = useState<CompetencyModel[]>([])
  const [softSkillsList, setSoftSkillsList] = useState<CompetencyModel[]>([])

  const [competenciesList, setCurrentCompetenciesList] = useState<CompetencyModel[]>([])

  useEffect(() => {
    admin.getCompetencies().then(res => {
      setCurrentCompetenciesList(res.data)
    })
  }, [])
  const hardCompetencies = competenciesList.filter(el => el.competencyType == CompetencyType.HardSkill)
  const softCompetencies = competenciesList.filter(el => el.competencyType == CompetencyType.SoftSkill)

  const [rolesList, setRolesList] = useState<string[]>([])

  const [initList, setInitList] = useState<AddProjectModel>({
    name: '',
    description: '',
    purpose: '',
    result: '',
    curator: '',
    organization: '',
    contacts: '',
    lifeScenarioName: '',
    keyTechnologyName: '',
    hardSkills: [],
    softSkills: [],
    roles: rolesList,
    teamsSize: null,
    teamLimit: null,
  })
  const handleAddProject = async({
    name,
    description,
    purpose,
    result,
    curator,
    organization,
    contacts,
    lifeScenarioName,
    keyTechnologyName,
    teamsSize,
    teamLimit,
  }: AddProjectModel) => {
    const rolesTmp = rolesList
    const hardSkillsTmp = hardSkillsList.map(x => x.name)
    const softSkillsTmp = softSkillsList.map(x => x.name)

    await admin.addProject({
      name,
      description,
      purpose,
      result,
      curator,
      organization,
      contacts,
      lifeScenarioName,
      keyTechnologyName,
      hardSkills: hardSkillsTmp,
      softSkills: softSkillsTmp,
      roles: rolesTmp,
      teamsSize,
      teamLimit,
    })

    setInitList({
      name: '',
      description: '',
      purpose: '',
      result: '',
      curator: '',
      organization: '',
      contacts: '',
      lifeScenarioName: '',
      keyTechnologyName: '',
      hardSkills: [],
      softSkills: [],
      roles: rolesList,
      teamsSize: null,
      teamLimit: null,
    })
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

  const addHardCompetency = (competency?: CompetencyModel) => {
    if (competency) {
      const competencyToAdd: CompetencyModel = {
        ...competency,
      }

      addCompetency(competencyToAdd, setHardSkillsList, hardSkillsList)
    }
  }

  const addSoftCompetency = (competency?: CompetencyModel) => {
    if (competency) {
      const competencyToAdd: CompetencyModel = {
        ...competency,
      }

      addCompetency(competencyToAdd, setSoftSkillsList, softSkillsList)
    }
  }

  const addCompetency = (competency: CompetencyModel, setState: React.Dispatch<React.SetStateAction<CompetencyModel[]>>, skills: CompetencyModel[]) => {
    const newCompetencies = [...skills, competency]

    setState(newCompetencies)
  }

  const handleHardCompetencyDelete = (id: number) => {
    const editedCompetencies = hardSkillsList.filter(item => item.id !== id)

    setHardSkillsList(editedCompetencies)
  }

  const handleSoftCompetencyDelete = (id: number) => {
    const editedCompetencies = softSkillsList.filter(item => item.id !== id)

    setSoftSkillsList(editedCompetencies)
  }

  return (
    <>

      <AppCard className={style.addProjectTitle}>
        <h2>???????????????? ?????????? ????????????</h2>
      </AppCard>

      <AppForm
        initValues={initList}
        onSubmit={handleAddProject}
        onReset={() => {
          setRolesList([])
          setSoftSkillsList([])
          setHardSkillsList([])
        }}
      >
        <div className={style.formContainer}>
          <AppCard className={style.containerInputs}>
            <AppFormTextField
              name='name'
              label='???????????????? ??????????????'
              className={style.input}
            />
            <AppFormTextarea
              name='description'
              label='???????????????? ??????????????'
              className={style.input}
            />
            <AppFormTextarea
              name='purpose'
              label='???????? ??????????????'
              className={style.input}
            />
            <AppFormTextarea
              name='result'
              label='?????????? ??????????????'
              className={style.input}
            />
            <AppFormTextField
              name='curator'
              label='??????????????'
              className={style.input}
            />
            <AppFormTextField
              name='organization'
              label='??????????????????????'
              className={style.input}
            />
            <AppFormTextField
              name='contacts'
              label='????????????????'
              className={style.input}
            />
            <AppFormTextField
              name='lifeScenarioName'
              label='?????????????????? ????????????????'
              className={style.input}
            />
            <AppFormTextField
              name='keyTechnologyName'
              label='???????????????? ????????????????????'
              className={style.input}
            />
            <RoleCard
              selectedItems={rolesList}
              header={'????????'}
              onAdd={(e) => addRole(e)}
              onDelete={() => deleteLastRole()}
            >
            </RoleCard>

            <AppFormTextField
              name='teamsSize'
              type={'number'}
              label='???????????? ??????????????'
              className={style.input}
            />
            <AppFormTextField
              name='teamLimit'
              type={'number'}
              label='?????????? ????????????????????'
              className={style.input}
            />
          </AppCard>
          <div className={style.competency_container}>
            <CompetenciesSkillsCard
              header='Hard Skills'
              className={style.competencyCard}
              selectedItems={hardSkillsList}
              competenciesList={hardCompetencies}
              onChange={addHardCompetency}
              onDelete={handleHardCompetencyDelete}
            />

            <CompetenciesSkillsCard
              header='Soft Skills'
              className={style.competencyCard}
              selectedItems={softSkillsList}
              competenciesList={softCompetencies}
              onChange={addSoftCompetency}
              onDelete={handleSoftCompetencyDelete}
            />
          </div>
        </div>
        <AppCard
          className={style.addCard}
          header={'???????????????? ????????????'}
        >
          <p>???????????? ?? ???????????????????? ?????????????????????? ?????????? ???????????????? ???? ????????????:</p>
          <AppButton
            type='submit'
            className={style.submitForm}
          >
            ???????????????? ????????????
          </AppButton>
          <AppButton
            type='reset'
            buttonType={'secondary'}
            className={style.submitForm}
          >
            ????????????????
          </AppButton>
        </AppCard>
      </AppForm>
    </>
  )
}

export default AddProjectLayout
