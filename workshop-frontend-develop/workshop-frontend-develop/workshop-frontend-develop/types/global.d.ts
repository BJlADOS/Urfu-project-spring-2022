// style loader
declare module '*.css' {
  const styles: any
  export = styles
}

declare module '*.scss' {
  const styles: any
  export = styles
}

declare module '*.json' {
  const value: any
  export default value
}

type PartialPick<T, K extends keyof T> = Partial<T> & Pick<T, K>
