"use client";
import { useQuery } from "@apollo/client";
import { GET_MOVIES } from "@/graphql/queries";
import Link from "next/link";
import { Card } from "@/components/Card";

export default function Page() {
  const { loading, error, data } = useQuery(GET_MOVIES);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error: {error.message}</p>;

  return (
    <div>
      <h1 className="text-3xl font-bold mb-4">Movies</h1>
      <p className="mb-4">All movies</p>
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
        {data.movies.map((movie: { movieId: string; title: string; description: string; releaseDate: string; posterPath: string }) => (
          <Link
            key={movie.movieId}
            href={`/movies/${movie.movieId}`}>
            <Card
              title={movie.title}
              description={movie.description}
              ReleaseDate={movie.releaseDate}
              posterFile={movie.posterPath}
            />
          </Link>
        ))}
      </div>
    </div>
  );
}
