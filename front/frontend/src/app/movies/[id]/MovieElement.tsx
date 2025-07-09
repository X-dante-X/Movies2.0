"use client";
import { GET_MOVIE_DETAILS } from "@/graphql/queries";
import { GetMovieDetailsResponse } from "@/types/movie.types";
import { getBackgroundImage, getClientMinIoUrl, getServerMinIoUrl } from "@/utils/getMinIoUrl";
import { useQuery } from "@apollo/client";
import { HTMLMotionProps, m } from "framer-motion";
import { MediaDetails } from "@/components/media/MediaDetails";
import { getImageProps } from "next/image";
import { CSSProperties } from "react";
import Link from "next/link";
import { VideoPlayer } from "@/components/videoplayer/VideoPlayer";
import { FavoriteButton } from "@/components/FavoriteButton/FavoriteButton";
import { MovieReviews } from "@/components/moviereviews/MovieReviews";

interface MovieElementProps {
  id: number;
}

function useMediaBackdrop(backdropPath: string) {
  const fullPath = getServerMinIoUrl(backdropPath);
  console.log(fullPath);

  const {
    props: { srcSet },
  } = getImageProps({
    alt: "",
    width: 1643,
    height: 692,
    src: fullPath,
    priority: true,
    quality: 100,
  });
  const backgroundImage = getBackgroundImage(srcSet);
  const style: CSSProperties = {
    height: 540,
    width: "100%",
    backgroundImage,
    transformStyle: "preserve-3d",
  };

  return {
    style,
  };
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

const descriptionAnimation: HTMLMotionProps<"div"> = {
  initial: { opacity: 0, y: 50 },
  animate: { opacity: 1, y: 0 },
  transition: { delay: 0.8, type: "spring", stiffness: 100 },
};

const sectionAnimation: HTMLMotionProps<"div"> = {
  initial: { opacity: 0, y: 50 },
  animate: { opacity: 1, y: 0 },
  transition: { delay: 1, type: "spring", stiffness: 100 },
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
            {/* Add the Favorite Button in the top right of the backdrop */}
            <div className="absolute right-6">
              <FavoriteButton movieId={movieId} />
            </div>
          </div>
        </m.div>

        <div className="relative flex flex-col overflow-hidden">
          {/* Background gradient layers */}
          <div className="absolute inset-0 bg-gradient-to-br from-[#0f2027] via-[#203a43] to-[#2c5364]"></div>
          <div className="absolute -top-20 -left-20 w-96 h-96 bg-pink-500 opacity-30 rounded-full blur-3xl"></div>
          <div className="absolute -bottom-10 -right-10 w-96 h-96 bg-blue-500 opacity-30 rounded-full blur-3xl"></div>
          <div className="absolute top-1/3 left-1/2 w-72 h-72 bg-purple-500 opacity-20 rounded-full blur-2xl transform -translate-x-1/2"></div>

          {/* Content */}
          <div className="relative flex flex-col space-y-6 p-6 rounded-2xl z-10">
            {/* Верхние два ряда с Description + Details / Cast */}
            <div className="flex flex-col lg:flex-row lg:space-x-6 space-y-6 lg:space-y-0">
              {/* Left: Description (занимает 2/3 высоты справа) */}
              <m.div
                {...descriptionAnimation}
                className="flex-1 p-8 shadow-xl rounded-2xl backdrop-blur-md bg-white/10 border border-white/20 text-white text-center flex flex-col justify-center">
                <p className="text-lg font-medium leading-relaxed">{movie.description}</p>
              </m.div>

              {/* Right column: Details on top, Cast below */}
              <div className="flex flex-col space-y-6 lg:w-1/2">
                {/* Movie Details */}
                <m.div
                  {...sectionAnimation}
                  className="p-8 rounded-2xl shadow-xl backdrop-blur-md bg-white/10 border border-white/20 text-white">
                  <h3 className="text-3xl font-semibold pb-3 mb-4">Movie Details</h3>
                  <div className="space-y-6 text-lg">
                    {[
                      ["Release Date", date.toLocaleDateString()],
                      ["Budget", `$${movie.budget.toLocaleString()}`],
                      ["Runtime", `${movie.runtime} minutes`],
                      ["Status", movie.movieStatus],
                      ["Popularity", movie.popularity],
                      ["Vote Average", movie.voteAverage],
                      ["Vote Count", movie.voteCount],
                    ].map(([label, value], i) => (
                      <div
                        key={i}
                        className="flex justify-between items-center">
                        <span className="font-medium text-white/70">{label}</span>
                        <span className="text-white">{value}</span>
                      </div>
                    ))}
                  </div>
                </m.div>

                {movie.movieCasts && movie.movieCasts.length > 0 && (
                  <m.div
                    {...sectionAnimation}
                    className="p-8 rounded-2xl shadow-xl backdrop-blur-md bg-white/10 border border-white/20 text-white text-center">
                    <h3 className="text-3xl font-semibold text-left pb-3 mb-4">Cast</h3>

                    {/* Actors */}
                    {movie.movieCasts.filter((cast) => cast.job.toLowerCase() === "actor").length > 0 && (
                      <div className="mb-6">
                        <h4 className="text-2xl font-semibold mb-3 border-b pb-3 border-amber-50">Actors</h4>
                        <ul className="space-y-3 text-lg">
                          {movie.movieCasts
                            .filter((cast) => cast.job.toLowerCase() === "actor")
                            .map((cast, index) => (
                              <Link
                                href={`/person/${cast.person.personId}`}
                                key={index}
                                className="flex justify-between items-center text-white/90">
                                <span>{cast.person.personName}</span>
                                <span className="font-semibold text-indigo-200">{cast.characterName}</span>
                              </Link>
                            ))}
                        </ul>
                      </div>
                    )}

                    {/* Directors */}
                    {movie.movieCasts.filter((cast) => cast.job.toLowerCase() === "director").length > 0 && (
                      <div>
                        <h4 className="text-2xl font-semibold mb-3 border-b pb-3 border-amber-50">Directors</h4>
                        <ul className="space-y-3 text-lg">
                          {movie.movieCasts
                            .filter((cast) => cast.job.toLowerCase() === "director")
                            .map((cast, index) => (
                              <Link
                                href={`/person/${cast.person.personId}`}
                                key={index}
                                className="flex justify-between items-center text-white/90">
                                <span>{cast.person.personName}</span>
                                <span className="font-semibold text-indigo-200">{cast.characterName}</span>
                              </Link>
                            ))}
                        </ul>
                      </div>
                    )}
                  </m.div>
                )}
              </div>
            </div>

            {/* Movie */}
            <m.div
              {...sectionAnimation}
              className="w-full p-8 rounded-2xl shadow-xl backdrop-blur-md bg-white/10 border border-white/20 text-white text-center">
              <h3 className="text-3xl font-semibold pb-3 mb-4">Movie</h3>
              <VideoPlayer src={getClientMinIoUrl(movie.moviePath)} />
            </m.div>
            <MovieReviews movieId={movieId} movieTitle={movie.title} />
          </div>
        </div>
      </div>
    </>
  );
}
