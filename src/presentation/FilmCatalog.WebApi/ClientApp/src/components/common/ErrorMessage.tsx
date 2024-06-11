import { Button, Group, Stack, Text } from "@mantine/core";
import { CircleX } from "tabler-icons-react";

export function ErrorMessage({
  errors = { "": "Error!" },
  closeHandler,
}: {
  errors?: string | Record<string, string>;
  closeHandler?: () => void;
}) {
  return (
    <Stack>
      <Group>
        <CircleX size={32} color="red" />
        <Stack>
          {typeof errors === "string" ? (
            <Text>{errors}</Text>
          ) : (
            Object.entries(errors).map(([_, v], i) => <Text key={i}>{v}</Text>)
          )}
        </Stack>
      </Group>
      {closeHandler && (
        <Button mt="xl" onClick={closeHandler}>
          Close
        </Button>
      )}
    </Stack>
  );
}
