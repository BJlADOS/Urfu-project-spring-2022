import React from 'react'

import styles from './styles.scss'

export const IntroductionSection: React.FC = () => (
    <div className={styles.introductionSection}>
        <h2>Добро пожаловать в сервис <span className={styles.textHighlight}>ПроКомпетенции</span></h2>
        <p>
            Сервис для выбора проектов на основе
            <span className={styles.textHighlight}> Жизненного сценария </span>
            и
            <span className={styles.textHighlight}> Ключевой технологии </span>
        </p>
    </div>
)
