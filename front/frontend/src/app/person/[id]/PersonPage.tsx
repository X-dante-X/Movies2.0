"use client";
import { GET_PERSON_DETAILS, GetPersonDetailsResponse } from "@/graphql/persondetails.query";
import { getServerMinIoUrl } from "@/utils/getMinIoUrl";
import { useQuery } from "@apollo/client";
import { Card } from "@/components/media/Card";
import Image from "next/image";

interface IdProps {
  id: number;
}

export function PersonPage({ id }: IdProps) {
  const personId = Number(id);
  const { loading, error, data } = useQuery<GetPersonDetailsResponse>(GET_PERSON_DETAILS, {
    variables: { personId: personId },
    skip: !id,
  });

  if (loading) return <div className="text-white">Loading...</div>;
  if (error) return <div className="text-red-400">Error: {error.message}</div>;
  if (!data || data.people.length === 0) return <div className="text-white">Person not found</div>;

  const person = data.people[0];

  const date = new Date(person.dateOfBirth);

  return (
    <div className="min-h-screen px-6 py-10 bg-gradient-to-br -mt-20 from-black via-gray-900 to-black text-white">
      <div className="max-w-4xl mt-20 mx-auto backdrop-blur-xl bg-white/10 rounded-3xl p-8 shadow-2xl">
        <div className="flex flex-col md:flex-row gap-6 items-center">
          {person.photoPath && (
            <Image
              src={getServerMinIoUrl(person.photoPath)}
              alt={person.personName}
              width={252}
              height={378}
              className="rounded-2xl shadow-lg"
            />
          )}
          <div>
            <h1 className="text-4xl font-bold mb-2">{person.personName}</h1>
            <p>
              <strong>Gender:</strong> {person.gender}
            </p>
            <p>
              <strong>Birthday:</strong> {date.toLocaleDateString("de-DE", { year: "numeric", month: "long", day: "numeric" })}
            </p>
            <p>
              <strong>Nationality:</strong> {person.nationality.countryName}
            </p>
          </div>
        </div>

        <div className="mt-8">
          <h2 className="text-2xl font-semibold mb-2">Biography</h2>
          <p className="text-white/90">{person.biography}</p>
        </div>

        <div className="mt-10">
          <h3 className="text-2xl font-semibold mb-4">Filmography</h3>
          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
            {person.filmography.map((movie) => (
              <Card
                key={movie.movieId}
                movieId={movie.movieId}
                title={movie.title}
                ReleaseDate={movie.releaseDate}
                popularity={movie.popularity}
                posterFile={movie.posterPath}
              />
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}
