import { gql } from "@apollo/client";

interface Country {
  countryId: number;
  countryName: string;
  countryIsoCode: string;
}

interface Movie {
  movieId: number;
  title: string;
  popularity: number;
  posterPath?: string;
  releaseDate: string;
}

interface Company {
  companyId: number;
  companyName: string;
  logoPath?: string;
  country: Country;
  filmography: Movie[];
}

export interface GetCompanyDetailsResponse {
  productionCompanies: Company[];
}

export const GET_COMPANY_DETAILS = gql`
  query GetCompanyDetails($companyId: Int!) {
    productionCompanies(where: { companyId: { eq: $companyId } }) {
      companyId
      companyName
      logoPath
      country {
        countryId
        countryName
        countryIsoCode
      }
      filmography {
        movieId
        title
        popularity
        posterPath
        releaseDate
      }
    }
  }
`;
