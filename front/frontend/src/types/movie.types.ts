export interface Genre {
    genreId: string;
    genreName: string;
}

export interface Person {
    personId: string;
    personName: string;
    gender: string;
}

export interface MovieCast {
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

export interface ProductionLanguage {
    languageId: string;
    languageName: string;
}

export interface ProductionCountry {
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
    pegi: string;
    genre: Genre[];
    movieCasts: MovieCast[];
    productionCompany: ProductionCompany[];
    productionLanguage: ProductionLanguage[];
    productionCountry: ProductionCountry[];
}

export interface GetMovieDetailsResponse {
    movies: Movie[];
}