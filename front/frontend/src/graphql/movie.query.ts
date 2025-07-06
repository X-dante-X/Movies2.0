import { gql } from "@apollo/client";

export const GET_MOVIES = gql`
  query GetMovies($first: Int, $after: String, $where: MovieFilterInput, $order: [MovieSortInput!]) {
    movies(first: $first, after: $after, where: $where, order: $order) {
      pageInfo {
        endCursor
        hasNextPage
      }
      nodes {
        movieId
        title
        popularity
        releaseDate
        posterPath
      }
    }
  }
`;

export const FIND_MOVIES_BY_TITLE = gql`
  query FindMoviesByTitle($first: Int, $after: String, $partOfTitle: String!) {
    findMoviesByTitle(first: $first, after: $after, partOfTitle: $partOfTitle) {
      pageInfo {
        endCursor
        hasNextPage
      }
      nodes {
        movieId
        title
        popularity
        releaseDate
        posterPath
      }
    }
  }
`;

export const GET_FILTERS = gql`
  query GetFilters {
    genres {
      genreId
      genreName
    }
    countries {
      countryId
      countryName
    }
    productionCompanies {
      companyId
      companyName
    }
    tags {
      tagId
      tagName
    }
  }
`;

export interface Movie {
  movieId: number;
  title: string;
  popularity: number;
  releaseDate: string;
  posterPath?: string;
}


export interface PageInfo {
  endCursor: string | null;
  hasNextPage: boolean;
}

export interface GetMoviesData {
  movies: {
    pageInfo: PageInfo;
    nodes: Movie[];
  };
}

export interface FindMoviesData {
  findMoviesByTitle: {
    pageInfo: PageInfo;
    nodes: Movie[];
  };
}

export interface MovieFilterInput {
  genres?: string[];
  countries?: string[];
  companies?: string[];
  tags?: string[];
  releaseYear?: number;
}

export interface Genre {
  genreId: number;
  genreName: string;
}

export interface Country {
  countryId: number;
  countryName: string;
}

export interface ProductionCompany {
  companyId: number;
  companyName: string;
}

export interface Tag {
  tagId: number;
  tagName: string;
}

export interface GetFiltersData {
  genres: Genre[];
  countries: Country[];
  productionCompanies: ProductionCompany[];
  tags: Tag[];
}


export enum MovieOrderField {
  RELEASE_DATE = 'releaseDate',
  POPULARITY = 'popularity',
}

export enum SortDirection {
  ASC = 'ASC',
  DESC = 'DESC',
}
