import { QueryClient, useQuery } from "@tanstack/react-query";
import { Role } from "../api/user";
import { customHistory } from "../helpers/history";

export interface User {
  id: number;
  token: string;
  email: string;
  username: string;
  role: Role;
}

export let currentUser: User | null = null;

let qc: QueryClient | null = null;

export function initUserService(queryClient: QueryClient) {
  qc = queryClient;

  try {
    const loadedUser = localStorage.getItem("user");
    if (loadedUser) {
      currentUser = JSON.parse(loadedUser);
      if (currentUser) {
        if (
          typeof currentUser.token !== "string" ||
          currentUser.token.trim() === ""
        )
          throw new Error("Missing token");

        if (typeof currentUser.username !== "string")
          throw new Error("Missing username");
      }
    }
  } catch (error) {
    currentUser = null;
  }
}

export function login(user: User) {
  currentUser = user;
  localStorage.setItem("user", JSON.stringify(user));

  if (qc) {
    qc.invalidateQueries(["CURRENT_USER"]);
  }

  // get return url from location state or default to home page
  // const { from } = (customHistory.location.state as { from: Location }) || {
  //   from: { pathname: "/" },
  // };

  // customHistory.push(from.pathname);
  customHistory.go(-1);
}

export function logout() {
  currentUser = null;
  localStorage.removeItem("user");

  if (qc) {
    qc.invalidateQueries(["CURRENT_USER"]);
  }

  customHistory.go(0);
}

export function isAuthorized(
  user: User,
  roles: Role | Role[] | null | undefined
) {
  let authorized = false;

  if (!roles) {
    authorized = user.role !== "None";
  } else if (Array.isArray(roles)) {
    authorized = roles.includes(user.role);
  } else if (typeof roles === "string") {
    authorized = roles === user.role;
  }

  return authorized;
}

export function useUser() {
  const { data } = useQuery(["CURRENT_USER"], () => currentUser, {
    initialData: currentUser,
    staleTime: 0,
    retry: true,
  });

  return data ?? null;
}
