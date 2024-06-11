import { ReactNode } from "react";

export type TableCellData = string | number | boolean | Date | null | undefined;

export type TableRow<TRowData> = {
  [Property in keyof TRowData as Exclude<Property, "id">]: TableCellData;
} & { id: number };

export interface TableColumn<TRowData> {
  key: keyof TRowData;
  header: string;
  formatter?: (v: TableCellData, rowIndex: number) => ReactNode;
  hidden?: boolean;
}
