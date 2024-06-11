export interface AddEditHandlers {
  doneHandler: () => void;
}

export interface AddEditProps extends AddEditHandlers {
  id: number;
}

export interface AddEditDialogProps extends AddEditHandlers {
  id: number | null;
}

export interface AddEditFormProps<TAddFormProps, TEditFormProps = {}>
  extends AddEditHandlers {
  initialValues: TAddFormProps & TEditFormProps;
}
