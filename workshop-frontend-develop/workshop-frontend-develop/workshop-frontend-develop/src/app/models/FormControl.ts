export interface FormControl<TControl> {
  label: string;

  dataKey: keyof TControl & string;

  inputType: string;

  items?: any[];

  validators?: ((value: any) => false | string)[];
}
