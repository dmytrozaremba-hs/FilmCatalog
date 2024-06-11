import { useState } from "react";
import { Stack } from "@mantine/core";
import { TagDto, useGetAllTags, useDeleteTag } from "../../api/tag";

import { ListTopBar } from "../../components/common/ListTopBar";
import { DeleteConfirmationDialog } from "../../components/common/DeleteConfirmationDialog";
import { AddEditTagDialog } from "../../components/AddEditTagDialog";

import { ListResultTable } from "../../components/common/ListResultTable";
import { useAddEditDialog } from "../../components/common/useAddEditDialog";
import { useDeleteConfirmationDialog } from "../../components/common/useDeleteConfirmationDialog";

function Tags() {
  const [search, setSearch] = useState("");
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(20);

  const { data, error } = useGetAllTags(search, pageNumber, pageSize);

  // --
  // add/edit
  const { editId, showAdd, showEdit, doneEdit } = useAddEditDialog<TagDto>();
  // --

  // --
  // delete
  const mutationDelete = useDeleteTag();

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
          addButtonCaption="Add new tag"
          onShowAdd={showAdd}
        />
        <ListResultTable<TagDto>
          columns={[{ key: "name", header: "Name" }]}
          rows={data?.items}
          onEdit={showEdit}
          onDelete={({ id, name }) =>
            askForDeleteConfirmation(id, `Delete tag ${name}?`)
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
      <AddEditTagDialog id={editId} doneHandler={doneEdit} />
    </div>
  );
}

export default Tags;
