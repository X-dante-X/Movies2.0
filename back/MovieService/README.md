# MovieService — GraphQL API for Movie Management

**Status:** Experimental  
**Author:** Yevhenii Solomchenko

---

## Overview

MovieService is a GraphQL-based service for managing movies, people, genres, countries, languages, tags, and production companies.  
It supports uploading files (videos, images) via gRPC and handles asynchronous communication using RabbitMQ.

The service provides both public queries (e.g., listing movies, finding movies by title) and private mutations (e.g., creating or updating movies, people, genres, production companies).

## Responsibilities

- Manage movies, people, genres, countries, languages, tags, and production companies.
- Support uploading media files (video, poster, backdrop, photos, logos) through gRPC.
- Process asynchronous requests through RabbitMQ.
- Provide filtering, sorting, and pagination for all entities.
- Normalize file names for consistent storage.

## GraphQL

For full SDL schema and types, see [GraphQL Schema](GraphqlSchema.sdl).

### Public Endpoints (GraphQL Queries)

- `movies` — Get a paginated list of movies with filtering and sorting support.
- `findMoviesByTitle` — Search movies by partial title.
- `countries` — List countries with filtering and sorting.
- `people` — List people with filtering and sorting.
- `movieCasts` — Get movie cast data with filtering.
- `genres` — List genres.
- `languages` — List languages.
- `tags` — List tags.
- `productionCompanies` — List production companies.

### Private Endpoints (GraphQL Mutations)

- `createMovie(movieDTO: MovieDTOInput!): MovieDTO!` — Create a new movie with uploads.
- `updateMovie(movie: MovieInput!): Movie!` — Update an existing movie.
- `deleteMovie(movie: MovieInput!): Movie!` — Delete a movie.
- `createPerson(personDTO: PersonDTOInput!): PersonDTO!` — Create a person with photo.
- `createGenre(genre: GenreInput!): Genre!` — Create a new genre.
- `createCountry(country: CountryInput!): Country!` — Create a new country.
- `createLanguage(language: LanguageInput!): Language!` — Create a new language.
- `createTag(tag: TagInput!): Tag!` — Create a new tag.
- `createProductionCompany(productionCompanyDTO: ProductionCompanyDTOInput!): ProductionCompanyDTO!` — Create a production company with logo.

## RabbitMQ Integration

- Listens on queue `movie_request`.
- Receives list of movie IDs (favorites) and returns movie details.
- Asynchronous consumer is implemented using `AsyncEventingBasicConsumer`.
- Supports cancellation and graceful shutdown.

> [!NOTE]
> This project is experimental and subject to breaking changes.
> Not intended yet for production workloads.
