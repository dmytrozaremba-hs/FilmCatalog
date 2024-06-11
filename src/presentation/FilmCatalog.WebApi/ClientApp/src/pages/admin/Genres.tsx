import { useState } from "react";
import { Stack } from "@mantine/core";
import { GenreDto, useGetAllGenres, useDeleteGenre } from "../../api/genre";

import { ListTopBar } from "../../components/common/ListTopBar";
import { DeleteConfirmationDialog } from "../../components/common/DeleteConfirmationDialog";
import { AddEditGenreDialog } from "../../components/AddEditGenreDialog";

import { ListResultTable } from "../../components/common/ListResultTable";
import { useAddEditDialog } from "../../components/common/useAddEditDialog";
import { useDeleteConfirmationDialog } from "../../components/common/useDeleteConfirmationDialog";

function Genres() {
  const [search, setSearch] = useState("");
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(20);

  const { data, error } = useGetAllGenres(search, pageNumber, pageSize);

  // --
  // add/edit
  const { editId, showAdd, showEdit, doneEdit } = useAddEditDialog<GenreDto>();
  // --

  // --
  // delete
  const mutationDelete = useDeleteGenre();

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
          addButtonCaption="Add new genre"
          onShowAdd={showAdd}
        />
        <ListResultTable<GenreDto>
          columns={[{ key: "name", header: "Name" }]}
          rows={data?.items}
          onEdit={showEdit}
          onDelete={({ id, name }) =>
            askForDeleteConfirmation(id, `Delete genre ${name}?`)
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
      <AddEditGenreDialog id={editId} doneHandler={doneEdit} />
    </div>
  );
}

export default Genres;
