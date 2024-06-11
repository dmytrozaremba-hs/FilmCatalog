import { Link, useNavigate, useParams } from "react-router-dom";
import { useGetAllGenres } from "../api/genre";
import { useGetFilm } from "../api/film";
import {
  ReviewDto,
  useGetAllReviews,
  useGetForFilmByUser,
  useUpsertUserReviewForFilm,
} from "../api/review";
import { Button, Rating, Textarea } from "@mantine/core";
import { useForm } from "@mantine/form";
import { useUser } from "../services/userService";
import { Fragment, useState } from "react";
import { extractErrors } from "../helpers/form";
import { AddRemoveFromList } from "../components/AddRemoveFromList";
import { useGetAllTags } from "../api/tag";

type FilmDetailsParams = {
  id: string;
};

function FilmDetails() {
  const user = useUser();

  const params = useParams<FilmDetailsParams>();

  const navigate = useNavigate();

  const id =
    params && typeof params.id === "string" ? parseInt(params.id, 10) : NaN;

  const { data: genres } = useGetAllGenres("", 1, 1000);
  const { data: tags } = useGetAllTags("", 1, 1000);
  const { data: film, isLoading: filmIsLoading } = useGetFilm(id);

  if (!genres || !tags || filmIsLoading) return <div>Loading ...</div>;

  if (!film) return <div>Not found</div>;

  return (
    <div className="flex flex-col px-4">
      <div className="mt-8 flex flex-col sm:flex-row">
        <div className="flex flex-col">
          <img src={film.posterUrl} alt="" className="w-64 self-center" />
        </div>
        <div className="ml-12 flex w-[75%] flex-col text-gray-700">
          <div className="mb-6 min-w-[20rem] border-b-2 border-gray-500 text-[2.5rem] font-bold">
            {film.title}
          </div>

          <div className="mb-4 flex justify-end">
            <div className="mr-2">
              <Rating value={film.averageRating} count={10} readOnly />
            </div>
            {film.numberOfVotes ? (
              <div className="italic">{`${film.averageRating.toFixed(
                1
              )} based on ${film.numberOfVotes} ${
                film.numberOfVotes > 1 ? "votes" : "vote"
              }`}</div>
            ) : (
              <div className="italic">No rating yet</div>
            )}
          </div>

          <FilterLinks
            variant="director"
            items={film.directors.map((x) => ({
              id: x.id,
              name: [x.firstName, x.middleName, x.lastName].join(" ").trim(),
            }))}
          />
          <FilterLinks
            variant="producer"
            items={film.producers.map((x) => ({
              id: x.id,
              name: [x.firstName, x.middleName, x.lastName].join(" ").trim(),
            }))}
          />
          <FilterLinks
            variant="actor"
            items={film.actors.map((x) => ({
              id: x.id,
              name: [x.firstName, x.middleName, x.lastName].join(" ").trim(),
            }))}
          />

          <FilterLinks
            variant="genre"
            items={genres.items.filter((g) => film.genres.includes(g.id))}
          />

          <FilterLinks
            variant="tag"
            items={tags.items.filter((t) => film.tags.includes(t.id))}
          />

          <div>
            <span className="font-bold">Release Year:</span> {film.year}
          </div>
          {film.durationInMinutes > 0 && (
            <div>
              <span className="font-bold">Duration:</span>{" "}
              {film.durationInMinutes} minutes
            </div>
          )}

          {user && <AddRemoveFromList film={film} variant="normal" />}
        </div>
      </div>
      <div className="mt-5 flex flex-col text-justify text-gray-700">
        {film.description}
      </div>
      {user && <AddEditReview filmId={film.id} />}
      {!user && (
        <div className="mt-6">
          <Button variant="outline" onClick={() => navigate("/login")}>
            Sign in to review this movie
          </Button>
        </div>
      )}
      <ReviewList filmId={film.id} />
    </div>
  );
}

function FilterLinks({
  variant,
  items: items,
}: {
  variant: "director" | "producer" | "actor" | "genre" | "tag";
  items: { id: number; name: string }[];
}) {
  if (items.length === 0) return null;

  const caption = {
    director: "Directors",
    producer: "Producers",
    actor: "Cast",
    genre: "Genres",
    tag: "Tags",
  }[variant];

  const baseTo = {
    director: "/?director=",
    producer: "/?producer=",
    actor: "/?actor=",
    genre: "/?genre=",
    tag: "/?tag=",
  }[variant];

  return (
    <div>
      <span className="font-bold">{caption}:</span>{" "}
      {items.map((x, i) => (
        <Fragment key={x.id}>
          {i !== 0 && ", "}
          <Link
            to={`${baseTo}${x.id}`}
            className="text-blue-700 hover:underline"
          >
            {x.name}
          </Link>
        </Fragment>
      ))}
    </div>
  );
}

function ReviewList({
  userId = 0,
  filmId = 0,
}: {
  userId?: number;
  filmId?: number;
}) {
  const { data: reviews } = useGetAllReviews(userId, filmId, 1, 1000);

  if (!reviews) return null;

  return (
    <div className="mt-5 flex flex-col">
      <div className="text-center text-xl font-bold">Reviews</div>
      {reviews.items.length > 0 ? (
        <div>
          {reviews.items.map((x) => (
            <ReviewCard key={x.id} review={x} />
          ))}
        </div>
      ) : (
        <div>No reviews yet</div>
      )}
    </div>
  );
}

function ReviewCard({ review }: { review: ReviewDto }) {
  return (
    <div className="my-8">
      <div className="flex flex-row items-center justify-between bg-slate-600 p-1">
        <div className="text-white">
          {review.username} on {review.onDateFormatted}
        </div>
        <div>
          <Rating value={review.rating} count={10} readOnly />
        </div>
      </div>
      <div className="p-1">{review.comment}</div>
    </div>
  );
}

function AddEditReview({ filmId }: { filmId: number }) {
  const { data } = useGetForFilmByUser(filmId);
  const [formVisible, setFormVisible] = useState(false);

  if (!data) return null;

  return (
    <>
      {!formVisible && (
        <div className="mt-6">
          <Button variant="outline" onClick={() => setFormVisible(true)}>
            Review this movie
          </Button>
        </div>
      )}
      {formVisible && (
        <ReviewForm
          filmId={filmId}
          initialRating={data.rating}
          initialComment={data.comment}
          doneHandler={() => setFormVisible(false)}
        />
      )}
    </>
  );
}

function ReviewForm({
  filmId,
  initialRating,
  initialComment,
  doneHandler,
}: {
  filmId: number;
  initialRating: number;
  initialComment: string;
  doneHandler: () => void;
}) {
  const form = useForm({
    initialValues: {
      rating: initialRating,
      comment: initialComment,
    },
  });

  const mutation = useUpsertUserReviewForFilm();

  const onSubmit = async ({ rating, comment }: typeof form.values) => {
    try {
      await mutation.mutateAsync({
        filmId,
        rating,
        comment,
      });

      doneHandler();
    } catch (e) {
      form.setErrors(extractErrors(e));
    }
  };

  return (
    <div className="my-6 rounded-lg border border-slate-400 p-4">
      <form onSubmit={form.onSubmit(onSubmit)}>
        <div className="flex">
          <div className="mr-4">Rate this movie</div>
          <div>
            <Rating
              defaultValue={5}
              count={10}
              {...form.getInputProps("rating")}
            />
          </div>
        </div>
        <div className="mt-6">
          <Textarea
            placeholder="Your comment"
            minRows={8}
            {...form.getInputProps("comment")}
          />
        </div>
        <div className="mt-6">
          <Button type="submit" variant="outline">
            Submit
          </Button>
        </div>
      </form>
    </div>
  );
}

export default FilmDetails;
