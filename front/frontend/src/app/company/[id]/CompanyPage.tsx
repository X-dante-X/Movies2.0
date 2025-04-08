"use client";
import { getServerMinIoUrl } from "@/utils/getMinIoUrl";
import { useQuery } from "@apollo/client";
import { Card } from "@/components/media/Card";
import Image from "next/image";
import { GET_COMPANY_DETAILS, GetCompanyDetailsResponse } from "@/graphql/companydetails.query";

interface IdProps {
  id: number;
}

export function CompanyPage({ id }: IdProps) {
  const companyId = Number(id);
  const { loading, error, data } = useQuery<GetCompanyDetailsResponse>(GET_COMPANY_DETAILS, {
    variables: { companyId: companyId },
    skip: !id,
  });
  console.log(companyId);

  console.log(data);

  if (loading) return <div className="text-white">Loading...</div>;
  if (error) return <div className="text-red-400">Error: {error.message}</div>;
  if (!data) return <div className="text-white">Person not found</div>;

  const company = data.productionCompanies[0];
  console.log(company);

  return (
    <div className="min-h-screen px-6 py-10 bg-gradient-to-br -mt-20 from-black via-gray-900 to-black text-white">
      <div className="max-w-4xl mt-20 mx-auto backdrop-blur-xl bg-white/10 rounded-3xl p-8 shadow-2xl">
        <div className="flex flex-col md:flex-row gap-6 items-center">
          {company.logoPath && (
            <Image
              src={getServerMinIoUrl(company.logoPath)}
              alt={company.companyName}
              width={252}
              height={378}
              className="rounded-2xl shadow-lg"
            />
          )}
          <div>
            <h1 className="text-4xl font-bold mb-2">{company.companyName}</h1>
            <p>
              <strong>Country:</strong> {company.country.countryName}
            </p>
          </div>
        </div>

        <div className="mt-10">
          <h3 className="text-2xl font-semibold mb-4">Filmography</h3>
          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
            {company.filmography.map((movie) => (
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
