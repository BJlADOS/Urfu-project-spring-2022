import React, { useState } from 'react'
import { connect } from 'react-redux'
import { GetAppRounded, PublishRounded } from '@material-ui/icons'
import { AppCard } from 'app/components/AppCard'
import { AppButton } from 'app/components/AppButton'
import { admin } from 'app/provider'
import { RootState } from 'app/reducers'
import { handleDownloadResponse } from 'app/utils/handleDownloadResponse'

import style from './style.scss'
import { ScenarioTechnologyEditSection } from './ScenarioTechnologyEditSection'

const mapStateToProps = (state: RootState) => ({ profile: state.profile })

type StateProps = ReturnType<typeof mapStateToProps>

type Props = StateProps

const UploadProjectsPageComponent: React.FC<Props> = ({
  profile,
}) => {
  const [fileToUpload, setFileToUpload] = useState<File>()
  const [reset, setReset] = useState(false)
  const [close, setClose] = useState(false)
  const [isLoadingSuccusfull, setIsLoadingSuccusfull] = useState<boolean>()
  const [isOpenProjectSuccussfull, setisOpenProjectSuccussfull] = useState<boolean>()

  const handleFileUpload = () => {
    // TODO: Remade form using Formik.
    if (fileToUpload && profile.profile) {
      const form = new FormData()

      form.append('file', fileToUpload)
      form.append('resetProjects', reset.toString())
      form.append('closeProjects', close.toString())
      form.append('eventId', profile.profile.eventId.toString())

      admin.uploadProjects(form)
        .then(() => {
          setIsLoadingSuccusfull(true)
        })
        .catch((error) => {
          console.error(error)
          setIsLoadingSuccusfull(false)
        })
    }
  }

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { target } = event

    if (target.files) {
      setFileToUpload(target.files[0])
    }

    setIsLoadingSuccusfull(undefined)
  }

  const downloadTemplate = () => {
    admin.downloadTemplateTable()
      .then(handleDownloadResponse)
  }

  const downloadStudentsProjects = () => {
    admin.getStudentsProjects()
      .then(handleDownloadResponse)
  }

  const header = (
    <div className={style.header}>
      <h3>
        Новые проекты
      </h3>
      {isLoadingSuccusfull === true && <span className={style.success}>Проекты загружены</span>}
      {isLoadingSuccusfull === false && <span className={style.error}>Ошибка</span>}
    </div>

  )

  return (
    <>
      <AppCard>
        <h2>Загрузить проекты</h2>
      </AppCard>
      <AppCard
        className={style.card}
        header='Инструкция'
      >
        <p>
          Чтобы добавить новые проекты в систему, вам нужно загрузить специальный Excel-файл с данными.
        </p>
        <AppButton
          icon={<GetAppRounded/>}
          className={style.downloadTemplate}
          type='button'
          onClick={downloadTemplate}
        >
          Скачать шаблон
        </AppButton>
      </AppCard>
      <AppCard
        className={style.card}
        header={header}
      >
        <input
          type='file'
          onChange={handleFileChange}
        />
        <label className={style.rewriteProjects}>
          Перезаписать проекты?
          <input
            type='checkbox'
            checked={reset}
            onChange={(e) => setReset(e.target.checked)}
          />
        </label>
        <p>
          Внимание! Если перезаписать данные, то все проекты и команды для выбранного события
          будут <b>удалены</b> из сисетмы.
        </p>
        <label className={style.rewriteProjects}>
          Закрыть запись?
          <input
            type='checkbox'
            checked={close}
            onChange={(e) => setClose(e.target.checked)}
          />
        </label>
        <p>
          При включении данной опции все новые добавленные проекты будут <b>закрыты</b> для записи.
        </p>
        <div className={style.projectButtonsContainer}>
          <AppButton
            icon={<PublishRounded/>}
            className={style.downloadTemplate}
            onClick={handleFileUpload}
            disabled={!fileToUpload}
          >
            Загрузить проекты
          </AppButton>
          <AppButton
            type={'button'}
            buttonType={'secondary'}
            onClick={downloadStudentsProjects}
          >
            Скачать проекты студентов
          </AppButton>
        </div>
        <p className={style.footnote}>
          <i>Примечание:</i> После загрузки проектов необходимо обновить страницу.
        </p>

      </AppCard>
      <AppCard
        header='Управление проектами'
        className={style.card}
      >
        <p>
          Выбранная опция откроет или закроет запись для <b>всех</b> проектов в текущем событии.
        </p>
        <div className={style.projectButtons}>
          <AppButton
            buttonType='primary'
            onClick={() => admin.openProjectForEntry().then(() => setisOpenProjectSuccussfull(true))}
          >
            Открыть запись на проекты
          </AppButton>
          {isOpenProjectSuccussfull && <span className={style.success}>Запись открыта</span>}
        </div>
      </AppCard>
      <ScenarioTechnologyEditSection/>
    </>
  )
}

export const UploadProjectsPage: React.FC = connect(
  mapStateToProps,
)(UploadProjectsPageComponent)
