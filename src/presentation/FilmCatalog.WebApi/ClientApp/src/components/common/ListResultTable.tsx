import {
  ActionIcon,
  Container,
  createStyles,
  Group,
  Loader,
  Pagination,
  Table,
} from "@mantine/core";

import { Pencil, Trash } from "tabler-icons-react";
import { extractErrors } from "../../helpers/form";
import { ErrorMessage } from "./ErrorMessage";
import { TableColumn, TableRow } from "./types";

const useStyles = createStyles((theme) => ({
  header: {
    position: "sticky",
    top: 0,
    backgroundColor:
      theme.colorScheme === "dark" ? theme.colors.dark[7] : theme.white,
    transition: "box-shadow 150ms ease",

    "&::after": {
      content: '""',
      position: "absolute",
      left: 0,
      right: 0,
      bottom: 0,
      borderBottom: `1px solid ${
        theme.colorScheme === "dark"
          ? theme.colors.dark[3]
          : theme.colors.gray[2]
      }`,
    },
  },
}));

export function ListResultTable<TRowData>({
  columns,
  rows,
  onEdit,
  onDelete,
  totalPages,
  pageNumber,
  setPageNumber,
  error,
  notFoundMessage = "Not found",
}: {
  columns: TableColumn<TRowData>[];
  rows?: TableRow<TRowData>[];
  onEdit?: (row: TRowData) => void;
  onDelete?: (row: TRowData) => void;
  totalPages?: number;
  pageNumber?: number;
  setPageNumber?: (pageNumber: number) => void;
  error: any;
  notFoundMessage?: string;
}) {
  const { classes, cx } = useStyles();

  return error ? (
    <Container my={40}>
      <ErrorMessage errors={extractErrors(error)} />
    </Container>
  ) : (
    <>
      <Table sx={{ minWidth: 700 }}>
        <thead className={cx(classes.header)}>
          <tr>
            {columns.map((c) => (
              <th key={`${String(c.key)}`}>{c.header}</th>
            ))}
            <th />
          </tr>
        </thead>
        <tbody>
          {rows &&
            rows.length > 0 &&
            rows.map((row, rowIndex) => (
              <tr key={row.id}>
                {columns.map((c) => (
                  <td key={`${String(c.key)}`}>
                    {c.formatter
                      ? c.formatter(row[c.key], rowIndex)
                      : row[c.key]?.toString()}
                  </td>
                ))}
                <td>
                  <Group spacing={0} position="right">
                    {onEdit ? (
                      <ActionIcon
                        onClick={() => onEdit(row as unknown as TRowData)}
                      >
                        <Pencil size={16} />
                      </ActionIcon>
                    ) : null}

                    {onDelete ? (
                      <ActionIcon
                        color="red"
                        onClick={() => onDelete(row as unknown as TRowData)}
                      >
                        <Trash size={16} />
                      </ActionIcon>
                    ) : null}
                  </Group>
                </td>
              </tr>
            ))}
          {rows && rows.length === 0 && (
            <tr>
              <td
                colSpan={columns.length + 1}
                style={{ textAlign: "center", padding: "40px" }}
              >
                {notFoundMessage}
              </td>
            </tr>
          )}
          {!rows && (
            <tr>
              <td
                colSpan={columns.length + 1}
                style={{ textAlign: "center", padding: "40px" }}
              >
                <Loader size="xl" my="md" mx="auto" variant="dots" />
              </td>
            </tr>
          )}
        </tbody>
      </Table>
      {totalPages && pageNumber && setPageNumber ? (
        <Pagination
          total={totalPages}
          siblings={5}
          value={pageNumber}
          onChange={setPageNumber}
        />
      ) : null}
    </>
  );
}
