import React from 'react'
import * as Yup from 'yup'
import { Formik, Form } from 'formik'

interface Props<T> {
  initValues: T;
  className?: string
  children?: React.ReactNode
  validationSchema?: Yup.ObjectSchema;
  onSubmit: (item: Record<keyof T & string, any>) => void;
  onReset? : (item: Record<keyof T & string, any>) => void;
}

export const AppForm = <T extends Record<keyof T, any>>({
  initValues,
  className,
  children,
  validationSchema,
  onSubmit,
  onReset,
}: Props<T>) => onReset ? (
    <Formik
    initialValues={initValues}
      onSubmit={onSubmit}
      validationSchema={validationSchema}
      onReset={onReset}
    >
      <Form className={className}>
        {children}
      </Form>
    </Formik>
  ) : (
    <Formik
    initialValues={initValues}
    onSubmit={onSubmit}
    validationSchema={validationSchema}
    >
      <Form className={className}>
        {children}
      </Form>
    </Formik>)
