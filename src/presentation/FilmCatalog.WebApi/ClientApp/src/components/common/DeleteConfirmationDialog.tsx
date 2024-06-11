import { Button, Group, Modal, Stack, Text } from "@mantine/core";
import { AlertCircle } from "tabler-icons-react";
import { ErrorMessage } from "./ErrorMessage";

export interface DeleteConfirmationRequest {
  id: number;
  prompt: string;
  errors?: Record<string, string>;
}

interface DeleteConfirmationDialogProps {
  request: DeleteConfirmationRequest | null;
  performDeleteHandler: () => void;
  cancelDeleteHandler: () => void;
}

export function DeleteConfirmationDialog({
  request,
  performDeleteHandler,
  cancelDeleteHandler,
}: DeleteConfirmationDialogProps) {
  return (
    <Modal
      opened={!!request}
      withCloseButton={false}
      closeOnClickOutside={false}
      onClose={() => {}}
      title="Delete confirmation"
    >
      {request && (
        <Stack>
          {request.errors ? (
            <ErrorMessage
              errors={request.errors}
              closeHandler={cancelDeleteHandler}
            />
          ) : (
            <>
              <Group>
                <AlertCircle size={32} color="red" />
                <Text>{request.prompt}</Text>
              </Group>
              <Group position="center">
                <Button onClick={performDeleteHandler}>Delete</Button>
                <Button onClick={cancelDeleteHandler}>Cancel</Button>
              </Group>
            </>
          )}
        </Stack>
      )}
    </Modal>
  );
}
