import "dayjs/locale/uk";
import dayjs from "dayjs";

export function dateFormatter(input: unknown) {
  if (input == null) return "";
  if (typeof input === "string") {
    input = input.trim();
    if (input === "") return "";
  }
  try {
    return dayjs(input as string | Date)
      .format("D MMMM YYYY");
  } catch {
    return "";
  }
}

export function toDate(input: string) {
  return dayjs(input).toDate();
}

export function toSerializedDate(input: Date) {
  return dayjs(input).format("YYYY-MM-DD");
}
