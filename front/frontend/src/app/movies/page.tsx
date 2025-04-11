"use client";
import { useQuery } from "@apollo/client";
import { GET_MOVIES } from "@/graphql/queries";
import { Card } from "@/components/media/Card";
import { m } from "framer-motion";
import { useState } from "react";

export default function Page() {
  const { loading, error, data } = useQuery(GET_MOVIES);
  const [selectedMovie, setSelectedMovie] = useState<number | null>(null);

  if (loading) return <p className="text-xl text-center text-gray-500">Loading...</p>;
  if (error) return <p className="text-xl text-center text-red-500">Error: {error.message}</p>;

  return (
    <>
      <div className="position absolute w-full -mt-20 h-screen bg-gradient-to-br from-gray-900/20 via-indigo-900/20 to-purple-900/20 bg-fixed" />
      <div className="p-4 mx-16">
        <div className="grid grid-cols-3 sm:grid-cols-4 lg:grid-cols-5 xl:grid-cols-6 gap-1">
          {data.movies.nodes.map((movie: { movieId: number; title: string; popularity: number; releaseDate: string; posterPath: string }) => (
            <m.div
              key={movie.movieId}
              onClick={() => setSelectedMovie(movie.movieId)}
              animate={
                selectedMovie === movie.movieId
                  ? {
                      scale: 1.2,
                      translateY: -10,
                      rotateX: -90,
                      transition: {
                        duration: 1.2,
                        ease: "easeOut",
                      },
                    }
                  : { scale: 1, translateY: 0, rotateX: 0, filter: "grayscale(0%)" }
              }
              className="relative cursor-pointer">
              <Card
                key={movie.movieId}
                movieId={movie.movieId}
                title={movie.title}
                popularity={movie.popularity}
                ReleaseDate={movie.releaseDate}
                posterFile={movie.posterPath}
              />
            </m.div>
          ))}
        </div>
      </div>
    </>
  );
}
