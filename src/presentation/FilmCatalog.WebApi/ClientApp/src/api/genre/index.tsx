import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import { API_ROOT } from "../../config";
import { PaginatedList } from "../types";

const rootKey = "GENRE";

export interface GenreDto {
  id: number;
  name: string;
}

export interface CreateGenreCommand {
  name: string;
}

export interface UpdateGenreCommand {
  id: number;
  name: string;
}

export const useGetAllGenres = (
  search = "",
  pageNumber = 1,
  pageSize: number
) =>
  useQuery({
    queryKey: [rootKey, search, pageNumber, pageSize],
    queryFn: async () => {
      const res = await axios.get(`${API_ROOT}/Genre`, {
        params: { search, pageNumber, pageSize },
      });
      return res.data as PaginatedList<GenreDto>;
    },
  });

export const useGetGenre = (id: number) =>
  useQuery({
    queryKey: [rootKey, id],
    queryFn: async () => {
      const res = await axios.get(`${API_ROOT}/Genre/${id}`);
      return res.data as GenreDto;
    },
  });

export const useCreateGenre = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (data: { name: string }) =>
      axios.post<GenreDto>(`${API_ROOT}/Genre`, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [rootKey] });
    },
  });
};

export const useUpdateGenre = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (data: { id: number; name: string }) =>
      axios.put<GenreDto>(`${API_ROOT}/Genre/${data.id}`, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [rootKey] });
    },
  });
};

export const useDeleteGenre = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (id: number) => axios.delete<number>(`${API_ROOT}/Genre/${id}`),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [rootKey] });
    },
  });
};
