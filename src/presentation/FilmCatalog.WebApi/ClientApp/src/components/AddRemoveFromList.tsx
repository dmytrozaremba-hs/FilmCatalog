import { Button } from "@mantine/core";
import { List, Trash } from "tabler-icons-react";
import {
  useToggleWatchLaterByUser,
  useToggleWatchedByUser,
} from "../api/filmList";

export function AddRemoveFromList({
  film,
  variant = "normal",
}: {
  film: {
    id: number;
    includedInWatchedList: boolean;
    includedInWatchLaterList: boolean;
  };
  variant: "small" | "normal";
}) {
  const mutationToggleWatched = useToggleWatchedByUser();
  const mutationToggleWatchLater = useToggleWatchLaterByUser();

  const onToggleWatchedClick = async () => {
    try {
      await mutationToggleWatched.mutateAsync({ filmId: film.id });
    } catch {
      //
    }
  };

  const onToggleWatchLater = async () => {
    try {
      await mutationToggleWatchLater.mutateAsync({ filmId: film.id });
    } catch {
      //
    }
  };

  return (
    <div className="mt-4 flex justify-end">
      <div className="mr-4">
        <Button
          size={variant === "small" ? "xs" : "sm"}
          compact={variant === "small"}
          variant="outline"
          leftIcon={
            film.includedInWatchedList ? (
              <Trash size={variant === "small" ? 14 : 24} />
            ) : (
              <List size={variant === "small" ? 14 : 24} />
            )
          }
          color={film.includedInWatchedList ? "red" : "green"}
          onClick={(e) => {
            e.preventDefault();
            onToggleWatchedClick();
          }}
        >
          Watched
        </Button>
      </div>
      <div>
        <Button
          size={variant === "small" ? "xs" : "sm"}
          compact={variant === "small"}
          variant="outline"
          leftIcon={
            film.includedInWatchLaterList ? (
              <Trash size={variant === "small" ? 14 : 24} />
            ) : (
              <List size={variant === "small" ? 14 : 24} />
            )
          }
          color={film.includedInWatchLaterList ? "red" : "green"}
          onClick={(e) => {
            e.preventDefault();
            onToggleWatchLater();
          }}
        >
          Watch Later
        </Button>
      </div>
    </div>
  );
}
