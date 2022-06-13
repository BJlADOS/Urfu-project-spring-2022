import React from 'react'
import coursesData from 'assets/data/courses.json'

import styles from './styles.scss'

interface Course {
  name: string
}

interface SectionProps {
  selctedEventId: number
}

export const CoursesSection: React.FC<SectionProps> = ({
  selctedEventId,
}) => (
  <>
    <h2>Направления</h2>
    <div className={styles.flexContainer}>
      {coursesData[selctedEventId].map((item: Course) => (
        <CourseItem
          key={item.name}
          data={item}
        />
      ))}
    </div>
  </>
)

interface ItemProps {
  data: Course
}

const CourseItem: React.FC<ItemProps> = ({
  data: {
    name,
  },
}) => (
  <div className={styles.courseItem}>
    <h3>{name}</h3>
  </div>
)
