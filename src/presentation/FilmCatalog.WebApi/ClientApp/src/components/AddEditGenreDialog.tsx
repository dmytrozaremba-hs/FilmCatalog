import { Alert, Button, Group, Modal, Stack, TextInput } from "@mantine/core";
import { useForm } from "@mantine/form";
import { AlertCircle } from "tabler-icons-react";
import {
  CreateGenreCommand,
  UpdateGenreCommand,
  useCreateGenre,
  useGetGenre,
  useUpdateGenre,
} from "../api/genre";
import { extractErrors } from "../helpers/form";
import { ErrorMessage } from "./common/ErrorMessage";
import { FormSkeleton } from "./common/FormSkeleton";
import { AddEditDialogProps, AddEditFormProps, AddEditProps } from "./types";

export function AddEditGenreDialog({ id, doneHandler }: AddEditDialogProps) {
  return (
    <Modal
      opened={id !== null}
      withCloseButton={false}
      closeOnClickOutside={false}
      onClose={() => void 0}
      title={id !== 0 ? "Edit genre" : "Add genre"}
    >
      {id !== null && <AddEditGenre {...{ id, doneHandler }} />}
    </Modal>
  );
}

function AddEditGenre({ id, doneHandler }: AddEditProps) {
  const { data, isLoading, error } = useGetGenre(id);
  if (!data) {
    if (isLoading) {
      return <FormSkeleton rows={1} cols={1} buttons={2} />;
    }
    return (
      <ErrorMessage errors={extractErrors(error)} closeHandler={doneHandler} />
    );
  }
  const initialValues = { ...data };
  return <GenreForm {...{ initialValues, doneHandler }} />;
}

type GenreFormProps = AddEditFormProps<CreateGenreCommand, UpdateGenreCommand>;

function GenreForm({ initialValues, doneHandler }: GenreFormProps) {
  const form = useForm({
    initialValues: initialValues,

    validate: {},
  });

  const mutationAdd = useCreateGenre();

  const mutationUpdate = useUpdateGenre();

  const onSave = async (formValues: typeof form.values) => {
    try {
      if (initialValues.id === 0) {
        //add
        await mutationAdd.mutateAsync(formValues);
      } else {
        //update
        await mutationUpdate.mutateAsync(formValues);
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
            label="Name"
            placeholder=""
            required
            {...form.getInputProps("name")}
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
