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

interface Person {
  personId: number;
  personName: string;
  gender: string;
  photoPath?: string;
  dateOfBirth: string;
  nationality: Country;
  biography: string;
  filmography: Movie[];
}

export interface GetPersonDetailsResponse {
  people: Person[];
}

export const GET_PERSON_DETAILS = gql`
  query GetPersonDetails($personId: Int!) {
    people(where: { personId: { eq: $personId } }) {
      personId
      personName
      gender
      photoPath
      dateOfBirth
      nationality {
        countryId
        countryName
        countryIsoCode
      }
      biography
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
