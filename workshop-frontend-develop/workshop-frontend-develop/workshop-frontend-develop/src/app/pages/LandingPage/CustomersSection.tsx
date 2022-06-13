import React, { useRef } from 'react'
import customerData from 'assets/data/customers.json'

import styles from './styles.scss'

interface Customer {
  name: string
  imgLink?: string
}

interface SectionProps {
  selctedEventId: number
}

export const CustomersSection: React.FC<SectionProps> = ({
  selctedEventId,
}) => (
  <div className={styles.customersSection}>
    <h2>Заказчики</h2>
    <div className={styles.flexContainer}>
      {customerData[selctedEventId].map((item: Customer) =>
        <CustomerItem
          key={item.name}
          data={item}
        />,
      )}
      <h3>и другие...</h3>
    </div>
  </div>
)

interface ItemProps {
  data: Customer
}

const CustomerItem: React.FC<ItemProps> = ({
  data: {
    name,
    imgLink,
  },
}) => {
  const cutomerNameElem = useRef<HTMLDivElement>(null)

  const handleImgLoadError = (event: React.SyntheticEvent<HTMLImageElement, Event>): void => {
    event.currentTarget.style.display = 'none'
    cutomerNameElem.current?.classList.add(styles.alwaysShow)
  }

  return (
    <div className={styles.customerImgWrap}>
      <img
        src={imgLink}
        alt={name}
        onError={handleImgLoadError}
      />
      <div
        title={name}
        className={styles.customerName}
        ref={cutomerNameElem}
      >
        <span>{name}</span>
      </div>
    </div>
  )
}
