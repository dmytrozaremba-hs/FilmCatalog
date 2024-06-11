import { Group, Skeleton, Stack } from "@mantine/core";

export function FormSkeleton({
  rows,
  cols,
  buttons,
}: {
  rows: number;
  cols: number;
  buttons: number;
}) {
  return (
    <Stack>
      {Array.from({ length: rows }, (_, i) => i).map((key) => (
        <Group key={key} spacing="xl" grow position="apart">
          {Array.from({ length: cols }, (_, i) => i).map((key) => (
            <div key={key}>
              <Skeleton height={22} width={70} mt={15}></Skeleton>
              <Skeleton height={36} width={200} mt={4}></Skeleton>
            </div>
          ))}
        </Group>
      ))}
      <Group position="center">
        {Array.from({ length: buttons }, (_, i) => i).map((key) => (
          <Skeleton key={key} height={34} width={100} mt={15}></Skeleton>
        ))}
      </Group>
    </Stack>
  );
}
