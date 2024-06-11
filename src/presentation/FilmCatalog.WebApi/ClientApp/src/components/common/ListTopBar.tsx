import {
  Button,
  createStyles,
  Group,
  NumberInput,
  Select,
  TextInput,
} from "@mantine/core";
import { useState } from "react";
import { Search } from "tabler-icons-react";
// import { useGetAllBranches } from "../../api/branch";
// import { useGetAllIngredientCategories } from "../../api/ingredientCategory";
// import { useGetAllProductCategories } from "../../api/productCategory";

// const useFloatingLabelInputStyles = createStyles(
//   (theme, { floating }: { floating: boolean }) => ({
//     root: {
//       position: "relative",
//     },

//     label: {
//       position: "absolute",
//       zIndex: 2,
//       top: 7,
//       left: theme.spacing.sm,
//       pointerEvents: "none",
//       color: floating
//         ? theme.colorScheme === "dark"
//           ? theme.white
//           : theme.black
//         : theme.colorScheme === "dark"
//         ? theme.colors.dark[3]
//         : theme.colors.gray[5],
//       transition:
//         "transform 150ms ease, color 150ms ease, font-size 150ms ease",
//       transform: floating ? `translate(-${theme.spacing.sm}px, -28px)` : "none",
//       fontSize: floating ? theme.fontSizes.xs : theme.fontSizes.sm,
//       fontWeight: floating ? 500 : 400,
//     },

//     required: {
//       transition: "opacity 150ms ease",
//       opacity: floating ? 1 : 0,
//     },

//     input: {
//       "&::placeholder": {
//         transition: "color 150ms ease",
//         color: !floating ? "transparent" : undefined,
//       },
//     },
//   })
// );

interface ListTopBarProps {
  search: string;
  onSearchChange: (search: string) => void;
  branchId?: number;
  onBranchIdChange?: (branchId: number) => void;
  ingredientCategoryId?: number;
  onIngredientCategoryChange?: (ingredientCategoryId: number) => void;
  productCategoryId?: number;
  onProductCategoryChange?: (productCategoryId: number) => void;
  statuses?: string;
  onStatusesChange?: (statuses: string) => void;
  isUsed?: boolean | null;
  onIsUsedChange?: (isUsed: boolean | null) => void;
  pageSize: number;
  onPageSizeChange: (pageSize: number) => void;
  addButtonCaption?: string;
  onShowAdd?: () => void;
}

export function ListTopBar({
  search,
  onSearchChange,
  branchId,
  onBranchIdChange,
  ingredientCategoryId,
  onIngredientCategoryChange,
  productCategoryId,
  onProductCategoryChange,
  statuses,
  onStatusesChange,
  isUsed,
  onIsUsedChange,
  pageSize,
  onPageSizeChange,
  addButtonCaption = "Add new",
  onShowAdd,
}: ListTopBarProps) {
  const [searchFocused, setSearchFocused] = useState(false);
  //   const { classes: searchClasses } = useFloatingLabelInputStyles({
  //     floating: search.trim().length !== 0 || searchFocused,
  //   });

  //   const { classes: pageSizeClasses } = useFloatingLabelInputStyles({
  //     floating: true,
  //   });

  //   const { data: branches } = useGetAllBranches("", 1, 1000);

  //   const branchSelectData =
  //     branches?.items.map((x) => ({
  //       value: x.id.toString(),
  //       label: x.name,
  //     })) || [];

  //   const { data: ingredientCategories } = useGetAllIngredientCategories(
  //     "",
  //     1,
  //     1000
  //   );

  //   const ingredientCategorySelectData = [
  //     { value: "0", label: "<усі>" },
  //     ...(ingredientCategories?.items.map((x) => ({
  //       value: x.id.toString(),
  //       label: x.name,
  //     })) || []),
  //   ];

  //   const { data: productCategories } = useGetAllProductCategories("", 1, 1000);

  //   const productCategorySelectData = [
  //     { value: "0", label: "<усі>" },
  //     ...(productCategories?.items.map((x) => ({
  //       value: x.id.toString(),
  //       label: x.name,
  //     })) || []),
  //   ];

  //   const statusSelectData = [
  //     { value: "", label: "<усі>" },
  //     { value: "1", label: "Чорнетка" },
  //     { value: "2", label: "Створено" },
  //     { value: "3", label: "Виконано частково" },
  //     { value: "4", label: "Виконано" },
  //   ];

  //   const isUsedSelectData = [
  //     { value: "", label: "<усі>" },
  //     { value: "0", label: "В наявності" },
  //     { value: "1", label: "Використано" },
  //   ];

  return (
    <Group position="apart">
      <Group>
        <TextInput
          label="Search"
          placeholder=""
          value={search}
          onChange={(event) => onSearchChange(event.currentTarget.value)}
          onFocus={() => setSearchFocused(true)}
          onBlur={() => setSearchFocused(false)}
          mt="md"
          rightSection={<Search size={16} />}
          //   classNames={searchClasses}
        />
        {/* {onBranchIdChange && (
          <Select
            label="Пекарня"
            data={branchSelectData}
            placeholder=""
            value={branchId?.toString()}
            onChange={(val) => onBranchIdChange(parseInt(val ?? "") || 0)}
            searchable
            clearable
            mt="md"
            classNames={pageSizeClasses}
          ></Select>
        )}
        {onIngredientCategoryChange && (
          <Select
            label="Категорія"
            data={ingredientCategorySelectData}
            placeholder=""
            value={ingredientCategoryId?.toString() || "0"}
            onChange={(val) =>
              onIngredientCategoryChange(parseInt(val ?? "") || 0)
            }
            searchable
            clearable
            mt="md"
            classNames={pageSizeClasses}
          ></Select>
        )}
        {onProductCategoryChange && (
          <Select
            label="Категорія"
            data={productCategorySelectData}
            placeholder=""
            value={productCategoryId?.toString() || "0"}
            onChange={(val) =>
              onProductCategoryChange(parseInt(val ?? "") || 0)
            }
            searchable
            clearable
            mt="md"
            classNames={pageSizeClasses}
          ></Select>
        )}
        {onStatusesChange && (
          <Select
            label="Статус"
            data={statusSelectData}
            placeholder=""
            value={statuses ?? ""}
            onChange={(val) => onStatusesChange(val ?? "")}
            searchable
            clearable
            mt="md"
            classNames={pageSizeClasses}
          ></Select>
        )}
        {onIsUsedChange && (
          <Select
            label="Наявність"
            data={isUsedSelectData}
            placeholder=""
            value={isUsed === null ? "" : isUsed ? "1" : "0"}
            onChange={(val) =>
              onIsUsedChange(val === null || val === "" ? null : val === "1")
            }
            searchable
            clearable
            mt="md"
            classNames={pageSizeClasses}
          ></Select>
        )} */}
        <NumberInput
          label="Rows per page"
          placeholder="20"
          min={10}
          max={100}
          defaultValue={20}
          value={pageSize}
          onChange={(val) => onPageSizeChange(val || 20)}
          mt="md"
          //   classNames={pageSizeClasses}
        />
      </Group>
      <Group>
        {onShowAdd && <Button onClick={onShowAdd}>{addButtonCaption}</Button>}
      </Group>
    </Group>
  );
}
