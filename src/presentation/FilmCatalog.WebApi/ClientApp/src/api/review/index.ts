import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import { API_ROOT } from "../../config";
import { PaginatedList } from "../types";

const rootKey = "REVIEW";

export interface ReviewDto {
  id: number;
  userId: number;
  username: string;
  filmId: number;
  filmTitle: string;
  comment: string;
  rating: number;
  onDateFormatted: string;
}

export const useGetAllReviews = (
  userId = 0,
  filmId = 0,
  pageNumber = 1,
  pageSize: number
) =>
  useQuery({
    queryKey: [rootKey, userId, filmId, pageNumber, pageSize],
    queryFn: async () => {
      const res = await axios.get(`${API_ROOT}/Review`, {
        params: { userId, filmId, pageNumber, pageSize },
      });
      return res.data as PaginatedList<ReviewDto>;
    },
  });

export const useGetForFilmByUser = (filmId: number) =>
  useQuery({
    queryKey: [rootKey, { myReviewFor: filmId }],
    queryFn: async () => {
      const res = await axios.get(`${API_ROOT}/Review/ForFilmByUser`, {
        params: { filmId },
      });
      return res.data as ReviewDto;
    },
  });

export const useUpsertUserReviewForFilm = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (data: { filmId: number; rating: number; comment: string }) =>
      axios.post<ReviewDto>(`${API_ROOT}/Review/ForFilmByUser`, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [rootKey] });
      queryClient.invalidateQueries({ queryKey: ["FILM"] });
    },
  });
};
