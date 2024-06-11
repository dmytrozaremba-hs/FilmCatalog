import {
  Alert,
  Button,
  Checkbox,
  Group,
  Modal,
  Stack,
  TextInput,
} from "@mantine/core";
import { useForm } from "@mantine/form";
import { AlertCircle } from "tabler-icons-react";
import {
  CreatePersonCommand,
  UpdatePersonCommand,
  useCreatePerson,
  useGetPerson,
  useUpdatePerson,
} from "../api/person";
import { extractErrors } from "../helpers/form";
import { ErrorMessage } from "./common/ErrorMessage";
import { FormSkeleton } from "./common/FormSkeleton";
import { AddEditDialogProps, AddEditFormProps, AddEditProps } from "./types";
import { DatePickerInput } from "@mantine/dates";
import { toDate, toSerializedDate } from "../helpers/dateTime";

export function AddEditPersonDialog({ id, doneHandler }: AddEditDialogProps) {
  return (
    <Modal
      opened={id !== null}
      withCloseButton={false}
      closeOnClickOutside={false}
      onClose={() => void 0}
      title={id !== 0 ? "Edit person" : "Add person"}
    >
      {id !== null && <AddEditPerson {...{ id, doneHandler }} />}
    </Modal>
  );
}

function AddEditPerson({ id, doneHandler }: AddEditProps) {
  const { data, isLoading, error } = useGetPerson(id);
  if (!data) {
    if (isLoading) {
      return <FormSkeleton rows={1} cols={1} buttons={2} />;
    }
    return (
      <ErrorMessage errors={extractErrors(error)} closeHandler={doneHandler} />
    );
  }
  const initialValues = { ...data };
  return <PersonForm {...{ initialValues, doneHandler }} />;
}

type PersonFormProps = AddEditFormProps<
  CreatePersonCommand,
  UpdatePersonCommand
>;

function PersonForm({ initialValues, doneHandler }: PersonFormProps) {
  const form = useForm({
    initialValues: {
      ...initialValues,
      birthDate: toDate(initialValues.birthDate),
    },

    validate: {},
  });

  const mutationAdd = useCreatePerson();

  const mutationUpdate = useUpdatePerson();

  const onSave = async (formValues: typeof form.values) => {
    try {
      if (initialValues.id === 0) {
        //add
        await mutationAdd.mutateAsync({
          ...formValues,
          birthDate: toSerializedDate(formValues.birthDate),
        });
      } else {
        //update
        await mutationUpdate.mutateAsync({
          ...formValues,
          birthDate: toSerializedDate(formValues.birthDate),
        });
      }
      doneHandler();
    } catch (e) {
      form.setErrors(extractErrors(e));
    }
  };

  return (
    <form onSubmit={form.onSubmit(onSave)}>
      <Stack>
        <Stack>
          <TextInput
            label="First Name"
            placeholder=""
            required
            {...form.getInputProps("firstName")}
          />
          <TextInput
            label="Last Name"
            placeholder=""
            required
            {...form.getInputProps("lastName")}
          />
          <TextInput
            label="Middle Name"
            placeholder=""
            {...form.getInputProps("middleName")}
          />
          <DatePickerInput
            dropdownType="modal"
            label="Birth Date"
            placeholder=""
            required
            valueFormat="D MMMM YYYY"
            clearable={false}
            {...form.getInputProps("birthDate")}
          />
          <Checkbox
            label="Is Director"
            {...form.getInputProps("isDirector", { type: "checkbox" })}
          />
          <Checkbox
            label="Is Producer"
            {...form.getInputProps("isProducer", { type: "checkbox" })}
          />
          <Checkbox
            label="Is Actor"
            {...form.getInputProps("isActor", { type: "checkbox" })}
          />
        </Stack>

        {form.errors[""] && (
          <Alert
            icon={<AlertCircle size={16} />}
            title="Error"
            color="red"
            mt="md"
          >
            {form.errors[""]}
          </Alert>
        )}
        <Group position="center">
          <Button type="submit">Save</Button>
          <Button onClick={doneHandler}>Cancel</Button>
        </Group>
      </Stack>
    </form>
  );
}
