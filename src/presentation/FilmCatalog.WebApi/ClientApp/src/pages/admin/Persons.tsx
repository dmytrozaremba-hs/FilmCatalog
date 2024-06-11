import { useState } from "react";
import { Stack } from "@mantine/core";
import {
  useGetAllPersons,
  useDeletePerson,
  PersonBriefDto,
} from "../../api/person";

import { ListTopBar } from "../../components/common/ListTopBar";
import { DeleteConfirmationDialog } from "../../components/common/DeleteConfirmationDialog";
import { AddEditPersonDialog } from "../../components/AddEditPersonDialog";

import { ListResultTable } from "../../components/common/ListResultTable";
import { useAddEditDialog } from "../../components/common/useAddEditDialog";
import { useDeleteConfirmationDialog } from "../../components/common/useDeleteConfirmationDialog";
import { dateFormatter } from "../../helpers/dateTime";

function Persons() {
  const [search, setSearch] = useState("");
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(20);

  const { data, error } = useGetAllPersons(
    search,
    false,
    false,
    false,
    pageNumber,
    pageSize
  );

  // --
  // add/edit
  const { editId, showAdd, showEdit, doneEdit } =
    useAddEditDialog<PersonBriefDto>();
  // --

  // --
  // delete
  const mutationDelete = useDeletePerson();

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
          addButtonCaption="Add new person"
          onShowAdd={showAdd}
        />
        <ListResultTable<PersonBriefDto>
          columns={[
            { key: "firstName", header: "First Name" },
            { key: "lastName", header: "Last Name" },
            { key: "middleName", header: "Middle Name" },
            {
              key: "birthDate",
              header: "Birth Date",
              formatter: dateFormatter,
            },
          ]}
          rows={data?.items}
          onEdit={showEdit}
          onDelete={({ id, firstName, lastName }) =>
            askForDeleteConfirmation(
              id,
              `Delete Person ${firstName} ${lastName}?`
            )
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
      <AddEditPersonDialog id={editId} doneHandler={doneEdit} />
    </div>
  );
}

export default Persons;
