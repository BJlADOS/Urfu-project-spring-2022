import * as React from 'react'
import { motion, AnimatePresence } from 'framer-motion'

import style from './style.scss'

interface Props {
  children: React.ReactNode
  content: React.ReactElement
}

export const AppTooltip: React.FC<Props> = ({ children, content, ...rest }) => {
  const [visible, setVisible] = React.useState(false)
  const variants = {
    hidden: { opacity: 0 },
    visible: { opacity: 1 },
  }

  return (
    <div className={style.wrapper}>
      <AnimatePresence>
        <motion.div
          transition={{ duration: 0.3 }}
          initial={{ opacity: 0 }}
          animate={visible ? 'visible' : 'hidden'}
          variants={variants}
          className={style.tooltip}
          {...rest}
        >
          {content}
        </motion.div>
      </AnimatePresence>
      <div
        onMouseEnter={() => setVisible(true)}
        onMouseLeave={() => setVisible(false)}
      >
        {children}
      </div>
    </div>
  )
}
