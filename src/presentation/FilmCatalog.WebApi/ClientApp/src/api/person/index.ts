import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import { API_ROOT } from "../../config";
import { PaginatedList } from "../types";

const rootKey = "PERSON";

export interface PersonDto {
  id: number;
  firstName: string;
  lastName: string;
  middleName: string;
  birthDate: string;
  isDirector: boolean;
  isProducer: boolean;
  isActor: boolean;
}

export interface PersonBriefDto {
  id: number;
  firstName: string;
  lastName: string;
  middleName: string;
  birthDate: string;
}

export interface CreatePersonCommand {
  firstName: string;
  lastName: string;
  middleName: string;
  birthDate: string;
  isDirector: boolean;
  isProducer: boolean;
  isActor: boolean;
}

export interface UpdatePersonCommand {
  id: number;
  firstName: string;
  lastName: string;
  middleName: string;
  birthDate: string;
  isDirector: boolean;
  isProducer: boolean;
  isActor: boolean;
}

export const useGetAllPersons = (
  search = "",
  mustBeDirector = false,
  mustBeProducer = false,
  mustBeActor = false,
  pageNumber = 1,
  pageSize: number
) =>
  useQuery({
    queryKey: [
      rootKey,
      search,
      mustBeDirector,
      mustBeProducer,
      mustBeActor,
      pageNumber,
      pageSize,
    ],
    queryFn: async () => {
      const res = await axios.get(`${API_ROOT}/Person`, {
        params: {
          search,
          mustBeDirector,
          mustBeProducer,
          mustBeActor,
          pageNumber,
          pageSize,
        },
      });
      return res.data as PaginatedList<PersonBriefDto>;
    },
  });

export const useGetPerson = (id: number) =>
  useQuery({
    queryKey: [rootKey, id],
    queryFn: async () => {
      const res = await axios.get(`${API_ROOT}/Person/${id}`);
      return res.data as PersonDto;
    },
  });

export const useCreatePerson = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (data: CreatePersonCommand) =>
      axios.post<PersonDto>(`${API_ROOT}/Person`, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [rootKey] });
    },
  });
};

export const useUpdatePerson = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (data: UpdatePersonCommand) =>
      axios.put<PersonDto>(`${API_ROOT}/Person/${data.id}`, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [rootKey] });
    },
  });
};

export const useDeletePerson = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (id: number) =>
      axios.delete<number>(`${API_ROOT}/Person/${id}`),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [rootKey] });
    },
  });
};
