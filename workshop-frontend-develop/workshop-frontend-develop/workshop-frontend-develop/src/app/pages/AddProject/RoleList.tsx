import React from 'react'
import cls from 'classnames'

import {AppChip} from 'app/components/AppChip'

import style from './style.scss'

interface Props {
    list: string[]
    className?: string
}

export const RoleList: React.FC<Props> = ({
                                              list,
                                              className,
                                          }) => {
    const items = list.map(item => (
        <AppChip
            key={item}
        >
      <span
          className={style.name}
          title={item}
      >
        {item}
      </span>
        </AppChip>
    ))

    const competenciesListStyles = cls([className, style.competenciesList])

    return (
        <div className={competenciesListStyles}>
            {items}
        </div>
    )
}
