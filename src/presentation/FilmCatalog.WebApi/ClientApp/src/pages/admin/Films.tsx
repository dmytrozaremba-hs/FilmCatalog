import { useState } from "react";
import { Stack } from "@mantine/core";
import {
  FilmDto,
  useGetAllFilms,
  useDeleteFilm,
  FilmBriefDto,
} from "../../api/film";

import { ListTopBar } from "../../components/common/ListTopBar";
import { DeleteConfirmationDialog } from "../../components/common/DeleteConfirmationDialog";
import { AddEditFilmDialog } from "../../components/AddEditFilmDialog";

import { ListResultTable } from "../../components/common/ListResultTable";
import { useAddEditDialog } from "../../components/common/useAddEditDialog";
import { useDeleteConfirmationDialog } from "../../components/common/useDeleteConfirmationDialog";

function Films() {
  const [search, setSearch] = useState("");
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(20);

  const { data, error } = useGetAllFilms(
    search,
    [],
    [],
    0,
    0,
    0,
    5,
    pageNumber,
    pageSize
  );

  // --
  // add/edit
  const { editId, showAdd, showEdit, doneEdit } =
    useAddEditDialog<FilmBriefDto>();
  // --

  // --
  // delete
  const mutationDelete = useDeleteFilm();

  const {
    deleteConfirmationRequest,
    askForDeleteConfirmation,
    performDelete,
    cancelDelete,
  } = useDeleteConfirmationDialog(mutationDelete.mutateAsync);
  // --

  return (
    <div>
      <Stack>
        <ListTopBar
          search={search}
          onSearchChange={setSearch}
          pageSize={pageSize}
          onPageSizeChange={setPageSize}
          addButtonCaption="Add new film"
          onShowAdd={showAdd}
        />
        <ListResultTable<FilmBriefDto>
          columns={[
            { key: "title", header: "Name" },
            { key: "year", header: "Release Year" },
            { key: "durationInMinutes", header: "Duration (m)" },
          ]}
          rows={data?.items}
          onEdit={showEdit}
          onDelete={({ id, title }) =>
            askForDeleteConfirmation(id, `Delete Film ${title}?`)
          }
          totalPages={data?.totalPages}
          pageNumber={pageNumber}
          setPageNumber={setPageNumber}
          error={error}
        />
      </Stack>
      <DeleteConfirmationDialog
        request={deleteConfirmationRequest}
        performDeleteHandler={performDelete}
        cancelDeleteHandler={cancelDelete}
      />
      <AddEditFilmDialog id={editId} doneHandler={doneEdit} />
    </div>
  );
}

export default Films;
