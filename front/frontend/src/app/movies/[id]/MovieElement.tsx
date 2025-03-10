"use client";
import { GET_MOVIE_DETAILS } from "@/graphql/queries";
import { GetMovieDetailsResponse } from "@/types/movie.types";
import { useQuery } from "@apollo/client";

interface MovieElementProps {
  id: number;
}

export function MovieElement({ id }: MovieElementProps) {
  const movieId = Number(id);

  const { loading, error, data } = useQuery<GetMovieDetailsResponse>(GET_MOVIE_DETAILS, {
    variables: { movieId: movieId },
    skip: !id,
  });

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error: {error.message}</p>;

  const movie = data?.movies[0];

  if (!movie) return <p>Movie not found</p>;

  return (
    <div>
      <h1 className="text-3xl font-bold">{movie.title}</h1>
      <p className="text-lg mt-2">{movie.description}</p>
      <div className="mt-4">
        <h3 className="text-2xl font-semibold">Details:</h3>
        <p>Release Date: {movie.releaseDate}</p>
        <p>Budget: {movie.budget}</p>
        <p>Runtime: {movie.runtime} minutes</p>
        <p>Status: {movie.movieStatus}</p>
        <p>Popularity: {movie.popularity}</p>
        <p>Vote Average: {movie.voteAverage}</p>
        <p>Vote Count: {movie.voteCount}</p>
        <p>PEGI: {movie.pegi}</p>
        <h3 className="mt-4 text-xl font-semibold">Genres:</h3>
        <ul>
          {movie.genre.map((genre) => (
            <li key={genre.genreId}>{genre.genreName}</li>
          ))}
        </ul>
        <h3 className="mt-4 text-xl font-semibold">Cast:</h3>
        <ul>
          {movie.movieCasts.map((cast, index) => (
            <li key={index}>
              {cast.person.personName} as {cast.characterName}
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
}
