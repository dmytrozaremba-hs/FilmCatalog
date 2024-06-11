import { useState } from "react";
import { extractErrors } from "../../helpers/form";
import { DeleteConfirmationRequest } from "./DeleteConfirmationDialog";

export function useDeleteConfirmationDialog(
  deleteHandler: (id: number) => Promise<unknown>
) {
  const [deleteConfirmationRequest, setDeleteConfirmationRequest] =
    useState<DeleteConfirmationRequest | null>(null);

  const askForDeleteConfirmation = (id: number, prompt: string) => {
    setDeleteConfirmationRequest({ id, prompt });
  };

  const performDelete = async () => {
    if (deleteConfirmationRequest) {
      const { id } = deleteConfirmationRequest;

      try {
        await deleteHandler(id);

        setDeleteConfirmationRequest(null);
      } catch (e) {
        setDeleteConfirmationRequest({
          ...deleteConfirmationRequest,
          errors: extractErrors(e),
        });
      }
    }
  };

  const cancelDelete = () => {
    setDeleteConfirmationRequest(null);
  };

  return {
    deleteConfirmationRequest,
    askForDeleteConfirmation,
    performDelete,
    cancelDelete,
  };
}
