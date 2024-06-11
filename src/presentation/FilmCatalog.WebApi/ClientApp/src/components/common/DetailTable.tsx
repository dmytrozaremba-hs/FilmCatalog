import {
  ActionIcon,
  createStyles,
  Group,
  Loader,
  ScrollArea,
  Sx,
  Table,
} from "@mantine/core";
import { useState } from "react";
import { CirclePlus, Pencil, Trash } from "tabler-icons-react";
import { TableColumn, TableRow } from "./types";

const useStyles = createStyles((theme) => ({
  header: {
    position: "sticky",
    top: 0,
    backgroundColor:
      theme.colorScheme === "dark"
        ? theme.colors.dark[8]
        : theme.colors.gray[0],
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

  scrolled: {
    boxShadow: theme.shadows.sm,
  },

  tbody: {
    "& > tr > td:last-child": {
      paddingRight: 16,
    },
  },

  thumb: {
    "&::before": {
      minWidth: 16,
    },
  },
}));

export function DetailTable<TRowData, TForm>({
  columns,
  rows,
  onAdd,
  onEdit,
  onDelete,
  notFoundMessage = "Not found",
  sx,
}: {
  form: TForm;
  columns: TableColumn<TRowData>[];
  rows?: TableRow<TRowData>[];
  onAdd?: () => void;
  onEdit?: (row: TRowData, rowIndex: number) => void;
  onDelete?: (row: TRowData, rowIndex: number) => void;
  error: any;
  notFoundMessage?: string;
  sx?: Sx;
}) {
  const { classes, cx } = useStyles();
  const [scrolled, setScrolled] = useState(false);

  const visibleColumns = columns.filter((c) => !c.hidden);
  const hasButtons = !!(onAdd || onEdit || onDelete);

  return (
    <>
      <ScrollArea
        type="auto"
        sx={sx}
        onScrollPositionChange={({ y }) => setScrolled(y !== 0)}
        classNames={{ thumb: classes.thumb }}
      >
        <Table sx={{ minWidth: 200, position: "relative" }}>
          <thead
            className={cx(classes.header, { [classes.scrolled]: scrolled })}
            style={{ zIndex: 1 }}
          >
            <tr>
              {visibleColumns.map((c) => (
                <th key={`${String(c.key)}`}>{c.header}</th>
              ))}
              {hasButtons && (
                <th>
                  <div style={{ display: "flex", justifyContent: "right" }}>
                    {onAdd && (
                      <ActionIcon
                        variant="filled"
                        color={"blue"}
                        onClick={() => onAdd()}
                      >
                        <CirclePlus size={16} />
                      </ActionIcon>
                    )}
                  </div>
                </th>
              )}
            </tr>
          </thead>
          <tbody className={classes.tbody}>
            {rows &&
              rows.length > 0 &&
              rows.map((row, rowIndex) => (
                <tr key={rowIndex}>
                  {visibleColumns.map((c) => (
                    <td key={`${String(c.key)}`}>
                      {c.formatter
                        ? c.formatter(row[c.key], rowIndex)
                        : row[c.key]?.toString()}
                    </td>
                  ))}
                  {hasButtons && (
                    <td>
                      <Group spacing={0} position="right">
                        {onEdit && (
                          <ActionIcon
                            onClick={() =>
                              onEdit(row as unknown as TRowData, rowIndex)
                            }
                          >
                            <Pencil size={16} />
                          </ActionIcon>
                        )}
                        {onDelete && (
                          <ActionIcon
                            color="red"
                            onClick={() =>
                              onDelete(row as unknown as TRowData, rowIndex)
                            }
                          >
                            <Trash size={16} />
                          </ActionIcon>
                        )}
                      </Group>
                    </td>
                  )}
                </tr>
              ))}
            {rows && rows.length === 0 && (
              <tr>
                <td
                  colSpan={visibleColumns.length + (hasButtons ? 1 : 0)}
                  style={{ textAlign: "center", padding: "10px" }}
                >
                  {notFoundMessage}
                </td>
              </tr>
            )}
            {!rows && (
              <tr>
                <td
                  colSpan={visibleColumns.length + (hasButtons ? 1 : 0)}
                  style={{ textAlign: "center", padding: "40px" }}
                >
                  <Loader size="xl" my="md" mx="auto" variant="dots" />
                </td>
              </tr>
            )}
          </tbody>
        </Table>
      </ScrollArea>
    </>
  );
}
