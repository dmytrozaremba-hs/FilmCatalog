import { API_ROOT } from "../config";
// import { pathToUrl } from "../utils/router";

// export const pathToApiUrl = (path: string, params: object = {}) =>
//   `${API_ROOT}${pathToUrl(path, params)}`;

// export function applyPrefix<T extends Record<string, string>>(
//   prefix: string = "",
//   partialRoutes: T
// ) {
//   const apiPrefix = `${API_ROOT}${prefix}`;
//   const routes = Object.fromEntries(
//     Object.entries(partialRoutes).map(([name, value]) => [
//       name,
//       `${prefix}${value}`,
//     ])
//   ) as Record<keyof T, string>;
//   return { apiPrefix, routes };
// }

export const isApiUrl = (url?: string) => {
  if (!url) return false;
  return url.startsWith(API_ROOT);
};
