"use client";
import { GET_MOVIE_DETAILS } from "@/graphql/queries";
import { GetMovieDetailsResponse } from "@/types/movie.types";
import { getClientMinIoUrl } from "@/utils/getMinIoUrl";
import { useQuery } from "@apollo/client";
import { HTMLMotionProps, m } from "framer-motion";
import { MediaDetails } from "@/components/media/MediaDetails";
import { useMediaBackdrop } from "./useMediaBackdrop";

interface MovieElementProps {
  id: number;
}

const backdropAnimation: HTMLMotionProps<"div"> = {
  initial: {
    clipPath: "inset(6.5% 40.5% round 20px)",
    rotateX: 89,
    opacity: 0.3,
    translateY: 92,
  },
  animate: {
    clipPath: "inset(0% 0% 0% 0%)",
    rotateX: 0,
    opacity: 1,
    translateY: 0,
  },
  transition: {
    type: "keyframes",
    duration: 1.5,
    ease: "easeInOut",
  },
};

export function MovieElement({ id }: MovieElementProps) {
  const movieId = Number(id);
  const { loading, error, data } = useQuery<GetMovieDetailsResponse>(GET_MOVIE_DETAILS, {
    variables: { movieId: movieId },
    skip: !id,
  });

  const movie = data?.movies.nodes[0];

  if (loading) return <p className="text-center text-lg font-semibold">Loading...</p>;
  if (error) return <p className="text-center text-lg text-red-500">Error: {error.message}</p>;
  if (!movie) return <p className="text-center text-lg font-semibold">Movie not found</p>;

  // eslint-disable-next-line react-hooks/rules-of-hooks
  const { style } = useMediaBackdrop(movie.backdropPath);
  const date = new Date(movie.releaseDate);

  if (!movie) return <p className="text-center text-lg font-semibold">Movie not found</p>;
  return (
    <>
      <div
        style={{
          perspective: "1500px",
        }}>
        <m.div
          {...backdropAnimation}
          style={style}
          className="relative left-0 z-0 -mt-25 bg-cover bg-no-repeat">
          <div className="absolute bottom-0 left-0 z-1 flex w-full items-end justify-between p-8">
            <MediaDetails mediaItem={movie} />
          </div>
        </m.div>
        <p className="text-lg mt-2 text-gray-300">{movie.description}</p>
        <div className="mt-6">
          <h3 className="text-2xl font-semibold border-b pb-2">Details</h3>
          <p>
            <strong>Release Date:</strong> {date.toLocaleDateString()}
          </p>
          <p>
            <strong>Budget:</strong> ${movie.budget.toLocaleString()}
          </p>
          <p>
            <strong>Runtime:</strong> {movie.runtime} minutes
          </p>
          <p>
            <strong>Status:</strong> {movie.movieStatus}
          </p>
          <p>
            <strong>Popularity:</strong> {movie.popularity}
          </p>
          <p>
            <strong>Vote Average:</strong> {movie.voteAverage}
          </p>
          <p>
            <strong>Vote Count:</strong> {movie.voteCount}
          </p>
        </div>
        <div className="mt-6">
          <h3 className="text-2xl font-semibold border-b pb-2">Cast</h3>
          <ul className="mt-2 space-y-1">
            {movie.movieCasts.map((cast, index) => (
              <li
                key={index}
                className="text-gray-300">
                {cast.person.personName} as <span className="font-semibold">{cast.characterName}</span>
              </li>
            ))}
          </ul>
        </div>
        <div className="mt-6 flex w-full flex-col items-center justify-center">
          <h3 className="w-full border-b pb-2 text-2xl font-semibold text-center">Movie</h3>
          <div className="relative mt-4 w-full max-w-4xl">
            <video
              className="w-full h-auto aspect-video rounded-lg shadow-md"
              controls
              src={getClientMinIoUrl(movie.moviePath)}
            />
          </div>
        </div>
      </div>
    </>
  );
}
