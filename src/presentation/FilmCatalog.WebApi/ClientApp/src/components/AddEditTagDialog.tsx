import { Alert, Button, Group, Modal, Stack, TextInput } from "@mantine/core";
import { useForm } from "@mantine/form";
import { AlertCircle } from "tabler-icons-react";
import {
  CreateTagCommand,
  UpdateTagCommand,
  useCreateTag,
  useGetTag,
  useUpdateTag,
} from "../api/tag";
import { extractErrors } from "../helpers/form";
import { ErrorMessage } from "./common/ErrorMessage";
import { FormSkeleton } from "./common/FormSkeleton";
import { AddEditDialogProps, AddEditFormProps, AddEditProps } from "./types";

export function AddEditTagDialog({ id, doneHandler }: AddEditDialogProps) {
  return (
    <Modal
      opened={id !== null}
      withCloseButton={false}
      closeOnClickOutside={false}
      onClose={() => void 0}
      title={id !== 0 ? "Edit tag" : "Add tag"}
    >
      {id !== null && <AddEditTag {...{ id, doneHandler }} />}
    </Modal>
  );
}

function AddEditTag({ id, doneHandler }: AddEditProps) {
  const { data, isLoading, error } = useGetTag(id);
  if (!data) {
    if (isLoading) {
      return <FormSkeleton rows={1} cols={1} buttons={2} />;
    }
    return (
      <ErrorMessage errors={extractErrors(error)} closeHandler={doneHandler} />
    );
  }
  const initialValues = { ...data };
  return <TagForm {...{ initialValues, doneHandler }} />;
}

type TagFormProps = AddEditFormProps<CreateTagCommand, UpdateTagCommand>;

function TagForm({ initialValues, doneHandler }: TagFormProps) {
  const form = useForm({
    initialValues: initialValues,

    validate: {},
  });

  const mutationAdd = useCreateTag();

  const mutationUpdate = useUpdateTag();

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
            label="Назва"
            placeholder=""
            required
            {...form.getInputProps("name")}
          />
        </Stack>
        {form.errors[""] && (
          <Alert
            icon={<AlertCircle size={16} />}
            title="Помилка"
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
