import { useSearchParams } from "react-router-dom";
import { useGetAllFilms } from "../api/film";
import { GenreDto, useGetAllGenres } from "../api/genre";
import { FilmCard, FilmCardProps } from "../components/FilmCard";
import { Button, MultiSelect, NativeSelect, Select } from "@mantine/core";
import { TagDto, useGetAllTags } from "../api/tag";
import { useMemo, useState } from "react";
import { useForm } from "@mantine/form";
import { PersonBriefDto, useGetAllPersons, useGetPerson } from "../api/person";

function FilmCatalog() {
  const [searchParams] = useSearchParams();

  const search = searchParams.get("search")?.trim() ?? "";

  const genres = [
    parseInt(searchParams.get("genre")?.trim() ?? "", 10) || 0,
  ].filter((x) => !!x);

  const tags = [
    parseInt(searchParams.get("tag")?.trim() ?? "", 10) || 0,
  ].filter((x) => !!x);

  const director =
    parseInt(searchParams.get("director")?.trim() ?? "", 10) || 0;
  const producer =
    parseInt(searchParams.get("producer")?.trim() ?? "", 10) || 0;
  const actor = parseInt(searchParams.get("actor")?.trim() ?? "", 10) || 0;

  const { data: directorData } = useGetPerson(director);
  const { data: producerData } = useGetPerson(producer);
  const { data: actorData } = useGetPerson(actor);

  const [selectedGenres, setSelectedGenres] = useState<number[]>(genres);
  const [selectedTags, setSelectedTags] = useState<number[]>(tags);

  const [selectedDirector, setSelectedDirector] = useState<number>(director);
  const [selectedProducer, setSelectedProducer] = useState<number>(producer);
  const [selectedActor, setSelectedActor] = useState<number>(actor);

  const [selectedSort, setSelectedSort] = useState<number>(5);

  const { data: allGenres } = useGetAllGenres("", 1, 1000);
  const { data: allTags } = useGetAllTags("", 1, 1000);

  const { data: films } = useGetAllFilms(
    search,
    selectedGenres,
    selectedTags,
    selectedDirector,
    selectedProducer,
    selectedActor,
    selectedSort,
    1,
    24
  );

  const filmCardDataList: FilmCardProps[] =
    allGenres && films
      ? films.items.map((x) => ({
          filmId: x.id,
          title: x.title,
          shortDescription: x.shortDescription,
          year: x.year,
          posterUrl: x.posterUrl,
          genres: allGenres.items
            .filter((g) => x.genres.includes(g.id))
            .map((g) => g.name),
          durationInMinutes: x.durationInMinutes,
          averageRating: x.averageRating,
          numberOfVotes: x.numberOfVotes,
          includedInWatchedList: x.includedInWatchedList,
          includedInWatchLaterList: x.includedInWatchLaterList,
        }))
      : [];

  const filterHandler = (
    genres: number[],
    tags: number[],
    director: number,
    producer: number,
    actor: number,
    sort: number
  ) => {
    setSelectedGenres(genres);
    setSelectedTags(tags);
    setSelectedDirector(director);
    setSelectedProducer(producer);
    setSelectedActor(actor);
    setSelectedSort(sort);
  };

  return (
    <div>
      {allGenres?.items &&
        allTags?.items &&
        directorData &&
        producerData &&
        actorData && (
          <FilmCatalogFilter
            genres={allGenres.items}
            tags={allTags.items}
            initialGenres={genres}
            initialTags={tags}
            initialDirector={directorData}
            initialProducer={producerData}
            initialActor={actorData}
            submitHandler={filterHandler}
          />
        )}

      {filmCardDataList.length > 0 ? (
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
          {filmCardDataList.map((film) => (
            <FilmCard key={film.filmId} {...film} />
          ))}
        </div>
      ) : (
        <div className="flex h-96 items-center justify-center text-lg">
          No movies found for your filter
        </div>
      )}
    </div>
  );
}

function FilmCatalogFilter({
  genres,
  tags,
  initialGenres,
  initialTags,
  initialDirector,
  initialProducer,
  initialActor,
  submitHandler,
}: {
  genres: GenreDto[];
  tags: TagDto[];
  initialGenres: number[];
  initialTags: number[];
  initialDirector: PersonBriefDto;
  initialProducer: PersonBriefDto;
  initialActor: PersonBriefDto;
  submitHandler: (
    selectedGenres: number[],
    selectedTags: number[],
    selectedDirector: number,
    selectedProducer: number,
    selectedActor: number,
    selectedSort: number
  ) => void;
}) {
  const genresData = useMemo(
    () =>
      genres.map((x) => ({
        value: x.id.toString(),
        label: x.name,
      })),
    [genres]
  );

  const tagsData = useMemo(
    () =>
      tags.map((x) => ({
        value: x.id.toString(),
        label: x.name,
      })),
    [tags]
  );

  const [searchTextDirectors, setSearchTextDirectors] = useState(
    `${initialDirector.firstName} ${initialDirector.lastName}`.trim()
  );
  const [searchTextProducers, setSearchTextProducers] = useState(
    `${initialProducer.firstName} ${initialProducer.lastName}`.trim()
  );
  const [searchTextActors, setSearchTextActors] = useState(
    `${initialActor.firstName} ${initialActor.lastName}`.trim()
  );

  const { data: directors } = useGetAllPersons(
    searchTextDirectors,
    true,
    false,
    false,
    1,
    3
  );
  const { data: producers } = useGetAllPersons(
    searchTextProducers,
    false,
    true,
    false,
    1,
    3
  );
  const { data: actors } = useGetAllPersons(
    searchTextActors,
    false,
    false,
    true,
    1,
    3
  );

  const directorsData = useMemo(
    () =>
      (directors?.items ?? [initialDirector]).map((x) => ({
        value: x.id.toString(),
        label: `${x.firstName} ${x.lastName}`.trim(),
      })),
    [directors, initialDirector]
  );

  const producersData = useMemo(
    () =>
      (producers?.items ?? [initialProducer]).map((x) => ({
        value: x.id.toString(),
        label: `${x.firstName} ${x.lastName}`.trim(),
      })),
    [producers, initialProducer]
  );

  const actorsData = useMemo(
    () =>
      (actors?.items ?? [initialActor]).map((x) => ({
        value: x.id.toString(),
        label: `${x.firstName} ${x.lastName}`.trim(),
      })),
    [actors, initialActor]
  );

  const form = useForm({
    initialValues: {
      genres: initialGenres.map((x) => `${x}`),
      tags: initialTags.map((x) => `${x}`),
      director: initialDirector.id.toString(),
      producer: initialProducer.id.toString(),
      actor: initialActor.id.toString(),
      sort: "5",
    },
  });

  const onSubmit = async ({
    genres,
    tags,
    director,
    producer,
    actor,
    sort,
  }: typeof form.values) => {
    submitHandler(
      genres.map((x) => parseInt(x, 10)).filter((x) => !!x),
      tags.map((x) => parseInt(x, 10)).filter((x) => !!x),
      parseInt(director) || 0,
      parseInt(producer) || 0,
      parseInt(actor) || 0,
      parseInt(sort) || 1
    );
  };

  return (
    <form onSubmit={form.onSubmit(onSubmit)}>
      <div className="m-4 flex flex-col rounded-lg border border-slate-400 py-4 md:items-center lg:flex-row lg:justify-between">
        <div className="flex flex-col justify-start sm:flex-row md:items-center">
          <div className="m-2">
            <MultiSelect
              data={genresData}
              label="Genres"
              placeholder="Pick some genres"
              searchable
              nothingFound="Nothing found"
              {...form.getInputProps("genres")}
            />
          </div>
          <div className="m-2">
            <MultiSelect
              data={tagsData}
              label="Tags"
              placeholder="Pick some tags"
              searchable
              nothingFound="Nothing found"
              {...form.getInputProps("tags")}
            />
          </div>
          <div className="m-2">
            <Select
              data={directorsData}
              label="Director"
              placeholder="Pick a director"
              searchable
              clearable
              searchValue={searchTextDirectors}
              onSearchChange={setSearchTextDirectors}
              {...form.getInputProps("director")}
            />
          </div>
          <div className="m-2">
            <Select
              data={producersData}
              label="Producer"
              placeholder="Pick a producer"
              searchable
              clearable
              searchValue={searchTextProducers}
              onSearchChange={setSearchTextProducers}
              {...form.getInputProps("producer")}
            />
          </div>
          <div className="m-2">
            <Select
              data={actorsData}
              label="Actor"
              placeholder="Pick a actor"
              searchable
              clearable
              searchValue={searchTextActors}
              onSearchChange={setSearchTextActors}
              {...form.getInputProps("actor")}
            />
          </div>
        </div>
        <div className="flex flex-col items-start justify-between sm:flex-row sm:items-center lg:justify-end">
          <div className="m-2">
            <NativeSelect
              data={[
                { value: "1", label: "Rating (highest first)" },
                { value: "2", label: "Rating (lowest first)" },
                { value: "3", label: "Year (newest first)" },
                { value: "4", label: "Year (oldest first)" },
                { value: "5", label: "Title (ascending)" },
                { value: "6", label: "Title (descending)" },
              ]}
              label="Order by"
              {...form.getInputProps("sort")}
            />
          </div>
          <div className="m-2 self-end">
            <Button type="submit" variant="outline">
              Apply Filters
            </Button>
          </div>
        </div>
      </div>
    </form>
  );
}

export default FilmCatalog;
