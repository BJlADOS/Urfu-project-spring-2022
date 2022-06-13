import React from 'react'

import styles from './styles.scss'

export const ContactsSection: React.FC = () => (
  <>
    <h2>Наши контакты</h2>
    <div className={styles.flexContainer}>
      <a
        href='https://rtf.urfu.ru/ru/'
        target='_blank'
        rel='noopener noreferrer'
      >
        <img
          src='assets/img/logos/png/ИРИТ-РТФ.png'
          alt='rtf'
          className={styles.rtfLogo}
        />
      </a>
      <a
        href='https://info.urfu.ru/ru/'
        target='_blank'
        rel='noopener noreferrer'
      >
        <img
          src='assets/img/logos/png/ИнФо.png'
          alt='info'
          className={styles.infoLogo}
        />
      </a>
    </div>
    <h3>Следите за новостями Проектного Практикума в группе ВКонтакте:</h3>
    <a
      className={styles.flexContainer}
      href='https://vk.com/project__it'
      target='_blank'
      rel='noopener noreferrer'
    >
      <img
        className={styles.vkGroupLogo}
        src='assets/img/logos/png/ПП логотип.png'
        alt='vk'
      />
    </a>
  </>
)
