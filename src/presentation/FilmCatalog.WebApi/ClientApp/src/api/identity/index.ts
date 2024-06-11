import { useMutation } from "@tanstack/react-query";

import { Role } from "../user";
import axios from "axios";
import { API_ROOT } from "../../config";

export interface AuthenticationResultDto {
  id: number;
  token: string;
  email: string;
  username: string;
  role: Role;
}

export const useLogin = () => {
  return useMutation({
    mutationFn: (data: { email: string; password: string }) =>
      axios.post<AuthenticationResultDto>(`${API_ROOT}/Identity/Login`, data),
  });
};

export const useSignup = () => {
  return useMutation({
    mutationFn: (data: {
      email: string;
      username: string;
      password: string;
      passwordAgain: string;
    }) =>
      axios.post<AuthenticationResultDto>(`${API_ROOT}/Identity/Signup`, data),
  });
};
