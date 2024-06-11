import {
  Alert,
  Button,
  Group,
  Modal,
  NumberInput,
  Select,
  Stack,
  TextInput,
  Textarea,
} from "@mantine/core";
import { useForm } from "@mantine/form";
import { AlertCircle } from "tabler-icons-react";
import {
  FilmFormProps,
  toFilmCommand,
  toFilmFormProps,
  useCreateFilm,
  useGetFilm,
  useUpdateFilm,
} from "../api/film";
import { extractErrors } from "../helpers/form";
import { ErrorMessage } from "./common/ErrorMessage";
import { FormSkeleton } from "./common/FormSkeleton";
import { AddEditDialogProps, AddEditFormProps, AddEditProps } from "./types";
import { DetailTable } from "./common/DetailTable";
import { useGetAllTags } from "../api/tag";
import { useGetAllGenres } from "../api/genre";
import { useGetAllPersons } from "../api/person";

export function AddEditFilmDialog({ id, doneHandler }: AddEditDialogProps) {
  return (
    <Modal
      opened={id !== null}
      withCloseButton={false}
      closeOnClickOutside={false}
      onClose={() => void 0}
      title={id !== 0 ? "Edit film" : "Add film"}
      size="980"
    >
      {id !== null && <AddEditFilm {...{ id, doneHandler }} />}
    </Modal>
  );
}

function AddEditFilm({ id, doneHandler }: AddEditProps) {
  const { data, isLoading, error } = useGetFilm(id);
  if (!data) {
    if (isLoading) {
      return <FormSkeleton rows={1} cols={1} buttons={2} />;
    }
    return (
      <ErrorMessage errors={extractErrors(error)} closeHandler={doneHandler} />
    );
  }

  const initialValues = toFilmFormProps(data);
  return <AddEditFilmForm {...{ initialValues, doneHandler }} />;
}

function AddEditFilmForm({
  initialValues,
  doneHandler,
}: AddEditFormProps<FilmFormProps>) {
  const form = useForm({
    initialValues: initialValues,

    validate: {},
  });

  const mutationAdd = useCreateFilm();

  const mutationUpdate = useUpdateFilm();

  const { data: genres } = useGetAllGenres("", 1, 1000);

  const genreSelectData =
    genres?.items.map((x) => ({
      value: x.id.toString(),
      label: x.name,
    })) || [];

  const { data: tags } = useGetAllTags("", 1, 1000);

  const tagSelectData =
    tags?.items.map((x) => ({
      value: x.id.toString(),
      label: x.name,
    })) || [];

  const { data: directors } = useGetAllPersons("", true, false, false, 1, 1000);

  const directorSelectData =
    directors?.items.map((x) => ({
      value: x.id.toString(),
      label: `${x.firstName} ${x.lastName}`,
    })) || [];

  const { data: producers } = useGetAllPersons("", false, true, false, 1, 1000);

  const producerSelectData =
    producers?.items.map((x) => ({
      value: x.id.toString(),
      label: `${x.firstName} ${x.lastName}`,
    })) || [];

  const { data: actors } = useGetAllPersons("", false, false, true, 1, 1000);

  const actorSelectData =
    actors?.items.map((x) => ({
      value: x.id.toString(),
      label: `${x.firstName} ${x.lastName}`,
    })) || [];

  const onSave = async (formValues: typeof form.values) => {
    try {
      if (initialValues.id === 0) {
        //add
        await mutationAdd.mutateAsync(toFilmCommand(formValues));
      } else {
        //update
        await mutationUpdate.mutateAsync(toFilmCommand(formValues));
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
            label="Title"
            placeholder=""
            required
            {...form.getInputProps("title")}
          />
          <Textarea
            label="Description"
            placeholder=""
            {...form.getInputProps("description")}
          />
          <TextInput
            label="Poster URL"
            placeholder=""
            type="url"
            {...form.getInputProps("posterUrl")}
          />
        </Stack>

        <Group position="apart" align="flex-start">
          <DetailTable<{ id: number }, typeof form>
            sx={{ height: 220 }}
            form={form}
            columns={[
              {
                key: "id",
                header: "Genre",
                formatter: (_, index) => (
                  <Select
                    data={genreSelectData.filter(
                      (x) =>
                        x.value === form.values.genres[index] ||
                        !form.values.genres.some((y) => y === x.value)
                    )}
                    {...form.getInputProps(`genres.${index}`)}
                  />
                ),
              },
            ]}
            rows={form.values.genres.map((x) => ({ id: parseInt(x) }))}
            onAdd={function (): void {
              form.insertListItem("genres", "0");
            }}
            onDelete={(_, index) => form.removeListItem("genres", index)}
            error={undefined}
            notFoundMessage="No genres found"
          />

          <DetailTable<{ id: number }, typeof form>
            sx={{ height: 220 }}
            form={form}
            columns={[
              {
                key: "id",
                header: "Tag",
                formatter: (_, index) => (
                  <Select
                    data={tagSelectData.filter(
                      (x) =>
                        x.value === form.values.tags[index] ||
                        !form.values.tags.some((y) => y === x.value)
                    )}
                    {...form.getInputProps(`tags.${index}`)}
                  />
                ),
              },
            ]}
            rows={form.values.tags.map((x) => ({ id: parseInt(x) }))}
            onAdd={function (): void {
              form.insertListItem("tags", "0");
            }}
            onDelete={(_, index) => form.removeListItem("tags", index)}
            error={undefined}
            notFoundMessage="No tags found"
          />

          <Stack>
            <TextInput
              label="Year Released"
              placeholder=""
              type="number"
              min={1850}
              max={2100}
              {...form.getInputProps("year")}
            />

            <TextInput
              label="Duration (minutes)"
              placeholder=""
              type="number"
              min={0}
              max={1000}
              {...form.getInputProps("durationInMinutes")}
            />
          </Stack>
        </Group>

        <Group position="apart" align="flex-start">
          <DetailTable<{ id: number }, typeof form>
            sx={{ height: 220 }}
            form={form}
            columns={[
              {
                key: "id",
                header: "Director",
                formatter: (_, index) => (
                  <Select
                    data={directorSelectData.filter(
                      (x) =>
                        x.value === form.values.directors[index] ||
                        !form.values.directors.some((y) => y === x.value)
                    )}
                    {...form.getInputProps(`directors.${index}`)}
                  />
                ),
              },
            ]}
            rows={form.values.directors.map((x) => ({ id: parseInt(x) }))}
            onAdd={function (): void {
              form.insertListItem("directors", "0");
            }}
            onDelete={(_, index) => form.removeListItem("directors", index)}
            error={undefined}
            notFoundMessage="No directors found"
          />

          <DetailTable<{ id: number }, typeof form>
            sx={{ height: 220 }}
            form={form}
            columns={[
              {
                key: "id",
                header: "Producer",
                formatter: (_, index) => (
                  <Select
                    data={producerSelectData.filter(
                      (x) =>
                        x.value === form.values.producers[index] ||
                        !form.values.producers.some((y) => y === x.value)
                    )}
                    {...form.getInputProps(`producers.${index}`)}
                  />
                ),
              },
            ]}
            rows={form.values.producers.map((x) => ({ id: parseInt(x) }))}
            onAdd={function (): void {
              form.insertListItem("producers", "0");
            }}
            onDelete={(_, index) => form.removeListItem("producers", index)}
            error={undefined}
            notFoundMessage="No producers found"
          />

          <DetailTable<{ id: number }, typeof form>
            sx={{ height: 220 }}
            form={form}
            columns={[
              {
                key: "id",
                header: "Actor",
                formatter: (_, index) => (
                  <Select
                    data={actorSelectData.filter(
                      (x) =>
                        x.value === form.values.actors[index] ||
                        !form.values.actors.some((y) => y === x.value)
                    )}
                    {...form.getInputProps(`actors.${index}`)}
                  />
                ),
              },
            ]}
            rows={form.values.actors.map((x) => ({ id: parseInt(x) }))}
            onAdd={function (): void {
              form.insertListItem("actors", "0");
            }}
            onDelete={(_, index) => form.removeListItem("actors", index)}
            error={undefined}
            notFoundMessage="No actors found"
          />
        </Group>

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
