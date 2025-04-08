export interface Genre {
    genreId: string;
    genreName: string;
}

export interface Tag {
    tagId: string;
    tagName: string;
}

export interface Person {
    personId: number;
    personName: string;
    gender: string;
    dateOfBirth: string;
    nationality: Country;
    biography: string;
    filmography: Movie[];
}
export interface MovieCast {
    movieId: string;
    movie: Movie;
    personId: string;
    person: Person;
    characterName: string;
    characterGender: string;
    job: string;
}

export interface Country {
    countryId: string;
    countryName: string;
    countryIsoCode: string;
}

export interface ProductionCompany {
    companyId: string;
    companyName: string;
    country: Country;
}

export interface Language {
    languageId: string;
    languageName: string;
}

export interface Country {
    countryId: string;
    countryName: string;
    countryIsoCode: string;
}

export interface Movie {
    movieId: string;
    title: string;
    releaseDate: string;
    budget: string;
    description: string;
    popularity: number;
    runtime: number;
    movieStatus: string;
    voteAverage: number;
    voteCount: number;
    moviePath: string;
    backdropPath: string;
    pegi: string;
    genre: Genre[];
    tags: Tag[];
    movieCasts: MovieCast[];
    productionCompany: ProductionCompany;
    language: Language;
    country: Country;
}

export interface GetMovieDetailsResponse {
    movies: Movie[];
}

export interface GetCountryDetailsResponse {
    countries: Country[];
}

export interface GetSelectionsResponse {
    countries: Country[];
    languages: Language[];
    productionCompanies: ProductionCompany[];
    tags: Tag[];
    genres: Genre[];
    people: Person[];
}

export interface MovieCastDTO {
    personId: number;
    characterName?: string;
    characterGender?: string;
    job: string;
}