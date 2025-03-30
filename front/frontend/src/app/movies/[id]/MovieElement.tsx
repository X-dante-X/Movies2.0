"use client";
import { GET_MOVIE_DETAILS } from "@/graphql/queries";
import { GetMovieDetailsResponse } from "@/types/movie.types";
import { getClientMinIoUrl, getServerMinIoUrl } from "@/utils/getMinIoUrl";
import { useQuery } from "@apollo/client";
import Image from "next/image";

interface MovieElementProps {
  id: number;
}

export function MovieElement({ id }: MovieElementProps) {
  const movieId = Number(id);

  const { loading, error, data } = useQuery<GetMovieDetailsResponse>(GET_MOVIE_DETAILS, {
    variables: { movieId: movieId },
    skip: !id,
  });

  if (loading) return <p className="text-center text-lg font-semibold">Loading...</p>;
  if (error) return <p className="text-center text-lg text-red-500">Error: {error.message}</p>;

  const movie = data?.movies[0];

  if (!movie) return <p className="text-center text-lg font-semibold">Movie not found</p>;
  return (
    <div className="max-w-4xl mx-auto p-6 bg-gray-900 text-white rounded-lg shadow-lg">
      <Image
        className="rounded-lg shadow-md"
        width={500}
        height={300}
        alt="Movie Poster"
        src={getServerMinIoUrl(movie.backdropPath)}
      />
      <h1 className="text-4xl font-bold mt-4">{movie.title}</h1>
      <p className="text-lg mt-2 text-gray-300">{movie.description}</p>
      <div className="mt-6">
        <h3 className="text-2xl font-semibold border-b pb-2">Details</h3>
        <p>
          <strong>Release Date:</strong> {movie.releaseDate}
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
        <p>
          <strong>PEGI:</strong> {movie.pegi}
        </p>
      </div>
      <div className="mt-6">
        <h3 className="text-2xl font-semibold border-b pb-2">Genres</h3>
        <ul className="flex flex-wrap gap-2 mt-2">
          {movie.genre.map((genre) => (
            <li
              key={genre.genreId}
              className="bg-gray-700 px-3 py-1 rounded-full text-sm">
              {genre.genreName}
            </li>
          ))}
        </ul>
      </div>
      <div className="mt-6">
        <h3 className="text-2xl font-semibold border-b pb-2">Tags</h3>
        <ul className="flex flex-wrap gap-2 mt-2">
          {movie.tags.map((tag) => (
            <li
              key={tag.tagId}
              className="bg-gray-700 px-3 py-1 rounded-full text-sm">
              {tag.tagName}
            </li>
          ))}
        </ul>
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
      <div className="mt-6">
        <h3 className="text-2xl font-semibold border-b pb-2">Trailer</h3>
        <video
          className="w-full mt-4 rounded-lg shadow-md"
          controls
          src={getClientMinIoUrl(movie.moviePath)}
        />
      </div>
    </div>
  );
}
