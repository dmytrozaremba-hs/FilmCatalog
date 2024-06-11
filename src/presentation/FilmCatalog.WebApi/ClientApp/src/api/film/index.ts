import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import axios from "axios";
import { API_ROOT } from "../../config";
import { PaginatedList } from "../types";
import { PersonBriefDto } from "../person";

const rootKey = "FILM";

export interface FilmBriefDto {
  id: number;
  title: string;
  shortDescription: string;
  year: string;
  posterUrl: string;
  genres: number[];
  durationInMinutes: number;
  averageRating: number;
  numberOfVotes: number;
  includedInWatchedList: boolean;
  includedInWatchLaterList: boolean;
}

export interface FilmDto {
  id: number;
  title: string;
  description: string;
  year: string;
  posterUrl: string;
  genres: number[];
  tags: number[];
  directors: PersonBriefDto[];
  producers: PersonBriefDto[];
  actors: PersonBriefDto[];
  durationInMinutes: number;
  averageRating: number;
  numberOfVotes: number;
  includedInWatchedList: boolean;
  includedInWatchLaterList: boolean;
}

export interface FilmCommand
  extends Omit<
    FilmDto,
    | "genres"
    | "tags"
    | "directors"
    | "producers"
    | "actors"
    | "includedInWatchedList"
    | "includedInWatchLaterList"
    | "numberOfVotes"
    | "averageRating"
  > {
  genres: number[];
  tags: number[];
  directors: number[];
  producers: number[];
  actors: number[];
}

export function toFilmCommand(data: FilmFormProps): FilmCommand {
  return {
    id: data.id,
    title: data.title,
    description: data.description,
    year: data.year,
    posterUrl: data.posterUrl,
    durationInMinutes: parseInt(data.durationInMinutes, 10) || 0,
    genres: data.genres.map((x) => parseInt(x, 10)),
    tags: data.tags.map((x) => parseInt(x, 10)),
    directors: data.directors.map((x) => parseInt(x, 10)),
    producers: data.producers.map((x) => parseInt(x, 10)),
    actors: data.actors.map((x) => parseInt(x, 10)),
  };
}

export interface FilmFormProps
  extends Omit<
    FilmDto,
    | "durationInMinutes"
    | "genres"
    | "tags"
    | "directors"
    | "producers"
    | "actors"
    | "includedInWatchedList"
    | "includedInWatchLaterList"
    | "numberOfVotes"
    | "averageRating"
  > {
  durationInMinutes: string;
  genres: string[];
  tags: string[];
  directors: string[];
  producers: string[];
  actors: string[];
}

export function toFilmFormProps(data: FilmDto): FilmFormProps {
  return {
    id: data.id,
    title: data.title,
    description: data.description,
    year: data.year,
    posterUrl: data.posterUrl,
    durationInMinutes: data.durationInMinutes
      ? `${data.durationInMinutes}`
      : "",
    genres: data.genres.map((x) => `${x}`),
    tags: data.tags.map((x) => `${x}`),
    directors: data.directors.map((x) => `${x.id}`),
    producers: data.producers.map((x) => `${x.id}`),
    actors: data.actors.map((x) => `${x.id}`),
  };
}

export const useGetAllFilms = (
  search = "",
  selectedFilms: number[] = [],
  selectedTags: number[] = [],
  selectedDirector = 0,
  selectedProducer = 0,
  selectedActor = 0,
  sort = 5,
  pageNumber = 1,
  pageSize: number
) => {
  const strFilms = [...selectedFilms].sort((a, b) => a - b).join(",");
  const strTags = [...selectedTags].sort((a, b) => a - b).join(",");
  return useQuery({
    queryKey: [
      rootKey,
      search,
      strFilms,
      strTags,
      selectedDirector,
      selectedProducer,
      selectedActor,
      sort,
      pageNumber,
      pageSize,
    ],
    queryFn: async () => {
      const res = await axios.get(`${API_ROOT}/Film`, {
        params: {
          search,
          selectedFilms: strFilms,
          selectedTags: strTags,
          selectedDirector,
          selectedProducer,
          selectedActor,
          sort,
          pageNumber,
          pageSize,
        },
      });
      return res.data as PaginatedList<FilmBriefDto>;
    },
  });
};

export const useGetFilm = (id: number) =>
  useQuery({
    queryKey: [rootKey, id],
    queryFn: async () => {
      const res = await axios.get(`${API_ROOT}/Film/${id}`);
      return res.data as FilmDto;
    },
  });

export const useCreateFilm = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (data: FilmCommand) =>
      axios.post<FilmDto>(`${API_ROOT}/Film`, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [rootKey] });
    },
  });
};

export const useUpdateFilm = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (data: FilmCommand) =>
      axios.put<FilmDto>(`${API_ROOT}/Film/${data.id}`, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [rootKey] });
    },
  });
};

export const useDeleteFilm = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (id: number) => axios.delete<number>(`${API_ROOT}/Film/${id}`),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [rootKey] });
    },
  });
};
