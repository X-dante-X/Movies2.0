import { gql } from "@apollo/client";

export const GET_COUNTRIES = gql`
  query GetCountries {
    countries {
      countryId
      countryName
      countryIsoCode
    }
  }
`;

export const GET_MOVIES = gql`
  query GetMovies {
    movies {
      nodes {
        movieId
        title
        description
        posterPath
      }
    }
  }
`;

export const GET_MOVIE_DETAILS = gql`
  query GetMovieDetails($movieId: Int!) {
    movies(where: { movieId: { eq: $movieId } }) {
      nodes {
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
        moviePath
        backdropPath
        genre {
          genreId
          genreName
        }
        tags {
          tagId
          tagName
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
  }
`;