import { gql } from "@apollo/client";

export const GET_MOVIES = gql`
  query GetMovies {
    movies {
      movieId
      title
      description
    }
  }
`;

export const GET_MOVIE_DETAILS = gql`
  query GetMovieDetails($movieId: Int!) {
    movies(where: { movieId: { eq: $movieId } }) {
      movieId
      title
      releaseDate
      budget
      description
      popularity
      runtime
      movieStatus
      voteAverage
      voteCount
      pegi
      genre {
        genreId
        genreName
      }
      movieCasts {
        person {
          personId
          personName
          gender
        }
        characterName
        characterGender
        job
      }
      productionCompany {
        companyId
        companyName
        country {
          countryId
          countryName
          countryIsoCode
        }
      }
      productionLanguage {
        languageId
        languageName
      }
      productionCountry {
        countryId
        countryName
        countryIsoCode
      }
    }
  }
`;