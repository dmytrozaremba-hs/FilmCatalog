import { useNavigate } from "react-router-dom";
import { User, useUser } from "../services/userService";
import { useEffect } from "react";
import { useGetWatchLaterByUser, useGetWatchedByUser } from "../api/filmList";
import { PaginatedList } from "../api/types";
import { FilmBriefDto } from "../api/film";
import { FilmCard, FilmCardProps } from "../components/FilmCard";
import { GenreDto, useGetAllGenres } from "../api/genre";

function UserProfile() {
  const user = useUser();

  const navigate = useNavigate();

  useEffect(() => {
    if (!user) {
      navigate("/");
    }
  }, [user, navigate]);

  if (!user) {
    return null;
  }

  return (
    <div className="flex flex-col px-4">
      <UserDetail user={user} />
      <WatchedList />
      <WatchLaterList />
    </div>
  );
}

function UserDetail({ user }: { user: User }) {
  return (
    <div className="m-4 flex flex-col">
      <div className="mb-2 text-xl font-bold">User Information</div>
      <div className="ml-6">
        <div>
          <span className="font-bold">Name:</span> {user.username}
        </div>
        <div>
          <span className="font-bold">Email:</span> {user.email}
        </div>
      </div>
    </div>
  );
}

function WatchedList() {
  const { data: films } = useGetWatchedByUser(1, 12);
  const { data: genres } = useGetAllGenres("", 1, 1000);

  if (!films || !genres) return null;

  return (
    <div className="mt-4 flex flex-col">
      <div className="mx-4 mb-2 text-xl font-bold">Watched List</div>
      <FilmList films={films} genres={genres} />
    </div>
  );
}

function WatchLaterList() {
  const { data: films } = useGetWatchLaterByUser(1, 12);
  const { data: genres } = useGetAllGenres("", 1, 1000);

  if (!films || !genres) return null;

  return (
    <div className="mt-4 flex flex-col">
      <div className="mx-4 mb-2 text-xl font-bold">Watch Later List</div>
      <FilmList films={films} genres={genres} />
    </div>
  );
}

function FilmList({
  films,
  genres,
}: {
  films: PaginatedList<FilmBriefDto>;
  genres: PaginatedList<GenreDto>;
}) {
  const filmCardDataList: FilmCardProps[] =
    genres && films
      ? films.items.map((x) => ({
          filmId: x.id,
          title: x.title,
          shortDescription: x.shortDescription,
          year: x.year,
          posterUrl: x.posterUrl,
          genres: genres.items
            .filter((g) => x.genres.includes(g.id))
            .map((g) => g.name),
          durationInMinutes: x.durationInMinutes,
          averageRating: x.averageRating,
          numberOfVotes: x.numberOfVotes,
          includedInWatchedList: x.includedInWatchedList,
          includedInWatchLaterList: x.includedInWatchLaterList,
        }))
      : [];

  return (
    <div>
      {films.items.length > 0 ? (
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
          {filmCardDataList.map((film) => (
            <FilmCard key={film.filmId} {...film} />
          ))}
        </div>
      ) : (
        <div>The list is empty</div>
      )}
    </div>
  );
}

export default UserProfile;
