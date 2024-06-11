import camelCase from "lodash.camelcase";

function formatFormErrors(
  errors: string | [string] | Record<string, string | [string]>
) {
  const result: Record<string, string> = {};

  if (typeof errors === "string" || Array.isArray(errors)) {
    errors = { "": errors };
  }

  for (const [name, value] of Object.entries(errors)) {
    const normalizedName = normalizeName(name);
    if (typeof value === "string") {
      const error = value.trim();
      if (error) {
        result[normalizedName] = error;
      }
    } else if (Array.isArray(value)) {
      const error = value
        .map((x) => x.trim())
        .filter((x) => !!x)
        .join(" ");
      if (error) {
        result[normalizedName] = error;
      }
    }
  }

  return result;
}

function normalizeName(name: string) {
  const re = /\[|\]|\./;
  const parts = name.split(re).filter((x) => x !== "");
  return parts.map(camelCase).join(".");
}

export function extractErrors(e: any) {
  if (e && typeof e === "object") {
    if (!e.response) {
      return formatFormErrors("Cannot connect to the server");
    }
    if (e.response.status === 403) {
      return formatFormErrors("You are not allowed to access this resource");
    }
    const data = e.response.data;
    if (data && typeof data === "object") {
      const errors = (data as { [key: string]: string }).errors;
      if (errors) {
        return formatFormErrors(errors);
      }
      const detail = (data as { [key: string]: string }).detail;
      if (detail && typeof detail === "string") {
        return formatFormErrors(detail);
      }
    }
  }
  return formatFormErrors("Unknown error");
}
