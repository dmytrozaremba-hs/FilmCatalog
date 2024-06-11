import { useState } from "react";

export function useAddEditDialog<T extends { id: number }>() {
  const [editId, setEditId] = useState<number | null>(null);

  const showAdd = () => {
    setEditId(0);
  };

  const showEdit = ({ id }: T) => {
    setEditId(id);
  };

  const doneEdit = () => {
    setEditId(null);
  };

  return { editId, showAdd, showEdit, doneEdit };
}
