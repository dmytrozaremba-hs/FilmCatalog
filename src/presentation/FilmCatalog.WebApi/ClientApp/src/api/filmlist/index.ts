import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import { API_ROOT } from "../../config";
import { PaginatedList } from "../types";
import { FilmBriefDto } from "../film";

const rootKey = "FILM_LIST";

export const useGetWatchedByUser = (pageNumber = 1, pageSize: number) => {
  return useQuery({
    queryKey: [rootKey, "WATCHED", pageNumber, pageSize],
    queryFn: async () => {
      const res = await axios.get(`${API_ROOT}/FilmList/WatchedByUser`, {
        params: {
          pageNumber,
          pageSize,
        },
      });
      return res.data as PaginatedList<FilmBriefDto>;
    },
  });
};

export const useGetWatchLaterByUser = (pageNumber = 1, pageSize: number) => {
  return useQuery({
    queryKey: [rootKey, "WATCH_LATER", pageNumber, pageSize],
    queryFn: async () => {
      const res = await axios.get(`${API_ROOT}/FilmList/WatchLaterByUser`, {
        params: {
          pageNumber,
          pageSize,
        },
      });
      return res.data as PaginatedList<FilmBriefDto>;
    },
  });
};

export const useToggleWatchedByUser = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (data: { filmId: number }) =>
      axios.post(`${API_ROOT}/FilmList/ToggleWatchedByUser`, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [rootKey] });
      queryClient.invalidateQueries({ queryKey: ["FILM"] });
    },
  });
};

export const useToggleWatchLaterByUser = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (data: { filmId: number }) =>
      axios.post(`${API_ROOT}/FilmList/ToggleWatchLaterByUser`, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [rootKey] });
      queryClient.invalidateQueries({ queryKey: ["FILM"] });
    },
  });
};
