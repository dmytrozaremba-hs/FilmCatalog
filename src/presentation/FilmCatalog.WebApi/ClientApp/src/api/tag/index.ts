import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import { API_ROOT } from "../../config";
import { PaginatedList } from "../types";

const rootKey = "TAG";

export interface TagDto {
  id: number;
  name: string;
}

export interface CreateTagCommand {
  name: string;
}

export interface UpdateTagCommand {
  id: number;
  name: string;
}

export const useGetAllTags = (search = "", pageNumber = 1, pageSize: number) =>
  useQuery({
    queryKey: [rootKey, search, pageNumber, pageSize],
    queryFn: async () => {
      const res = await axios.get(`${API_ROOT}/Tag`, {
        params: { search, pageNumber, pageSize },
      });
      return res.data as PaginatedList<TagDto>;
    },
  });

  export const useGetTag = (id: number) =>
  useQuery({
    queryKey: [rootKey, id],
    queryFn: async () => {
      const res = await axios.get(`${API_ROOT}/Tag/${id}`);
      return res.data as TagDto;
    },
  });

export const useCreateTag = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (data: { name: string }) =>
      axios.post<TagDto>(`${API_ROOT}/Tag`, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [rootKey] });
    },
  });
};

export const useUpdateTag = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (data: { id: number; name: string }) =>
      axios.put<TagDto>(`${API_ROOT}/Tag/${data.id}`, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [rootKey] });
    },
  });
};

export const useDeleteTag = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (id: number) => axios.delete<number>(`${API_ROOT}/Tag/${id}`),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [rootKey] });
    },
  });
};
