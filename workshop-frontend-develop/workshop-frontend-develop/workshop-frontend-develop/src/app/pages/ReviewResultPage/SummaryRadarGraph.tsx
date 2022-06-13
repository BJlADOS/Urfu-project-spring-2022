import React from 'react'
import { AppCard } from 'app/components/AppCard'
import { ResponsiveRadar } from '@nivo/radar'
import { AverageReviewModel } from 'app/models'

import style from './style.scss'

interface Props {
  data: AverageReviewModel[]
}

export const SummaryRadarGraph: React.FC<Props> = ({ data }) => (
  <AppCard
    className={style.radarGraphCol}
    contentClassName={style.radarGraph}
    header={'Суммарная оценка по каждому критерию'}
  >
    <ResponsiveRadar
      data={data}
      keys={['value']}
      indexBy='criterion'
      maxValue='auto'
      margin={{ top: 32, right: 0, bottom: 32, left: 0 }}
      colors={['var(--primary-light)']}
      gridLevels={4}
      gridLabelOffset={16}
      enableDots={true}
      dotSize={8}
      dotLabelYOffset={-6}
      enableDotLabel={true}
      animate={true}
      isInteractive={false}
      gridShape={'linear'}
      theme={{
        fontFamily: 'var(--content-font-family)',
        fontSize: 12,
        textColor: 'var(--text-main)',
        dots: {
          text: {
            fontSize: 16,
            fontWeight: 700,
          },
        },
        grid: {
          line: {
            stroke: 'var(--contrast-dark-faint-color)',
            strokeWidth: 1,
          },
          stroke: 'var(--contrast-dark-faint-color)',
          strokeWidth: 1,
        } as any,
        tooltip: {
          container: {
            color: 'var(--text-main)',
            backgroundColor: 'var(--bg-secondary)',
          },
        },
      }}
    />
  </AppCard>
)
