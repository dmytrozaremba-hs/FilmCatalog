import { Link } from "react-router-dom";
import { Rating } from "@mantine/core";
import { AddRemoveFromList } from "./AddRemoveFromList";
import { useUser } from "../services/userService";

export interface FilmCardProps {
  filmId: number;
  title: string;
  shortDescription: string;
  year: string;
  posterUrl: string;
  genres: string[];
  durationInMinutes: number;
  averageRating: number;
  numberOfVotes: number;
  includedInWatchedList: boolean;
  includedInWatchLaterList: boolean;
}

export function FilmCard({
  filmId,
  title,
  shortDescription,
  genres,
  year,
  posterUrl,
  averageRating,
  numberOfVotes,
  includedInWatchedList,
  includedInWatchLaterList,
}: FilmCardProps) {
  const user = useUser();

  return (
    <Link
      to={`/film/${filmId}`}
      className="hover:scale-x-1 hover:scale-y-1 m-4 flex flex-col justify-between rounded-lg p-4 shadow-md shadow-slate-400 duration-200 hover:shadow-lg hover:shadow-slate-400"
    >
      <div>
        <div
          className="m-2 mx-auto h-60 w-60 bg-contain bg-center bg-no-repeat"
          style={{ backgroundImage: `url(${posterUrl})` }}
        ></div>
      </div>
      <div className="text-lg font-bold">{title}</div>
      <div className="text-justify text-[.75rem]">{shortDescription}</div>
      <div className="mt-2 flex">
        <div className="flex flex-row flex-wrap justify-start">
          {genres.map((x, index) => (
            <div
              key={index}
              className="mx-1 my-1 inline-block rounded-lg bg-emerald-600 px-[0.65em] pb-[0.25em] pt-[0.35em] text-[0.75em] font-bold text-white"
            >
              {x}
            </div>
          ))}
        </div>
      </div>
      <div className="mt-2 text-lg font-bold text-gray-500">{year}</div>

      <div className="flex flex-col items-end">
        {numberOfVotes > 0 && (
          <div>
            <Rating value={averageRating} count={10} readOnly />
          </div>
        )}
        {numberOfVotes ? (
          <div className="text-sm italic">{`${averageRating.toFixed(
            1
          )} based on ${numberOfVotes} ${
            numberOfVotes > 1 ? "votes" : "vote"
          }`}</div>
        ) : (
          <div className="text-sm italic">No rating yet</div>
        )}
      </div>
      {user && (
        <div>
          <AddRemoveFromList
            film={{
              id: filmId,
              includedInWatchedList,
              includedInWatchLaterList,
            }}
            variant="small"
          />
        </div>
      )}
    </Link>
  );
}
