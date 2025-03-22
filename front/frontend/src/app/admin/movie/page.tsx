"use client";

import { useState } from "react";
import { gql, useMutation, useQuery } from "@apollo/client";
import { GetSelectionsResponse, MovieCastDTO } from "@/types/movie.types";

const CREATE_MOVIE = gql`
  mutation CreateMovie(
    $title: String!
    $releaseDate: DateTime
    $budget: Int
    $description: String!
    $popularity: Decimal
    $runtime: Int
    $movieStatus: String!
    $voteAverage: Decimal
    $voteCount: Int
    $pegi: String!
    $movie: Upload!
    $poster: Upload!
    $backdrop: Upload!
    $tags: [Int!]!
    $genres: [Int!]!
    $productionCompanyId: Int
    $languageId: Int!
    $countryId: Int!
    $movieCasts: [MovieCastDTOInput!]
  ) {
    createMovie(
      movieDTO: {
        title: $title
        releaseDate: $releaseDate
        budget: $budget
        description: $description
        popularity: $popularity
        runtime: $runtime
        movieStatus: $movieStatus
        voteAverage: $voteAverage
        voteCount: $voteCount
        pegi: $pegi
        movie: $movie
        poster: $poster
        backdrop: $backdrop
        tags: $tags
        genre: $genres
        productionCompanyId: $productionCompanyId
        languageId: $languageId
        countryId: $countryId
        movieCasts: $movieCasts
      }
    ) {
      title
    }
  }
`;

const GET_SELECTIONS = gql`
  query GetSelections {
    countries {
      countryId
      countryName
    }
    languages {
      languageId
      languageName
    }
    productionCompanies {
      companyId
      companyName
    }
    tags {
      tagId
      tagName
    }
    genres {
      genreId
      genreName
    }
    people {
      personId
      personName
    }
  }
`;

export default function Page() {
  const { data } = useQuery<GetSelectionsResponse>(GET_SELECTIONS);
  const [createMovie, { data: movieData, loading: creating, error }] = useMutation(CREATE_MOVIE);

  const [title, setTitle] = useState("");
  const [releaseDate, setReleaseDate] = useState("");
  const [budget, setBudget] = useState("");
  const [description, setDescription] = useState("");
  const [popularity, setPopularity] = useState("");
  const [runtime, setRuntime] = useState("");
  const [movieStatus, setMovieStatus] = useState("");
  const [voteAverage, setVoteAverage] = useState("");
  const [voteCount, setVoteCount] = useState("");
  const [pegi, setPegi] = useState("");
  const [movie, setMovie] = useState<File | null>(null);
  const [poster, setPoster] = useState<File | null>(null);
  const [backdrop, setBackdrop] = useState<File | null>(null);
  const [country, setCountry] = useState("");
  const [productionCompany, setProductionCompany] = useState("");
  const [language, setLanguage] = useState("");
  const [genres, setGenres] = useState<number[]>([]);
  const [tags, setTags] = useState<number[]>([]);
  const [movieCasts, setMovieCasts] = useState<MovieCastDTO[]>([]);

  const addMovieCast = () => {
    setMovieCasts([...movieCasts, { personId: 0, characterName: "", characterGender: "", job: "" }]);
  };

  const updateMovieCast = (index: number, field: string, value: string) => {
    const updatedCasts = [...movieCasts];
    if (field === "personId") {
      updatedCasts[index] = { ...updatedCasts[index], [field]: Number(value) };
    } else {
      updatedCasts[index] = { ...updatedCasts[index], [field]: value };
    }
    setMovieCasts(updatedCasts);
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value, type } = e.target;

    if (type === "file") {
      const target = e.target as HTMLInputElement;
      if (name === "movie") {
        setMovie(target.files?.[0] || null);
      } else if (name === "poster") {
        setPoster(target.files?.[0] || null);
      } else if (name === "backdrop") {
        setBackdrop(target.files?.[0] || null);
      }
    } else if (type === "select-multiple") {
      const target = e.target as HTMLSelectElement;
      const selectedValues: number[] = [];
      for (let i = 0; i < target.options.length; i++) {
        if (target.options[i].selected && target.options[i].value) {
          selectedValues.push(Number(target.options[i].value));
        }
      }
      if (name === "genres") {
        setGenres(selectedValues);
      } else if (name === "tags") {
        setTags(selectedValues);
      }
    } else {
      switch (name) {
        case "title":
          setTitle(value);
          break;
        case "releaseDate":
          setReleaseDate(value);
          break;
        case "budget":
          setBudget(value);
          break;
        case "description":
          setDescription(value);
          break;
        case "popularity":
          setPopularity(value);
          break;
        case "runtime":
          setRuntime(value);
          break;
        case "movieStatus":
          setMovieStatus(value);
          break;
        case "voteAverage":
          setVoteAverage(value);
          break;
        case "voteCount":
          setVoteCount(value);
          break;
        case "pegi":
          setPegi(value);
          break;
        case "country":
          setCountry(value);
          break;
        case "productionCompany":
          setProductionCompany(value);
          break;
        case "language":
          setLanguage(value);
          break;
        default:
          break;
      }
    }
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    try {
      const formattedReleaseDate = releaseDate ? new Date(releaseDate + "T00:00:00Z").toISOString() : null;

      await createMovie({
        variables: {
          title,
          releaseDate: formattedReleaseDate,
          budget: budget ? Number(budget) : null,
          description,
          popularity: popularity ? Number(popularity) : null,
          runtime: runtime ? Number(runtime) : null,
          movieStatus,
          voteAverage: voteAverage ? Number(voteAverage) : null,
          voteCount: voteCount ? Number(voteCount) : null,
          pegi,
          movie,
          poster,
          backdrop,
          tags,
          genres,
          productionCompanyId: productionCompany ? Number(productionCompany) : null,
          languageId: language ? Number(language) : null,
          countryId: country ? Number(country) : null,
          movieCasts: movieCasts,
        },
      });
    } catch (err) {
      console.error("Error creating movie:", err);
    }
  };

  return (
    <div className="p-4 max-w-md mx-auto">
      <h1 className="text-xl font-bold mb-4">Create a New Movie</h1>
      <form
        onSubmit={handleSubmit}
        className="flex flex-col gap-2">
        <input
          type="text"
          name="title"
          value={title}
          onChange={handleChange}
          placeholder="Title"
          className="p-2 border rounded"
        />
        <input
          type="date"
          name="releaseDate"
          value={releaseDate}
          onChange={handleChange}
          className="p-2 border rounded"
        />
        <input
          type="number"
          name="budget"
          value={budget}
          onChange={handleChange}
          placeholder="Budget"
          className="p-2 border rounded"
        />
        <textarea
          name="description"
          value={description}
          onChange={handleChange}
          placeholder="Description"
          className="p-2 border rounded"
        />
        <input
          type="number"
          name="popularity"
          value={popularity}
          onChange={handleChange}
          placeholder="Popularity"
          className="p-2 border rounded"
        />
        <input
          type="number"
          name="runtime"
          value={runtime}
          onChange={handleChange}
          placeholder="Runtime"
          className="p-2 border rounded"
        />
        <input
          type="text"
          name="movieStatus"
          value={movieStatus}
          onChange={handleChange}
          placeholder="Status"
          className="p-2 border rounded"
        />
        <input
          type="number"
          name="voteAverage"
          value={voteAverage}
          onChange={handleChange}
          placeholder="Vote Average"
          className="p-2 border rounded"
        />
        <input
          type="number"
          name="voteCount"
          value={voteCount}
          onChange={handleChange}
          placeholder="Vote Count"
          className="p-2 border rounded"
        />
        <input
          type="text"
          name="pegi"
          value={pegi}
          onChange={handleChange}
          placeholder="Pegi"
          className="p-2 border rounded"
        />
        <select
          name="country"
          value={country}
          onChange={handleChange}
          className="p-2 border rounded">
          <option value="">Select Country</option>
          {data?.countries.map((c) => (
            <option
              key={c.countryId}
              value={c.countryId}>
              {c.countryName}
            </option>
          ))}
        </select>
        <select
          name="productionCompany"
          value={productionCompany}
          onChange={handleChange}
          className="p-2 border rounded">
          <option value="">Select Production Company</option>
          {data?.productionCompanies.map((c) => (
            <option
              key={c.companyId}
              value={c.companyId}>
              {c.companyName}
            </option>
          ))}
        </select>
        <select
          name="language"
          value={language}
          onChange={handleChange}
          className="p-2 border rounded">
          <option value="">Select Language</option>
          {data?.languages.map((l) => (
            <option
              key={l.languageId}
              value={l.languageId}>
              {l.languageName}
            </option>
          ))}
        </select>
        <select
          name="genres"
          value={genres.map(String)}
          onChange={handleChange}
          className="p-2 border rounded"
          multiple>
          {data?.genres.map((g) => (
            <option
              key={g.genreId}
              value={g.genreId}>
              {g.genreName}
            </option>
          ))}
        </select>
        <select
          name="tags"
          value={tags.map(String)}
          onChange={handleChange}
          className="p-2 border rounded"
          multiple>
          {data?.tags.map((t) => (
            <option
              key={t.tagId}
              value={t.tagId}>
              {t.tagName}
            </option>
          ))}
        </select>
        <label className="p-2 border rounded cursor-pointer">
          Choose movie
          <input
            type="file"
            name="movie"
            accept="video/*"
            onChange={handleChange}
            className="hidden"
          />
        </label>
        <label className="p-2 border rounded cursor-pointer">
          Choose poster
          <input
            type="file"
            name="poster"
            accept="image/*"
            onChange={handleChange}
            className="hidden"
          />
        </label>
        <label className="p-2 border rounded cursor-pointer">
          Choose backdrop
          <input
            type="file"
            name="backdrop"
            accept="image/*"
            onChange={handleChange}
            className="hidden"
          />
        </label>
        <button
          type="button"
          onClick={addMovieCast}
          className="bg-green-500 text-white px-4 py-2 rounded">
          Add Movie Cast
        </button>
        {movieCasts.map((cast, index) => (
          <div
            key={index}
            className="p-2 border rounded">
            <select
              value={cast.personId}
              onChange={(e) => updateMovieCast(index, "personId", e.target.value)}>
              <option value="">Select Person</option>
              {data?.people.map((p) => (
                <option
                  key={p.personId}
                  value={p.personId}>
                  {p.personName}
                </option>
              ))}
            </select>
            <input
              type="text"
              value={cast.characterName}
              onChange={(e) => updateMovieCast(index, "characterName", e.target.value)}
              placeholder="Character Name"
              className="p-2 border rounded"
            />
            <input
              type="text"
              value={cast.characterGender}
              onChange={(e) => updateMovieCast(index, "characterGender", e.target.value)}
              placeholder="Character Gender"
              className="p-2 border rounded"
            />
            <input
              type="text"
              value={cast.job}
              onChange={(e) => updateMovieCast(index, "job", e.target.value)}
              placeholder="Job"
              className="p-2 border rounded"
            />
          </div>
        ))}
        <button
          type="submit"
          disabled={creating}
          className="bg-blue-500 text-white px-4 py-2 rounded disabled:opacity-50">
          {creating ? "Creating..." : "Create Movie"}
        </button>
      </form>
      {error && <p className="text-red-500 mt-2">Error: {error.message}</p>}
      {movieData && <p className="text-green-500 mt-2">Created movie: {movieData.createMovie.title}</p>}
    </div>
  );
}
